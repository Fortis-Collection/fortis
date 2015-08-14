﻿using System;
using System.Collections.Generic;
using System.Linq;
using Fortis.Model.Fields;
using Fortis.Model;

namespace Fortis.Providers
{
    public class TemplateMapProvider : ITemplateMapProvider
    {
        private readonly IEnumerable<IModelAssemblyProvider> _modelAssemblyProviders;

        public TemplateMapProvider(IModelAssemblyProvider modelAssemblyProvider)
        {
            _modelAssemblyProviders = new List<IModelAssemblyProvider> { modelAssemblyProvider };
        }

        public TemplateMapProvider(IEnumerable<IModelAssemblyProvider> modelAssemblyProviders)
        {
            _modelAssemblyProviders = modelAssemblyProviders;
        }

        private object _lock = new object();
        private Dictionary<Guid, List<Type>> _templateMap = null;

        public Dictionary<Guid, List<Type>> TemplateMap
        {
            get
            {
                lock (_lock)
                {
                    if (_templateMap == null)
                    {
                        _templateMap = new Dictionary<Guid, List<Type>>();

                        foreach (var modelAssemblyProvider in _modelAssemblyProviders)
                        {
                            var types = modelAssemblyProvider.Assembly.GetTypes();
                            foreach (var t in types)
                            {
                                var customAttributes = t.GetCustomAttributes(typeof(TemplateMappingAttribute), false);
                                foreach (TemplateMappingAttribute templateAttribute in customAttributes)
                                {
                                    if (string.IsNullOrEmpty(templateAttribute.Type))
                                    {
                                        if (!_templateMap.Keys.Contains(templateAttribute.Id))
                                        {
                                            _templateMap.Add(templateAttribute.Id, new List<Type> {t});
                                        }
                                        else
                                        {
                                            _templateMap[templateAttribute.Id].Add(t);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    return _templateMap;
                }
            }
        }

        private Dictionary<Type, Guid> _interfaceTemplateMap;
        public Dictionary<Type, Guid> InterfaceTemplateMap
        {
            get
            {
                lock (_lock)
                {

                    if (_interfaceTemplateMap == null)
                    {
                        _interfaceTemplateMap = new Dictionary<Type, Guid>();

                        foreach (var modelAssemblyProvider in _modelAssemblyProviders)
                        {
                            foreach (var t in modelAssemblyProvider.Assembly.GetTypes())
                            {
                                foreach (
                                    TemplateMappingAttribute templateAttribute in
                                        t.GetCustomAttributes(typeof(TemplateMappingAttribute), false))
                                {
                                    if (string.Equals(templateAttribute.Type, "InterfaceMap"))
                                    {
                                        if (!_interfaceTemplateMap.Keys.Contains(t))
                                        {
                                            _interfaceTemplateMap.Add(t, templateAttribute.Id);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    return _interfaceTemplateMap;
                }
            }
        }

        private Dictionary<Guid, List<Type>> _renderingParametersTemplateMap = null;
        public Dictionary<Guid, List<Type>> RenderingParametersTemplateMap
        {
            get
            {
                lock (_lock)
                {

                    if (_renderingParametersTemplateMap == null)
                    {
                        _renderingParametersTemplateMap = new Dictionary<Guid, List<Type>>();

                        foreach (var modelAssemblyProvider in _modelAssemblyProviders)
                        {
                            foreach (var t in modelAssemblyProvider.Assembly.GetTypes())
                            {
                                foreach (
                                    TemplateMappingAttribute templateAttribute in t.GetCustomAttributes(typeof(TemplateMappingAttribute), false))
                                {
                                    if (templateAttribute.Type == "RenderingParameter")
                                    {
                                        if (!_renderingParametersTemplateMap.Keys.Contains(templateAttribute.Id))
                                        {
                                            _renderingParametersTemplateMap.Add(templateAttribute.Id, new List<Type> {t});
                                        }
                                        else
                                        {
                                            _renderingParametersTemplateMap[templateAttribute.Id].Add(t);
                                        }
                                    }
                                }
                            }
                        }
                    }

                    return _renderingParametersTemplateMap;
                }
            }
        }

        public Type GetImplementation<T>() where T : IItemWrapper
        {
            var typeOfT = typeof(T);

            if (!typeOfT.IsInterface)
            {
                throw new Exception("Fortis: An interface implementing IITemWrapper must be passed as the generic argument to get the corresponding implementation. " + typeOfT.Name + " is not an interface.");
            }

            if (!InterfaceTemplateMap.ContainsKey(typeOfT))
            {
                throw new Exception("Fortis: Type " + typeOfT.Name + " does not exist in interface template map");
            }

            var templateId = InterfaceTemplateMap[typeOfT];

            if (!TemplateMap.ContainsKey(templateId))
            {
                throw new Exception("Fortis: Template ID " + templateId + " does not exist in template map");
            }

            foreach (var type in TemplateMap[templateId])
            {
                if (typeOfT.IsAssignableFrom(type))
                    return type;
            }

            return null;
        }

        public bool IsCompatibleTemplate<T>(Guid templateId) where T : IItemWrapper
        {
            return IsCompatibleTemplate(templateId, typeof(T));
        }

        public bool IsCompatibleTemplate(Guid templateId, Type template)
        {
            // template Type must at least implement IItemWrapper
            if (template != typeof(IItemWrapper))
            {
                // TODO: Implement
            }

            return true;
        }

        public bool IsCompatibleFieldType<T>(string fieldType) where T : IFieldWrapper
        {
            return IsCompatibleFieldType(fieldType, typeof(T));
        }

        public bool IsCompatibleFieldType(string scFieldType, Type fieldType)
        {
            switch (scFieldType.ToLower())
            {
                case "checkbox":
                    return fieldType == typeof(BooleanFieldWrapper);
                case "image":
                    return fieldType == typeof(ImageFieldWrapper);
                case "date":
                case "datetime":
                    return fieldType == typeof(DateTimeFieldWrapper);
                case "checklist":
                case "treelist":
                case "treelistex":
                case "multilist":
                case "multilist with search":
                case "treelist with search":
                    return fieldType == typeof(ListFieldWrapper);
                case "file":
                    return fieldType == typeof(FileFieldWrapper);
                case "droplink":
                case "droptree":
                    return fieldType == typeof(LinkFieldWrapper);
                case "general link":
                case "general link with search":
                    return fieldType == typeof(GeneralLinkFieldWrapper);
                case "text":
                case "single-line text":
                case "multi-line text":
                case "droplist":
                    return fieldType == typeof(TextFieldWrapper);
                case "rich text":
                    return fieldType == typeof(RichTextFieldWrapper);
                case "number":
                    return fieldType == typeof(NumberFieldWrapper);
                case "integer":
                    return fieldType == typeof(IntegerFieldWrapper);
                case "name value list":
                    return fieldType == typeof(NameValueListFieldWrapper);
                default:
                    return false;
            }
        }
    }
}

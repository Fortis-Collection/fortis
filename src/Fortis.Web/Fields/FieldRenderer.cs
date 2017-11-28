using Fortis.Fields;
using Sitecore.Collections;
using Sitecore.Data.Fields;
using Sitecore.Pipelines;
using Sitecore.Pipelines.RenderField;
using Sitecore.Xml.Xsl;
using System;
using System.IO;
using System.Web;

namespace Fortis.Web.Fields
{
    public class FieldRenderer : IFieldRenderer
    {
        public virtual IHtmlString Render(IField field, object parameters = null)
        {
            return RenderField(field, parameters, false);
        }

        public virtual IHtmlString RenderEditable(IField field, object parameters = null)
        {
            return RenderField(field, parameters, true);
        }

        public virtual IHtmlString RenderField(IField field, object parameters, bool editable)
        {
			if (field == null)
			{
				throw new ArgumentNullException("field");
			}

            var renderResult = RenderScField(field, parameters, editable);

            return new HtmlString(string.Concat(renderResult.FirstPart, renderResult.LastPart));
        }

        public virtual IFieldRenderResult BeginRender(IField field, TextWriter textWriter, object parameters = null)
        {
            return BeginRenderField(field, textWriter, parameters, false);
        }

        public virtual IFieldRenderResult BeginRenderEditable(IField field, TextWriter textWriter, object parameters = null)
        {
            return BeginRenderField(field, textWriter, parameters, true);
        }

        public virtual IFieldRenderResult BeginRenderField(IField field, TextWriter textWriter, object parameters, bool editable)
        {
			if (field == null)
			{
				throw new ArgumentNullException("field");
			}

			var renderResult = RenderScField(field, parameters, editable);

            return new FieldRenderResult(textWriter, renderResult.FirstPart, renderResult.LastPart);
        }

        public virtual RenderFieldResult RenderScField(IField field, object parameters = null, bool editable = true)
        {
			if (field == null)
			{
				throw new ArgumentNullException("field");
			}

			var scField = GetScField(field);
            var renderFieldArgs = new RenderFieldArgs
            {
                Item = scField.Item,
                FieldName = scField.Name,
                DisableWebEdit = !editable
            };

            if (parameters != null)
            {
                CopyProperties(parameters, renderFieldArgs);
                CopyProperties(parameters, renderFieldArgs.Parameters);
            }

            CorePipeline.Run("renderField", renderFieldArgs);

            return renderFieldArgs.Result;
        }

        public virtual Field GetScField(IField field)
        {
            if (!(field is IBaseField))
            {
				var fieldType = field == null ? "field" : field.GetType().FullName;

				throw new Exception($"Fortis :: \"{fieldType}\" must implement \"{typeof(IBaseField).FullName}\"");
            }

            var scField = ((IBaseField)field).Field;

            if (scField == null)
            {
                throw new Exception($"Fortis :: unable to get Sitecore field for {field.Name}");
            }

            return scField;
        }

        public static void CopyProperties(object source, object target)
        {
            var type = target.GetType();
            foreach (var propertyInfo in source.GetType().GetProperties())
            {
                var property = type.GetProperty(propertyInfo.Name);
                if (!(property == null) && property.PropertyType.IsAssignableFrom(propertyInfo.PropertyType))
                {
                    property.SetValue(target, propertyInfo.GetValue(source, null), null);
                }
            }
        }

        public static void CopyProperties(object source, SafeDictionary<string, string> target)
        {
            foreach (var propertyInfo in source.GetType().GetProperties())
            {
                var obj = propertyInfo.GetValue(source, null);
                if (obj != null)
                {
                    target[propertyInfo.Name.Replace("_", "-")] = obj.ToString();
                }
            }
        }
    }
}

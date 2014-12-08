using System;
using System.Collections.Generic;
using Fortis.Helpers;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Pipelines;
using Sitecore.Pipelines.RenderField;
using System.Web;
using Fortis.Providers;

namespace Fortis.Model.Fields
{
	public class FieldWrapper : IFieldWrapper
	{
		protected readonly ISpawnProvider SpawnProvider;
		private bool _modified;
		private Field _field;
		private ItemWrapper _item;
		private string _rawValue;
		private string _key;

		private Stack<string> _endFieldStack;

		protected virtual Stack<string> EndFieldStack
		{
			get
			{
				return _endFieldStack ?? (_endFieldStack = new Stack<string>());
			}
		}

		public virtual IHtmlString RenderBeginField(string parameters = null, bool editing = true)
		{
			var renderFieldArgs = new RenderFieldArgs
			{
				Item = Field.Item,
				FieldName = Field.Name,
				DisableWebEdit = !editing,
				RawParameters = parameters ?? string.Empty
			};

			if (renderFieldArgs.Item == null)
			{
				return new HtmlString(string.Empty);
			}

			CorePipeline.Run("renderField", renderFieldArgs);
			var result = renderFieldArgs.Result;
			var str = result.FirstPart ?? string.Empty;
			EndFieldStack.Push(result.LastPart ?? string.Empty);

			return new HtmlString(str);
		}

		public virtual IHtmlString RenderBeginField(object parameters, bool editing = true)
		{
			var renderFieldArgs = new RenderFieldArgs
			{
				Item = Field.Item,
				FieldName = Field.Name,
				DisableWebEdit = !editing
			};

			if (parameters != null)
			{
				TypeHelper.CopyProperties(parameters, renderFieldArgs);
				TypeHelper.CopyProperties(parameters, renderFieldArgs.Parameters);
			}

			if (renderFieldArgs.Item == null)
			{
				return new HtmlString(string.Empty);
			}

			CorePipeline.Run("renderField", renderFieldArgs);
			var result = renderFieldArgs.Result;
			var str = result.FirstPart ?? string.Empty;
			EndFieldStack.Push(result.LastPart ?? string.Empty);

			return new HtmlString(str);
		}

		public virtual IHtmlString RenderEndField()
		{
			if (EndFieldStack.Count == 0)
			{
				throw new InvalidOperationException("There was a call to EndField with no corresponding call to BeginField");
			}

			return new HtmlString(EndFieldStack.Pop());
		}

		public FieldWrapper(Field field, ISpawnProvider spawnProvider)
		{
			Sitecore.Diagnostics.Assert.ArgumentNotNull(field, "field");

			_modified = false;
			_field = field;

			SpawnProvider = spawnProvider;
		}

		public FieldWrapper(string key, ref ItemWrapper item, string value, ISpawnProvider spawnProvider)
		{
			Sitecore.Diagnostics.Assert.ArgumentNotNull(key, "key");
			Sitecore.Diagnostics.Assert.ArgumentNotNull(item, "item");

			_key = key;
			_item = item;
			_rawValue = value;

			SpawnProvider = spawnProvider;
		}

		protected Field Field
		{
			get
			{
				if (IsLazy && _item != null)
				{
					_field = _item.Item.Fields[_key];

					if (_field == null)
					{
						throw new Exception("Fortis: Field " + _key + " does not exist in item " + _item.ItemID);
					}

					if (!SpawnProvider.TemplateMapProvider.IsCompatibleFieldType(_field.Type, this.GetType()))
					{
						throw new Exception("Fortis: Field " + _key + " of type " + _field.Type + " for item " + _item.ItemID + " is not compatible with Fortis type " + this.GetType());
					}
				}

				return _field;
			}
		}

		public Database Database
		{
			get
			{
				if (Sitecore.Context.Database == null)
				{
					if (Field != null)
					{
						return Field.Database;
					}
					if (_item != null)
					{
						return _item.Database;
					}
				}
				return Sitecore.Context.Database;
			}
		}

		public bool Modified
		{
			get { return _modified; }
		}

		public object Original
		{
			get { return Field; }
		}

		public string RawValue
		{
			get
			{
				if (_field == null)
				{
					return _rawValue;
				}

				return Field.Value;
			}
			set
			{
				if (!Field.Item.Editing.IsEditing)
				{
					Field.Item.Editing.BeginEdit();
				}

				Field.Value = value;
				_rawValue = value;
				_modified = true;
			}
		}

		public virtual IHtmlString Render(string parameters = null, bool editing = true)
		{
			return new HtmlString(RenderBeginField(parameters, editing) + RenderEndField().ToString());
		}

		public IHtmlString Render(object parameters, bool editing = true)
		{
			return new HtmlString(RenderBeginField(parameters, editing) + RenderEndField().ToString());
		}

		public override string ToString()
		{
			return RawValue;
		}

		public static implicit operator string(FieldWrapper field)
		{
			return field.RawValue;
		}

		public string ToHtmlString()
		{
			return Render().ToString();
		}

		public bool IsLazy
		{
			get { return _field == null; }
		}
	}
}

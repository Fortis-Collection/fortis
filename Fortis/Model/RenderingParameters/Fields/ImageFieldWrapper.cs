using System;
using System.Web;
using Fortis.Model.Fields;
using Fortis.Providers;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Resources.Media;
using Sitecore.Web.UI.WebControls;
using Sitecore.Xml;

namespace Fortis.Model.RenderingParameters.Fields
{
	public class ImageFieldWrapper : FieldWrapper, IImageFieldWrapper
	{
	    private static MediaItem _media;
	    public MediaItem Media
	    {
	        get
	        {
	            if (_media != null)
	                return _media;

	            if (string.IsNullOrWhiteSpace(Value))
                    return null;

	            var imageId = XmlUtil.GetAttribute("mediaid", XmlUtil.LoadXml(Value));
	            _media = Sitecore.Context.Database.GetItem(imageId);

                return _media;
	        }
	    }

	    public ImageFieldWrapper(string value, ISpawnProvider spawnProvider)
			: base(value, spawnProvider) { }

		public string AltText => Media?.Alt;

	    public string GetSourceUri()
	    {
	        if (Media == null)
	            return string.Empty;

	        return MediaManager.GetMediaUrl(Media);
	    }

	    public string GetSourceUri(bool absolute)
        {
            if (Media == null)
            {
                return string.Empty;
            }

            return MediaManager.GetMediaUrl(Media, new MediaUrlOptions() { AbsolutePath = absolute });
        }

	    public IHtmlString Render(ImageFieldWrapperOptions options)
	    {
	        throw new NotImplementedException();
	    }

	    public T GetTarget<T>() where T : IItemWrapper
	    {
	        throw new NotImplementedException();
	    }

	    public string Value { get; set; }
    }
}

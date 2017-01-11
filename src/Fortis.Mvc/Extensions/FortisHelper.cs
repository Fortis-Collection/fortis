namespace Fortis.Mvc.Extensions
{
	using System.Web.Mvc;
	using Fortis.Mvc.Ui;

	public static class FortisHelper
	{
		/// <summary>
		/// Begins the edit frame.
		/// </summary>
		/// <param name="helper"></param>
		/// <param name="buttons">The buttons.</param>
		/// <param name="dataSource">The data source.</param>
		/// <returns></returns>
		public static FortisEditFrame BeginEditFrame(this HtmlHelper helper, string buttons, string dataSource)
		{
			return BeginEditFrame(helper, buttons, dataSource, string.Empty);
		}

		/// <summary>
		/// Begins the edit frame.
		/// </summary>
		/// <param name="helper"></param>
		/// <param name="buttons">The buttons.</param>
		/// <param name="dataSource">The data source.</param>
		/// <param name="title">The title for the edit frame</param>
		/// <returns></returns>
		public static FortisEditFrame BeginEditFrame(this HtmlHelper helper, string buttons, string dataSource, string title)
		{
			var frame = new FortisEditFrame(title, buttons, dataSource, helper.ViewContext);
			frame.RenderFirstPart();
			return frame;
		}

		/// <summary>
		/// Creates an Edit Frame using the Default Buttons list
		/// </summary>
		/// <param name="helper"></param>
		/// <param name="dataSource"></param>
		/// <returns></returns>
		public static FortisEditFrame BeginEditFrame(this HtmlHelper helper, string dataSource)
		{
			var frame = new FortisEditFrame(string.Empty, FortisEditFrame.DefaultEditButtons, dataSource, helper.ViewContext);
			frame.RenderFirstPart();
			return frame;
		}

		/// <summary>
		/// Creates an edit frame using the current context item
		/// </summary>
		/// <returns></returns>
		public static FortisEditFrame BeginEditFrame(this HtmlHelper helper)
		{
			var frame = new FortisEditFrame(string.Empty, FortisEditFrame.DefaultEditButtons, helper.ViewContext);
			frame.RenderFirstPart();
			return frame;
		}
	}
}

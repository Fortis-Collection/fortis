namespace Fortis.Mvc.Ui
{
	using System;
	using System.Web.Mvc;
	using System.Web.UI;
	using Sitecore.Web.UI.WebControls;

	public class FortisEditFrame : IDisposable
	{
		public const string DefaultEditButtons = "/sitecore/content/Applications/WebEdit/Edit Frame Buttons/Default";

		private readonly EditFrame frame;
		private readonly Html32TextWriter writer;

		private bool disposed;

		/// <summary>
		/// Initializes a new instance of the <see cref="FortisEditFrame" /> class.
		/// </summary>
		/// <param name="title">The title of the frame</param>
		/// <param name="buttons">The buttons.</param>
		/// <param name="viewContext"></param>
		public FortisEditFrame(string title, string buttons, ViewContext viewContext) : this(title, buttons, "", viewContext)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="FortisEditFrame" /> class.
		/// </summary>
		/// <param name="title">The title of the frame</param>
		/// <param name="buttons">The buttons.</param>
		/// <param name="dataSource">The data source.</param>
		/// <param name="viewContext"></param>
		public FortisEditFrame(string title, string buttons, string dataSource, ViewContext viewContext)
		{
			this.writer = new Html32TextWriter(viewContext.Writer);
			this.frame = new EditFrame
			{
				DataSource = dataSource,
				Buttons = buttons,
				Title = title
			};
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="FortisEditFrame"/> class.
		/// </summary>
		/// <param name="frame">The frame.</param>
		/// <param name="viewContext"></param>
		public FortisEditFrame(EditFrame frame, ViewContext viewContext)
		{
			this.frame = frame;
			this.writer = new Html32TextWriter(viewContext.Writer);
		}

		/// <summary>
		/// Renders the first part.
		/// </summary>
		public virtual void RenderFirstPart()
		{
			this.frame.RenderFirstPart(this.writer);
		}

		/// <summary>Releases all resources that are used by the current instance of the <see cref="T:System.Web.Mvc.Html.MvcForm" /> class.</summary>
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases unmanaged and, optionally, managed resources used by the current instance of the <see cref="T:System.Web.Mvc.Html.MvcForm" /> class.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		protected virtual void Dispose(bool disposing)
		{
			if (this.disposed)
			{
				return;
			}
			this.disposed = true;
			this.frame.RenderLastPart(this.writer);
		}

		/// <summary>Ends the form and disposes of all form resources.</summary>
		public void EndForm()
		{
			this.Dispose(true);
		}
	}
}

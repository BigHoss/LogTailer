namespace LogTailer.Ui.Views.File
{
    using System;

    public partial class FileDisplayView
    {
        protected override void OnInitialized(EventArgs e)
        {
            LinesDataGrid.SearchHelper = new FileDisplaySearchExtension(LinesDataGrid);
            base.OnInitialized(e);
        }
    }
}

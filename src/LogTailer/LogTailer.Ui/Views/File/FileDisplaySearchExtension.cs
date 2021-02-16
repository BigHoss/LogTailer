namespace LogTailer.Ui.Views.File
{
    using Models;
    using Syncfusion.UI.Xaml.Grid;

    public class FileDisplaySearchExtension : SearchHelper
    {
        public FileDisplaySearchExtension(SfDataGrid sfDataGrid) : base(sfDataGrid)
        {
        }

        protected override bool SearchCell(DataColumnBase column,
                                           object record,
                                           bool ApplySearchHighlightBrush)
        {
            if(column.GridColumn.MappingName == nameof(FileLine.Text))
            {
                return base.SearchCell(column, record, ApplySearchHighlightBrush);
            }
            return false;
        }
    }
}

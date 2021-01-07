namespace LogTailer.Ui.Pages.File
{
    using Syncfusion.UI.Xaml.Grid;
    using Syncfusion.UI.Xaml.ScrollAxis;

    public partial class FileDisplayView
    {
      private void SelectionChanged(object sender, GridSelectionChangedEventArgs e)
      {
          var selectedIndex = LinesDataGrid.SelectedIndex;
          if (selectedIndex < 0)
          {
              selectedIndex = 0;
          }

          LinesDataGrid.ScrollInView(new RowColumnIndex(selectedIndex,
              LinesDataGrid.ResolveToStartColumnIndex()));
      }
    }
}

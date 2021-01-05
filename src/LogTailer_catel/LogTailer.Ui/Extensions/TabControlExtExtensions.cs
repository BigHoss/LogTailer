namespace LogTailer.Ui.Extensions
{
    using System.Linq;
    using Models;
    using Syncfusion.Windows.Tools.Controls;

    public static class TabControlExtExtensions
    {
        public static bool RemoveAndUpdateSelection(this TabControlExt tabcontrol,
                                                    object item)
        {
            var index = tabcontrol.Items.IndexOf(item);
            if (index == -1)
            {
                return false;
            }

            var wasSelected = ReferenceEquals(tabcontrol.SelectedItem, item);

            tabcontrol.Items.RemoveAt(index);

            if (wasSelected)
            {
                var newIndex = index;
                var newItem = newIndex < tabcontrol.Items.Count
                    ? tabcontrol.Items[newIndex]
                    : tabcontrol.Items.OfType<MainTabItem>().LastOrDefault();

                tabcontrol.SetCurrentValue(System.Windows.Controls.Primitives.Selector.SelectedItemProperty, newItem);
            }

            return true;
        }
    }
}

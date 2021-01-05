namespace LogTailer.Ui.Services
{
    using System;
    using System.Collections.Generic;
    using Models;
    using Syncfusion.Windows.Tools.Controls;

    public interface IMainTabService
    {
        IEnumerable<MainTabItem> Tabs { get; }
        MainTabItem? SelectedTab { get; }
        void SetTabControl(TabControlExt inputTabControl);
        event EventHandler<TabItemEventArgs> SelectedTabChanged;
        bool IsVisible(MainTabItem item);
        bool IsActive(MainTabItem item);
        void Add(MainTabItem item);
        void Activate(MainTabItem item);
        void Remove(MainTabItem item);
    }
}

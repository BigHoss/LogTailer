// ***********************************************************************
// Assembly         : LogTailer.Ui
// Author           : Rku
// Created          : 01-04-2021
// ***********************************************************************
// ReSharper disable ClassNeverInstantiated.Global
namespace LogTailer.Ui.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Controls;
    using System.Windows.Threading;
    using Catel;
    using Catel.IoC;
    using Catel.Logging;
    using Extensions;
    using Models;
    using Syncfusion.Windows.Tools.Controls;

    /// <summary>
    /// Class MainTabService.
    /// Implements the <see cref="LogTailer.Ui.Services.IMainTabService" />
    /// </summary>
    /// <seealso cref="LogTailer.Ui.Services.IMainTabService" />
    [ServiceLocatorRegistration(typeof(IMainTabService))]
    public class MainTabService : IMainTabService
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        private TabControlExt? tabControl;


        /// <summary>
        /// Gets the tabs.
        /// </summary>
        /// <value>The tabs.</value>
        public IEnumerable<MainTabItem> Tabs
            => tabControl == null ? new List<MainTabItem>() : tabControl.Items.OfType<MainTabItem>();

        /// <summary>
        /// Gets the selected tab.
        /// </summary>
        /// <value>The selected tab.</value>
        public MainTabItem? SelectedTab => tabControl?.SelectedItem as MainTabItem;

        /// <summary>
        /// Occurs when [selected tab changed].
        /// </summary>
        public event EventHandler<TabItemEventArgs>? SelectedTabChanged;

        /// <summary>
        /// Sets the tab control.
        /// </summary>
        /// <param name="inputTabControl">The tab control.</param>
        public void SetTabControl(TabControlExt inputTabControl)
        {
            Argument.IsNotNull(() => inputTabControl);
            if (tabControl != null)
            {
                tabControl.SelectionChanged -= TabControlOnSelectionChanged;
            }

            tabControl = inputTabControl;
            tabControl.SelectionChanged += TabControlOnSelectionChanged;

        }

        /// <summary>
        /// Determines whether the specified item is visible.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns><c>true</c> if the specified item is visible; otherwise, <c>false</c>.</returns>
        public bool IsVisible(MainTabItem item)
        {
            Argument.IsNotNull(() => item);

            return tabControl != null && tabControl.Items.OfType<MainTabItem>().Any(x => ReferenceEquals(item, x));
        }

        /// <summary>
        /// Determines whether the specified item is active.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns><c>true</c> if the specified item is active; otherwise, <c>false</c>.</returns>
        public bool IsActive(MainTabItem item)
        {
            Argument.IsNotNull(() => item);

            return tabControl != null && ReferenceEquals(SelectedTab, item);
        }

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Add(MainTabItem item)
        {
            Argument.IsNotNull(() => item);

            if (tabControl == null)
            {
                return;
            }

            Insert(tabControl.Items.Count, item);
        }

        private void Insert(int index,
                            MainTabItem item)
        {
            Argument.IsNotNull(() => item);

            if (tabControl == null)
            {
                return;
            }

            if (IsVisible(item))
            {
                return;
            }

            item.Closed += OnTabItemClosed;

            tabControl.Items.Insert(index, item);
        }

        /// <summary>
        /// Activates the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Activate(MainTabItem item)
        {
            Argument.IsNotNull(() => item);

            if (tabControl == null)
            {
                return;
            }

            if (!IsVisible(item))
            {
                throw Log.ErrorAndCreateException<InvalidOperationException>(
                    "Tab item is not visible, use Show() method first");
            }

            tabControl.SetCurrentValue(System.Windows.Controls.Primitives.Selector.SelectedItemProperty, item);
        }

        /// <summary>
        /// Removes the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Remove(MainTabItem item)
        {
            Argument.IsNotNull(() => item);

            if (tabControl == null)
            {
                return;
            }

            item.Closed -= OnTabItemClosed;

            tabControl.RemoveAndUpdateSelection(item);
        }

        private void OnTabItemClosed(object? sender,
                                  EventArgs e)
        {
            if (sender is MainTabItem item)
            {
                item.Closed -= OnTabItemClosed;

                if (tabControl == null)
                {
                    return;
                }

                tabControl.Dispatcher.BeginInvoke(() => Remove(item));
            }
        }

        private void TabControlOnSelectionChanged(object sender,
                                                  SelectionChangedEventArgs e)
        {
            if (SelectedTab != null)
            {
                SelectedTabChanged?.Invoke(this, new TabItemEventArgs(SelectedTab));
            }
        }
    }

    /// <summary>
    /// Class TabItemEventArgs.
    /// </summary>
    public class TabItemEventArgs
    {
        /// <summary>
        /// Gets or sets the item.
        /// </summary>
        /// <value>The item.</value>
        public MainTabItem Item { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TabItemEventArgs"/> class.
        /// </summary>
        /// <param name="item">The item.</param>
        public TabItemEventArgs(MainTabItem item) => Item = item;
    }
}

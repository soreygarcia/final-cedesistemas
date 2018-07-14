using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Traveler.Controls
{
    public class ExtendedListView : ListView
    {
        public ExtendedListView() : this(ListViewCachingStrategy.RecycleElement)
        {
        }

        public ExtendedListView(ListViewCachingStrategy cachingStrategy) : base(cachingStrategy)
        {
            this.ItemSelected += OnItemSelected;
            this.ItemTapped += OnItemTapped;
            this.ItemAppearing += OnItemAppearing;
        }

        #region -- Public properties --

        public static readonly BindableProperty TappedCommandProperty =
            BindableProperty.Create(nameof(TappedCommand), typeof(ICommand), typeof(ExtendedListView), default(ICommand));

        public ICommand TappedCommand
        {
            get { return (ICommand)GetValue(TappedCommandProperty); }
            set { SetValue(TappedCommandProperty, value); }
        }

        public static readonly BindableProperty ItemAppearingCommandProperty =
            BindableProperty.Create(nameof(ItemAppearingCommand), typeof(ICommand), typeof(ExtendedListView), default(ICommand));

        public ICommand ItemAppearingCommand
        {
            get { return (ICommand)GetValue(ItemAppearingCommandProperty); }
            set { SetValue(ItemAppearingCommandProperty, value); }
        }

        public static readonly BindableProperty PageSizeProperty =
            BindableProperty.Create(nameof(PageSize), typeof(int?), typeof(ExtendedListView), default(int?));

        public int? PageSize
        {
            get { return (int?)GetValue(PageSizeProperty); }
            set { SetValue(PageSizeProperty, value); }
        }

        public static readonly BindableProperty LoadMoreCommandProperty =
                    BindableProperty.Create(nameof(LoadMoreCommand), typeof(ICommand), typeof(ExtendedListView), default(ICommand));

        public ICommand LoadMoreCommand
        {
            get { return (ICommand)GetValue(LoadMoreCommandProperty); }
            set { SetValue(LoadMoreCommandProperty, value); }
        }

        public static readonly BindableProperty IsSelectionEnabledProperty =
            BindableProperty.Create(nameof(IsSelectionEnabled), typeof(bool), typeof(ExtendedListView), default(bool));

        public bool IsSelectionEnabled
        {
            get { return (bool)GetValue(IsSelectionEnabledProperty); }
            set { SetValue(IsSelectionEnabledProperty, value); }
        }
        #endregion

        #region -- Private helpers --

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var listView = (ExtendedListView)sender;

            if (!listView.IsSelectionEnabled)
            {
                if (e == null) return;

                listView.SelectedItem = null;
            }
        }

        private void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (TappedCommand != null)
            {
                TappedCommand?.Execute(e.Item);
            }
            if (!IsSelectionEnabled)
                SelectedItem = null;
        }

        private void OnItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            if (ItemAppearingCommand != null)
            {
                ItemAppearingCommand?.Execute(e.Item);
            }

            if (LoadMoreCommand != null)
            {
                if (IsGroupingEnabled)
                {
                    if (PageSize == null)
                    {
                        var firstItem = ((IList)ItemsSource.Cast<object>().First()).Cast<object>().First();

                        if (e.Item == firstItem && !IsRefreshing)
                        {
                            LoadMoreCommand?.Execute(null);
                        }
                    }
                    else
                    {
                        var firstItem = ((IList)ItemsSource.Cast<object>().First()).Cast<object>().First();

                        var totalItems = 0;
                        foreach (var item in ItemsSource.Cast<object>())
                        {
                            totalItems = totalItems + ((IList)item).Cast<object>().Count();
                        }

                        if (e.Item == firstItem && !IsRefreshing
                            & totalItems >= PageSize.Value)
                        {
                            LoadMoreCommand?.Execute(null);
                        }
                    }
                }
                else
                {
                    if (PageSize == null)
                    {
                        if (e.Item == ItemsSource.Cast<object>().First() && !IsRefreshing)
                        {
                            LoadMoreCommand?.Execute(null);
                        }
                    }
                    else
                    {
                        if (e.Item == ItemsSource.Cast<object>().First() && !IsRefreshing
                            & ItemsSource.Cast<object>().Count() >= PageSize.Value)
                        {
                            LoadMoreCommand?.Execute(null);
                        }
                    }
                }
            }

        }

        #endregion
    }
}
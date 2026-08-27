using System;
using System.Collections.Generic;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Windows.ApplicationModel.DataTransfer;
using NotchBox.Core;
using NotchBox.OS;
using WinRT.Interop;

namespace NotchBox.UI
{
    public sealed partial class NotchShell : Window
    {
        private readonly StateManager _stateManager;
        private readonly GhostSyncEngine _ghostSyncEngine;
        private readonly List<string> _shelfItems = new();

        public NotchShell()
        {
            this.InitializeComponent();

            var hWnd = WindowNative.GetWindowHandle(this);
            WindowHooks.ApplyTopMostAndToolWindow(hWnd);

            this.Title = AppInfo.FullTitle;

            _stateManager = new StateManager();
            _ghostSyncEngine = new GhostSyncEngine();

            _stateManager.OnStateChanged += HandleStateChanged;

            _ghostSyncEngine.OnGhostItemReceived += (meta) =>
            {
                DispatcherQueue.TryEnqueue(() => _stateManager.TransitionTo(AppState.GhostPending));
            };

            _ghostSyncEngine.OnGhostItemRemoved += (fileName) =>
            {
                DispatcherQueue.TryEnqueue(() => _stateManager.TransitionTo(AppState.Idle));
            };
        }

        private void RootGrid_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            if (_stateManager.CurrentState == AppState.Idle)
            {
                _stateManager.TransitionTo(AppState.Expanded);
            }
        }

        private void RootGrid_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            if (_stateManager.CurrentState == AppState.Expanded && _shelfItems.Count == 0)
            {
                _stateManager.TransitionTo(AppState.Idle);
            }
        }

        private void RootGrid_DragOver(object sender, DragEventArgs e)
        {
            e.AcceptedOperation = DataPackageOperation.Copy;
            if (_stateManager.CurrentState != AppState.Expanded)
            {
                _stateManager.TransitionTo(AppState.Expanded);
            }
        }

        private async void RootGrid_Drop(object sender, DragEventArgs e)
        {
            if (e.DataView.Contains(StandardDataFormats.StorageItems))
            {
                var items = await e.DataView.GetStorageItemsAsync();
                foreach (var item in items)
                {
                    _shelfItems.Add(item.Path);
                }

                if (_shelfItems.Count > 0)
                {
                    _stateManager.TransitionTo(AppState.HoldingItems);
                }
            }
        }

        private void HandleStateChanged(AppState newState)
        {
            DispatcherQueue.TryEnqueue(() =>
            {
                switch (newState)
                {
                    case AppState.Idle:
                        PillBorder.Width = 180;
                        PillBorder.Height = 32;
                        StatusDot.Fill = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 0, 255, 102));
                        BrandingText.Text = AppInfo.Name;
                        break;

                    case AppState.Expanded:
                        PillBorder.Width = 320;
                        PillBorder.Height = 120;
                        StatusDot.Fill = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 0, 153, 255));
                        BrandingText.Text = "Drop items here...";
                        break;

                    case AppState.HoldingItems:
                        PillBorder.Width = 360;
                        PillBorder.Height = 140;
                        StatusDot.Fill = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 255, 187, 0));
                        BrandingText.Text = $"{_shelfItems.Count} item(s) on shelf";
                        break;

                    case AppState.GhostPending:
                        PillBorder.Width = 320;
                        PillBorder.Height = 100;
                        StatusDot.Fill = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 187, 51, 255));
                        BrandingText.Text = "Shared item available!";
                        break;

                    case AppState.Downloading:
                        StatusDot.Fill = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 255, 85, 0));
                        BrandingText.Text = "Transferring payload...";
                        break;
                }
            });
        }

        public string GetHeaderBranding()
        {
            return $"{AppInfo.Name} v{AppInfo.Version} by {AppInfo.Author}";
        }
    }
}

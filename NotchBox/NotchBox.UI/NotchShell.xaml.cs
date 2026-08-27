using Microsoft.UI.Xaml;
using NotchBox.Core;
using NotchBox.OS;
using WinRT.Interop;

namespace NotchBox.UI
{
    public sealed partial class NotchShell : Window
    {
        private readonly StateManager _stateManager;
        private readonly GhostSyncEngine _ghostSyncEngine;

        public NotchShell()
        {
            this.InitializeComponent();

            var hWnd = WindowNative.GetWindowHandle(this);
            WindowHooks.ApplyTopMostAndToolWindow(hWnd);

            this.Title = AppInfo.FullTitle;

            _stateManager = new StateManager();
            _ghostSyncEngine = new GhostSyncEngine();

            _ghostSyncEngine.OnGhostItemReceived += (meta) =>
            {
                _stateManager.TransitionTo(AppState.GhostPending);
            };

            _ghostSyncEngine.OnGhostItemRemoved += (fileName) =>
            {
                _stateManager.TransitionTo(AppState.Idle);
            };
        }

        public string GetHeaderBranding()
        {
            return $"{AppInfo.Name} v{AppInfo.Version} by {AppInfo.Author}";
        }
    }
}

using System;

namespace NotchBox.Core
{
    public class StateManager
    {
        public AppState CurrentState { get; private set; } = AppState.Idle;

        public event Action<AppState>? OnStateChanged;

        public void TransitionTo(AppState newState)
        {
            if (CurrentState != newState)
            {
                CurrentState = newState;
                OnStateChanged?.Invoke(CurrentState);
            }
        }
    }
}

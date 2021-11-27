using System;
using Enums;

namespace GlobalManagers
{
    public static class PointerStateManager
    {
        private static PointerStateType currentState;

        public static event Action<PointerStateType> onPointerStateChanged;

        public static PointerStateType CurrentState
        {
            get => currentState;

            set
            {
                if (currentState == value)
                    return;

                currentState = value;

                onPointerStateChanged?.Invoke(currentState);
            }
        }
    }
}
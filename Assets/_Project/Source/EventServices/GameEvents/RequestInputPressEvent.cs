using UnityEngine.InputSystem;

namespace Source.EventServices.GameEvents
{
    public class RequestInputPressEvent : GameEvent
    {
        public readonly double Duration;
        public readonly InputActionPhase CurrentPhase;

        public RequestInputPressEvent(double duration, InputActionPhase currentPhase)
        {
            Duration = duration;
            CurrentPhase = currentPhase;
        }
    }
}


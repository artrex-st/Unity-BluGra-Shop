using UnityEngine;
using UnityEngine.InputSystem;

namespace Source.EventServices.GameEvents
{
    public class InputAxisEvent : GameEvent
    {
        public readonly Vector2 MoveAxis;
        public readonly double Duration;
        public readonly InputActionPhase CurrentPhase;

        public InputAxisEvent(Vector2 moveAxis, double duration, InputActionPhase currentPhase)
        {
            MoveAxis = moveAxis;
            Duration = duration;
            CurrentPhase = currentPhase;
        }
    }
}

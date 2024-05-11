using UnityEngine;

namespace Source.EventServices.GameEvents
{
    public class RequestMoveAnimationEvent : GameEvent
    {
        public readonly Vector2 Speed;

        public RequestMoveAnimationEvent(Vector2 speed)
        {
            Speed = speed;
        }
    }
}

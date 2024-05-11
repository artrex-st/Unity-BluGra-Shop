namespace Source.EventServices.GameEvents
{
    public class RequestMoveAnimationEvent : GameEvent
    {
        public readonly float Speed;

        public RequestMoveAnimationEvent(float speed)
        {
            Speed = speed;
        }
    }
}

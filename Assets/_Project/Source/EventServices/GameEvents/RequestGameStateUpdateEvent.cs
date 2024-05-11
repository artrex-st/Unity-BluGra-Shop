namespace Source.EventServices.GameEvents
{
    public class RequestGameStateUpdateEvent : GameEvent
    {
        public readonly GameStates NewGameState;

        public RequestGameStateUpdateEvent(GameStates newGameState)
        {
            NewGameState = newGameState;
        }
    }
}

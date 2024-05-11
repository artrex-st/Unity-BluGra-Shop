namespace Source.EventServices.GameEvents
{
    public class ResponseGameStateUpdateEvent : GameEvent
    {
        public readonly GameStates CurrentGameState;

        public ResponseGameStateUpdateEvent(GameStates currentGameState)
        {
            CurrentGameState = currentGameState;
        }
    }
}

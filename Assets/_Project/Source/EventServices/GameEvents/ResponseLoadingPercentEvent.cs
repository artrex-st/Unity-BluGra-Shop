namespace Source.EventServices.GameEvents
{
    public class ResponseLoadingPercentEvent : GameEvent
    {
        public readonly float Percent;
        public readonly float FakeLoadingTime;

        public ResponseLoadingPercentEvent(float percent, float fakeLoadingTime = 0)
        {
            Percent = percent;
            FakeLoadingTime = fakeLoadingTime;
        }
    }
}

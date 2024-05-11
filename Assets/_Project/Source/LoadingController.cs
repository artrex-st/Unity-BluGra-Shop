using Source.EventServices.GameEvents;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Source
{
    public class LoadingController : BaseScreen
    {
        [SerializeField] private Button _cancelButton;
        [SerializeField] private TextMeshProUGUI _textLoadingPercent;
        [SerializeField] private Slider _loadBar;
        [SerializeField] private float _loadBarSpeed = 10f;
        private float _target;

        private IEventsService _eventsService;

        private void Start()
        {
            Initialize();
        }

        private void Update()
        {
            _loadBar.value = Mathf.Lerp(_loadBar.value, _target, _loadBarSpeed * Time.deltaTime);
            _textLoadingPercent.text = $"<color=#00FFFF>Loading...</color> {(_loadBar.value * 100):N0}%";
        }

        private void OnDestroy()
        {
            Dispose();
        }

        private new void Initialize()
        {
            base.Initialize();
            _eventsService = ServiceLocator.Instance.GetService<IEventsService>();

            _loadBar.value = 0;
            _eventsService.AddListener<ResponseLoadingPercentEvent>(HandlerResponseLoadingPercentEvent, GetHashCode());
        }

        private void HandlerResponseLoadingPercentEvent(ResponseLoadingPercentEvent e)
        {
            _target = e.Percent;
        }

        private new void Dispose()
        {
            base.Dispose();
            _eventsService.RemoveListener<ResponseLoadingPercentEvent>(GetHashCode());
        }
    }
}

using Source.EventServices.GameEvents;
using InputSystem;
using UnityEngine;

namespace GamePlay
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerStatus _status;

        private PlayerMover _playerMover;
        private PlayerInteraction _playerInteraction;
        private InputManager _inputManager;
        private bool _isGameRunning;
        private Rigidbody2D RigidBody => GetComponent<Rigidbody2D>();
        private IEventsService _eventsService;

        private void OnEnable()
        {
            Initialize();
        }

        private void OnDisable()
        {
            Dispose();
        }

        private void Initialize()
        {
            _eventsService = ServiceLocator.Instance.GetService<IEventsService>();
            _eventsService.AddListener<ResponseGameStateUpdateEvent>(HandlerRequestNewGameStateEvent, GetHashCode());

            _inputManager = gameObject.AddComponent<InputManager>();

            _playerMover = gameObject.AddComponent<PlayerMover>();
            _playerMover.Initialize(_status, RigidBody);

            _playerInteraction = gameObject.AddComponent<PlayerInteraction>();
            _playerInteraction.Initialize(_inputManager);
        }

        private void Dispose()
        {
            _eventsService.RemoveListener<ResponseGameStateUpdateEvent>(GetHashCode());
            _playerMover.Dispose();
        }

        private void HandlerRequestNewGameStateEvent(ResponseGameStateUpdateEvent e)
        {
            _isGameRunning = e.CurrentGameState.Equals(GameStates.GameRunning);
            RigidBody.isKinematic = !_isGameRunning;
            _playerInteraction.enabled = _isGameRunning;
            _playerMover.enabled = _isGameRunning;
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            //Gizmos.DrawRay(start.position, target.position);
        }
#endif
    }
}

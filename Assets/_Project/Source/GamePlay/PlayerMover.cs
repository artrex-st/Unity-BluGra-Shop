using Source.EventServices.GameEvents;
using UnityEngine;

namespace GamePlay
{
    public class PlayerMover : MonoBehaviour
    {
        private PlayerStatus _status;
        private Vector2 _inputDirection;
        private Rigidbody2D _rigidBody;
        private IEventsService _eventsService;

        public void Initialize(PlayerStatus status, Rigidbody2D rigidBody)
        {
            _eventsService = ServiceLocator.Instance.GetService<IEventsService>();
            _status = status;
            _rigidBody = rigidBody;
            _eventsService.AddListener<InputAxisEvent>(HandlerStartInputRotationEvent, GetHashCode());
        }

        public void Dispose()
        {
            _eventsService.RemoveListener<InputAxisEvent>(GetHashCode());
        }

        private void FixedUpdate()
        {
            ApplyMove();
        }

        private void ApplyMove()
        {
            _rigidBody.velocity = _inputDirection * _status.MoveSpeed;
        }

        private void HandlerStartInputRotationEvent(InputAxisEvent e)
        {
            _inputDirection = e.MoveAxis;
            _eventsService.Invoke(new RequestMoveAnimationEvent(e.MoveAxis));
        }
    }
}

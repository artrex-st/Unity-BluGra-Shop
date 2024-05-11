using Source.EventServices.GameEvents;
using UnityEngine;

namespace GamePlay
{
    public class PlayerMover : MonoBehaviour
    {
        private PlayerStatus _status;
        private float _inputForward;
        private float _inputRotation;
        private Rigidbody _rigidBody;
        private IEventsService _eventsService;

        public void Initialize(PlayerStatus status, Rigidbody rigidBody)
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

        private void Update()
        {
            ApplyRotation();
        }

        private void FixedUpdate()
        {
            ApplySpeed();
        }

        private void ApplyRotation()
        {
            float rotationAmount = _inputRotation * _status.RotationSpeed * Time.deltaTime;
            Quaternion inputRotation = Quaternion.Euler(0f, rotationAmount, 0f);
            transform.rotation *= inputRotation;
        }

        private void ApplySpeed()
        {
            Vector3 speed = transform.forward * (_inputForward * _status.MoveSpeed);
            speed.y = _rigidBody.velocity.y;
            _rigidBody.velocity = speed;
        }

        private void HandlerStartInputRotationEvent(InputAxisEvent e)
        {
            _inputForward = e.MoveAxis.x;
            _inputRotation = e.MoveAxis.y;
            _eventsService.Invoke(new RequestMoveAnimationEvent(_inputForward));
        }
    }
}

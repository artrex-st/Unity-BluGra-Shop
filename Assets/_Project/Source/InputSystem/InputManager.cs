using Source.EventServices.GameEvents;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InputSystem
{
    public class InputManager : MonoBehaviour
    {
        private InputActions _inputsActions;
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
            _inputsActions = new InputActions();
            _inputsActions.Player.Enable();

            _inputsActions.Player.Axis.started += MoveAxis;
            _inputsActions.Player.Axis.performed += MoveAxis;
            _inputsActions.Player.Axis.canceled += MoveAxis;

            _inputsActions.Player.Press.started += PressStarted;
            _inputsActions.Player.Press.performed += PressStarted;
            _inputsActions.Player.Press.canceled += PressStarted;
        }

        private void MoveAxis(InputAction.CallbackContext context)
        {
            _eventsService.Invoke(new InputAxisEvent(context.ReadValue<Vector2>(), context.duration, context.phase));
        }

        private void PressStarted(InputAction.CallbackContext context)
        {
            _eventsService.Invoke(new RequestInputPressEvent(context.duration, context.phase));
        }

        private static Vector3 ScreenToWorld(Camera camera, Vector3 position)
        {
            position.z = camera.nearClipPlane;
            return camera.ScreenToWorldPoint(position);
        }

        private void Dispose()
        {
            _inputsActions.Player.Axis.Dispose();
            _inputsActions.Player.Press.Dispose();
        }
    }
}

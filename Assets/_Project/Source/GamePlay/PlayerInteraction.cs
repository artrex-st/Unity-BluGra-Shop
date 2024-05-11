using InputSystem;
using UnityEngine;

namespace GamePlay
{
    public class PlayerInteraction : MonoBehaviour
    {
        private InputManager _playerInput;
        public void Initialize(InputManager playerInput)
        {
            _playerInput = playerInput;
        }
    }
}

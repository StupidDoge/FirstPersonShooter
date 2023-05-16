using UnityEngine;
using Core;

namespace StarterAssets
{
    public class UICanvasControllerInput : MonoBehaviour
    {
        private PlayerInputHolder _playerInputHolder;

        private void Start()
        {
            _playerInputHolder = FindObjectOfType<PlayerInputHolder>();
        }

        public void VirtualMoveInput(Vector2 virtualMoveDirection)
        {
            _playerInputHolder.MoveInput(virtualMoveDirection);
        }

        public void VirtualLookInput(Vector2 virtualLookDirection)
        {
            _playerInputHolder.LookInput(virtualLookDirection);
        }

        public void VirtualJumpInput(bool virtualJumpState)
        {
            _playerInputHolder.JumpInput(virtualJumpState);
        }

        public void VirtualSprintInput(bool virtualSprintState)
        {
            _playerInputHolder.SprintInput(virtualSprintState);
        }
    }
}

using System;
using UnityEngine;

namespace InputActions
{
     public abstract class InputActions
    {
        protected PlayerInput _input;

        public InputActions(PlayerInput input)
        {
            _input = input;
        }

        public abstract void EnableInput();
        public abstract void DisableInput();
    }

    public class CameraInput : InputActions
    {
        public event Action<Vector2> OnMouseMove;
        public event Action<Vector2> OnKeyboardMove;
        public event Action<float> OnScroll;

        public CameraInput(PlayerInput input) : base(input)
        {
            _input.Camera.MoveWASD.performed += _ => OnKeyboardMove?.Invoke(_.ReadValue<Vector2>());
            _input.Camera.MoveMouse.performed += _ => OnMouseMove?.Invoke(_.ReadValue<Vector2>());
            _input.Camera.Zoom.performed += _=> OnScroll?.Invoke(_.ReadValue<float>());
        }

        public override void EnableInput()
        {
            _input.Camera.Enable();
        }

        public override void DisableInput()
        {
            _input.Camera.Disable();
        }

    }
}
   
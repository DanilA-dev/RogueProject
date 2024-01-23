using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputActions;

public class InputSystem : BaseMonoSystem
{
    private PlayerInput _input;
    private CameraInput _cameraInput;

    public CameraInput CameraInput => _cameraInput;

    public override void Init()
    {
        _input = new PlayerInput();
        _cameraInput = new CameraInput(_input);

        EnableInput();
    }

    private void EnableInput()
    {
        _cameraInput.EnableInput();
    }

    
}

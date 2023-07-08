using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, CameraControls.IMyCamActions
{
    public Vector2 MovementValue { get; private set; }
    public Vector2 RotateValue { get; private set; }
    public float ZoomValue { get; private set; }
    public bool UnlockRotation { get; private set; }

    public GameObject target { get; private set; }

    private CameraControls controls;
    private void Start()
    {
        // At start, create new controls object 🔫
        controls = new CameraControls();

        // point that new controls object to this script to see its methods
        controls.MyCam.SetCallbacks(this);

        controls.MyCam.Enable();
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        MovementValue = context.ReadValue<Vector2>();
    }

    public void OnRotateCamera(InputAction.CallbackContext context)
    {
        if (!UnlockRotation) return;
        RotateValue = context.ReadValue<Vector2>();
    }

    public void OnZoomCamera(InputAction.CallbackContext context)
    {
        
    }

    public void OnSelect(InputAction.CallbackContext context)
    {

    }

    public void OnCancel(InputAction.CallbackContext context)
    {
    }

    public void OnEnableRotion(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            UnlockRotation = true;
        }
        else if (context.canceled)
        {
            UnlockRotation = false;
        }
    }
}

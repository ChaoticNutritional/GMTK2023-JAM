using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEditor.Events;


public class CameraController : MonoBehaviour, CameraControls.IMyCamActions
{
    private CameraControls camControls;
    private InputAction movement;
    public Transform cameraTransform;
    public Vector2 MovementValue { get; private set; }
    public Vector2 RotateValue { get; private set; }
    public float ZoomValue { get; private set; }
    public bool UnlockRotation { get; private set; }
    public GameObject target { get; private set; }

    private CameraControls controls;

    // Variables for camera controls
    // panning motion
    [SerializeField] private float panMoveSpeed = 5f;
    [SerializeField] private float panTopSpeed = 15f;
    [SerializeField] private float panAcceleration = 10f;
    [SerializeField] private float damping = 10f;

    // zoom motion
    [SerializeField] private float zoomSpeed;
    [SerializeField] private float stepSize;
    [SerializeField] private float zoomDamping;
    [SerializeField] private float minHeight = 5f;
    [SerializeField] private float maxHeight = 50f;

    // rotation
    [SerializeField] private float rotationTopSpeed = 3f;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float rotationAcceleration = 5f;

    // screen edge motion
    [SerializeField] private bool useScreenEdge = true;

    // Where we're trying to move TO
    // We will be lerping towards that position
    // this way multiple functions can affect this
    private Vector3 targetPosition;
    private float zoomHeight;
    private Vector3 horizontalVelocity;
    private Vector3 lastPos;

    // Stores the position on screen where we started to drag the mouse
    // can this be vector2 and reference screenpos?
    private Vector3 startDrag;

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
        float rotationValue = context.ReadValue<Vector2>().x;
        transform.rotation = Quaternion.Euler(0f, rotationValue * rotationSpeed + transform.rotation.eulerAngles.y, 0f);
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

    private void Awake()
    {
        camControls = new CameraControls();
    }

    private void OnEnable()
    {
        zoomHeight = cameraTransform.localPosition.y;
        cameraTransform.LookAt(this.transform);
        lastPos = this.transform.position;
        movement = camControls.MyCam.Movement;
        camControls.MyCam.Enable();
    }

    private void ZoomCam(InputAction.CallbackContext context)
    {
        float value = -context.ReadValue<Vector2>().y / 100f;

        if (Mathf.Abs(value) > 0.1f)
        {
            zoomHeight = cameraTransform.localPosition.y + value * stepSize;
            if (zoomHeight < minHeight)
            {
                zoomHeight = minHeight;
            }
            else if (zoomHeight > maxHeight)
            {
                zoomHeight = maxHeight;
            }
        }
    }

    private void Update()
    {
        CalculateMovement();
        UpdateZoom();
        UpdateVelocity();
    }

    private Vector3 GetCameraForward()
    {
        Vector3 forward = cameraTransform.forward;
        forward.y = 0;
        return forward;
    }

    private Vector3 GetCameraRight()
    {
        Vector3 right = cameraTransform.right;
        right.y = 0;
        //Debug.Log("CAMERA RIGHT: " + right);
        return right;
    }

    private void UpdateVelocity()
    {
        horizontalVelocity = (this.transform.position - lastPos) / Time.deltaTime;
        horizontalVelocity.y = 0;
        lastPos = this.transform.position;
    }

    private void CalculateMovement()
    {

        targetPosition += MovementValue.x * transform.right;
        targetPosition += MovementValue.y * transform.forward;
        targetPosition = targetPosition.normalized;
        //Debug.Log(targetPosition);


        if (targetPosition.sqrMagnitude > 0.1f)
        {
            // move towards it
            panMoveSpeed = Mathf.Lerp(panMoveSpeed, panTopSpeed, Time.deltaTime * panAcceleration);
            transform.position += targetPosition * panMoveSpeed * Time.deltaTime;
        }
        else
        {
            // ramp back down
            horizontalVelocity = Vector3.Lerp(horizontalVelocity, Vector3.zero, Time.deltaTime * damping);
            transform.position += horizontalVelocity * Time.deltaTime;
            panMoveSpeed = 0f;
        }
        targetPosition = Vector3.zero;
    }

    private void UpdateZoom()
    {
        Vector3 zoomTarget = new Vector3(cameraTransform.localPosition.x, zoomHeight, cameraTransform.localPosition.z);
        zoomTarget -= zoomSpeed * (zoomHeight - cameraTransform.localPosition.y) * Vector3.forward;

        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, zoomTarget, Time.deltaTime * zoomDamping);
        cameraTransform.LookAt(this.transform);
    }

    public void OnZoomCamera(InputAction.CallbackContext context)
    {
        float value = -context.ReadValue<Vector2>().y / 100f;

        if (Mathf.Abs(value) > 0.1f)
        {
            zoomHeight = cameraTransform.localPosition.y + value * stepSize;
            if (zoomHeight < minHeight)
            {
                zoomHeight = minHeight;
            }
            else if (zoomHeight > maxHeight)
            {
                zoomHeight = maxHeight;
            }
        }
    }
}
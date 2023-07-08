using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEditor.Events;


public class CameraController : MonoBehaviour, CameraControls.IMyCamActions
{
    private float m_TargetPitch = 45f;
    private int m_CurrentZoomLevel;
    private CameraControls camControls;
    private InputAction movement;
    public Transform cameraTransform;
    private Vector3 m_NewZoom;
    public Vector2 MovementValue { get; private set; }
    public Vector2 RotateValue { get; private set; }
    public float ZoomValue { get; private set; }
    public bool UnlockRotation { get; private set; }

    public GameObject target { get; private set; }

    private CameraControls controls;

    // Variables for camera controls
    // panning motion
    [SerializeField] private float _panMoveSpeed;
    [SerializeField] private static float _panTopSpeed = 5f;
    [SerializeField] private float _panAcceleration = 10f;
    [SerializeField] private float damping = 10f;

    // zoom motion
    [SerializeField] private float _zoomSpeed;
    [SerializeField] private float _stepSize;
    [SerializeField] private float _zoomDamping;
    [SerializeField] private float _minHeight = 5f;
    [SerializeField] private float _maxHeight = 50f;

    // rotation
    [SerializeField] private float _rotationTopSpeed = 3f;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _rotationAcceleration = 5f;

    // screen edge motion
    [SerializeField] private bool _useScreenEdge = true;

    private enum ZoomLevelDirection
    {
        Up,
        Down
    }

    [SerializeField]
    [Tooltip("Tells the camera which direction the Zoom Level percentages go. Up means that the first index is fully zoomed in (0), and ascends from there." +
         " Down means that the first index is fully zoomed out (1), and descends from there.")]
    private ZoomLevelDirection m_ZoomLevelDirection = ZoomLevelDirection.Down;
    [SerializeField]
    [Tooltip("If true, when zooming in and out, the camera will zoom to the next or previous level defined in Zoom Levels")]
    private bool m_UseZoomLevels = false;

    [SerializeField]
    [Tooltip("List of zoom levels to use (as percentages between 0 and 1) with 1 being all the way zoomed out and 0 being all the way zoomed in." +
             " These are only used if Use Zoom Levels is true")]
    private List<float> m_ZoomLevels = null;

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
        transform.rotation = Quaternion.Euler(0f, rotationValue * _rotationSpeed + transform.rotation.eulerAngles.y, 0f);
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
        m_NewZoom = cameraTransform.localPosition;
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
            zoomHeight = cameraTransform.localPosition.y + value * _stepSize;
            if (zoomHeight < _minHeight)
            {
                zoomHeight = _minHeight;
            }
            else if (zoomHeight > _maxHeight)
            {
                zoomHeight = _maxHeight;
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
            _panMoveSpeed = Mathf.Lerp(_panMoveSpeed, _panTopSpeed, Time.deltaTime * _panAcceleration);
            transform.position += targetPosition * _panMoveSpeed * Time.deltaTime;
        }
        else
        {
            // ramp back down
            horizontalVelocity = Vector3.Lerp(horizontalVelocity, Vector3.zero, Time.deltaTime * damping);
            transform.position += horizontalVelocity * Time.deltaTime;
        }
        targetPosition = Vector3.zero;
    }

    private void UpdateZoom()
    {
        Vector3 zoomTarget = new Vector3(cameraTransform.localPosition.x, zoomHeight, cameraTransform.localPosition.z);
        zoomTarget -= _zoomSpeed * (zoomHeight - cameraTransform.localPosition.y) * Vector3.forward;

        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, zoomTarget, Time.deltaTime * _zoomDamping);
        cameraTransform.LookAt(this.transform);
    }

    public void OnZoomCamera(InputAction.CallbackContext context)
    {
        float value = -context.ReadValue<Vector2>().y / 100f;

        if (Mathf.Abs(value) > 0.1f)
        {
            zoomHeight = cameraTransform.localPosition.y + value * _stepSize;
            if (zoomHeight < _minHeight)
            {
                zoomHeight = _minHeight;
            }
            else if (zoomHeight > _maxHeight)
            {
                zoomHeight = _maxHeight;
            }
        }
    }
}

/*
    private void Update()
    {
        GetMoveInput();

        UpdateVelocity();
        UpdateBasePosition();
    }


    private void GetMoveInput()
    {

    }



    private void UpdateBasePosition()
    {
        if (targetPosition.sqrMagnitude > .1f)
        {
            _panMoveSpeed = Mathf.Lerp(_panMoveSpeed, _panTopSpeed, Time.deltaTime * _panAcceleration);
            Debug.Log("move speed = " + _panMoveSpeed);
            transform.position += targetPosition * _panMoveSpeed * Time.deltaTime;
        }
        else
        {
            Debug.Log("We're not moving anymore");
            horizontalVelocity = Vector3.Lerp(horizontalVelocity, Vector3.zero, Time.deltaTime * damping);
            transform.position += horizontalVelocity * Time.deltaTime;
        }

        targetPosition = Vector3.zero;
    }

*/
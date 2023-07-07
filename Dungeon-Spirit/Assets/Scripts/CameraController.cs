using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEditor.Events;


public class CameraController : MonoBehaviour
{
    private CameraControls camControls;
    private InputAction movement;
    public Transform cameraTransform;

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

    // Where we're trying to move TO
    // We will be lerping towards that position
    // this way multiple functions can affect this
    private Vector3 targetPosition;
    private float zoomHeight;
    private Vector3 horizontalVelocity;
    private Vector3 lastPos;

    [SerializeField] private InputReader inputReader;

    // Stores the position on screen where we started to drag the mouse
    // can this be vector2 and reference screenpos?
    private Vector3 startDrag;

    private void Awake()
    {
        Debug.Log("Hello we're awake");
        camControls = new CameraControls();
    }

    private void OnEnable()
    {
        lastPos = this.transform.position;
        movement = camControls.MyCam.Movement;
        camControls.MyCam.Enable();
    }

    private void RotateCamera()
    {
        if (!inputReader.UnlockRotation) return;
        float rotationValue = inputReader.RotateValue.x;
        transform.rotation = Quaternion.Euler(0f, rotationValue * _rotationSpeed + transform.rotation.eulerAngles.y, 0f);
    }

    private void OnDisable()
    {

    }

    private void Update()
    {
        Debug.Log(targetPosition);
        RotateCamera();
        UpdateVelocity();

    }

    private void FixedUpdate()
    {
        CalculateMovement();
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

        targetPosition += inputReader.MovementValue.x * transform.right;
        targetPosition += inputReader.MovementValue.y * transform.forward;
        targetPosition = targetPosition.normalized;
        //Debug.Log(targetPosition);


        if (targetPosition.sqrMagnitude > 0.1f)
        {  
            Debug.Log("are we accelerating?");
            // move towards it
            _panMoveSpeed = Mathf.Lerp(_panMoveSpeed, _panTopSpeed, Time.deltaTime * _panAcceleration);
            transform.position += targetPosition * _panMoveSpeed * Time.deltaTime;
        }
        else
        {
            Debug.Log("No lOnger accelerating");
            // ramp back down
            horizontalVelocity = Vector3.Lerp(horizontalVelocity, Vector3.zero, Time.deltaTime * damping);
            transform.position += horizontalVelocity * Time.deltaTime;
        }
        targetPosition = Vector3.zero;
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 6f;
    [SerializeField] private float _dragValue = 6f;
    [SerializeField] private float _sensitivityX;
    [SerializeField] private float _sensitivityY;

    private Vector3 _moveDirection;
    private Rigidbody _rigidbody;
    private float _horizontalMovement;
    private float _verticalMovement;
    private float movementMultiplier = 10f;
    private Camera _camera;
    private float _mouseInputX;
    private float _mouseInputY;
    private float _rotationX;
    private float _rotationY;
    private float _rotationMultiplier = 0.01f;


    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.freezeRotation = true;

        _camera = GetComponentInChildren<Camera>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        CalculateMoveDirection();
        CalculateRotation();

        _camera.transform.localRotation = Quaternion.Euler(_rotationX, 0, 0);
        //_camera.transform.localRotation = Quaternion.Euler(10f, 0, 0);
        transform.rotation = Quaternion.Euler(0, _rotationY, 0);
    }

    private void FixedUpdate()
    {
        MovePlayer();
        ControlDrag();
    }

    private void CalculateRotation()
    {
        _mouseInputX = Input.GetAxisRaw("Mouse X");
        _mouseInputY = Input.GetAxisRaw("Mouse Y");

        _rotationY += _mouseInputX * _sensitivityX * _rotationMultiplier;
        _rotationX -= _mouseInputY * _sensitivityY * _rotationMultiplier;

        _rotationX = Mathf.Clamp(_rotationX, -90f, 90f);
    }




    private void MovePlayer()
    {
        _rigidbody.AddForce(_moveDirection.normalized * _speed * movementMultiplier, ForceMode.Acceleration);
    }

    private void ControlDrag()
    {
        _rigidbody.drag = _dragValue;
    }

    private void CalculateMoveDirection()
    {
        _horizontalMovement = Input.GetAxisRaw("Horizontal");
        _verticalMovement = Input.GetAxisRaw("Vertical");

        _moveDirection = transform.forward * _verticalMovement + transform.right * _horizontalMovement;
    }
}

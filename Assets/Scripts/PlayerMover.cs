using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed = 6f;
    [SerializeField] private float _dragValue = 6f;
    [SerializeField] private Vector2 _sensitivity;

    private Vector3 _moveDirection;
    private Rigidbody _rigidbody;
    private float _horizontalMovement;
    private float _verticalMovement;
    private float _movementMultiplier = 10f;
    private Camera _camera;
    private Vector2 _mouseInput;
    private Vector2 _rotation;
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

        _camera.transform.localRotation = Quaternion.Euler(_rotation.x, 0, 0);
        //_camera.transform.localRotation = Quaternion.Euler(10f, 0, 0);
        transform.rotation = Quaternion.Euler(0, _rotation.y, 0);
    }

    private void FixedUpdate()
    {
        MovePlayer();
        ControlDrag();
    }

    private void CalculateRotation()
    {
        _mouseInput.x = Input.GetAxisRaw("Mouse X");
        _mouseInput.y = Input.GetAxisRaw("Mouse Y");

        _rotation.y += _mouseInput.x * _sensitivity.x * _rotationMultiplier;
        _rotation.x -= _mouseInput.y * _sensitivity.y * _rotationMultiplier;

        _rotation.x = Mathf.Clamp(_rotation.x, -90f, 90f);
    }




    private void MovePlayer()
    {
        _rigidbody.AddForce(_moveDirection.normalized * _speed * _movementMultiplier, ForceMode.Acceleration);
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

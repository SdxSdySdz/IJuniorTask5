using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed = 6f;
    [SerializeField] private float _dragValue = 6f;

    private Vector3 _moveDirection;
    private Rigidbody _rigidbody;
    private float _horizontalMovement;
    private float _verticalMovement;
    private float _movementMultiplier = 10f;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.freezeRotation = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        CalculateMoveDirection();
    }

    private void FixedUpdate()
    {
        MovePlayer();
        ControlDrag();
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

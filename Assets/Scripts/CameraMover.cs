using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Vector2 _sensitivity;
    private Camera _camera;
    private Vector2 _mouseInput;
    private Vector2 _rotation;
    private float _rotationMultiplier = 0.01f;

    private float _maxRotation = 90f;
    private float _minRotation = -90f;

    private void Start()
    {
        _camera = GetComponentInChildren<Camera>();
    }

    private void Update()
    {
        CalculateRotation();

        _camera.transform.localRotation = Quaternion.Euler(_rotation.x, 0, 0);
        transform.rotation = Quaternion.Euler(0, _rotation.y, 0);
    }

    private void CalculateRotation()
    {
        _mouseInput.x = Input.GetAxisRaw("Mouse X");
        _mouseInput.y = Input.GetAxisRaw("Mouse Y");

        _rotation.y += _mouseInput.x * _sensitivity.x * _rotationMultiplier;
        _rotation.x -= _mouseInput.y * _sensitivity.y * _rotationMultiplier;

        _rotation.x = Mathf.Clamp(_rotation.x, _minRotation, _maxRotation);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public float RunSpeed = 2f;
    public Camera mainCamera;

    private Vector3 _movementVector;
    private Vector3 _mousePosition;
    private Animator _animator;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _movementVector = Vector3.zero;
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        GetMovementInput();
    }

    private void FixedUpdate()
    {
        Move();
        Rotate();
    }
    
    private void GetMovementInput()
    {
        _movementVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    }

    private void Move()
    {
        var forward = mainCamera.transform.TransformDirection(_movementVector.normalized);
        forward = new Vector3(forward.x, 0, forward.z);
        _mousePosition = transform.position + forward;
        _rigidbody.MovePosition(transform.position + forward * (RunSpeed) / 10f);
        _animator.SetBool("Run", forward != Vector3.zero);
    }

    private void Rotate()
    {
        transform.LookAt(_mousePosition);
    }
}

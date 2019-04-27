using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public float RunSpeed = 2f;
    public float SprintSpeed = 2f;
    public Camera mainCamera;
    public float JumpForce;
    [Header("Ground Check")]
    public LayerMask GroundLayer;
    public Transform GroundChecker;
    public float GroundCheckerRadius;

    private Vector3 _movementVector;
    private Vector3 _mousePosition;
    private Animator _animator;
    private Rigidbody _rigidbody;
    private bool _jump;
    private bool _sprinting;

    private void Start()
    {        
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

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
        _movementVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        _sprinting = Input.GetButton("Sprint");
        var canJump = CanJump();
        _jump = !_jump && Input.GetButtonDown("Jump") && canJump;
        _animator.SetBool("OnGround", canJump);
    }

    private bool CanJump()
    {
        return Physics.SphereCastAll(GroundChecker.position, GroundCheckerRadius, Vector3.down, GroundCheckerRadius, GroundLayer).Any();
    }

    private void Move()
    {
        var forward = mainCamera.transform.TransformDirection(_movementVector.normalized);
        forward = new Vector3(forward.x, 0, forward.z);
        _mousePosition = transform.position + forward;
        _rigidbody.MovePosition(transform.position + forward * (_sprinting ? SprintSpeed : RunSpeed) / 10f);
        if (_jump)
        {
            _rigidbody.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        }

        _animator.SetBool("Run", forward != Vector3.zero);
        _animator.SetFloat("VerticalSpeed", _rigidbody.velocity.y);
    }

    private void Rotate()
    {
        transform.LookAt(_mousePosition);
    }

}

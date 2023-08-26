using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement variables")]
    [SerializeField]
    private float velocity = 2.0f;

    [Header("Player Components")]
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Collider col;

    #region Movement variables
    private Vector2 _movementInput;
    private Vector3 _movementDirection;
    private Vector3 _playerMovement;

    private Quaternion _targetRotation = new Quaternion(0, 1, 0, 0);
    
    private static readonly int IsWalking = Animator.StringToHash("IsWalking");

    #endregion

    private void OnValidate()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }

        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        if (col == null)
        {
            col = GetComponent<Collider>();
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        _movementInput = context.ReadValue<Vector2>();

        _movementDirection.x = _movementInput.x;
        _movementDirection.z = _movementInput.y;
        
        if (_movementInput.x != 0 || _movementInput.y != 0)
        {
           // animator.SetBool(IsWalking, true);
            _targetRotation = Quaternion.LookRotation(_movementDirection, Vector3.up);
        }
        else
        {
            //animator.SetBool(IsWalking, false);
        }
    }
    
    public void Update()
    {

    }

    public void FixedUpdate()
    {
        _playerMovement = _movementDirection * (Time.fixedDeltaTime * velocity * 100);
        rb.AddForce(_playerMovement, ForceMode.Force);
        rb.rotation = Quaternion.Slerp(rb.rotation, _targetRotation.normalized, 0.2f);       
    }
}

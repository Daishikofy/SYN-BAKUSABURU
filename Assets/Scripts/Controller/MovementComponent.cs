using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    public float WalkSpeed = 3.0f;

    private float _currentSpeed;
    private Rigidbody _rigidbody;

    private Vector3 _movementDirection;


    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        WalkSpeed /= 10.0f;
        _currentSpeed = WalkSpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var rbPosition = _rigidbody.position + _movementDirection * (_currentSpeed / _rigidbody.mass);
        _rigidbody.MovePosition(rbPosition);
    }

    public void SetMovementDirection(Vector3 movementDirection)
    {
        _movementDirection = movementDirection.normalized;
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class AIController : MonoBehaviour
{
    [Serializable]
    public enum AIStates
    {
        Idle,
        Chasing,
        Patrolling
    }
    
    [SerializeField]
    private MovementComponent MovementComponent;

    [SerializeField] private AIStates CurrentState = AIStates.Idle;

    [SerializeField] private float WalkSpeed = 2.0f;

    private Transform _playerTransform;
    
    private Vector3 _targetPoint;
    public void Setup(float walkSpeed)
    {
        if (MovementComponent == null)
        {
            MovementComponent = gameObject.AddComponent<MovementComponent>();
            MovementComponent.WalkSpeed = walkSpeed;
        }

        _playerTransform = FindFirstObjectByType<PlayerController>().transform;
    }

    private void Start()
    {
        Setup(WalkSpeed);
    }
    
    void Update()
    {
        switch (CurrentState)
        {
            case AIStates.Idle:
                break;
            case AIStates.Chasing:
                Vector3 direction = _playerTransform.position - transform.position;
                direction.y = 0;
                MovementComponent.SetMovementDirection(direction);
                break;
            case AIStates.Patrolling:
                if (Vector3.Distance(transform.position, _targetPoint) < 0.1f)
                {
                    Vector2 target = Random.insideUnitCircle * 5.0f;
                    _targetPoint.x = target.x;
                    _targetPoint.z = target.y;
                    
                    MovementComponent.SetMovementDirection(_targetPoint - transform.position);
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}

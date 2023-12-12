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
   
    //_ _ _ _ AI COMPONENTS _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ 
    [SerializeField] private MovementComponent MovementComponent;

    [SerializeField] private AIStates CurrentState = AIStates.Idle;
    [SerializeField] private float WalkSpeed = 2.0f;

    //_ _ TODO PATROL COMPONENT _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ 
    [SerializeField] private float PatrolCooldown = 1.0f;
    private Vector3 _targetPoint;
    private float _patrolCooldownTimer = 0.0f;
    
    private Transform _playerTransform;
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
                MovementComponent.SetMovementDirection(Vector3.zero);
                break;
            case AIStates.Chasing:
                UpdateChase();
                break;
            case AIStates.Patrolling:
               UpdatePatrol();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void UpdatePatrol()
    {
        //TODO : Move this to patrol controller
        if (_patrolCooldownTimer > 0.0) //in cooldown
        {
            _patrolCooldownTimer -= Time.deltaTime;
            
            if (_patrolCooldownTimer <= 0.0f) //cooldown finished
            {
                Vector2 target = Random.insideUnitCircle * 5.0f;
                _targetPoint.x = target.x;
                _targetPoint.z = target.y;
                    
                MovementComponent.SetMovementDirection(_targetPoint - transform.position);
            }
        }
        else if (Vector3.Dot(_targetPoint- transform.position, MovementComponent.GetMovementDirection()) <= 0f) //moving and arrived at target, starts cooldown
        {
            _patrolCooldownTimer = PatrolCooldown;
            MovementComponent.SetMovementDirection(Vector3.zero);
        }
    }

    private void UpdateChase()
    {
        ////TODO : Move this to chase(?) controller
        Vector3 direction = _playerTransform.position - transform.position;
        direction.y = 0;
        MovementComponent.SetMovementDirection(direction);
    }
}
// A.x * B.x + A.y * B.y

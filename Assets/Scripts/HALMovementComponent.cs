using UnityEngine;
using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine.InputSystem;

[Serializable]
public enum HALCharacterMovementTypes
{
    Continuous,
    Horizontal,
    Vertical,
    Automatic,
    None
}

public class HALMovementComponent : MonoBehaviour
{
    [Header("Physics setup")]
    [SerializeField]
    private float walkingSpeed = 1;
    [SerializeField]
    private float runningSpeed = 3;
    [SerializeField]
    private float maxStamina = 5;
    [SerializeField]
    private HALCharacterMovementTypes currentMovementType = HALCharacterMovementTypes.Continuous;
    
    //TODO : What is this variable for?
    public bool automaticMovement;
    
    //Physics and movement
    private float currentStamina;
    private float currentSpeed;
    private Rigidbody rb;
    private Vector3 characterMovement;
    private Vector3 characterDirection;
    public Action currentMovement;

    private Vector3 targetPoint;
    private float lastDistance;

    private bool isStaminaInCoolDown = false;
    private Vector2 _movementInput;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        SetMovement(currentMovementType);
        SetDirection(new Vector2(0, -1));
        currentStamina = maxStamina;
    }

    private void Update()
    {
        currentMovement();
    }

    private void FixedUpdate()
    {
        var rbPosition = rb.position + characterMovement * (currentSpeed / rb.mass);
        rb.MovePosition(rbPosition);
        Debug.Log("Move : " + rb.position);
    }
    
    public void Move(InputAction.CallbackContext context)
    {
        _movementInput = context.ReadValue<Vector2>();
    }
    
    public void SetMovement(HALCharacterMovementTypes movementTypes)
    {
        currentMovementType = movementTypes;
        switch (movementTypes)
        {
            case HALCharacterMovementTypes.Continuous:
                currentMovement = () => ContinuousMovement();
                break;
            case HALCharacterMovementTypes.Automatic:
                currentMovement = () => AutomaticMovement();
                break;
            case HALCharacterMovementTypes.Horizontal:
                currentMovement = () => HorizontalMovement();
                break;
            case HALCharacterMovementTypes.Vertical:
                currentMovement = () => VerticalMovement();
                break;
            case HALCharacterMovementTypes.None:
                currentMovement = () => { characterMovement = Vector2.zero; };
                break;
            default:
                break;
        }
    }
    
    private void SetDirection(Vector3 direction)
    {
        if (direction == characterDirection || direction.x + direction.y == 0)
        {
            return;
        }
        
        characterDirection = direction;
    }
    
    public void GoTo(Vector2 destination)
    {
        targetPoint = destination; 

        float deltaX = math.abs(rb.position.x - destination.x);
        float deltaZ = math.abs(rb.position.z - destination.y);

        Vector2 startPoint = rb.position;
        if (deltaX > deltaZ)
        {
            startPoint.y = destination.y;
        }
        else
        {
            startPoint.x = destination.x;
        }

        transform.position = startPoint;

        characterMovement = (destination - startPoint).normalized;
        lastDistance = Vector2.Distance(transform.position, targetPoint);

        SetDirection(characterMovement);
        
        automaticMovement = true;
        SetMovement(HALCharacterMovementTypes.Automatic);
    }
    
    private void ContinuousMovement()
    {
        characterMovement.x = _movementInput.x;
        characterMovement.z = _movementInput.y;

        if (characterMovement.x != 0 && characterMovement.z != 0)
        {
            characterMovement.z = 0;
        }
        currentSpeed = walkingSpeed;
        /*    
        if (Input.GetKey(KeyCode.LeftShift) && !isStaminaInCoolDown)
        {
            currentSpeed = runningSpeed;
            currentStamina -= Time.deltaTime;

            if (currentStamina <= 0)
            {
                isStaminaInCoolDown = true;
            }
        }
        else
        {
            currentSpeed = walkingSpeed;
            
            if (isStaminaInCoolDown)
            {
                currentStamina += Time.deltaTime;
                
                if (currentStamina >= maxStamina)
                    isStaminaInCoolDown = false;
            }
        }*/
        
        SetDirection(characterMovement);
    }

    private void HorizontalMovement()
    {
        characterMovement.x = _movementInput.x;
        characterMovement.z = 0;
        SetDirection(characterMovement);
    }
    private void VerticalMovement()
    {
        characterMovement.x = 0;
        characterMovement.z = _movementInput.y;
        SetDirection(characterMovement);
    }

    private void AutomaticMovement()
    {
        var distance = Vector2.Distance(transform.position, targetPoint);
        if (lastDistance < distance)
        {
            characterMovement = Vector2.zero;
            automaticMovement = false;
            SetMovement(HALCharacterMovementTypes.Continuous);
        }
        lastDistance = distance;
    }
}

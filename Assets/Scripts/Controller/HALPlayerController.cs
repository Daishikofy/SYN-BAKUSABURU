/*using UnityEngine;

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
}*/
using UnityEngine;
using System;
using Unity.Mathematics;

[RequireComponent(typeof(HALMovementComponent))]
public class HALPlayerController : MonoBehaviour
{

    public HALMovementComponent movementComponent;
        
    [Header("Object setup")]
    public GameObject[] candyPrefabs;
   
    [SerializeField]
    private RelativeJoint2D joint = null;
    public Transform raycastStart;

    [Header("Runtime variables")]
    //public RoomObject currentRoom;
    public bool isHidding;
    

    private bool isDragingObject;
    private bool hasStamina = true;

   // public InventoryController inventory;
/*
    // Use this for initialization
    void Start () {
        
        //Desable isDraging
        joint.connectedBody = null;
        joint.enabled = false;

        if (inventory == null)
        {
            inventory = new InventoryController();
        }
    }
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            ReleaseInteraction();
        }
    }
    
    private void Interact()
    {
        if (isHidding)
        {
            Unhides(null);            
            return;
        }

        RaycastHit2D hit = Physics2D.Raycast(raycastStart.transform.position, playerDirection, 0.5f);
        //Debug.Log("fraction: " + hit.fraction);
        Debug.DrawRay(raycastStart.transform.position, playerDirection, Color.red, 0.5f);

        if (hit.collider == null)
        {
            Debug.Log("No interactions");
            DropCandy();
            return;
        }
        var other = hit.collider.gameObject;
        IInteractable interactable = other.GetComponent<IInteractable>();
        if (interactable != null)
        {
            interactable.OnInteraction(this);
        }
        else if (other.GetComponent<IDragable>() != null)
        {
            GrabObject(other);
        }
    }

    public void CollectItem(CollectibleObject item)
    {
        //TODO: Animations
        inventory.GetObject(item.type, 1);
        currentRoom.RemoveItem(item);
    }

    private void DropCandy()
    {
        if (inventory.UseObject(CollectibleType.Candy))
        {
            //TODO: Animations drop candy
            var index = UnityEngine.Random.Range(0, candyPrefabs.Length);
            var obj = Instantiate(candyPrefabs[index], transform.position, quaternion.identity);
            currentRoom.objectsInRoom.Enqueue(obj);
        }
    }

    private void ReleaseInteraction()
    {
        if (isDragingObject)
            ReleaseObject();
    }

    private void GrabObject(GameObject other)
    {
        var otherRb = other.GetComponent<Rigidbody2D>();

        otherRb.bodyType = RigidbodyType2D.Dynamic;
        isDragingObject = true;
        rb.mass += otherRb.mass;
        joint.connectedBody = otherRb;
        joint.enabled = true;

        if (playerDirection.x != 0)
            SetMovement(HALCharacterMovementTypes.Horizontal);
        else
            SetMovement(HALCharacterMovementTypes.Vertical);
    }

    private void ReleaseObject()
    {
        var otherRb = joint.connectedBody;

        otherRb.bodyType = RigidbodyType2D.Kinematic;
        isDragingObject = false;
        rb.mass -= otherRb.mass;
        joint.connectedBody = null;
        joint.enabled = false;
        otherRb.velocity = Vector2.zero;

        SetMovement(HALCharacterMovementTypes.Continuous);
    }

    public void Hides()
    {
        isHidding = true;
        currentRoom.isPlayerInRoom = false;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        SetMovement(HALCharacterMovementTypes.None);
    }
    public void Unhides(DestroyableObject destroyable)
    {
        isHidding = false;
        currentRoom.isPlayerInRoom = true;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        GetComponent<Collider2D>().enabled = true;
        GetComponent<SpriteRenderer>().enabled = true;
        playerDirection *= -1;
        if (destroyable)
            transform.position = destroyable.transform.position;
        SetMovement(HALCharacterMovementTypes.Continuous);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            GameController.Instance.OnPlayerLose();
        }
    }
*/
}

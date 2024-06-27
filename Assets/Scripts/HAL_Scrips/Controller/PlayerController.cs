using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Components;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // _ _ COMPONENTS _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _
    public MovementComponent PlayerMovementComponent;
    
    // _ _ TODO Hiding component _ _ _ _ _ _ _ _ _ _  _ _ _ _ _ 
    [SerializeField] private SpriteRenderer Renderer;
    [SerializeField] private CanvasRenderer FadeImage;
    private bool _isHiding = false;
    
    // _ _ TODO Holding Component _ _ _ _ _ _ _ _ _ _  _ _ _ _ _ 
    [SerializeField] private Joint HoldingJoint;

    private void Start()
    {
        FadeImage.SetAlpha(0.0f);
    }

    public void Move(InputAction.CallbackContext context) //Input system
    {
        Vector2 input = context.ReadValue<Vector2>();
        Vector3 movementDirection = new Vector3(input.x, .0f, input.y);
        PlayerMovementComponent.SetMovementDirection(movementDirection);
    }
    
    public void Interact(InputAction.CallbackContext context) //Input system
    {
        if (context.phase == InputActionPhase.Started)
        {
            if (TryHide())
            {
                return;
            }

            if (TryHold())
            {
                return;
            }
        }
    }

    private bool TryHide()
    {
        HideoutController closestHidingPoint = null;
        float minDistance = 5f;
        HideoutController[] hideoutControllers = FindObjectsByType<HideoutController>(sortMode:FindObjectsSortMode.None);
        foreach (var hideoutController in hideoutControllers)
        {
            float distance = Vector3.Distance(hideoutController.transform.position, transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestHidingPoint = hideoutController;
            }
        }

        if (closestHidingPoint != null)
        {
            closestHidingPoint.Interact(this);
            return true;
        }

        return false;
    }

    private bool TryHold()
    {
        Vector3 origin = PlayerMovementComponent.Rigidbody.position;
        Vector3 movementDirection = PlayerMovementComponent.MovementDirection;
        RaycastHit hitInfo;
        var result = Physics.Raycast(origin, movementDirection, out hitInfo, 4f);

        Debug.Log(hitInfo.rigidbody.gameObject.name);
        
        return false;
    }

    //TODO : HidingComponent
    public void Hide(Vector3 newPosition, Quaternion newRotation)
    {
        //TODO : disable player controller, switch to small camera controller? 
        _isHiding = !_isHiding;

        RepositionPlayer(newPosition, newRotation);
    }

    private async void RepositionPlayer(Vector3 newPosition, Quaternion newRotation)
    {
        float alpha = 0.0f; 
        while (alpha < 1.0f)
        {
            FadeImage.SetAlpha(alpha);
            alpha += 0.05f;
            await Task.Delay(1);
        }

        if (_isHiding)
        {
            Renderer.enabled = false;
            CameraController.Instance.SetPovCamera();
        }
        else
        {
            Renderer.enabled = true;
            CameraController.Instance.SetTopDownCamera();
        }
        
        transform.position = newPosition;
        transform.rotation = newRotation;

        await Task.Delay(10);
        while (alpha >= 0.0f)
        {
            FadeImage.SetAlpha(alpha);
            alpha -= 0.05f;
            await Task.Delay(1);
        }
    }
}

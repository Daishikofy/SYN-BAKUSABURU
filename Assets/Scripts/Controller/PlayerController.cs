using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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

    private void Start()
    {
        FadeImage.SetAlpha(0.0f);
    }

    public void Move(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        Vector3 movementDirection = new Vector3(input.x, .0f, input.y);
        PlayerMovementComponent.SetMovementDirection(movementDirection);
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            Hide();
        }
    }

    //TODO : HidingComponent
    private void Hide()
    {
        //TODO : disable player controller, switch to small camera controller? 
        //TODO : Switch camera
        _isHiding = !_isHiding;
        RepositionCamera();
    }

    private async void RepositionCamera()
    {
        float alpha = 0.0f; 
        while (alpha < 1.0f)
        {
            FadeImage.SetAlpha(alpha);
            alpha += 0.05f;
            await Task.Delay(1);
        }
        
        SwitchCamera();
        
        await Task.Delay(10);
        while (alpha >= 0.0f)
        {
            FadeImage.SetAlpha(alpha);
            alpha -= 0.05f;
            await Task.Delay(1);
        }
    }

    private void SwitchCamera()
    {
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
    }
}

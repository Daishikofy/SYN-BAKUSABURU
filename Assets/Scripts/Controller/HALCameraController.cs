using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Helpers;
using Unity.Mathematics;
using UnityEngine;

[Serializable]
public enum CameraMovement
{
    Fixed,
    Horizontal,
    Forward
}

public class CameraController : MonoBehaviour
{
    public Transform playerPosition;
    public float transitionSpeed;
    public CameraMovement cameraMovimentation = CameraMovement.Fixed;
    [Space]
    [Header("Runtime variables")]
    public float minPosition;
    public float maxPosition;

    private Vector3 cameraMovement;
    //private Vector2 targetPoint;
    private float distance;
    private Action currentMovement;

    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        if (playerPosition == null)
        {
            playerPosition = FindAnyObjectByType<HALPlayerController>().transform;
        }
        
        offset = transform.position;
        cameraMovement = transform.position - playerPosition.transform.position;
        SetMovement(cameraMovimentation);
    }

    public void Setup(CameraMovement movement, Vector2 min, Vector2 max)
    {
        Debug.Log("Setup: " + movement.ToString());
        cameraMovimentation = movement;
        SetupMinMax(min, max);
        var aux = HALMathHelpers.NearestPointOnSegment(transform.position, min, max);
        transform.position = new Vector3(aux.x, aux.y, -10);
    }

    // Update is called once per frame
    void Update()
    {
        currentMovement();
    }

    private void FixedMovement()
    {
        return;
    }
    private void HorizontalMovement()
    {
        if (playerPosition.position.x < minPosition || playerPosition.position.x > maxPosition)
            return;
        /*
        cameraMovement.x = playerPosition.position.x + offset.x;
        cameraMovement.y = offset.y;
        cameraMovement.z = offset.z;*/

        cameraMovement = offset;
        cameraMovement.x += playerPosition.position.x;
        
        transform.position = cameraMovement;
    }
    private void ForwardMovement()
    {
        if (playerPosition.position.z < minPosition || playerPosition.position.z > maxPosition)
            return;
        /*
        cameraMovement.z = playerPosition.position.z + offset.z;
        cameraMovement.y = -offset.y;
        */
        cameraMovement = offset;
        cameraMovement.z += playerPosition.position.z;
        
        transform.position = cameraMovement;
    }
    private void SetMovement(CameraMovement movement)
    {
        switch (movement)
        {
            case CameraMovement.Fixed:
                cameraMovement = transform.position;
                currentMovement = () => FixedMovement();
                break;
            case CameraMovement.Horizontal:
                currentMovement = () => HorizontalMovement();
                break;
            case CameraMovement.Forward:
                currentMovement = () => ForwardMovement();
                break;
            default:
                break;
        }
    }
/*
    public async void GoTo(CameraMovement movimentation, Vector2 min, Vector2 max)
    {
        var targetPoint = Utils.NearestPointOnSegment(transform.position, min, max);        

        cameraMovimentation = movimentation;
        Vector2 startPoint = transform.position;

        cameraMovement = (targetPoint - startPoint).normalized;
        distance = Vector2.Distance(transform.position, targetPoint);

        float time = distance / transitionSpeed;
        transform.LeanMove(targetPoint, time);

        SetupMinMax(min, max);
        SetMovement(cameraMovimentation);

        await Task.Delay((int)time*1000);
    */

    private void SetupMinMax(Vector2 min, Vector2 max)
    {
        if (cameraMovimentation == CameraMovement.Horizontal)
        {
            cameraMovement.y = min.y;
            minPosition = min.x;
            maxPosition = max.x;
        }
        else if (cameraMovimentation == CameraMovement.Forward)
        {
            cameraMovement.x = min.x;
            minPosition = min.y;
            maxPosition = max.y;
        }
    }
}
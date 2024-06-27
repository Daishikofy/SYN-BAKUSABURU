using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private enum CameraState
    {
        TopDown,
        POV
    }

    public static CameraController Instance => _instance;
    private static CameraController _instance;

    [SerializeField] private Transform PlayerTransform;
    [SerializeField] private Transform TopDownContainer;
    [SerializeField] private Transform PovContrainer;

    private Vector3 _offset;
    
    private CameraState _currentState = CameraState.TopDown;

    private void Start()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        _offset = transform.position - PlayerTransform.position;

        switch (_currentState)
        {
              case CameraState.TopDown:
                  transform.SetParent(TopDownContainer);
                  break;
              case CameraState.POV:
                  transform.SetParent(PovContrainer);
                  break;
        }
    }

    private void LateUpdate()
    {
        switch (_currentState)
        {
            case CameraState.TopDown:
                UpdateTopDown();
                break;
            case CameraState.POV:
                break;
        }
    }

    private void UpdateTopDown()
    {
        TopDownContainer.position = PlayerTransform.position + _offset;
    }

    public void SetTopDownCamera()
    {
        _currentState = CameraState.TopDown;

        transform.SetParent(TopDownContainer, false);
    }

    public void SetPovCamera()
    {
        _currentState = CameraState.POV;
        
        transform.SetParent(PovContrainer, false);
    }
}
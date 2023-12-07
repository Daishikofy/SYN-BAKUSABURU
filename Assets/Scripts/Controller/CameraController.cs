using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform PlayerTransform;
    private Vector3 _offset;


    private void Start()
    {
        _offset = transform.position - PlayerTransform.position;
    }

    private void LateUpdate()
    {
        transform.position = PlayerTransform.position + _offset;
    }
}

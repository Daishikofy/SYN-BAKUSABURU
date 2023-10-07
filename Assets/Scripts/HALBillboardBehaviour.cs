using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HALBillboardBehaviour : MonoBehaviour
{
    private Camera camera;

    private void Start()
    {
        camera = Camera.main;
    }

    void LateUpdate()
    {
        transform.rotation = camera.transform.rotation;
        //transform.rotation = Quaternion.Euler(0.0f, camera.transform.rotation.eulerAngles.y, 0.5f);
    }
}

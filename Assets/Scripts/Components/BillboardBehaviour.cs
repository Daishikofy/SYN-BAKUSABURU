using UnityEngine;

public class BillboardBehaviour : MonoBehaviour
{
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    void LateUpdate()
    {
        transform.rotation = _camera.transform.rotation;
        //transform.rotation = Quaternion.Euler(0.0f, camera.transform.rotation.eulerAngles.y, 0.5f);
    }
}

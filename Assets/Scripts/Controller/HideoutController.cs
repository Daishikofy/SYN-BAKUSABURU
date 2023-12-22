using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideoutController : MonoBehaviour
{
    public Transform HidingTransform;

    private bool _isHiding = false;
    private Vector3 _playerPreviousPosition;
    private Quaternion _playerPreviousRotation;

    public void Interact(PlayerController player)
    {
        if (_isHiding)
        {
            player.Hide(_playerPreviousPosition, _playerPreviousRotation);
            _isHiding = false;
        }
        else
        {
            _playerPreviousPosition = player.transform.position;
            player.Hide(HidingTransform.position, HidingTransform.rotation);
            _isHiding = true;
        }
    }
}

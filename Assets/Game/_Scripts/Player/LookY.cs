using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class LookY : MonoBehaviour
{
    [SerializeField]
    private float verticalSensitivity = 1f;
    private PhotonView photonView;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }
    void Start()
    {
        
    }


    void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        else
        {
            MouseLookUpAndDown();
        }
        

    }

    private void MouseLookUpAndDown()
    {
        float _mouseY = Input.GetAxis("Mouse Y");
        Vector3 newVerticalRotation = transform.eulerAngles;
        newVerticalRotation.x -= _mouseY * verticalSensitivity;
        transform.eulerAngles = newVerticalRotation;
    }
}

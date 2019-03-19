using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class LookX : MonoBehaviour
{
    [SerializeField]
    private float horizontalSensitivity = 1f;
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
        if (photonView.IsMine)
        {
            MouseRotation();
        }
        
    }

    private void MouseRotation()
    {

        //Eixo X
        float _mouseX = Input.GetAxis("Mouse X");
        Vector3 newRotation = transform.localEulerAngles;
        newRotation.y += _mouseX * horizontalSensitivity;
        transform.eulerAngles = newRotation;

        //A linha a baixo realiza a mesma função da linha a cima!
        /*transform.localEulerAngles = new Vector3(transform.localEulerAngles.x,
         * transform.localEulerAngles.y + (_mouseX * sensitivity),
           transform.localEulerAngles.z);*/


    }
}

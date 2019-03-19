using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerController : MonoBehaviourPun//, IPunObservable
{

    [SerializeField]
    private float horizontalSensitivity = 1f;

    [SerializeField]
    private GameObject lookY;
    [SerializeField]
    private float verticalSensitivity = 1f;

    private float _speed = 3.5f;
    private CharacterController _controller;
    private float _gravity = 9.807f;
    private PhotonView photonView;

    public GameObject myCameraPref, myCamAux;
    public Transform cameraLocal;






    private void Awake()
    {

        myCamAux = Instantiate(myCameraPref, cameraLocal);
        myCamAux.transform.position = cameraLocal.position;
        cameraLocal.transform.rotation = cameraLocal.rotation;

        photonView = GetComponent<PhotonView>();
        _controller = GetComponent<CharacterController>();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;


    }

    void Start()
    {

    }

    void Update()
    {
        Debug.Log("Current Player in Room: " + PhotonNetwork.CurrentRoom.PlayerCount);

        if (photonView.IsMine)
        {
            Movement();
            MouseRotation();
            GetItems();
            ESC();

        }
        else
        {
            myCamAux.SetActive(false);
        }
        


    }

    /*
    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);

            
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, (Vector3)stream.ReceiveNext(),Mathf.Abs((float)(PhotonNetwork.Time -  info.SentServerTime)));
            transform.rotation = Quaternion.Lerp(transform.rotation,(Quaternion)stream.ReceiveNext(), Mathf.Abs((float)(PhotonNetwork.Time - info.SentServerTime)));
            _controller = (CharacterController)stream.ReceiveNext();

            float lag = Mathf.Abs((float)(PhotonNetwork.Time - info.SentServerTime));
            _controller.transform.position += charCon.velocity * lag;


        }
    }*/

    private void Movement()
    {
        Vector3 d = Vector3.zero;
        d = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        d = transform.TransformDirection(d);
        d = d * _speed;

        d.y = d.y - (_gravity * Time.deltaTime);
        _controller.Move(d * Time.deltaTime);

    }

    private void MouseRotation()
    {

        //Eixo X
        float _mouseX = Input.GetAxis("Mouse X");
        Vector3 newRotation = transform.localEulerAngles;
        newRotation.y += _mouseX * horizontalSensitivity;
        transform.eulerAngles = newRotation;

    }

    private void MouseLookUpAndDown()
    {
        float _mouseY = Input.GetAxis("Mouse Y");
        Vector3 newVerticalRotation = lookY.gameObject.transform.eulerAngles;
        newVerticalRotation.x -= _mouseY * verticalSensitivity;
        lookY.gameObject.transform.eulerAngles = newVerticalRotation;
    }

    private void GetItems()
    {
        if (Input.GetMouseButton(0))
        {
            Ray rayOrigin = Camera.main.ViewportPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            RaycastHit hitInfo;

            if (Physics.Raycast(rayOrigin, out hitInfo))
            {
                Debug.Log("Hit: " + hitInfo.transform.name);
            }
        }
    }

    private void ESC()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !Cursor.visible)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && Cursor.visible)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

}

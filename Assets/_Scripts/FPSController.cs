using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FPSController : MonoBehaviour
{
    public float playerMoveSpeed = 5f;
    public Vector2 cameraSpeed = new Vector2(5f, 5f);

    private Vector3 playerMoveDirection = Vector3.zero;
    private Vector3 playerRotateDirection = Vector3.zero;

    private CharacterController charCtrl;
    private Camera cam;

    void Start()
    {
        charCtrl = GetComponent<CharacterController>();
        cam = GetComponentInChildren<Camera>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Move();
        Rotate();
    }

    void Move(){
        if (charCtrl.isGrounded)
        {
            playerMoveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            playerMoveDirection = transform.TransformDirection(playerMoveDirection);
            playerMoveDirection *= playerMoveSpeed;
        }
        playerMoveDirection.y -= 20f * Time.deltaTime;
        charCtrl.Move(playerMoveDirection * Time.deltaTime);
    }

    void Rotate(){
        float mhorizontal = Input.GetAxis("Mouse X");
        float mvertical = Input.GetAxis("Mouse Y");
        // playerRotateDirection = new Vector3(mvertical,mhorizontal, 0);
        cam.transform.Rotate(-mvertical * cameraSpeed.y, 0f, 0f);
        transform.Rotate(0f, mhorizontal * cameraSpeed.x, 0f);
    }
}

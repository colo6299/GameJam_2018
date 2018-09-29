using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public GameObject player;
    public Rigidbody rbodyPlayer;
    public Camera playerCam;

    public float mouseSensitivity = 2f;
    public float moveSpeed = 10;
    public float sprintSpeed = 15;
    public float moveAccel = 5;
    public float moveDecel = 2;
    public float jumpVel = 3;

    public bool invertY = false;
    public float cameraMaxUp = 80;
    public float cameraMaxDown = 80;

    private bool canJump = true;
    private bool jump = false;
    private float speed;

    CursorLockMode wantedMode;



    void Start()
    {
        speed = moveSpeed;
        Cursor.lockState = CursorLockMode.Locked;
        // Hide cursor when locking
        Cursor.visible = (CursorLockMode.Locked != wantedMode);
    }


    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
    }




    void FixedUpdate()
    {
        PlayerMove();




    }

    

    void PlayerMove()
    {



        rbodyPlayer.angularVelocity = Vector3.zero;

        player.transform.eulerAngles += new Vector3(0, Input.GetAxis("Mouse X"), 0) * mouseSensitivity;

        playerCam.transform.localEulerAngles -= new Vector3(Input.GetAxis("Mouse Y"), 0, 0) * mouseSensitivity;
        
        if (playerCam.transform.localEulerAngles.x > cameraMaxDown & playerCam.transform.localEulerAngles.x < 180)
        {
            playerCam.transform.localEulerAngles = new Vector3(cameraMaxDown, 0, 0);        
        }
        else if (playerCam.transform.localEulerAngles.x > 180 & playerCam.transform.localEulerAngles.x < 360 - cameraMaxUp)
        {
            playerCam.transform.localEulerAngles = new Vector3(360 - cameraMaxUp, 0, 0);
        }





        if (!canJump)
        {
            if (Physics.BoxCast(player.transform.position + Vector3.up, Vector3.one * 0.4f, Vector3.down, player.transform.rotation, 0.8f))
            {
                canJump = true;
            }
        }

        if (jump & canJump)
        {
            rbodyPlayer.velocity = new Vector3(rbodyPlayer.velocity.x, jumpVel, rbodyPlayer.velocity.z);
            canJump = false;
        }
        else
        {
            jump = false;
        }


        if (Input.GetButtonDown("Sprint"))
        {
            speed = sprintSpeed;
        }

        if (Input.GetButtonUp("Sprint"))
        {
            speed = moveSpeed;
        }


        float forwardMove = Input.GetAxis("Vertical");
        float sideMove = Input.GetAxis("Horizontal");

        Vector3 localVelocity = player.transform.InverseTransformDirection(rbodyPlayer.velocity);

        Vector2 clampVel2 = new Vector2(localVelocity.z, localVelocity.x);
        Vector2 movVec2 = new Vector2(forwardMove, sideMove).normalized;

        Vector2 calcVel2 = clampVel2 + movVec2 * moveAccel;
        if (clampVel2.magnitude > speed)
        {
            calcVel2 = Vector2.ClampMagnitude(calcVel2, clampVel2.magnitude);

        }
        else
        {
            calcVel2 = Vector2.ClampMagnitude(calcVel2, speed);
        }

        if (forwardMove == 0)
        {
            calcVel2.x = calcVel2.x / moveDecel;
        }

        if (sideMove == 0)
        {
            calcVel2.y = calcVel2.y / moveDecel;
        }


        localVelocity = new Vector3(calcVel2.y, rbodyPlayer.velocity.y, calcVel2.x);

        rbodyPlayer.velocity = player.transform.TransformDirection(localVelocity);


        








    }






}

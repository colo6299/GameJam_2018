using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement3ps : MonoBehaviour {


    public float speed = 1;
    public float accel = 1;
    public GameObject playerVisuals;
    public float lr;
    private Rigidbody thisRigidbody;
    public Animator animator;
    private Camera playerCam;
    public float moveAccel = 5f;
    public float moveDecel = 5f;
    private int layerMask = 1 << 14;



    void Start()
    {
        thisRigidbody = GetComponent<Rigidbody>();
        playerCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }




    void Update()
    {
        MovePlayer();
        AnimatePlayer();
    }




    

    void AnimatePlayer()
    {
        //dont use top bit
        /*
        animator.SetFloat("Horizontal", Input.GetAxis("AnimX"));
        animator.SetFloat("Forward", Input.GetAxis("AnimY"));
        */
        
        
        //Vector3 animVector = new Vector3(Input.GetAxis("AnimX"), 0, Input.GetAxis("AnimY"));

        playerVisuals.transform.rotation = CursorDir();

        //animVector = animator.transform.InverseTransformDirection(animVector);
        //animVector = transform.TransformDirection(animVector);

        //animator.SetFloat("Horizontal", animVector.x);
        //animator.SetFloat("Forward", animVector.z);
        
    }


    void MovePlayer()
    {
        /*
        float leftRight = Input.GetAxis("Horizontal");
        float upDown = Input.GetAxis("Vertical");
        Vector2 movePos = new Vector2(leftRight, upDown).normalized;
        movePos = movePos * Time.deltaTime * speed;
        Vector3 movePosV3 = new Vector3(movePos.x, 0, movePos.y);
        movePosV3 = transform.TransformDirection(movePosV3);


        thisRigidbody.velocity = movePosV3;
        */
        //dfasew

        float forwardMove = Input.GetAxis("Vertical");
        float sideMove = Input.GetAxis("Horizontal");

        Vector3 localVelocity = transform.InverseTransformDirection(thisRigidbody.velocity);

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


        localVelocity = new Vector3(calcVel2.y, thisRigidbody.velocity.y, calcVel2.x);

        thisRigidbody.velocity = transform.TransformDirection(localVelocity);

    }



    public Vector2 RotateVector(Vector2 v, float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        float _x = v.x * Mathf.Cos(radian) - v.y * Mathf.Sin(radian);
        float _y = v.x * Mathf.Sin(radian) + v.y * Mathf.Cos(radian);
        return new Vector2(_x, _y);
    }

    public Quaternion CursorDir()
    {
        Ray dRay = playerCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit dRayHit;
        Physics.Raycast(dRay, out dRayHit, 100f, layerMask);
        Vector3 cursorPoint = dRayHit.point;
        cursorPoint.y = transform.position.y;

        return Quaternion.LookRotation((cursorPoint - transform.position), Vector3.up);
    }
}

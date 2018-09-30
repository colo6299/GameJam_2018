using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {


    public GameObject attackPattern;
    public Transform playerCam;
    public float attackSpeed = 0.5f;

    private float attackTime = 0f;


    void Update()
    {
        if (Input.GetMouseButtonDown(0) & Time.time > attackTime)
        {
            if (FlowObject.falseTimescale > 0)
            {
                if (FlowObject.falseTimescale < 1)
                {
                    Destroy(Instantiate(attackPattern, playerCam.position, playerCam.rotation, playerCam), 0.5f);
                    attackTime = Time.time + 1 / attackSpeed;
                }
            }
        }
    }








}

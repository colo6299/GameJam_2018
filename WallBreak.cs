using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBreak : MonoBehaviour {

    private Rigidbody[] rbodies;
    private bool breakReady;

    void Awake()
    {
        rbodies = GetComponentsInChildren<Rigidbody>();
    }


    void Update()
    {
        if (breakReady & FlowObject.falseTimescale < 0)
        {
            foreach (Rigidbody rbd in rbodies)
            {
                rbd.isKinematic = true;
            }
        }
        if (FlowObject.falseTimescale == 1)
        {
            foreach (Rigidbody rbd in rbodies)
            {
                rbd.detectCollisions = true;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerCollider")
        {
            PlayerBreak();
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "PlayerCollider")
        {
            PlayerBreak();
        }
    }


    void PlayerBreak()
    {
        if (FlowObject.slowing & FlowObject.falseTimescale > 0)
        {
            if (FlowObject.falseTimescale == 1)
            {
                return;
            }
            foreach (Rigidbody rbd in rbodies)
            {
                rbd.isKinematic = false;
                breakReady = true;
            }
            Debug.Log(rbodies.Length);
        }
    }


}

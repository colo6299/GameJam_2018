using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clicker : MonoBehaviour {

    private Camera thisCamera;
    private int layerMask = 1 << 0;

    private int overrideMask = 1 << 15;
    private bool overrideFlag = false;


    void Awake()
    {
        thisCamera = GetComponent<Camera>();
    }


    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Click();
        }/*
        if (Input.GetButtonDown("Fire2"))
        {
            OverClick(15);
        }*/
    }



    void OverClick(int layer)
    {
        overrideFlag = !overrideFlag;
        overrideMask = 1 << layer;
    }

    void Click()
    {
        int mask = layerMask;
        if (overrideFlag)
        {
            mask = overrideMask;
        }
        Ray cRay = thisCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit cRayHit;
        Physics.Raycast(cRay, out cRayHit, 100f, mask);
        if (cRayHit.transform != null)
        {
            if (cRayHit.transform.gameObject.GetComponent<Reciever>() != null)
            {
                cRayHit.transform.gameObject.GetComponent<Reciever>().Click();
            }
        }
    }






}

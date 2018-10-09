using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowObjectMaster : MonoBehaviour {


    private float startTime;
    private bool started = false;
    private bool st = false;
    private Camera playerCam;

    private float normalFOV;
    private float FOVspread = 50f;
    private float FOVdistance = 0.5f;
    private float FOVretDistance = 0.1f;
    private bool returning = false;
    public GameObject waahhhDio;


    void Awake()
    {
        
        playerCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        normalFOV = 60;
        playerCam.fieldOfView = normalFOV;
        started = false;
        st = false;
        returning = false;
        startTime = 0;
        FlowObject.slowing = false;
        FlowObject.reversing = false;
        FlowObject.falseTimescale = 1;
        FlowObject.falseTimescalePrev = 1;

        

    }


    void Update()
    {
        if (Input.GetMouseButtonDown(1) && !FlowObject.slowing)
        {
            FlowObject.slowing = true;
            st = true;
            Destroy(Instantiate(waahhhDio, transform.position, transform.rotation, null), 10f);
        }

        if ((startTime + FlowObject.slowDistance * 2) - Time.time < 1f)
        {
            if (!(FlowObject.falseTimescale > 0))
            {
                playerCam.fieldOfView += FOVspread * Time.deltaTime / FOVdistance;
            }           
        }

        if (returning)
        {
            if (playerCam.fieldOfView > normalFOV)
            {
                playerCam.fieldOfView -= FOVspread * Time.deltaTime / FOVretDistance;
            }
            else if (playerCam.fieldOfView < normalFOV)
            {
                playerCam.fieldOfView = normalFOV;
            }
            else
            {
                returning = false;
            }
        }


    }

    void FixedUpdate()
    {
        if (st)
        {
            Crunch();
        }
    }

    void Crunch()
    {
        if (!started)
        {
            FlowObject.falseTimescale = 1f;
            started = true;
            startTime = Time.time;
        }
        FlowObject.falseTimescalePrev = FlowObject.falseTimescale;
        FlowObject.falseTimescale = (FlowObject.slowDistance - (Time.time - startTime)) / FlowObject.slowDistance;
        FlowObject.falseTimescale = FlowObject.falseTimescale * Mathf.Abs(FlowObject.falseTimescale);
        if (FlowObject.falseTimescale < -1)
        {
            FlowObject.falseTimescale = 1;
            st = false;
            started = false;
            FlowObject.slowing = false;
            returning = true;
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowObjectMaster : MonoBehaviour {


    private float startTime;
    private bool started = false;
    private bool st = false;


    void Update()
    {
        if (Input.GetMouseButtonDown(0) & !FlowObject.slowing)
        {
            FlowObject.slowing = true;
            st = true;

        }
    }

    void FixedUpdate()
    {
        if (st)
        {
            Crunch();
        }
        Debug.Log(FlowObject.falseTimescale);
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
        if (FlowObject.falseTimescale < -1)
        {
            FlowObject.falseTimescale = 1;
            st = false;
        }
        
    }
}

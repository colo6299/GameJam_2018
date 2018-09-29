using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertHere : MonoBehaviour {

    private Renderer rend;


    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        rend.material.SetFloat("_InvertAmount", FlowObject.falseTimescale);
    }

}


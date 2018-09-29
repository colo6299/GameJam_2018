using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetTimeScale : MonoBehaviour {
    Renderer objRenderer;

    void Start () {
        objRenderer = GetComponent<Renderer>();

	}
	
	// Update is called once per frame
	void Update () {
        objRenderer.material.SetFloat("_InvertAmount", FlowObject.falseTimescale);
        print(FlowObject.falseTimescale);
    }



}

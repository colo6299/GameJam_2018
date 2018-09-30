using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QDmanager : MonoBehaviour {

    void Update()
    {
        if (FlowObject.falseTimescale == 1)
        {
            GetComponent<Animation>().Play();
        }
    }
}

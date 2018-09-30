using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSpeedController : MonoBehaviour {

    private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        anim.speed = FlowObject.falseTimescale;
    }
}

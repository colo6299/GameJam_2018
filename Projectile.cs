using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {


    private Rigidbody rbody;
    public float projSpeed = 30f;
    private float rSpeed;



    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        rSpeed = projSpeed * FlowObject.falseTimescale;
    }
















}

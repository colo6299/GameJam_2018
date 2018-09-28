using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowObject : MonoBehaviour {


    public bool inFlow;
    public float slowDistance = 3f;

    private ObjectPast[] past;
    private static bool reversing;
    private static bool slowing;
    private Rigidbody rbody;

    private float mass;
    private Vector3 vel;
    private Vector3 angVel;
    private static float falseTimescale = 1;

    private float startTime;
    private bool started;

    private List<ObjectPast> pastList = new List<ObjectPast>();
    



    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        mass = rbody.mass;
        vel = rbody.velocity;
        angVel = rbody.angularVelocity;
    }






    void Update()
    {
        if (Input.GetMouseButtonDown(0) & !slowing)
        {
            slowing = true;
            Debug.Log(slowing);
        }
    }


    void FixedUpdate()
    {
        if (slowing)
        {
            Slow();
        }
        if (reversing)
        {
            Reverse();
        }
    }






    void Slow()
    {


        Track();
      


        if (!started)
        {
            falseTimescale = 1f;
            started = true;
            startTime = Time.time;
            mass = rbody.mass;
            vel = rbody.velocity;
            angVel = rbody.angularVelocity;
        }
        float prev = falseTimescale;
        falseTimescale = (slowDistance - (Time.time - startTime)) / slowDistance;

        if (falseTimescale > 0)
        {
            float rl = falseTimescale / prev;

            rbody.mass *= rl;
            rbody.velocity *= rl;
            rbody.angularVelocity *= rl;

            rbody.AddForce(Vector3.up * rbody.mass * 9.81f * (1 - falseTimescale), ForceMode.Force);
        }
        else
        {
            slowing = false;
            reversing = true;
            started = false;
            Reverse();
        }

        Debug.Log(falseTimescale);

    }

    void Reverse()
    {
        if (!started)
        {
            rbody.mass = mass;
            rbody.velocity = Vector3.zero;
            rbody.angularVelocity = Vector3.zero;
            rbody.isKinematic = true;
        }

        if (pastList.Count != 0)
        {
            ObjectPast past = pastList[pastList.Count - 1];
            pastList.RemoveAt(pastList.Count - 1);

            transform.position = past.position;
            transform.rotation = past.rotation;
        }
        else
        {
            reversing = false;
            started = false;
            rbody.isKinematic = false;
            rbody.velocity = vel;
            rbody.angularVelocity = angVel;
            rbody.mass = mass;

        }

        




    }






    void Track()
    {
        ObjectPast past = new ObjectPast(transform.position, transform.rotation);
        pastList.Add(past);
        Debug.Log(past);
    }




}

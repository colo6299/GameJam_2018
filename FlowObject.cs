using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FlowObject : MonoBehaviour {


    public bool inFlow;
    public static float slowDistance = 5f;

    private ObjectPast[] past;
    public static bool reversing;
    public static bool slowing;
     
    private Rigidbody rbody;
    private Enemy enemy;
    private float agentSpeed;
    private float agentTurn;

    private float mass;
    private Vector3 vel;
    private Vector3 angVel;
    public static float falseTimescale = 1;
    public static float falseTimescalePrev = 1;

    private float startTime;
    private bool started;
    private bool destroyAtEnd;
    

    public bool wallFlag = false;
    public bool enemyFlag = false;

    private List<ObjectPast> pastList = new List<ObjectPast>();
    



    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        mass = rbody.mass;
        vel = rbody.velocity;
        angVel = rbody.angularVelocity;
        destroyAtEnd = slowing;
        if (enemyFlag)
        {
            enemy = GetComponent<Enemy>();

        }
    }






    void Update()
    {

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
            started = true;
            startTime = Time.time;
            mass = rbody.mass;
            vel = rbody.velocity;
            angVel = rbody.angularVelocity;
            if (enemyFlag)
            {
                agentSpeed = enemy.agent.speed;
                agentTurn = enemy.agent.angularSpeed;
            }
        }


        if (falseTimescale > 0)
        {
            float rl = falseTimescale / falseTimescalePrev;

            rbody.mass *= rl;
            rbody.velocity *= rl;
            rbody.angularVelocity *= rl;

            rbody.AddForce(Vector3.up * rbody.mass * 9.81f * (1 - falseTimescale), ForceMode.Force);

            if (enemyFlag)
            {
                enemy.agent.speed = agentSpeed * falseTimescale;
                enemy.agent.angularSpeed = agentTurn * falseTimescale;
            }
        }
        else
        {

            slowing = false;
            reversing = true;
            started = false;
            Reverse();
        }



    }

    void Reverse()
    {
        
        if (!started)
        {
            rbody.mass = mass;
            rbody.velocity = Vector3.zero;
            rbody.angularVelocity = Vector3.zero;
            rbody.isKinematic = true;
            
            if (enemyFlag)
            {
                enemy.agent.enabled = false;
                enemy.enabled = false;
            }
        }

        if (pastList.Count != 0)
        {
            ObjectPast past = pastList[pastList.Count - 1];
            pastList.RemoveAt(pastList.Count - 1);

            transform.position = past.position;
            transform.rotation = past.rotation;
            rbody.detectCollisions = false;
        }
        else
        {
            if (destroyAtEnd)
            {
                Destroy(gameObject);
            }

            reversing = false;
            started = false;
            if (!wallFlag)
            {
                rbody.isKinematic = false;
            }
            rbody.velocity = vel;
            rbody.angularVelocity = angVel;
            rbody.mass = mass;
            rbody.detectCollisions = true;
            if (enemyFlag)
            {
                enemy.agent.enabled = true;
                enemy.enabled = true;

            }
        }

        




    }






    void Track()
    {
        ObjectPast past = new ObjectPast(transform.position, transform.rotation);
        pastList.Add(past);
    }




}

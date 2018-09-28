using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {


    public float time;
    public float speed;

    public float fireRate;
    public GameObject projectile;
    public Transform firePos;
    public Animator animator;
    public Transform eyePos;

    public GameObject player;
    public Transform waypoints;
    public float waypointDelay = 10f;
    private float wptTime;
    private int wptIter = 0;

    private float seeTotal;
    private float seeThreshold = 5f;
    private bool sawStart;
    private bool seesPlayer;
    private Vector3 sawPos;

    public float sightCeil = 1.5f;
    public float sightDrop = 0.5f;

    private float seeChance;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }



    void Update()
    {
        Look();
    }





    void Move()
    {
        if (seesPlayer)
        {

        }
        else
        {
            WaypointMove();
        }
    }

    void WaypointMove()
    {
        if (Time.time >= wptTime)
        {
            wptIter++;
            wptTime = Time.time + waypointDelay;
        }
    }




    void Fire()
    {
        Instantiate(projectile, firePos.position, firePos.rotation, null);
    }


    void See()
    {
        RaycastHit hit;
        if (Physics.Raycast(eyePos.position, player.transform.position - eyePos.position, out hit))
        {
            if (hit.transform.gameObject.tag == "Player")
            {
                Fire();
            }
        }
    }

    void Look()
    {
        RaycastHit hit;
        if (Physics.Raycast(eyePos.position, player.transform.position - eyePos.position, out hit))
        {
            if (hit.transform.gameObject.tag == "Player")
            {

                Vector3 targetDir = player.transform.position - transform.position;
                float angle = Vector3.Angle(targetDir, eyePos.forward);

                if (angle > 130)
                {
                    seeChance = 0;
                }
                else
                {
                    angle /= 180;
                    seeChance = 1 - angle;
                    seeChance /= targetDir.magnitude / 5f;
                    seeChance += 0.2f;
                }

                seeTotal += seeChance / Time.deltaTime;
                
            }
        }

        seeTotal -= 0.2f / Time.deltaTime;

        if (seeTotal > seeThreshold * sightCeil)
        {
            seeTotal = seeThreshold * sightCeil;
        }

        if (seeTotal > seeThreshold & !seesPlayer)
        {
            seesPlayer = true;
        }
        else if (seeTotal > seeThreshold * sightDrop & seesPlayer)
        {
            seesPlayer = true;
        }
        else if (seeTotal < 0)
        {
            seeTotal = 0f;
        }
        else
        {
            seesPlayer = false;
        }

        if (seesPlayer)
        {
            See();
        }

    }

}

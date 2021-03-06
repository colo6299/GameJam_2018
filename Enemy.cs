﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {


    public float time;
    public float speed;

    public float fireRate;
    private float fireTime;
    public GameObject projectile;
    public Transform firePos;
    public Animator animator;
    public Transform eyePos;
    public NavMeshAgent agent;
    public GameObject dieShards;
    public GameObject fireSound;

    public GameObject player;
    public Transform waypoints;
    public Transform currentTransform;
    public float waypointDelay = 10f;
    private float wptTime;
    private int wptIter = 0;

    private float seeTotal;
    public float seeThreshold = 5f;
    private bool sawStart;
    private bool seesPlayer;
    private Vector3 sawPos;

    public float sightCeil = 1.5f;
    public float sightDrop = 0.5f;

    private float seeChance;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = true;
        seeTotal = 0;
        sawStart = false;
        seesPlayer = false;
        seeChance = 0;
    }



    void Update()
    {
        Look();
        Move();
    }



    public GameObject Die()
    {
        GameObject g = Instantiate(dieShards, transform.position, transform.rotation, null);
        Destroy(g, 30);
        Destroy(gameObject);
        return g;
    }

    void Move()
    {
        if (seesPlayer)
        {
            AttackMove();
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
            currentTransform = waypoints.GetChild(wptIter % waypoints.childCount);
        }
        agent.SetDestination(currentTransform.position);
    }

    void AttackMove()
    {
        agent.SetDestination(player.transform.position);
    }



    void Fire()
    {
        if (Time.time > fireTime)
        {
            Destroy(Instantiate(projectile, firePos.position, firePos.rotation, null), 20);
            fireTime = Time.time + 1 / fireRate;
            Destroy(Instantiate(fireSound, firePos.position, firePos.rotation, null), 10f);
        }
    }


    void See()
    {
        RaycastHit hit;
        if (Physics.Linecast(eyePos.position, player.transform.position, out hit))
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
        if (Physics.Linecast(eyePos.position, player.transform.position, out hit))
        {
            if (hit.transform.gameObject.tag == "Player")
            {
                Vector3 targetDir = player.transform.position + Vector3.up - transform.position;
                float angle = Vector3.Angle(targetDir, eyePos.forward);

                if (angle > 130)
                {
                    seeChance = 0;
                }
                else
                {
                    angle /= 180;
                    seeChance = 1 - angle * angle;
                    seeChance /= targetDir.magnitude / 5f;
                    seeChance += 0.2f;
                }

                seeTotal += seeChance * Time.deltaTime * 10;
                
            }
        }

        seeTotal -= 2f * Time.deltaTime;

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPattern : MonoBehaviour {

    GameObject shards;
    GameObject enemy;
    public Transform player;
    public float force = 10f;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void OnTriggerEnter(Collider other)
    {
        shards = other.GetComponent<Enemy>().Die();
        enemy = other.gameObject;
        Explode();
        Debug.Log("boom");
    }


    void Explode()
    {
        foreach (Rigidbody r in shards.GetComponentsInChildren<Rigidbody>())
        {
            r.isKinematic = false;
            if (r.name != "Cube")
            {
                r.AddExplosionForce(force * r.mass, enemy.transform.position, 3f);
            }

        }
    }




}

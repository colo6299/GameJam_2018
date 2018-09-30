using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Projectile : MonoBehaviour {


    private Rigidbody rbody;
    public float projSpeed = 30f;
    private float rSpeed;
    private bool destroy;
    private Vector3 startPos;

    


    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        rSpeed = projSpeed * FlowObject.falseTimescale;
        destroy = FlowObject.slowing;
        startPos = transform.position;       
    }


    void Update()
    {
        rbody.angularVelocity = Vector3.zero;
        rSpeed = projSpeed * FlowObject.falseTimescale;
        Fly();

        if ((startPos - transform.position).sqrMagnitude > (startPos - transform.position - transform.forward).sqrMagnitude)
        {
            Destroy(gameObject);
        }
    }


    void Fly()
    {
        rbody.velocity = rSpeed * transform.forward;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerCollider")
        {
            other.transform.parent.GetComponent<PlayerSceneManager>().Die();
        }
        else
        {
            Destroy(gameObject);
        }
    }








}

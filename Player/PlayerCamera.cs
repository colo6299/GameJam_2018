using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {

    public Transform player;

    private Vector3 offset;


    void Start()
    {
        if (offset == Vector3.zero)
        {
            offset = player.position - transform.position;
        }
    }

    void Update()
    {
        transform.position = player.position - offset;

    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDissolve : MonoBehaviour {

    GameObject particleEffect;
    
    void OnTriggerEnter(Collider other)
    {
        Destroy(Instantiate(particleEffect, other.transform.position, other.transform.rotation, null), 2f);
        Destroy(other.gameObject);
    }








}

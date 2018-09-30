using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDissolve : MonoBehaviour {

    public GameObject particleEffect;
    
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("af");
        GameObject p = Instantiate(particleEffect, other.transform.position, other.transform.rotation, null);
        Destroy(p, 2f);
        Destroy(other.gameObject);
    }








}

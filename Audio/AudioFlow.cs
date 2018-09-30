using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFlow : MonoBehaviour {

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(audioSource.clip);
    }

    void Update()
    {
        audioSource.pitch = FlowObject.falseTimescale;
    }
}

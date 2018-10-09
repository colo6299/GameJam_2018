using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinCondition : MonoBehaviour {

    public string nextLevel;
    public Camera playerCam;
    public float FOVspread = 60;
    public float FOVdistance = 1;

    private float timeToGo;
    private bool going;

    void Update()
    {
        if (timeToGo - Time.time < 1f)
        {
            if (going)
            {
                playerCam.fieldOfView += FOVspread * Time.deltaTime / FOVdistance;
            }
        }
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && !going)
        {
            timeToGo = Time.time + 12f;
            going = true;
            Debug.Log("going");

        }

        if (timeToGo < Time.time && going)
        {
            SceneManager.LoadSceneAsync(nextLevel);
        }

    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour {

    public bool toStart;
    public bool toTitle;
    public bool toDesktop;

    public Reciever reciever;

    void Update()
    {
        if (reciever.clicked)
        {
            OnClickky();
        }
    }


    void OnClickky()
    {
        if (toStart)
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        }

        if (toTitle)
        {
            SceneManager.LoadSceneAsync("Main Menu");
        }

        if (toDesktop)
        {
            Application.Quit();
        }
    }
}

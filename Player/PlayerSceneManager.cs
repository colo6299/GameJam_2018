using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSceneManager : MonoBehaviour {

    private PlayerMovement3ps plrMv;
    private bool ded;
    public bool restart;
    public bool toMenu;
    public bool exit;
    public GameObject deathMenu;
    public GameObject escMenu;

    public bool inMenu = false;

    public string currentLevel;

    void Awake()
    {
        plrMv = GetComponent<PlayerMovement3ps>();
    }


    void Update()
    {


        if (transform.position.y < -10)
        {
            Die();
        }

        Time.timeScale = 1;
        if (Input.GetButtonDown("Cancel") && !inMenu && !ded)
        {
            OpenMenu();
        }
        else if (Input.GetButtonDown("Cancel") && inMenu && !ded)
        {
            inMenu = false;
            escMenu.SetActive(false);
            plrMv.enabled = true;
        }








        if (ded && restart)
        {
            SceneManager.LoadSceneAsync(currentLevel);
        }

        if (ded && exit)
        {
            Application.Quit();
        }

        if (ded && toMenu)
        {
            SceneManager.LoadSceneAsync("Main Menu");
        }
    }

    void OpenMenu()
    {
        inMenu = true;
        escMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        plrMv.enabled = false;
    }

    public void Die()
    {
        plrMv.enabled = false;
        ded = true;
        OpenMenu();
        deathMenu.SetActive(true);

    }
}

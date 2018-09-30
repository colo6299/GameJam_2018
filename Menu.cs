using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public CanvasRenderer rend;
    private bool clicked;
    private bool quit_clicked = false;
    float i = -1;
    string level = "LevelZero";


    public void PlayGame(string LevelName)
    {
        SceneManager.LoadSceneAsync(LevelName);
    }


    private void Update()
    {
        if (quit_clicked)
        {
            Application.Quit();
        }
        else if (clicked)
        {
            if (i < 1f)
            {
                UpdatePixels(i);
                i += 1.4f * Time.deltaTime;
            }
            else 
            {
                i = -1;
                UpdatePixels(i);
                clicked = false;
                PlayGame(level);
            }
        }
    }

    public void LevelZeroButtonClicked()
    {
        clicked = true;
        level = "LevelZero";
    }
    public void LevelOneButtonClicked()
    {
        clicked = true;
        level = "LevelOne";
    }
    public void LevelTwoButtonClicked()
    {
        clicked = true;
        level = "LevelTwo";
    }
    public void LevelThreeButtonClicked()
    {
        clicked = true;
        level = "LevelThree";
    }


    public void MenuButtonClicked()
    {
        clicked = true;
        level = "Main Menu";
    }

    public void LevelButtonClicked()
    {
        clicked = true;
        level = "LevelSelect";
    }

    public void QuitButtonClicked()
    {
        quit_clicked = true;
    }

    void UpdatePixels(float i)
    {
        print(i);
        rend.GetMaterial().SetFloat("_InvertAmount", i);
        foreach (CanvasRenderer childRend in gameObject.GetComponentsInChildren<CanvasRenderer>()) {
            childRend.GetMaterial().SetFloat("_InvertAmount", i);
        }
    }

}



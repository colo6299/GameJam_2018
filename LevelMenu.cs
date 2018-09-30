using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{

    public CanvasRenderer rend;
    private bool clicked;
    float i = -1;
    string level = "LevelZero";


    public void PlayGame(string LevelName)
    {
        SceneManager.LoadSceneAsync(LevelName);
    }


    private void Update()
    {
        if (clicked)
        {
            if (i < 1f)
            {
                UpdatePixels(i);
                i += 0.8f * Time.deltaTime;
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

    void UpdatePixels(float i)
    {
        print(i);
        rend.GetMaterial().SetFloat("_InvertAmount", i);
    }

}



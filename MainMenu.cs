using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public CanvasRenderer rend;
    private bool play_clicked;
    private bool quit_clicked;
    float i = -1;


    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("LevelSelect");
    }

    public void QuitGame()
    {
        print("quit");
        Application.Quit();
    }

    private void Update()
    {  
        if (play_clicked || quit_clicked)
        {
            if (i < 1f)
                {
                    UpdatePixels(i);
                    i += 0.5f * Time.deltaTime;
                }
            else if (play_clicked)
            {
                i = -1;
                UpdatePixels(i);
                play_clicked = false;
                PlayGame();
            }
            else if (quit_clicked)
            {
                i = -1;
                UpdatePixels(i);
                quit_clicked = false;
                QuitGame();
            }
        }     
    }

    public void PlayButtonClicked()
    {
        play_clicked = true;
    }

    public void QuitButtonClicked()
    {
        quit_clicked = true;
    }

    void UpdatePixels(float i)
    {
        print(i);
        rend.GetMaterial().SetFloat("_InvertAmount", i);
    }

}




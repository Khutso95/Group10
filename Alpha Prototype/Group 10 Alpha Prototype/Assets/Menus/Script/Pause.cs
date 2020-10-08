using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject howtoMenu;
    public GameObject pauseMenu;
    public static bool isPause;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        howtoMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPause)
            {
                Resume();
            }
            else
            {
                pauseGame();
            }
        }
    }

    public void pauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPause = true;

    }
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPause = false;

    }

    public void GoToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void quit()
    {
        Application.Quit();
    }

    public void Howto()
    {
        howtoMenu.SetActive(true);
        pauseMenu.SetActive(false);
        Time.timeScale = 0f;
        
    }

    public void Back()
    {
        howtoMenu.SetActive(false);
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
       
    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreen : MonoBehaviour
{
    public GameObject pauseScreen;
    public GameObject controls;
    public GameObject audioManager;
    public bool isPaused;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = true;
            PauseGame();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseScreen.SetActive(true);
        audioManager.SetActive(false);
       /* if (Input.GetKeyDown(KeyCode.Escape))
        {
            ResumeGame();
        }*/
    }

    public void ResumeGame()
    {
        isPaused = false;
        audioManager.SetActive(true);
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
    }

    public void HowToPlay()
    {
        controls.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}

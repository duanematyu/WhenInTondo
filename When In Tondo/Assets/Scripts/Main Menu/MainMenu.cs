using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject[] activeGameObject;
    public GameObject controls;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        //SceneManager.LoadScene(1);
        foreach(GameObject gameObj in activeGameObject)
        {
            gameObj.SetActive(true);
            this.gameObject.SetActive(false);
        }
        FindObjectOfType<AudioManager>().Play("BGM");
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsPopUp : MonoBehaviour
{
    public GameObject[] controlPages;
    public GameObject controlPopUp;
    public GameObject next;
    public GameObject previous;
    public int pageCount;
    public int currentPageCount;
    // Start is called before the first frame update
    void Start()
    {
      
        previous.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (pageCount == 0)
        {
            previous.SetActive(false);
        }

        if (pageCount == 4)
        {
            next.SetActive(false);
        }
        else
        {
            next.SetActive(true);
        }
    }

    public void NextPage()
    {
        previous.SetActive(true);
        
        controlPages[pageCount].SetActive(false);
        pageCount += 1;
        controlPages[pageCount].SetActive(true);
    }

    public void PreviousPage()
    {
        /*pageCount -= 1;
        for (int i = 0; i < controlPages.Length; i--)
        {
            controlPages[i].SetActive(false);
            controlPages[pageCount].SetActive(true);
        }*/
        controlPages[pageCount].SetActive(false);
        pageCount -= 1;
        controlPages[pageCount].SetActive(true);
    }

    public void CloseTab()
    {
        controlPopUp.SetActive(false);
    }    
}

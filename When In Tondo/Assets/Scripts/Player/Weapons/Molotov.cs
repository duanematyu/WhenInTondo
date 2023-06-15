using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Molotov: MonoBehaviour
{
    public int molotovCount;
    public GrenadeController grenadeController;
    public Transform throwPoint;
    public TextMeshProUGUI molotovCountText;
    public Animator playerAnim;
    PauseScreen pauseScreen;

    // Start is called before the first frame update
    void Start()
    {
        pauseScreen = GetComponent<PauseScreen>();
        molotovCountText.text = molotovCount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
      if(Input.GetButtonDown("Fire2"))
        {
           if(molotovCount > 0 && pauseScreen.isPaused == false)
           {
                playerAnim.SetTrigger("throw");
                Instantiate(grenadeController, throwPoint.position, transform.rotation);
                molotovCount--;
                molotovCountText.text = molotovCount.ToString();
            }
        }
    }
}

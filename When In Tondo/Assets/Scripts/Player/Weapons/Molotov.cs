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

    // Start is called before the first frame update
    void Start()
    {
        molotovCountText.text = molotovCount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
      if(Input.GetButtonDown("Fire2"))
        {
           if(molotovCount > 0)
           {
                Instantiate(grenadeController, throwPoint.position, transform.rotation);
                molotovCount--;
                molotovCountText.text = molotovCount.ToString();
            }
        }
    }
}

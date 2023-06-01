using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AmmoCountUI : MonoBehaviour
{
    public TextMeshProUGUI ammoCountText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AmmoCountUpdate (int ammoCount)
    {
        ammoCountText.text = ammoCount.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleOverTime : MonoBehaviour
{
    public float scaleSpeed;
    public float minScale, maxScale;

    Vector2 scale;

    // Start is called before the first frame update
    void Start()
    {
        scale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        scale = new Vector2(Mathf.Clamp(scale.x += scaleSpeed * Time.deltaTime, minScale, maxScale), scale.y);
        scale = new Vector2(scale.x, Mathf.Clamp(scale.y += scaleSpeed * Time.deltaTime, minScale, maxScale));

        transform.localScale = scale;
    }
}

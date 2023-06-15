using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISpriteAnim : MonoBehaviour
{
    public Image m_Image;
    public GameObject parentObj;
    public Sprite[] m_SpriteArray;
    public float m_Speed = .02f;

    private int m_IndexSprite;
    Coroutine m_CoroutineAnim;
    bool IsDone;

    private void Start()
    {
        StartCoroutine(Func_PlayAnimUI());
    }

    private void Update()
    {
        if(parentObj.activeInHierarchy)
        {
           // StartCoroutine(Func_PlayAnimUI());
        }
    }
    public void ReplayAnim()
    {
        StartCoroutine(Func_PlayAnimUI());
    }

    public void Func_PlayUIAnim()
    {
        IsDone = false;
        StartCoroutine(Func_PlayAnimUI());
    }

    IEnumerator Func_PlayAnimUI()
    {
        yield return new WaitForSeconds(m_Speed);
        if (m_IndexSprite >= m_SpriteArray.Length)
        {
            m_IndexSprite = 0;
        }
        m_Image.sprite = m_SpriteArray[m_IndexSprite];
        m_IndexSprite += 1;
        if (IsDone == false)
            m_CoroutineAnim = StartCoroutine(Func_PlayAnimUI());
    }

    public void PlayAnim()
    {
        if (m_IndexSprite >= m_SpriteArray.Length)
        {
            m_IndexSprite = 0;
        }
        m_Image.sprite = m_SpriteArray[m_IndexSprite];
        m_IndexSprite += 1;
        if (IsDone == false)
            m_CoroutineAnim = StartCoroutine(Func_PlayAnimUI());
    }
}

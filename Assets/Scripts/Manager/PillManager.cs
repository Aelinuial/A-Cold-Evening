using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillManager : GameSingleton<PillManager>
{
    //控制药片的生成
    //大概十秒钟出现一片

    public GameObject pill_prefab;
    private GameObject[] m_pill;

    void CreatePill()
    {
        GameObject new_pill = GameObject.Instantiate(pill_prefab);
        //int scale_width = Screen.width / 30;
        //int scale_height = Screen.height / 20;
        new_pill.GetComponent<RectTransform>().localPosition = new Vector2(Random.Range(100, Screen.width-100), Screen.height);
        new_pill.transform.SetParent(GameObject.Find("Canvas").transform);
        new_pill.tag = Consts.Pill;
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (GameManager.Instance.timer != 0 && GameManager.Instance.timer % Consts.PillGap == 0)
        {
            m_pill = GameObject.FindGameObjectsWithTag(Consts.Pill);
            if (m_pill.Length < 1)
            {
                CreatePill();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Pill : MonoBehaviour
{
    // 药片逻辑：
    // 从最高处掉落到地面，总共显示时长是一定的
    private int _lifetime;// 药片的显示时长
    private int _borntime;// 药片生成的时间（即当前游戏时间）
    
    void Start()
    {        
        _lifetime = 0;        
        _borntime = GameManager.Instance.timer;
    }
    
    void Update()
    {
        _lifetime = GameManager.Instance.timer - _borntime;
        if(_lifetime >= Consts.PillLife)
        {
            Destroy(this.gameObject);
        }
    }
}

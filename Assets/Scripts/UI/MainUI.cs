using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    [Header("压力值文本")]
    public Text pressureText;

    [Header("时间文本")]
    public Text timeText;

    [Header("药片数量")]
    public Text pillText;

    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag(Consts.Player).GetComponent<Player>();
        pressureText.text = "50";
        timeText.text = "0";
        pillText.text = "0";
    }
    
    public void UpdateInfo()
    {
        pressureText.text = player.pressure.ToString();
        timeText.text = GameManager.Instance.timer.ToString();
        pillText.text = player.pillnum.ToString();
    }
}

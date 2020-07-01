using UnityEngine;

public class Globals : UnitySingleton<Globals>
{
    [Header("输入名字")]
    public GameObject inputPanel;

    public string username { get; set; }
    //public int chapter { get; set; }
    //public int level { get; set; }
    public bool signIn = false;
    public bool hasLoadIntro = false;

    void Start()
    {
        //inputPanel.SetActive(true);
        if (PlayerPrefs.HasKey("Has_Load_Intro"))
            hasLoadIntro = PlayerPrefs.GetInt("Has_Load_Intro") > 0;
    }
}

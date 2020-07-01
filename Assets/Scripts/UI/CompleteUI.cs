using UnityEngine;
using UnityEngine.UI;

public class CompleteUI : MonoBehaviour
{
    [Header("总分")]
    public Text allScoreText;
    //[Header("提示文本")]
    //public GameObject hintText;
    [Header("游戏胜利返回按钮")]
    public Button winBackBtn;
    [Header("游戏胜利重来按钮")]
    public Button winResetButton;
    [Header("游戏结束返回按钮")]
    public Button overBackBtn;
    [Header("游戏结束重来按钮")]
    public Button overResetBtn;

    void Start()
    {
        // 注册按钮事件
        winBackBtn.onClick.AddListener(delegate { OnBackBtnClick(); });
        overBackBtn.onClick.AddListener(delegate { OnBackBtnClick(); });
        winResetButton.onClick.AddListener(delegate { OnResetButtonClick(); });
        overResetBtn.onClick.AddListener(delegate { OnResetButtonClick(); });
    }

    void OnBackBtnClick()
    {
        Time.timeScale = 1;
        SceneMgr.Instance.LoadScene(Consts.Scene_Menu);
        AudioMgr.Instance.PlayEffect(Consts.Audio_Click);

        AudioMgr.Instance.PlayMusic(Consts.Audio_BGM);
    }

    void OnResetButtonClick()
    {
        Time.timeScale = 1;
        // 重新加载场景
        SceneMgr.Instance.LoadScene(Consts.Scene_Main);
        AudioMgr.Instance.PlayEffect(Consts.Audio_Click);

        AudioMgr.Instance.PlayMusic(Consts.Audio_BGM);
    }

    /// <summary>
    /// 游戏胜利更新分数
    /// </summary>
    public void UpdateScore()
    {
        //int time_score = GameManager.Instance.timer;
        int score = Consts.Max_Time - (GameManager.Instance.timer-1) + Consts.Max_Pressure - GameManager.Instance.pressure;

        allScoreText.text = score.ToString();
    }

    
}

using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    [Header("确认按钮")]
    public Button confirmBtn;
    [Header("输入框")]
    public InputField inputField;
    [Header("输入面板")]
    public GameObject inputPanel;
    [Header("随机按钮")] 
    public Button randomBtn;

    [Header("开始按钮")]
    public Button startBtn;

    [Header("帮助按钮")]
    public Button helpBtn;
    [Header("帮助面板")]
    public GameObject helpPanel;

    [Header("排行按钮")]
    public Button rankBtn;
    [Header("排行关闭按钮")]
    public Button rankClose;
    [Header("排行面板")]
    public GameObject rankPanel;

    [Header("选项按钮")]
    public Button optionBtn;
    [Header("选项关闭按钮")]
    public Button optionClose;
    [Header("选项面板")]
    public GameObject optionPanel;

    [Header("退出按钮")]
    public Button quitBtn;

    [Header("音量滑块")]
    public Slider musicSlider;
    [Header("音效滑块")]
    public Slider effectSlider;

    [Header("背景介绍面板")]
    public GameObject introPanel;
    [Header("背景继续按钮")]
    public Button continueButton;

    [Header("雨滴特效")]
    public RainScript2D rainEffect;

    void Start()
    {
        // 注册按钮事件
        randomBtn.onClick.AddListener(delegate { OnRandomBtnClick(); });
        confirmBtn.onClick.AddListener(delegate { OnConfirmBtnClick(); });
        startBtn.onClick.AddListener(delegate { OnStartBtnClick(); });
        rankBtn.onClick.AddListener(delegate { OnRankBtnClick(); });
        rankClose.onClick.AddListener(delegate { OnRankCloseClick(); });
        optionBtn.onClick.AddListener(delegate { OnOptionBtnClick(); });
        optionClose.onClick.AddListener(delegate { OnOptionCloseClick(); });
        quitBtn.onClick.AddListener(delegate { OnQuitBtnClick(); });
        helpBtn.onClick.AddListener(delegate { OnHelpBtnClick(); });
        continueButton.onClick.AddListener(delegate { OnContinueBtnClick(); });

        // 注册滑块事件
        musicSlider.onValueChanged.AddListener(delegate { OnMusicSliderChange(); });
        effectSlider.onValueChanged.AddListener(delegate { OnEffectSliderChange(); });

        // 读取音量存档 默认0.5
        float musicVolume = PlayerPrefs.GetFloat(Consts.Music_Volume, 0.5f);
        musicSlider.value = musicVolume;
        AudioMgr.Instance.SetMusicVolume(musicVolume);

        // 读取音效存档 默认0.5
        float effectVolume = PlayerPrefs.GetFloat(Consts.Effect_Volume, 0.5f);
        effectSlider.value = effectVolume;
        AudioMgr.Instance.SetEffectVolume(effectVolume);

        // 开启雨滴特效
        rainEffect.RainIntensity = 0.3f;
    }

    void LoadScene(int index)
    {
        SceneMgr.Instance.LoadSceneAsnc(index);
        //this.gameObject.SetActive(false);
    }

    /// <summary>
    /// 随机
    /// </summary>
    void OnRandomBtnClick()
    {
        string name = "Player" + (int)(Random.value * 10000);
        inputField.text = name;

        AudioMgr.Instance.PlayEffect(Consts.Audio_Click);
    }

    /// <summary>
    /// 确认
    /// </summary>
    void OnConfirmBtnClick()
    {
        string username = inputField.text;

        if (string.IsNullOrEmpty(username)) { return; }

        Globals.Instance.username = username;
        Globals.Instance.signIn = true;
        inputPanel.SetActive(false);

        AudioMgr.Instance.PlayEffect(Consts.Audio_Click);

        introPanel.SetActive(true);
        //rainEffect.RainIntensity = 0.1f;
        //LoadScene(Consts.Scene_Main);
    }

    /// <summary>
    /// 继续
    /// </summary>
    void OnContinueBtnClick()
    {
        introPanel.SetActive(false);

        // 雨停
        rainEffect.RainIntensity = 0;

        // 进入游戏
        AudioMgr.Instance.PlayEffect(Consts.Audio_Click);
        LoadScene(Consts.Scene_Main);

        Globals.Instance.hasLoadIntro = true;
        PlayerPrefs.SetInt("Has_Load_Intro", 1);
    }

    /// <summary>
    /// 开始
    /// </summary>
    void OnStartBtnClick()
    {
        if (!Globals.Instance.signIn)
        {
            inputPanel.SetActive(true);
        }
        else
        {
            introPanel.SetActive(true);
            //LoadScene(Consts.Scene_Main);
        }
        AudioMgr.Instance.PlayEffect(Consts.Audio_Click);
    }

    /// <summary>
    /// 排行
    /// </summary>
    void OnRankBtnClick()
    {
        rankPanel.SetActive(true);
        AudioMgr.Instance.PlayEffect(Consts.Audio_Click);
    }

    void OnRankCloseClick()
    {
        rankPanel.SetActive(false);
        AudioMgr.Instance.PlayEffect(Consts.Audio_Click);
    }


    /// <summary>
    /// 帮助
    /// </summary>
    void OnHelpBtnClick()
    {
        //if (!Globals.Instance.signIn)
        //{
        //    inputPanel.SetActive(true);
        //}
        //else
        //{
            helpPanel.SetActive(true);
        //}
        AudioMgr.Instance.PlayEffect(Consts.Audio_Click);
    }

    /// <summary>
    /// 选项
    /// </summary>
    void OnOptionBtnClick()
    {
        optionPanel.SetActive(true);
        AudioMgr.Instance.PlayEffect(Consts.Audio_Click);
    }

    void OnOptionCloseClick()
    {
        optionPanel.SetActive(false);
        AudioMgr.Instance.PlayEffect(Consts.Audio_Click);
    }

    /// <summary>
    /// 退出
    /// </summary>
    void OnQuitBtnClick()
    {
        Application.Quit();
        AudioMgr.Instance.PlayEffect(Consts.Audio_Click);
    }

    /// <summary>
    /// 音量控制
    /// </summary>
    void OnMusicSliderChange()
    {
        float value = musicSlider.value;
        AudioMgr.Instance.SetMusicVolume(value);
        PlayerPrefs.SetFloat(Consts.Music_Volume, value);
    }

    /// <summary>
    /// 音效控制
    /// </summary>
    void OnEffectSliderChange()
    {
        float value = effectSlider.value;
        AudioMgr.Instance.SetEffectVolume(value);
        PlayerPrefs.SetFloat(Consts.Effect_Volume, value);
    }
}

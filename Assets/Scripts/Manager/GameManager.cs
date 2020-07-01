using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    Ready,
    Start,
    Run, // 游戏运行
    GameWin,
    GameOver,
    End
}

public class GameManager : GameSingleton<GameManager>
{
    [Header("游戏状态")]
    public GameState gameState;

    [Header("UI管理类")]
    public GameObject uiMgr;

    [Header("游戏胜利界面")]
    public GameObject gameWinPanel;
    [Header("游戏结束界面")]
    public GameObject gameOverPanel;
    
    public int pressure
    {
        get
        {
            return GameObject.FindGameObjectWithTag(Consts.Player).GetComponent<Player>().pressure;
        }
    }
    
    private float _timer = 0.0f; // 计时
    public int timer
    {
        get { return (int)Mathf.Ceil(_timer); }
    }

    private Canvas _canvas;

    void Start()
    {
        uiMgr = GameObject.Find("UIMgr");
        _canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
    }
    
    void Update()
    {
        switch (gameState)
        {
            case GameState.Ready:
                Debug.Log("GameState.Ready");

                _timer = 0;
                _canvas.renderMode = RenderMode.ScreenSpaceOverlay;

                break;
            case GameState.Start:
                Debug.Log("GameState.Start");

                // 计时
                _timer += Time.deltaTime;

                // 更新UI
                uiMgr.GetComponent<MainUI>().UpdateInfo();

                // 压力满时死亡
                if (pressure >= Consts.Max_Pressure)
                {
                    gameState = GameState.GameOver;
                    break;
                }

                // 到最大时间且仍存活 / 压力值为0时胜利
                if (_timer >= Consts.Max_Time || pressure <= 0) { gameState = GameState.GameWin; }
                break;
            case GameState.Run:

                // NONE
                Debug.Log("GameState.Run");

                break;
            case GameState.GameWin:
                Debug.Log("GameState.GameWin");
                gameState = GameState.End;

                gameWinPanel.SetActive(true);
                gameWinPanel.transform.SetAsLastSibling();// 置于顶层
                // 让人和地板出现在背景里
                //GameObject.Find(Consts.Player).transform.SetAsLastSibling();
                //GameObject.Find(Consts.Wall).transform.SetAsLastSibling();
                // 雨出现在背景里（粒子效果出现在UI上）
                _canvas.renderMode = RenderMode.ScreenSpaceCamera;
                _canvas.worldCamera = Camera.main;

                // 播放胜利音乐
                AudioMgr.Instance.PlayMusic(Consts.Audio_Win);

                // 雨停
                RainManager.Instance.rain_stop = true;

                // 更新UI
                uiMgr.GetComponent<CompleteUI>().UpdateScore();

                // 保存分数（即剩余时间+压力，用时越短且压力越小者分数越高）
                //Debug.Log(timer.ToString() + " " + pressure.ToString());
                string info = Globals.Instance.username + " " + (Consts.Max_Time - (timer - 1) + Consts.Max_Pressure - pressure).ToString();
                Util.WriteIntoTxt(info);

                break;
            case GameState.GameOver:
                Debug.Log("GameState.GameOver");
                gameState = GameState.End;

                gameOverPanel.SetActive(true);
                gameOverPanel.transform.SetAsLastSibling();// 置于顶层
                // 让人和地板出现在背景里
                GameObject.Find(Consts.Player).transform.SetAsLastSibling();
                GameObject.Find(Consts.Wall).transform.SetAsLastSibling();
                // 雨出现在背景里
                _canvas.renderMode = RenderMode.ScreenSpaceCamera;
                _canvas.worldCamera = Camera.main;


                // 销毁玩家（为了不出现玩家跳到空中还没落下就被销毁，还是不销毁了？）
                Destroy(GameObject.Find(Consts.Player));
                Rigidbody2D playerRigidbody2D = GameObject.FindGameObjectWithTag(Consts.Player).GetComponent<Rigidbody2D>();
                Destroy(playerRigidbody2D);

                // 生成失败特效（雨变大）
                RainManager.Instance.rain_heavy = true;
                //GameObject overEffect = Instantiate(Resources.Load(Consts.Effect_Over) as GameObject);
                //overEffect.transform.position = carTrans.position + new Vector3(0, 5, 0);

                // 播放失败音乐
                AudioMgr.Instance.PlayMusic(Consts.Audio_Over);

                break;
            case GameState.End:
                Debug.Log("GameState.End");

                if (gameWinPanel.activeInHierarchy)
                {
                    // 胜利特效（日出效果+雨停）200/255 200/255 170/255
                    Color color = gameWinPanel.GetComponent<Image>().color;
                    float step = Consts.ColorChangeSpeed * Time.deltaTime;
                    //gameWinPanel.GetComponent<Image>().color = new Color(Mathf.Lerp(color.r, 200 / 255.0f, step), Mathf.Lerp(color.g, 200 / 255.0f, step), Mathf.Lerp(color.b, 170 / 255.0f, step), 1);

                    //RainManager.Instance.rain_stop = true;
                }

                break;
            default:
                break;
        }
    }
}

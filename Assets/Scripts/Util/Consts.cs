public class Consts
{
    // tag
    public const string MainCamera = "MainCamera";
    public const string Player = "Player";
    public const string Barrage_String = "Barrage_String";
    public const string Barrage_Word = "Barrage_Word";
    public const string Barrage_Good = "Barrage_Good";
    public const string Wall = "Wall";
    public const string Pill = "Pill";

    // scene
    public const int Scene_Menu = 0;
    public const int Scene_Main = 1;
    //public const int Scene_End = 2;

    // save
    public const string Music_Volume = "MusicVolume";
    public const string Effect_Volume = "EffectVolume";

    // loading
    public const string Loading_Scene = "LoadingScene";
    public const string Loading_Progress = "LoadingProgress";
    public const string Loading_Bar = "LoadingBar";
    // public const string Loading_Icon = "LoadingIcon";

    // audio
    public const string Audio_BGM = "music/Evening";
    public const string Audio_Click = "music/click";
    public const string Audio_Win = "music/he_1";
    public const string Audio_Over = "music/Longest_Night";
    public const string Audio_Hurt = "music/hurt";
    public const string Audio_Get_Medicine = "music/get_medicine";
    public const string Audio_Eat_Medicine = "music/eat_medicine";
    //public const string Audio_Die = "music/die";

    // effect
    public const string Effect_Win = "effect/winEffect";
    public const string Effect_Over = "effect/overEffect";

    // 弹幕数据库 每次生成的时候随机取
    public static string[] BarragesAll = {
        "你就是太懒了",
        "你怎么不去死呢",
        "天天说想自杀怎么不做呢",
        "抑郁症不是病",
        "你这就是矫情",
        "每个人都是这样的",
        "你想明白就行了",
        "都是这样过来的",
        "不过是博人眼球吧",
        "说不定是太闲了",
        "是不是还要给你筹个款去看病？",
        "真不懂事，洗洗睡吧",
        "大家都不容易，怎么就你发牢骚",
        "坚强点啊，多大点事",
        "XX得了癌症都没你这样",
        "年轻没见过世面才会抑郁",
        "听说抑郁症是富贵病，羡慕",
        "你平时都很开心啊，怎么可能抑郁",
        "是你自己想太多了，想开点就好了",
        "真正的抑郁症才不会说出来",
        "你有精神病啊？那我可要离你远点",
        "别散播负能量了",
        "之前怎么没发现你这么矫情",
        "假的吧",
        "看你就不像抑郁",
        "说这些有意思吗",
    };

    // 友善弹幕数据库 每次生成的时候随机取
    public static string[] BarragesGoodAll = {
        "心疼，你怎么啦",
        "跟我聊聊吧",
        "我可以给你一些帮助",
        "要不要出去走走？",
        "总有人在心疼和爱着你的",
        "今天的天气很好哦",
        "世间万物可爱",
        "我懂你的感觉",
        "我明白了",
        "我在楼下等你~",
        "摸摸你",
        "一切都会好起来的"
    };

    //需要有游戏难度这一项吗，可以改变speed或者弹幕数量之类的？

    // 游戏数值
    public const int Max_Time = 60;  // 每局游戏时间上限
    public const int Max_Pressure = 100; // 压力值上限           
    public const float MoveSpeed = 100.0f; // 小男孩移动速度    
    public const float JumpSpeed = 300.0f; // 小男孩跳跃速度
     
    public const float BarrageSpeed_Gather = 5f; // 弹幕初始化聚集速度  
    public const float BarrageSpeed_Move = 15f; // 弹幕移动速度    
    public const int BarrageDamage = 5; // 每个弹幕的伤害    
    public const int BarrageNum = 8; // 开始时产生坏弹幕数
    public const int BarrageNum_g = 2;// 开始时产生好弹幕数
    public const int WordNum = 80; // 字数限制
    public const int PillCover = 10; // 每次嗑药的压力减少
    public const int PillLife = 8; // 药片的生命周长
    public const int PillGap = 10; // 药片的出现间隔
    public const float DestroyRadius = 300.0f; // 嗑药后弹幕消失的范围
    public const float HUDSpeed = 20.0f;// 特效文本上升速度
    public const float ColorChangeSpeed = 0.07f;// 渐变速度
    public const float RainHeavySpeed = 0.05f;// 雨变大速度
    public const float RainStopSpeed = 0.3f;// 雨停速度

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarrageManager : GameSingleton<BarrageManager>
{
    [Header("弹幕Prefab")]
    public GameObject string_prefab;
    [Header("弹幕中的字Prefab")]
    public GameObject word_prefab;
    [Header("弹幕初始聚集位置")]
    public GameObject target;

    // 存储当前所有弹幕对象
    private List<GameObject> _barrages_list;
    // 随机序列
    private int[] _random_pos_sequence;
    private int[] _random_barrage_sequence;

    private int[] _random_barrage_sequence_g;
    // 索引
    private int _index = 0;
    // 记录使用了的弹幕数
    private int _count = 0;

    // 创建新弹幕
    void CreateBarrage(string str, bool good)
    {
        // 生成一条弹幕
        GameObject new_barrage = Instantiate(string_prefab);
        // 这条弹幕的中间位置
        //Debug.Log(_random_pos_sequence[_index]);
        //new_barrage.GetComponent<RectTransform>().localPosition = new Vector2(50 * Random.Range(4, 16), 50 * Random.Range(6, 14));
        int scale_width = Screen.width / 30;
        int scale_height = Screen.height / 20;
        new_barrage.GetComponent<RectTransform>().localPosition = new Vector2(/*40*/scale_width * Random.Range(6, 20), /*40*/scale_height * _random_pos_sequence[_index++]);
        new_barrage.transform.SetParent(GameObject.Find("Canvas").transform);

        string temp_str;
        int barrage_size = str.Length;
        GameObject[] all_words = new GameObject[barrage_size];
        for(int i = 0; i < barrage_size; i++)
        {
            temp_str = str.Substring(i, 1);
            all_words[i]= Instantiate(word_prefab);
            if (good)
            {
                all_words[i].GetComponent<Text>().color = new Color(57/255, 133/255, 203/255);
                all_words[i].tag = "Barrage_Good";
            }
            all_words[i].transform.SetParent(new_barrage.transform);
            all_words[i].GetComponent<Barrage>().born_time = GameManager.Instance.timer;
            // x坐标偏移量应该是一个字的宽度，这样才能排成一排
            all_words[i].GetComponent<RectTransform>().anchoredPosition = new Vector2((i-barrage_size/2) * 40, 0);
            all_words[i].GetComponent<Text>().text = temp_str;
        }

        _barrages_list.Add(new_barrage);
    }
    
    void Start()
    {
        _barrages_list = new List<GameObject>();
        _random_pos_sequence = Util.GetRandomSequence(10, Consts.BarrageNum + Consts.BarrageNum_g, 5, 1);// 从10行中选BarrageNum行作为其y坐标(范围：5~)
        _random_barrage_sequence = Util.GetRandomSequence(Consts.BarragesAll.Length, Consts.BarrageNum);// 从所有弹幕中选BarrageNum条显示
        _random_barrage_sequence_g= Util.GetRandomSequence(Consts.BarragesGoodAll.Length, Consts.BarrageNum_g);

        for (int i = 0; i < Consts.BarrageNum+Consts.BarrageNum_g; i++)
        {
            if (i < Consts.BarrageNum)
                CreateBarrage(Consts.BarragesAll[_random_barrage_sequence[i]], false);
            else
                CreateBarrage(Consts.BarragesGoodAll[_random_barrage_sequence_g[i - Consts.BarrageNum]], true);
        }
    }

    // 弹幕消失
    //public void DestroyByRadius(Vector3 player_pos, float radius)
    //{
    //    List<GameObject> tmp_list = new List<GameObject>(_barrages_list.ToArray());
    //    foreach(GameObject barrage in tmp_list)
    //    {
    //        //Debug.Log(player_pos.ToString() + " " + barrage.transform.position.ToString());
    //        Debug.Log((barrage.transform.position - player_pos).magnitude);
    //        // 玩家radius范围内的弹幕消失
    //        if ((barrage.transform.position - player_pos).magnitude <= radius)
    //        {
    //            _barrages_list.Remove(barrage);
    //            Destroy(barrage);
    //            // TODO 特效 & 音效
    //        }
    //    }
    //}

    // 字消失
    public void DestroyByRadius(Vector3 player_pos, float radius)
    {
        List<GameObject> tmp_list = new List<GameObject>(_barrages_list.ToArray());
        foreach (GameObject word in GameObject.FindGameObjectsWithTag(Consts.Barrage_Word))
        {
            //Debug.Log(player_pos.ToString() + " " + barrage.transform.position.ToString());
            //Debug.Log((word.transform.position - player_pos).magnitude);
            // 玩家radius范围内的字消失
            if ((word.transform.position - player_pos).magnitude <= radius)
            {
                Destroy(word);
                // TODO 特效 & 音效

            }
        }
    }

    void Update()
    {
        GameObject[] word_good= GameObject.FindGameObjectsWithTag(Consts.Barrage_Good);
        GameObject[] word_bad = GameObject.FindGameObjectsWithTag(Consts.Barrage_Word);
        GameObject[] word_arr = new GameObject[word_good.Length + word_bad.Length];
        for(int i = 0; i < word_arr.Length; i++)
        {
            if (i < word_good.Length) word_arr[i] = word_good[i];
            else word_arr[i] = word_bad[i - word_good.Length];
        }

        foreach (GameObject word in word_arr)
        {
            Barrage barrage = word.GetComponent<Barrage>();
            // 生成后停留3s再聚集
            int delta_time = GameManager.Instance.timer - barrage.born_time;
            //Debug.Log(delta_time);
            if (delta_time > 3)
            {
                //Debug.Log((word.transform.localPosition - target.transform.localPosition).magnitude);
                if (!barrage.init_flag && (word.transform.localPosition - target.transform.localPosition).magnitude > 0.01f){
                    word.transform.SetParent(GameObject.Find("Canvas").transform);// 重新设置parent，方便后续定位字
                    float step = Consts.BarrageSpeed_Gather * Time.deltaTime;
                    //Debug.Log("word:" + word.transform.localPosition.ToString());
                    //Debug.Log("target:" + target.transform.localPosition.ToString());
                    word.transform.localPosition = new Vector3(Mathf.Lerp(word.transform.localPosition.x, target.transform.localPosition.x, step), 
                        Mathf.Lerp(word.transform.localPosition.y, target.transform.localPosition.y, step), 
                        Mathf.Lerp(word.transform.localPosition.z, target.transform.localPosition.z, step));
                }else
                {
                    // 设置初始化flag
                    barrage.init_flag = true;

                    // destroy聚集后的字的父亲                        
                    foreach (GameObject parent in GameObject.FindGameObjectsWithTag(Consts.Barrage_String))
                    {
                        if (parent.transform.childCount == 0)
                        {
                            _barrages_list.Remove(parent);
                            Destroy(parent);
                        }                            
                    }

                    // 随机散开
                    word.transform.Translate(barrage.dir * Time.deltaTime * Consts.BarrageSpeed_Move, Space.Self);
                }
            }
        }
        // 小于一定数量则继续生成
        if (GameManager.Instance.gameState == GameState.Start && GameManager.Instance.timer>=4 && word_arr.Length < Consts.WordNum 
            /* && GameObject.FindGameObjectsWithTag(Consts.Barrage_String).Length < 3*/)// 可控制同时出现的弹幕数
        {
            //先弄个随机数判断生成的弹幕是好的还是坏的
            int random_num = Random.Range(1, 10);
            bool good = random_num > 7 ? true : false;
            if (_count == 0)
            {
                _random_pos_sequence = Util.GetRandomSequence(10, Consts.BarrageNum + Consts.BarrageNum_g, 5, 1);
                _random_barrage_sequence = Util.GetRandomSequence(Consts.BarragesAll.Length, Consts.BarrageNum + Consts.BarrageNum_g);
                _random_barrage_sequence_g = Util.GetRandomSequence(Consts.BarragesGoodAll.Length, Consts.BarrageNum + Consts.BarrageNum_g);
                _count = Consts.BarrageNum + Consts.BarrageNum_g;
                _index = 0;// 位置索引清零
            }
            if(good) CreateBarrage(Consts.BarragesGoodAll[_random_barrage_sequence_g[--_count]], true);
            else CreateBarrage(Consts.BarragesAll[_random_barrage_sequence[--_count]], false);
        }
    }
}
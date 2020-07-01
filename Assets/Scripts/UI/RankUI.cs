using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankUI : MonoBehaviour
{
    [Header("排行列表")]
    public RankItemUI[] rankItemUI;

    private Dictionary<string, int> scores_dict;

    void Start()
    {
        scores_dict = Util.ReadScores();

        // 分数排序
        List<KeyValuePair<string, int>> sort_list = new List<KeyValuePair<string, int>>(scores_dict);
        sort_list.Sort(delegate (KeyValuePair<string, int> s1, KeyValuePair<string, int> s2) {
            return s2.Value.CompareTo(s1.Value);
        });
        scores_dict.Clear();
        foreach (KeyValuePair<string, int> pair in sort_list)
        {
            scores_dict.Add(pair.Key, pair.Value);
        }
        // 更新UI
        int index = 0;
        foreach (KeyValuePair<string, int> name_score in scores_dict)
        {
            // 只显示前7个
            if (index >= rankItemUI.Length) break;
            rankItemUI[index++].UpdateUI(name_score.Key, name_score.Value);
        }
        // 防止排行榜数量不足
        for (int i = index; i < rankItemUI.Length; ++i)
        {
            rankItemUI[i].UpdateUI("?", 0);
        }
    }
}

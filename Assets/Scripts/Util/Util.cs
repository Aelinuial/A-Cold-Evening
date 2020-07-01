using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class Util
{
    static StreamWriter writer;
    static StreamReader reader;

    // 把所有的数据写入文本中
    public static void WriteIntoTxt(string info)
    {
        FileInfo file = new FileInfo(Application.dataPath + "/scores.txt");
        if (!file.Exists)
        {
            writer = file.CreateText();
        }
        else
        {
            writer = file.AppendText();
        }
        writer.WriteLine(info);
        writer.Flush();
        writer.Dispose();
        writer.Close();
    }

    // 读取分数 存储到列表中（同个玩家只取最高分）
    public static Dictionary<string, int> ReadScores()
    {
        Dictionary<string, int> scores = new Dictionary<string, int>();
        
        reader = new StreamReader(Application.dataPath + "/scores.txt", Encoding.UTF8);
        string text;
        while ((text = reader.ReadLine()) != null)
        {
            string name = text.Split(' ')[0];
            int score = int.Parse(text.Split(' ')[1]);
            //Debug.Log(score.ToString() + " " + name.ToString());
            if (scores.ContainsKey(name))
            {
                if (scores[name] < score) scores[name] = score;
            }
            else
            {
                scores.Add(name,score);
                //Debug.Log(scores[name]);
            }
               
        }
        reader.Dispose();
        reader.Close();
        return scores;
    }

    // 生成随机不重复数字的数组
    public static int[] GetRandomSequence(int total, int n, int start=0,int interval=0)
    {
        // 随机总数组
        int[] sequence = new int[total];
        // 取到的不重复数字的数组长度
        int[] output = new int[n];
        int index = 0;
        for (int i = start; i < total+start; i++)
        {
            sequence[index++] = i + interval;
        }
        int end = total - 1;
        for (int i = 0; i < n; i++)
        {
            // 随机一个数，每随机一次，随机区间-1
            int num = Random.Range(0, end + 1);
            output[i] = sequence[num];
            // 将区间最后一个数赋值到取到数上
            sequence[num] = sequence[end];
            end--;
        }
        return output;
    }
}


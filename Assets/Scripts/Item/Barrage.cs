using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrage : MonoBehaviour
{
    private int _born_time = 0;
    public int born_time
    {
        get
        {
            return _born_time;
        }
        set
        {
            _born_time = value;
        }
    }

    private Vector3 _dir;
    public Vector3 dir
    {
        get
        {
            return _dir;
        }
    }

    private bool _init_flag = false;
    public bool init_flag
    {
        get { return _init_flag; }
        set { _init_flag = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        _dir = new Vector3(Random.Range(-10, 10), Random.Range(-10, 0), 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

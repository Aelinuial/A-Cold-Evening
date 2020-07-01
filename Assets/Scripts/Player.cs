using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("药片特效")]
    public GameObject pillCircle;
    [Header("文本特效prefab")]
    public GameObject hud_text_prefab;

    // 小男孩当前压力值
    private int _pressure;
    public int pressure
    {
        get
        {
            return _pressure;
        }
    }

    // 药片数量
    private int _pillnum;
    public int pillnum
    {
        get
        {
            return _pillnum;
        }
    }

    private Rigidbody2D _rigidbody;
    private RectTransform _recttransform;
    private BoxCollider2D _collider;

    //private bool LeftButtonClick = false;
    //private bool RightButtonClick = false;
    //private bool JumpButtonClick = false;
    //private float UpForce = 0;

    void Awake()
    {
        _pressure = 50;// 初始压力值
        _pillnum = 0;

        _rigidbody = GetComponent<Rigidbody2D>();
        _recttransform = GetComponent<RectTransform>();
        _collider = GetComponent<BoxCollider2D>();
        pillCircle.SetActive(false);
        pillCircle.GetComponent<RectTransform>().sizeDelta =
            new Vector2(2 * Consts.DestroyRadius, 2 * Consts.DestroyRadius);
    }

    void Start()
    {
        GameManager.Instance.gameState = GameState.Start;

        // 动态初始化角色碰撞盒（自适应分辨率）
        _collider.offset = Vector2.zero;
        _collider.size = new Vector2((_recttransform.rect.xMax - _recttransform.rect.xMin)/2, _recttransform.rect.yMax - _recttransform.rect.yMin - 6.0f);
    }

    void Update()
    {
        if (_rigidbody == null || GameManager.Instance.gameState != GameState.Start) return;

        float h = Input.GetAxis("Horizontal");

        if (!h.Equals(0))
        {
            _rigidbody.velocity = new Vector2(h * Consts.MoveSpeed, _rigidbody.velocity.y);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (_rigidbody.velocity.y.Equals(0) || transform.localPosition.y < -175) // 小于某个高度能继续跳
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, Consts.JumpSpeed);
        }

        // 空格吃药
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_pillnum > 0)
            {
                pillCircle.GetComponent<RectTransform>().localPosition = this.GetComponent<RectTransform>().localPosition;
                pillCircle.SetActive(true);
                _pillnum -= 1;
                _pressure -= Consts.PillCover;
                if (_pressure < 0) _pressure = 0;
                // 把周围一圈的弹幕Destroy掉
                BarrageManager.Instance.DestroyByRadius(transform.position, Consts.DestroyRadius);

                // 压力减少特效、音效
                GameObject hud_text = Instantiate(hud_text_prefab);
                hud_text.GetComponent<HudText>().Init(transform.localPosition, "压力-" + Consts.PillCover.ToString(), Color.green);
                AudioMgr.Instance.PlayEffect(Consts.Audio_Eat_Medicine);
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            pillCircle.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == Consts.Barrage_Word && GameManager.Instance.gameState == GameState.Start)
        {
            // 碰撞后销毁这个字
            Destroy(other.gameObject);
            // 压力值增加
            _pressure += Consts.BarrageDamage;

            // 压力增加特效、音效
            GameObject hud_text = Instantiate(hud_text_prefab);
            hud_text.GetComponent<HudText>().Init(transform.localPosition,"压力+" + Consts.BarrageDamage.ToString(),Color.red);
            AudioMgr.Instance.PlayEffect(Consts.Audio_Hurt);         
        }
        //Do sth
        else if (other.tag == Consts.Barrage_Good && GameManager.Instance.gameState == GameState.Start)
        {
            // 碰撞后销毁这个字
            Destroy(other.gameObject);
            // 压力值减少
            _pressure -= Consts.BarrageDamage;

            // 压力减少特效、音效
            GameObject hud_text = Instantiate(hud_text_prefab);
            hud_text.GetComponent<HudText>().Init(transform.localPosition, "压力-" + Consts.BarrageDamage.ToString(), Color.red);
            AudioMgr.Instance.PlayEffect(Consts.Audio_Hurt);
        }
    }

    // 这里不能写成trigger，否则药片会穿透地板
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("碰撞了");
        if (collision.gameObject.tag == Consts.Pill && GameManager.Instance.gameState==GameState.Start)
        {
            Destroy(collision.gameObject);
            //要有药片才能放技能
            _pillnum += 1;

            // 药片数增加特效、音效
            GameObject hud_text = Instantiate(hud_text_prefab);
            hud_text.GetComponent<HudText>().Init(transform.localPosition, "药片+1", Color.green);
            AudioMgr.Instance.PlayEffect(Consts.Audio_Get_Medicine);
        }
    }
}

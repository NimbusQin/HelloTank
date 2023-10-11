using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 3;

    public Sprite[] tankSprite;  // up, right, down, left
    public GameObject bullectPrefab;
    public GameObject explosionPrefab;
    public GameObject defandedPrefab;

    private SpriteRenderer sr;
    private Vector3 bullectEulerAngles;
    private bool isDefended = true;

    // 子弹cd
    private float bullectTimeVal = 0.4f;
    // 无敌cd
    private float defendedTimeVal = 3;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDefended)
        {
            defandedPrefab.SetActive(true);
            defendedTimeVal -= Time.deltaTime;
            if (defendedTimeVal <= 0)
            {
                isDefended = false;
                defandedPrefab.SetActive(false);
            }
        }

        if (bullectTimeVal >= 0.4f)
        {
            Attack();
        }
        else
        {
            bullectTimeVal += Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (PlayerManage.Instance.isDefeat)
        {
            return;
        }

        Move();
    }

    private void Move()
    {
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

        transform.Translate(Vector3.up * v * moveSpeed * Time.fixedDeltaTime, Space.World);
        if (v < 0)
        {
            // picture change
            sr.sprite = tankSprite[2];
            bullectEulerAngles = new Vector3(0, 0, -180);
        }
        else if (v > 0)
        {
            sr.sprite = tankSprite[0];
            bullectEulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            transform.Translate(Vector3.right * h * moveSpeed * Time.fixedDeltaTime, Space.World);
            if (h < 0)
            {
                sr.sprite = tankSprite[3];
                bullectEulerAngles = new Vector3(0, 0, 90);
            }
            else if (h > 0)
            {
                sr.sprite = tankSprite[1];
                bullectEulerAngles = new Vector3(0, 0, -90);
            }
        }
    }

    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 子弹产生的角度为，当前坦克的角度+子弹应该旋转的角度
            Instantiate(bullectPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + bullectEulerAngles));
            bullectTimeVal = 0f;
        }
    }

    private void Die()
    {
        if (isDefended)
        {
            return;
        }

        PlayerManage.Instance.isDead = true;

        // 特效
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        // 死亡
        Destroy(gameObject);
    }
}

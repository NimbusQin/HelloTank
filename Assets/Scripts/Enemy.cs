using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 3;

    public Sprite[] tankSprite;  // up, right, down, left
    public GameObject bullectPrefab;
    public GameObject explosionPrefab;

    private SpriteRenderer sr;
    private Vector3 bullectEulerAngles;
    private float v = -1;
    private float h;

    // �ӵ�cd
    private float bullectTimeVal;
    // ת��cd
    private float rotationTimeVal;

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
        if (bullectTimeVal >= 3f)
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
        Move();
    }

    private void Move()
    {
        if (rotationTimeVal >= 4f)
        {
            int num = Random.Range(0, 8);

            if (num > 5)  // down
            {
                v = -1f;
                h = 0f;
            }
            else if (num == 0)
            {
                v = 1f;
                h = 0f;
            }
            else if (num > 0 && num <= 2)
            {
                v = 0f;
                h = -1f;
            }
            else if (num > 2 && num <= 4)
            {
                v = 0f;
                h = -1f;
            }

            rotationTimeVal = 0;
        }
        else
        {
            rotationTimeVal += Time.fixedDeltaTime;
        }

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
 
        // �ӵ������ĽǶ�Ϊ����ǰ̹�˵ĽǶ�+�ӵ�Ӧ����ת�ĽǶ�
        Instantiate(bullectPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + bullectEulerAngles));
        bullectTimeVal = 0f;
    }

    private void Die()
    {
        PlayerManage.Instance.playerScore++;
        // ��Ч
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        // ����
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            rotationTimeVal = 4f;
        }
    }
}

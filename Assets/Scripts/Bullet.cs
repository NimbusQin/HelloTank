using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float moveSpeed = 10;
    public bool fromPlayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.up * moveSpeed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Tank":
                if (!fromPlayer)
                {
                    collision.SendMessage("Die");
                    Destroy(gameObject);
                }
                break;
            case "Heart":
               // if (!fromPlayer)
               // {
                    collision.SendMessage("Die");
                // }
                Destroy(gameObject);
                break;
            case "Wall":
                Destroy(collision.gameObject);
                Destroy(gameObject);
                break;
            case "Barriar":
                collision.SendMessage("PlayAudio");
                Destroy(gameObject);
                break;
            case "Enemy":
                if (fromPlayer)
                {
                    collision.SendMessage("Die");
                    Destroy(gameObject);
                }
                break;
            default:
                break;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    public Sprite BrokenSprite;
    public GameObject explodePrefab;
    public AudioClip dieAudio;

    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    public void Die()
    {
        sr.sprite = BrokenSprite;
        Instantiate(explodePrefab, transform.position, transform.rotation);
        PlayerManage.Instance.isDefeat = true;
        AudioSource.PlayClipAtPoint(dieAudio, transform.position);
    }
}

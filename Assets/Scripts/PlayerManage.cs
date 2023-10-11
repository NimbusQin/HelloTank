using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerManage : MonoBehaviour
{
    public int lifeValue = 3;
    public int playerScore = 0;
    public bool isDead;
    public bool isDefeat = false;

    public GameObject born;
    public Text playerScoreText;
    public Text playerLifeText;
    public GameObject GameOverUI;

    private static PlayerManage instance;

    public static PlayerManage Instance { get => instance; set => instance = value; }

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDefeat)
        {
            GameOverUI.SetActive(true);
            return;
        }

        if (isDead)
        {
            Recover();
        }
        playerScoreText.text = playerScore.ToString();
        playerLifeText.text = lifeValue.ToString();
    }

    private void Recover()
    {
        if (lifeValue <= 0)
        {
            isDefeat = true;
            Invoke("GoTile", 3);
            return;
        }

        GameObject go = Instantiate(born, new Vector3(-2, -8, 0), Quaternion.identity);
        go.GetComponent<born>().isPlayer = true;
        isDead = false;
        lifeValue--;
    }

    private void GoTile()
    {
        SceneManager.LoadScene(0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class born : MonoBehaviour
{
    public bool isPlayer;

    public GameObject playerPrefab;

    public GameObject[] emeyPrefabList;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("BornTank", 1f);
        Destroy(gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void BornTank()
    {
        if (isPlayer)
        {
            Instantiate(playerPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            int num = Random.Range(0, 2);
            Instantiate(emeyPrefabList[num], transform.position, Quaternion.identity);
        }
    }
}

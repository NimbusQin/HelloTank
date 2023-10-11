using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreation : MonoBehaviour
{
    // 0 基地， 1 墙， 2 障碍， 3 出生效果， 4 河流， 5 草， 6 空气墙
    public GameObject[] item;

    // 已经被占据的地方
    private List<Vector3> itemPositonList = new List<Vector3>();

    private void Awake()
    {
        InitMap();
    }

    private void InitMap()
    {
        // 基地
        CreateItem(item[0], new Vector3(0, -8, 0), Quaternion.identity);
        CreateItem(item[1], new Vector3(-1, -8, 0), Quaternion.identity);
        CreateItem(item[1], new Vector3(1, -8, 0), Quaternion.identity);
        for (int i = -1; i < 2; i++)
        {
            CreateItem(item[1], new Vector3(i, -7, 0), Quaternion.identity);
        }

        // 空气墙
        for (int i = -11; i < 12; i++)
        {
            CreateItem(item[6], new Vector3(i, 9, 0), Quaternion.identity);
        }
        for (int i = -11; i < 12; i++)
        {
            CreateItem(item[6], new Vector3(i, -9, 0), Quaternion.identity);
        }
        for (int i = -8; i < 9; i++)
        {
            CreateItem(item[6], new Vector3(-11, i, 0), Quaternion.identity);
        }
        for (int i = -8; i < 9; i++)
        {
            CreateItem(item[6], new Vector3(11, i, 0), Quaternion.identity);
        }

        // 坦克出生
        GameObject playerBorn = Instantiate(item[3], new Vector3(-2, -8, 0), Quaternion.identity);
        playerBorn.GetComponent<born>().isPlayer = true;

        CreateItem(item[3], new Vector3(-10, 8, 0), Quaternion.identity);
        CreateItem(item[3], new Vector3(0, 8, 0), Quaternion.identity);
        CreateItem(item[3], new Vector3(10, 8, 0), Quaternion.identity);

        InvokeRepeating("CreateEnemy", 4, 5);

        // 随机地形
        for (int i = 0; i < 30; i++)
        {
            CreateItem(item[1], CreateRandomPosition(), Quaternion.identity);
            CreateItem(item[2], CreateRandomPosition(), Quaternion.identity);
            CreateItem(item[4], CreateRandomPosition(), Quaternion.identity);
            CreateItem(item[5], CreateRandomPosition(), Quaternion.identity);
        }
    }

    private void CreateItem(GameObject createGameObject, Vector3 createPositon, Quaternion createRotation)
    {
        GameObject itemGo = Instantiate(createGameObject, createPositon, createRotation);
        itemGo.transform.SetParent(gameObject.transform);
        itemPositonList.Add(createPositon);
    }

    private Vector3 CreateRandomPosition()
    {
        while (true)
        {
            Vector3 createPosition = new Vector3(Random.Range(-9, 10), Random.Range(-7, 8), 0);
            if (!ExistPosition(createPosition))
            {
                return createPosition;
            }
        }
    }

    private bool ExistPosition(Vector3 position)
    {
        for (int i = 0; i < itemPositonList.Count; i++)
        {
            if (position == itemPositonList[i])
            {
                return true;
            }
        }

        return false;
    }

    private void CreateEnemy()
    {
        int num = Random.Range(0, 3);
        Vector3 EnemyPos = new Vector3();
        if (num == 0)
        {
            EnemyPos = new Vector3(-10, 8, 0);
        }
        else if (num == 1)
        {
            EnemyPos = new Vector3(0, 8, 0);
        }
        else
        {
            EnemyPos = new Vector3(10, 8, 0);
        }

        CreateItem(item[3], EnemyPos, Quaternion.identity);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TSpawn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Transform startPositions = GameObject.Find("StartPositions").transform;
        Transform firstPlayerStartPosition = startPositions.GetChild(0);
        Transform firstDragonStartPosition = startPositions.GetChild(1);
        Transform secondPlayerStartPosition = startPositions.GetChild(2);
        Transform secondDragonStartPosition = startPositions.GetChild(3);

            // PoolManager.Instance.Pop("Player", firstPlayerStartPosition.position, firstPlayerStartPosition.rotation);
            // PoolManager.Instance.Pop("BlueThinDragon", firstDragonStartPosition.position, firstDragonStartPosition.rotation);
            // PoolManager.Instance.Pop("EnemyPlayer", secondPlayerStartPosition.position, secondPlayerStartPosition.rotation);
            // PoolManager.Instance.Pop("EnemyDragon", secondDragonStartPosition.position, secondDragonStartPosition.rotation);

            PoolManager.Instance.Pop("Player", secondPlayerStartPosition.position, secondPlayerStartPosition.rotation);
            PoolManager.Instance.Pop("BlueThinDragon", secondDragonStartPosition.position, secondDragonStartPosition.rotation);
            PoolManager.Instance.Pop("EnemyPlayer", firstPlayerStartPosition.position, firstPlayerStartPosition.rotation);
            PoolManager.Instance.Pop("EnemyDragon", firstDragonStartPosition.position, firstDragonStartPosition.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

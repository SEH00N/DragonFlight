using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance {
        get {
            if(instance == null)
                instance = FindObjectOfType<GameManager>();

            return instance;
        }
    }

    [SerializeField] bool cursorActive = false;
    public bool CursorActive {
        get => cursorActive;
        set {
            cursorActive = value;
            Cursor.visible = cursorActive;
            Cursor.lockState = cursorActive ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }

    private Transform startPositions;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            Debug.Log("as");
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        Cursor.visible = cursorActive;
        Cursor.lockState = cursorActive ? CursorLockMode.None : CursorLockMode.Locked;
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.LeftAlt))
            if(Input.GetKey(KeyCode.LeftShift))
                if(Input.GetKeyDown(KeyCode.Comma))
                    CursorActive = !CursorActive;
    }

    public void SetGame(int order)
    {
        startPositions = GameObject.Find("StartPositions").transform;
        Transform firstPlayerStartPosition = startPositions.GetChild(0);
        Transform firstDragonStartPosition = startPositions.GetChild(1);
        Transform secondPlayerStartPosition = startPositions.GetChild(2);
        Transform secondDragonStartPosition = startPositions.GetChild(3);

        if(order == 0)
        {
            PoolManager.Instance.Pop("Player", firstPlayerStartPosition.position, firstPlayerStartPosition.rotation);
            PoolManager.Instance.Pop("BlueThinDragon", firstDragonStartPosition.position, firstDragonStartPosition.rotation);
            PoolManager.Instance.Pop("EnemyPlayer", secondPlayerStartPosition.position, secondPlayerStartPosition.rotation);
            PoolManager.Instance.Pop("EnemyDragon", secondDragonStartPosition.position, secondDragonStartPosition.rotation);
        }
        else 
        {
            PoolManager.Instance.Pop("Player", secondPlayerStartPosition.position, secondPlayerStartPosition.rotation);
            PoolManager.Instance.Pop("BlueThinDragon", secondDragonStartPosition.position, secondDragonStartPosition.rotation);
            PoolManager.Instance.Pop("EnemyPlayer", firstPlayerStartPosition.position, firstPlayerStartPosition.rotation);
            PoolManager.Instance.Pop("EnemyDragon", firstDragonStartPosition.position, firstDragonStartPosition.rotation);
        }
    }
}

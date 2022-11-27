using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private static SceneLoader instance = null;
    public static SceneLoader Instance {
        get {
            if(instance == null)
                instance = FindObjectOfType<SceneLoader>();

            return instance;
        }
    }

    public Scene CurrentScene;
    private GameObject loadingPanel = null;
    private GameObject reconnectPanel = null;

    private void Awake()
    {
        if(instance != null)
            return;

        CurrentScene = SceneManager.GetActiveScene();
        loadingPanel = DEFINE.StaticCanvas.Find("LoadImage").gameObject;
        reconnectPanel = DEFINE.StaticCanvas.Find("ReconnectPanel").gameObject;
    }

    private void Start()
    {
        reconnectPanel.SetActive(true);
    }

    public void Reconnect(bool toggle) => reconnectPanel.SetActive(toggle);

    public void RemoveDontDestroyOnLoad(GameObject target) => SceneManager.MoveGameObjectToScene(target, CurrentScene);

    public void LoadAsync(string name, Action callback = null) => StartCoroutine(LoadAsyncCoroutine(name, callback));

    private IEnumerator LoadAsyncCoroutine(string name, Action callback)
    {
        AudioManager.Instance.PauseBGM();
        AsyncOperation oper = SceneManager.LoadSceneAsync(name);
        loadingPanel.SetActive(true);

        while(!oper.isDone)
            yield return null;

        yield return null;
        loadingPanel.SetActive(false);
        CurrentScene = SceneManager.GetActiveScene();
        callback?.Invoke();
    }
}

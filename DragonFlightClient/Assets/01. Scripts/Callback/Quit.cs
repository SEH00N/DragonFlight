using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit : MonoBehaviour
{
    public void DoQuit(float delay) => StartCoroutine(QuitCoroutine(delay));

    private IEnumerator QuitCoroutine(float delay) 
    {
        yield return new WaitForSeconds(delay);
        Application.Quit();
    }
}

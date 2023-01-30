using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    public GameObject loadingObj;
    public Slider slider;

    AsyncOperation async;

    public void LoadScreen(string scene) 
    {
        StartCoroutine(LoadingScreen(scene));
    }

    IEnumerator LoadingScreen(string scene) 
    {
        loadingObj.SetActive(true);
        async = SceneManager.LoadSceneAsync(scene);
        async.allowSceneActivation = false;
        while (async.isDone == false)
        {
            slider.value = async.progress;
            if (async.progress == 0.9f) 
            {
                slider.value = 1f;
                async.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}

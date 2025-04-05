using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;

    [Header("Loading UI")]
    public GameObject loadingScreen; // 加载动画的UI
    //public Slider progressBar; // 进度条

    public AsyncOperation asyncOperation;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;

    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadAsynchronously(sceneName));
    }

    private IEnumerator LoadAsynchronously(string sceneName)
    {
        loadingScreen.SetActive(true); // 显示加载界面
        loadingScreen.GetComponent<SceneCover>().Active();

        asyncOperation = SceneManager.LoadSceneAsync(sceneName);

        // 确保场景加载完成后不会自动激活
        asyncOperation.allowSceneActivation = false;

        
        yield return null;
        
    }
}
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class TaskWindow : SingletonMonoBehavior<TaskWindow>
{

    [HideInInspector] public CanvasGroup canvasGroup;
    private Coroutine fadeCoroutine;

    private Transform taskLayout;
    public List<TaskView> taskViewList = new();
    

    protected override void Awake()
    {
        DontDestroyOnLoad(gameObject);

        taskLayout = gameObject.GetChildControl<Transform>("TaskLayout");




        
    }

    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        foreach (var kv in RefTask.cacheMap)
        {
            GameObject TaskPrefab = ObjectPool.Instance.Get("Task", taskLayout, false);
            TaskView taskView = new TaskView(TaskPrefab);
            TaskInfo taskInfo = new TaskInfo(kv.Key);
            taskView.SetData(taskInfo);
            taskInfo.Init();
            taskViewList.Add(taskView);
        }
        //InGameTaskView.Instance.taskViewList = taskViewList;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Refresh()
    {
        foreach(var taskView in taskViewList)
        {
            taskView.Refresh();
        }
    }

    public void ShowWindow()
    {
        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeCanvasGroup(canvasGroup, canvasGroup.alpha, 1f, 0.25f));
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        //Time.timeScale = 0.0f;
        PlayerManager.instance.player.isIdle = true;
        Refresh();
    }

    public void HideWindow()
    {
        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeCanvasGroup(canvasGroup, canvasGroup.alpha, 0f, 0.25f));
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        //Time.timeScale = 1.0f;
        PlayerManager.instance.player.isIdle = false;
    }

    private IEnumerator FadeCanvasGroup(CanvasGroup group, float from, float to, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            group.alpha = Mathf.Lerp(from, to, elapsed / duration);
            yield return null;
        }
        group.alpha = to;
    }

    public void ChangeWindowState()
    {
        if (canvasGroup.alpha > 0f)
        {
            HideWindow();
        }
        else
        {
            ShowWindow();
        }
    }
}

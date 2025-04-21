using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ShopWindow : SingletonMonoBehavior<ShopWindow>
{

    [HideInInspector] public CanvasGroup canvasGroup;
    private Coroutine fadeCoroutine;
    //private Image imgCoin;
    //private TextMeshProUGUI textCoin;

    protected override void Awake()
    {
        DontDestroyOnLoad(gameObject);

        //imgCoin = gameObject.GetChildControl<Image>("imgCoin");
        //textCoin = gameObject.GetChildControl<TextMeshProUGUI>("textCoin");
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
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ShowWindow()
    {
        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeCanvasGroup(canvasGroup, canvasGroup.alpha, 1f, 0.25f));
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        //Time.timeScale = 0.0f;
        PlayerManager.instance.player.isIdle = true;
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

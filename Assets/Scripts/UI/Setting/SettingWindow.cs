using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingWindow : SingletonMonoBehavior<SettingWindow>
{
    [HideInInspector] public Slider Volume;
    [HideInInspector] public Slider Music;
    [HideInInspector] public Slider Effects;
    [HideInInspector] public Button BtnPlay;
    [HideInInspector] public Button BtnReplay;
    [HideInInspector] public Button BtnHome;
    [HideInInspector] public CanvasGroup canvasGroup;

    private Coroutine fadeCoroutine;

    protected override void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void Start()
    {
        Volume = gameObject.GetChildControl<Slider>("Voice/Volume");
        Music = gameObject.GetChildControl<Slider>("Voice/Music");
        Effects = gameObject.GetChildControl<Slider>("Voice/Effects");
        BtnPlay = gameObject.GetChildControl<Button>("Btn/Play");
        BtnReplay = gameObject.GetChildControl<Button>("Btn/Replay");
        BtnHome = gameObject.GetChildControl<Button>("Btn/Home");

        Volume.onValueChanged.AddListener(VolumeOnValueChanged);
        Music.onValueChanged.AddListener(MusicOnValueChanged);
        Effects.onValueChanged.AddListener(EffectsOnValueChanged);
        BtnPlay.onClick.AddListener(BtnPlayClick);
        BtnReplay.onClick.AddListener(BtnReplayClick);
        BtnHome.onClick.AddListener(BtnHomeClick);

        canvasGroup = gameObject.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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

    public void ShowWindow()
    {
        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeCanvasGroup(canvasGroup, canvasGroup.alpha, 1f, 0.25f));
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        Time.timeScale = 0.0f;
    }

    public void HideWindow()
    {
        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeCanvasGroup(canvasGroup, canvasGroup.alpha, 0f, 0.25f));
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        Time.timeScale = 1.0f;
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

    public void BtnPlayClick()
    {
        HideWindow();
    }

    public void BtnReplayClick()
    {
        Door.instance.sceneNumber = 0;
        SceneLoader.instance.LoadScene("CityScene");
        HideWindow();
    }

    public void BtnHomeClick()
    {
        HideWindow();
        Destroy(gameObject);
        Door.instance.sceneNumber = 0;
        SceneLoader.instance.LoadScene("MenuScene");
    }

    public void VolumeOnValueChanged(float value)
    {
        GameObject.Find("Background").GetComponent<AudioSource>().volume = value * Music.value;
        PlayerManager.instance.player.gameObject.GetChildControl<AudioSource>("Anim").volume = value * 0.2f * Effects.value;
        SkillManager.instance.gameObject.GetComponent<AudioSource>().volume = value * Effects.value;
    }

    public void MusicOnValueChanged(float value)
    {
        GameObject.Find("Background").GetComponent<AudioSource>().volume = value;
    }

    public void EffectsOnValueChanged(float value)
    {
        PlayerManager.instance.player.gameObject.GetChildControl<AudioSource>("Anim").volume = value * 0.2f;
        SkillManager.instance.gameObject.GetComponent<AudioSource>().volume = value;
    }

    public float GetEffectsAudioValue()
    {
        return Volume.value * Effects.value;
    }
}

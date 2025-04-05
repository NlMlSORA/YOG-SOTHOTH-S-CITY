using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIEffectMgr : MonoBehaviour
{
    public static UIEffectMgr Instance;

    [SerializeField] private Image healOverlay;
    [SerializeField] float a = .3f;

    private void Awake()
    {
        Instance = this;
        if (healOverlay != null)
            healOverlay.gameObject.SetActive(false);
    }

    public void ShowHealOverlayUI(float duration = 0.5f)
    {
        StartCoroutine(PlayHealOverlay(duration));
    }

    private IEnumerator PlayHealOverlay(float duration)
    {
        if (healOverlay == null)
            yield break;

        healOverlay.gameObject.SetActive(true);

        Color color = healOverlay.color;
        float t = 0f;

        // 淡入
        while (t < duration)
        {
            t += Time.unscaledDeltaTime;
            float alpha = Mathf.Lerp(0, a, t / duration); // 0.5 是最大透明度
            healOverlay.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        yield return new WaitForSecondsRealtime(0.2f);

        t = 0f;
        // 淡出
        while (t < duration)
        {
            t += Time.unscaledDeltaTime;
            float alpha = Mathf.Lerp(0.5f, 0f, t / duration);
            healOverlay.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        healOverlay.gameObject.SetActive(false);
    }
}

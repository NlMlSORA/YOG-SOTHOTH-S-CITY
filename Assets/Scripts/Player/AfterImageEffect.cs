using UnityEngine;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

public class AfterImageEffect : MonoBehaviour
{
    public static AfterImageEffect instance;
    private bool flag = false;

    public float duration; // 拖影持续时间
    public float interval; // 创建拖影的时间间隔
    public float alpha;
    private float timer = 0f;
    private Transform playerTransform;
    private SpriteRenderer playerSpriteRenderer;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }

    void Start()
    {
        playerTransform = transform; // 假设该脚本直接挂载在玩家对象上
        playerSpriteRenderer = playerTransform.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (flag)
        {
            timer += Time.deltaTime;
            if (timer >= interval)
            {
                CreateAfterImage();
                timer = 0f;
            }
        }
    }

    public void StartAfterImageEffect()
    {
        flag = true;
        timer = 0f;
    }

    public void EndAfterImageEffect()
    {
        flag = false;
    }

    void CreateAfterImage()
    {
        GameObject afterImageObj = new GameObject("AfterImage");
        afterImageObj.transform.position = playerTransform.position;
        afterImageObj.transform.rotation = playerTransform.rotation;
        afterImageObj.transform.localScale = playerTransform.localScale;

        AfterImageRenderer afterImageRendererScript = afterImageObj.AddComponent<AfterImageRenderer>();
        
        afterImageRendererScript.sprite = playerSpriteRenderer.sprite;
        afterImageRendererScript.color = new Color(1f, 1f, 1f, alpha); // 初始透明度
        afterImageRendererScript.duration = duration;

        // 设置排序图层与玩家相同
        afterImageRendererScript.sortingLayerName = playerSpriteRenderer.sortingLayerName;
        afterImageRendererScript.sortingOrder = playerSpriteRenderer.sortingOrder;

        // 复制Flip状态
        afterImageRendererScript.flipX = playerSpriteRenderer.flipX;
        afterImageRendererScript.flipY = playerSpriteRenderer.flipY;

        // 自动销毁
        Destroy(afterImageObj, duration);
    }
}

public class AfterImageRenderer : MonoBehaviour
{
    public Sprite sprite;
    public Color color;
    public float duration;
    private SpriteRenderer spriteRenderer;
    private float creationTime;

    void Awake()
    {
        spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        creationTime = Time.time;
    }

    void Start()
    {
        spriteRenderer.sprite = sprite;
        spriteRenderer.color = color;
        spriteRenderer.sortingLayerName = sortingLayerName;
        spriteRenderer.sortingOrder = sortingOrder;
        spriteRenderer.flipX = flipX;
        spriteRenderer.flipY = flipY;
    }

    void Update()
    {
        // 计算经过的时间比例
        float timeElapsed = Time.time - creationTime;
        float alpha = Mathf.Clamp01((1f - (timeElapsed / duration)) * AfterImageEffect.instance.alpha);
        spriteRenderer.color = new Color(color.r, color.g, color.b, alpha);

        // 如果时间超过持续时间，则销毁对象
        if (timeElapsed >= duration)
        {
            Destroy(gameObject);
        }
    }

    private string _sortingLayerName;
    public string sortingLayerName
    {
        get => _sortingLayerName;
        set
        {
            _sortingLayerName = value;
            if (spriteRenderer != null) // 确保spriteRenderer已经初始化
            {
                spriteRenderer.sortingLayerName = value;
            }
        }
    }

    private int _sortingOrder;
    public int sortingOrder
    {
        get => _sortingOrder;
        set
        {
            _sortingOrder = value;
            if (spriteRenderer != null) // 确保spriteRenderer已经初始化
            {
                spriteRenderer.sortingOrder = value;
            }
        }
    }

    private bool _flipX;
    public bool flipX
    {
        get => _flipX;
        set
        {
            _flipX = value;
            if (spriteRenderer != null) // 确保spriteRenderer已经初始化
            {
                spriteRenderer.flipX = value;
            }
        }
    }

    private bool _flipY;
    public bool flipY
    {
        get => _flipY;
        set
        {
            _flipY = value;
            if (spriteRenderer != null) // 确保spriteRenderer已经初始化
            {
                spriteRenderer.flipY = value;
            }
        }
    }
}
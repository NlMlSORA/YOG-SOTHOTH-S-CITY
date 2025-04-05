using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPManager : MonoBehaviour
{
    public static HPManager instance;

    [SerializeField] GameObject hpGameobject;
    [SerializeField] float hpDistance;
    private int hpNum = 0;
    private int hpStartedNum = 0;
    [HideInInspector] public bool hpCreated = false;

    private List<GameObject> hpGameobjects = new List<GameObject>();

    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;

    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(PlayerManager.instance.player.stats.currentHealth);
    }

    public void StartCreateHP()
    {
        InvokeRepeating("CreateHP", 0, .3f);
    }

    private void CreateHP()
    {
        Vector2 hpTransform = new Vector2(transform.position.x + hpNum * hpDistance, transform.position.y);
        GameObject newHpGameobject = Instantiate(hpGameobject, hpTransform, Quaternion.identity, transform);
        hpGameobjects.Add(newHpGameobject);
        hpNum++;
        if(hpNum == 6)
        {
            CancelInvoke("CreateHP");
            InvokeRepeating("BlingBling", 5, 5);
        }
    }

    public void IsCreatedHP()
    {
        hpStartedNum++;
        if (hpStartedNum == 6)
        {
            foreach (GameObject hp in hpGameobjects)
            {
                hp.GetComponent<HP>().HPStart();
            }
        }
    }

    public void ReduceHP()
    {
        //Debug.Log("reduce" + PlayerManager.instance.player.stats.currentHealth);
        hpGameobjects[PlayerManager.instance.player.stats.currentHealth - 1].GetComponent<HP>().ReduceHP();
    }

    public void AddHP()
    {
        hpGameobjects[PlayerManager.instance.player.stats.currentHealth].GetComponent<HP>().AddHP();
    }
    public void InterruptHeal()
    {
        hpGameobjects[PlayerManager.instance.player.stats.currentHealth].GetComponent<HP>().InterruptHeal();
    }

    private void BlingBling()
    {
        for (int i = 0; i < PlayerManager.instance.player.stats.currentHealth; i++)
        {
            hpGameobjects[i].GetComponent<HP>().BlingBling();
        }
    }
}

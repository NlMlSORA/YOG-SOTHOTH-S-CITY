using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{

    private RectTransform myTransform;
    private Entity entity;
    private Slider slider;
    private CharacterStats stats;

    // Start is called before the first frame update
    void Start()
    {
        myTransform = GetComponent<RectTransform>();
        entity = GetComponentInParent<Entity>();
        slider = GetComponentInChildren<Slider>();
        stats = GetComponentInParent<CharacterStats>();

        entity.OnFlipped += FlipHealthBar;
        stats.OnHealthChanged += UpdateHealthBar;

        UpdateHealthBar();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateHealthBar()
    {
        slider.maxValue = stats.maxHealth.GetValue();
        slider.value = stats.currentHealth;


        if (stats.currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }



    private void FlipHealthBar() => myTransform.Rotate(0, 180, 0);

    private void OnDisable()
    {
        entity.OnFlipped -= FlipHealthBar;
        stats.OnHealthChanged -= UpdateHealthBar;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public static Action<Enemy> OnEnemyKilled;
    public static Action<Enemy> OnEnemyHit;

    [SerializeField] private GameObject healthBarPrefab;
    [SerializeField] private Transform barPosition;

    [SerializeField] private float initialHealth = 100f;
    [SerializeField] private float maxHealth = 100f;

    public float CurrentHealth { get; set; }

    private Image healthBar;
    private Enemy enemy;

    private void Start()
    {
        CreateHealthBar();
        CurrentHealth = initialHealth;

        enemy = GetComponent<Enemy>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            DealDamage(10f);
        }

        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, CurrentHealth / maxHealth, Time.deltaTime * 10f);
    }

    private void CreateHealthBar()
    {
        GameObject newBar = Instantiate(healthBarPrefab, barPosition.position, Quaternion.identity);
        newBar.transform.SetParent(transform);

        EnemyHealthContainer container = newBar.GetComponent<EnemyHealthContainer>();
        healthBar = container.FillAmountImage;
    }

    public void DealDamage(float damageRecieved)
    {
        CurrentHealth -= damageRecieved;
        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            Die();
        }
        else
        {
            OnEnemyHit?.Invoke(enemy);
        }
    }

    public void ResetHealth()
    {
        CurrentHealth = initialHealth;
        healthBar.fillAmount = 1f;
    }

    private void Die()
    {
        OnEnemyKilled?.Invoke(enemy);
    }
}

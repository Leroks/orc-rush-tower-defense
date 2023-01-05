using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int lives = 1;

    public int TotalLives { get; set; }

    private void Start()
    {
        TotalLives = lives;
    }

    private void ReduceLives()
    {
        TotalLives--;
        if (TotalLives <= 0)
        {
            // Game Over
        }
    }


    private void OnEnable()
    {
        Enemy.OnEnemyReachedEnd += ReduceLives;
    }

    private void OnDisable()
    {
        Enemy.OnEnemyReachedEnd -= ReduceLives;
    }
}

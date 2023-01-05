using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    private Animator animator;
    private Enemy enemy;
    private EnemyHealth enemyHealth;

    private void Start()
    {
        animator = GetComponent<Animator>();
        enemy = GetComponent<Enemy>();
        enemyHealth = GetComponent<EnemyHealth>();
    }

    private void PlayHurtAnimation()
    {
        animator.SetTrigger("Hurt");
    }

    private void PlayDieAnimation()
    {
        animator.SetTrigger("Die");
    }

    private float GetCurrentAnimationLength()
    {
        float animationLength = animator.GetCurrentAnimatorStateInfo(0).length;
        return animationLength;
    }

    private IEnumerator PlayHurt()
    {
        enemy.StopMovement();
        PlayHurtAnimation();
        yield return new WaitForSeconds(GetCurrentAnimationLength() + 0.3f);
        enemy.ResumeMovement();
    }

    private IEnumerator PlayDead()
    {
        enemy.StopMovement();
        PlayDieAnimation();
        yield return new WaitForSeconds(GetCurrentAnimationLength() + 0.3f);
        enemy.ResumeMovement();
        enemyHealth.ResetHealth();
        ObjectPooler.ReturToPool(enemy.gameObject);
    }

    private void EnemyHit(Enemy enemy)
    {
        if (enemy == this.enemy)
        {
            StartCoroutine(PlayHurt());
        }

    }

    private void EnemyDead(Enemy enemy)
    {
        if (enemy == this.enemy)
        {
            StartCoroutine(PlayDead());
        }
    }

    private void OnEnable()
    {
        EnemyHealth.OnEnemyHit += EnemyHit;
        EnemyHealth.OnEnemyKilled += EnemyDead;
    }

    private void OnDisable()
    {
        EnemyHealth.OnEnemyHit -= EnemyHit;
        EnemyHealth.OnEnemyKilled -= EnemyDead;
    }
}

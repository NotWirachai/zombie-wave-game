using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    private Animator _animator;
    private Enemy _enemy;
    private EnemyHealth _enemyHealth;

    [SerializeField] protected AudioClip Hurt;
    [SerializeField] protected AudioClip Die;

    private AudioSource Sound;
    void Start()
    {
        _animator = GetComponent<Animator>();
        _enemy = GetComponent<Enemy>();
        _enemyHealth = GetComponent<EnemyHealth>();
        Sound = GetComponent<AudioSource>();
    }

    private void PlaytHurtAnimation() 
    {
        _animator.SetTrigger("Hurt");
        Sound.PlayOneShot(Hurt);
    }

    private void PlayDieAnimation() 
    {
        _animator.SetTrigger("Die");
        Sound.PlayOneShot(Die);
    }

    private float GetCurrentAnimationLenght()
    {
        float animationLenght = _animator.GetCurrentAnimatorStateInfo(0).length;
        return animationLenght;
    }

    private IEnumerator PlayHurt()
    {
        _enemy.StopMovement();
        PlaytHurtAnimation();
        yield return new WaitForSeconds(GetCurrentAnimationLenght() + 0.3f);
        _enemy.ResumeMovement();
    }

    private IEnumerator PlayDead()
    {
        _enemy.StopMovement();
        PlayDieAnimation();
        yield return new WaitForSeconds(GetCurrentAnimationLenght() + 0.3f);
        _enemy.ResumeMovement();
        _enemyHealth.ResetHealth();
        ObjectPooler.ReturnToPooler(_enemy.gameObject);
    }

    private void EnemyHit(Enemy enemy) 
    {
        if (_enemy == enemy)
        {
            StartCoroutine(PlayHurt());
        }
    }

    private void EnemyDead(Enemy enemy) 
    {
        if (_enemy == enemy)
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

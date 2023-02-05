using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    public float health = 100f;

    public AudioSource enemyHurt;

    public void HurtEnemy(float damage)
    {
        health -= damage;

        enemyHurt.Play();

        CheckAlive();
    }

    void CheckAlive()
    {
        if (health < 0)
        {
            Destroy(gameObject);
        }
    }
}

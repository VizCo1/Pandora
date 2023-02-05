using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    public float health = 100f;

    public void HurtEnemy(float damage)
    {
        health -= damage;

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

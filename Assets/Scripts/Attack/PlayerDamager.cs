using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerDamager : MonoBehaviour
{
    private Stats playerStats;

    public int playerNumber = 0;

    public float kockBack = 2f;

    public GameObject hitParticleSystem;

    private void Awake()
    {
        playerStats = playerNumber == 0 ? StatsManager.instance.statsJugador1 : StatsManager.instance.statsJugador2;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            Rigidbody2D enemyRb = collision.GetComponent<Rigidbody2D>();

           

            if (enemyRb != null)
            {
                // Particles
                GameObject particlesGO = Instantiate(hitParticleSystem, enemyRb.transform);
                ParticleSystem damagedParticles = particlesGO.GetComponent<ParticleSystem>();
                Debug.Log(damagedParticles);
                damagedParticles.Play();
                Destroy(damagedParticles, 2f);
                Destroy(particlesGO, 2f);

                Vector2 forceDirection = enemyRb.transform.position - transform.position;

                forceDirection = forceDirection.normalized * kockBack;
                enemyRb.AddForce(forceDirection, ForceMode2D.Impulse);

                Sequence seq = DOTween.Sequence();
                seq.Append(
                DOVirtual.Float(0, 28, 0.1f, ChangeRbDrag)
                );
                seq.Append(
                    DOVirtual.Float(28, 0, 0.2f, ChangeRbDrag)
                    );

                void ChangeRbDrag(float drag)
                {
                    enemyRb.drag = drag;
                }

                
            }
        }

        
    }
}

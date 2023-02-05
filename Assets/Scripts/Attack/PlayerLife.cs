using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class PlayerLife : MonoBehaviour
{

    public float health = 100f;

    private Stats playerStats;

    public int playerNumber = 0;

    public float knockback = 10f;

    public GameObject hitParticleSystem;

    public UnityEvent onPlayerDeath;

    private void Start()
    {
        playerStats = playerNumber == 0 ? StatsManager.instance.statsJugador1 : StatsManager.instance.statsJugador2;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {

            Debug.Log("PlayerHit");

            health -= 5f - playerStats.defensa * 3;

            playerStats.humanLife = health;

            Rigidbody2D rb = GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                // Particles
                GameObject particlesGO = Instantiate(hitParticleSystem, transform);
                ParticleSystem damagedParticles = particlesGO.GetComponent<ParticleSystem>();
               
                damagedParticles.Play();
                Destroy(damagedParticles, 2f);
                Destroy(particlesGO, 2f);

                // Force
                Vector2 forceDirection = transform.position - collision.transform.position;

                if (playerStats.velocidad < 0.3f)
                {
                    forceDirection = forceDirection.normalized * knockback * (knockback * 0.3f + 1.2f);
                }
                else
                {
                    forceDirection = forceDirection.normalized * knockback * (knockback * playerStats.velocidad + 1.2f);
                }
                rb.AddForce(forceDirection, ForceMode2D.Impulse);

                Sequence seq = DOTween.Sequence();
                seq.Append(
                DOVirtual.Float(0, 25, 0.1f, ChangeRbDrag)
                );
                seq.Append(
                    DOVirtual.Float(25, 0, 0.2f, ChangeRbDrag)
                    );

                void ChangeRbDrag(float drag)
                {
                    rb.drag = drag;
                }

                if (health <= 0)
                    onPlayerDeath.Invoke();
            }
        }
    }
}

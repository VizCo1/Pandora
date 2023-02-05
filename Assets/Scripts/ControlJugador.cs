using UnityEngine;
using UnityEngine.InputSystem;

public class ControlJugador : MonoBehaviour
{

    public StatsManager statManager;
    public int playerNumber = 0;
    private Stats playerStats;

    private Animator anim;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private ParticleSystem attackParticles;

    [SerializeField]
    private float velocidad = 5.0f;

    [SerializeField]
    private float smoothing = 0.2f;

    private Vector3 playerVelocity;
    
    private Vector2 movimientoInput = Vector2.zero;


    private bool canMove = true;

    private bool attacking = false;


    private void Awake()
    {
        if (playerNumber == 0)
            playerStats = statManager.statsJugador1;
        else
            playerStats = statManager.statsJugador2;

        anim = GetComponent<Animator>();
        attackParticles.Stop();
    }

    public void EnMovimiento(InputAction.CallbackContext context)
    {
        movimientoInput = context.ReadValue<Vector2>();
    }

    public void EnAtaque(InputAction.CallbackContext context)
    {
        if (context.performed && !attacking)
        {
            attacking = true;
            canMove = false;
            rb.velocity *= 0.5f;
            anim.SetTrigger("attack");
            attackParticles.Play();

            Invoke("StopAttackParticles", 0.2f);
        }
    }

    public void StopAttackParticles()
    {
        attackParticles.Stop();
        canMove = true;
        attacking = false;
    }

    public void PlayAttackParticles()
    {
        attackParticles.Play();
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            Vector3 move = new Vector2(movimientoInput.x, movimientoInput.y);

            move = move.normalized;
            //rb.MovePosition(rb.transform.position + move * velocidad * Time.deltaTime); 

            if (move != Vector3.zero)
            {

                anim.SetFloat("moveX", move.x);
                anim.SetFloat("moveY", move.y);

                

                if (move.x > 0)
                {
                    sr.flipX = true;
                }
                else
                {
                    sr.flipX = false;
                }
            }

            anim.SetBool("moving", move != Vector3.zero);

            if (playerStats.velocidad < 0.2f)
            {
                rb.velocity = Vector2.Lerp(rb.velocity * 0.2f,
                move * velocidad, smoothing);
            }
            else
            {
                rb.velocity = Vector2.Lerp(rb.velocity * playerStats.velocidad * 1.2f,
                move * velocidad, smoothing);
            }
            
        }
    }
}

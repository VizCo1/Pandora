using UnityEngine;
using UnityEngine.InputSystem;

public class ControlJugador : MonoBehaviour
{
    
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

    private void Start()
    {
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

    void Update()
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

                anim.SetBool("moving", true);

                if (move.x > 0)
                {
                    sr.flipX = true;
                }
                else
                {
                    sr.flipX = false;
                }
            }
            else
            {
                anim.SetBool("moving", false);
            }

            rb.velocity = Vector2.Lerp(rb.velocity, move * velocidad, smoothing);
        }
    }
}

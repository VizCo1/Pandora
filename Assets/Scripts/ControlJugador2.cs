using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlJugador2 : MonoBehaviour
{
    public float velocidad2 = 10f;
    private Rigidbody2D rb;
    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float horizontal2 = Input.GetAxis("Horizontal2");
        float vertical2 = Input.GetAxis("Vertical2");

        rb.velocity = new Vector2(horizontal2 * velocidad2, vertical2 * velocidad2);

        if (horizontal2 != 0 || vertical2 != 0)
        {
            anim.SetFloat("movimiento_x", horizontal2);
            anim.SetFloat("movimiento_y", vertical2);
            anim.SetBool("moviendo", true);
        }
        else
        {
            anim.SetBool("moviendo", false);
        }
    }
}

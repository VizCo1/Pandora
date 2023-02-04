using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlJugador : MonoBehaviour
{
    public float velocidad = 10f;
    private Rigidbody2D rb;
    private Animator anim;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        rb.velocity = new Vector2(horizontal * velocidad, vertical * velocidad);

        if (horizontal != 0 || vertical != 0)
        {
            anim.SetFloat("movimiento_x", horizontal);
            anim.SetFloat("movimiento_y", vertical);
            anim.SetBool("moviendo", true);
        }
        else
        {
            anim.SetBool("moviendo", false);
        }
    }
}

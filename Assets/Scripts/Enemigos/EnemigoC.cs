using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoC : Enemigo
{
    const string PLAYER = "Player";
    const string WALL = "Wall";

    Rigidbody2D rb;
    bool puedeEmbestir = true;
    bool embestidaPreparada = false;

    SpriteRenderer spriteRenderer;

    override protected void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    void ComprobarFlipX(Vector2 direccion)
    {
        if (direccion.x > 0.5f)
            spriteRenderer.flipX = false;
        else
            spriteRenderer.flipX = true;
    }

    override protected void Update()
    {
        base.Update();

        if (desplazamientoInicialCompletado)
        {
            if (jugadorObjetivo == 0 && distanciaJugador1 < distanciaVision && puedeEmbestir)
            {
                PrepararEmbestida();
                Vector2 direccion = transform.position - objetivos[jugadorObjetivo].position;
                direccion.Normalize();
                ComprobarFlipX(direccion);
            }
            else if (distanciaJugador2 < distanciaVision && puedeEmbestir)
            {
                PrepararEmbestida();
                Vector2 direccion = transform.position - objetivos[jugadorObjetivo].position;
                direccion.Normalize();
                ComprobarFlipX(direccion);
            }
            else
            {
                Patrullar();
                Vector2 direccion = transform.position - walkPoint;
                direccion.Normalize();
                ComprobarFlipX(direccion);
            }
        }
    }

    private void PrepararEmbestida()
    {
        if (!embestidaPreparada)
        {
            anim.SetTrigger("Embestida");
        }
    }
    public void Embestir()
    {
        Vector2 direccion = objetivos[jugadorObjetivo].position - transform.position;
        direccion /= direccion.magnitude;

        puedeEmbestir = false;

        rb.velocity = direccion * 16f;

        embestidaPreparada = false;

        StartCoroutine(ResetEmbestida());  
    }

    IEnumerator ResetEmbestida()
    {
        yield return new WaitForSeconds(4f);
        puedeEmbestir = true;
        //embestidaPreparada = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(PLAYER) || collision.transform.CompareTag(WALL))
        {
            rb.velocity = Vector2.zero;
            embestidaPreparada = false;
        }
    }
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }
}

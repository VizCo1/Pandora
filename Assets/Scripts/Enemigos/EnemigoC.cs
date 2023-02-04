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


    override protected void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
    }

    override protected void Update()
    {
        base.Update();

        if (desplazamientoInicialCompletado)
        {
            if (jugadorObjetivo == 0 && distanciaJugador1 < distanciaVision && puedeEmbestir)
            {
                PrepararEmbestida();
            }
            else if (distanciaJugador2 < distanciaVision && puedeEmbestir)
            {
                PrepararEmbestida();
            }
            else
                Patrullar();
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

        rb.velocity = direccion * 20;

        embestidaPreparada = false;

        StartCoroutine(ResetEmbestida());  
    }

    IEnumerator ResetEmbestida()
    {
        yield return new WaitForSeconds(3f);
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

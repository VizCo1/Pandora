using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoA : Enemigo
{
    override protected void Start()
    {
        base.Start();   
    }

    override protected void Update()
    {
        base.Update();

        if (desplazamientoInicialCompletado)
        {
            if (jugadorObjetivo == 0 && distanciaJugador1 >= distanciaVision)
            {
                Patrullar();
                Vector2 direccion = transform.position - walkPoint;
                direccion.Normalize();
                anim.SetFloat("DireccionX", direccion.x);
                anim.SetFloat("DireccionY", direccion.y);
            }    
            else if (jugadorObjetivo == 1 && distanciaJugador2 >= distanciaVision)
            {
                Patrullar();
                Vector2 direccion = transform.position - walkPoint;
                direccion.Normalize();
                anim.SetFloat("DireccionX", direccion.x);
                anim.SetFloat("DireccionY", direccion.y);
            }
            else
            {
                MoverA(objetivos[jugadorObjetivo].position);
                Vector2 direccion = transform.position - objetivos[jugadorObjetivo].position;
                direccion.Normalize();
                anim.SetFloat("DireccionX", direccion.x);
                anim.SetFloat("DireccionY", direccion.y);
            }
        }
    }
}

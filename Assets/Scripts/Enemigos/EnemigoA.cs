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
        if (jugadorObjetivo == 0 && distanciaJugador1 >= distanciaVision)        
            Patrullar();        
        else if (distanciaJugador2 >= distanciaVision)        
            Patrullar();       
        else
            MoverA(objetivos[jugadorObjetivo].position);
    }
}

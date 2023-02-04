using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoD : Enemigo
{
    [SerializeField] GameObject balasCirculo;
    [SerializeField] Vector2[] direcciones;
    bool puedeDisparar = true;

    override protected void Start()
    {
        base.Start();
    }

    override protected void Update()
    {
        base.Update();

        MoverA(objetivos[jugadorObjetivo].position);

        if (jugadorObjetivo == 0 && distanciaJugador1 < distanciaVision)
        {
            if (puedeDisparar)
                LanzarProyectiles();
        }
        else if (distanciaJugador2 < distanciaVision)
        {         
            if (puedeDisparar)        
                LanzarProyectiles();
        }
    }

    private void LanzarProyectiles()
    {
        Vector2 direccion = objetivos[jugadorObjetivo].position - transform.position;
        direccion /= direccion.magnitude;

        GameObject contenedorDeProyectiles = Instantiate(balasCirculo, transform.position, Quaternion.identity); 
        for(int i = 0; i < contenedorDeProyectiles.transform.childCount; i++)
        {
            contenedorDeProyectiles.transform.GetChild(i).GetComponent<Rigidbody2D>().velocity = direcciones[i] * 15;
        }

        puedeDisparar = false;

        StartCoroutine(ResetDisparo(contenedorDeProyectiles));
    }

    IEnumerator ResetDisparo(GameObject contenedor)
    {
        yield return new WaitForSeconds(2.5f);
        puedeDisparar = true;
        Destroy(contenedor);
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }
}

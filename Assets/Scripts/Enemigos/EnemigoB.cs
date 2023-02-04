using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoB : Enemigo
{
    [SerializeField] GameObject bala;
    [SerializeField] Transform posDisparo;
    bool puedeDisparar = true;
    float porcentajeDeDistancia = 1f;

    override protected void Start()
    {
        base.Start();   
    }

    override protected void Update()
    {
        base.Update();

        if (desplazamientoInicialCompletado)
        {
            if (jugadorObjetivo == 0 && distanciaJugador1 < distanciaVision)
                EvitarJugador(objetivos[jugadorObjetivo].position);
            else if (distanciaJugador2 < distanciaVision)
                EvitarJugador(objetivos[jugadorObjetivo].position);
            else if (puedeDisparar)
                LanzarProyectil();
        }

    }

    private void EvitarJugador(Vector3 pos)
    {
        if (!walkPointSet) BuscarPunto(pos);

        if (walkPointSet)
            MoverA(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1.33f)
        {
            porcentajeDeDistancia = 1f;
            walkPointSet = false;
        }
    }

    private void BuscarPunto(Vector3 pos)
    {
        Vector3 v = pos * porcentajeDeDistancia;

        walkPoint = transform.position - v;

        if (Physics2D.Raycast(walkPoint, transform.forward, 2f, mascaraSuelo))
        {
            walkPointSet = true;
        }
        else if (porcentajeDeDistancia > 0f)
        {
            porcentajeDeDistancia -= 0.1f;
        }
    }

    private void LanzarProyectil()
    {
        Vector2 direccion = objetivos[jugadorObjetivo].position - transform.position;
        direccion /= direccion.magnitude;

        GameObject proyectil = Instantiate(bala, posDisparo.position, Quaternion.identity);
        proyectil.GetComponent<Rigidbody2D>().velocity = direccion * 10;

        puedeDisparar = false;

        StartCoroutine(DestruirBala(proyectil));

    }

    IEnumerator DestruirBala(GameObject bala)
    {
        yield return new WaitForSeconds(2.5f);

        Destroy(bala);
        puedeDisparar = true;
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }
}

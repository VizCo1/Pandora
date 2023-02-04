using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    const string AMARILLO = "Amarillo";
    const string VERDE = "Verde";
    const string AZUL = "Azul";

    [SerializeField] MovementController2D movementController2D;
    [SerializeField] protected Transform[] objetivos;
    protected int jugadorObjetivo = 0;
    public float vida;
    public float ataque;
    public float velocidad; // 0.05 es bastantante rapido

    [SerializeField] protected float distanciaVision;

    bool walkPointSet = false;
    private Vector3 walkPoint;
    [SerializeField] LayerMask suelo;

    protected float distanciaJugador1;
    protected float distanciaJugador2;


    protected virtual void Start()
    {
        movementController2D.SetSpeed(velocidad);
        SeleccionarSubtipo();
    }

    protected virtual void Update()
    {
        ComprobarTarget();
        //MoverA(objetivos[jugadorObjetivo].position);
        //Debug.Log(jugadorObjetivo);
    }

    private void ComprobarTarget()
    {
        distanciaJugador1 = Vector2.Distance(transform.position, objetivos[0].position);
        distanciaJugador2 = Vector2.Distance(transform.position, objetivos[1].position);

        if (distanciaJugador1 < distanciaJugador2)
            jugadorObjetivo = 0;
        else
            jugadorObjetivo = 1;
    }

    protected void MoverA(Vector3 pos)
    {
        movementController2D.MoveTo(pos);
    }

    protected void Patrullar()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            movementController2D.MoveTo(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomY = UnityEngine.Random.Range(-4, 4);
        float randomX = UnityEngine.Random.Range(-4, 4);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y + randomY, transform.position.z + 0.5f);

        if (Physics2D.Raycast(walkPoint, transform.forward, 2f, suelo))
        {
            Debug.Log("Si");
            walkPointSet = true;
        }
        else
            Debug.Log("No");
    }

    void SeleccionarSubtipo()
    {
        switch (LayerMask.LayerToName(gameObject.layer))
        {
            case AMARILLO:

                break;

            case VERDE:

                break;

            case AZUL:

                break;

            default:

                break;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, distanciaVision);
    }
}

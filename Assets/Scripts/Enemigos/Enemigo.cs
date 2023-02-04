using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemigo : MonoBehaviour
{
    const string AMARILLO = "Amarillo";
    const string VERDE = "Verde";
    const string AZUL = "Azul";

    [SerializeField] Transform suelo;

    [SerializeField] MovementController2D movementController2D;
    [SerializeField] protected Transform[] objetivos;
    protected int jugadorObjetivo = 0;
    public float vida;
    public float ataque;
    public float velocidad; // 0.05 es bastantante rapido

    [SerializeField] protected float distanciaVision;

    protected bool walkPointSet = false;
    protected private Vector3 walkPoint;
    [SerializeField] protected LayerMask mascaraSuelo;

    protected float distanciaJugador1;
    protected float distanciaJugador2;


    Vector2 posFuturaInicial;

    private void Awake()
    {
        posFuturaInicial = new Vector2(Random.Range(suelo.GetChild(0).position.x, suelo.GetChild(1).position.x),
                                       Random.Range(suelo.GetChild(1).position.y, suelo.GetChild(0).position.y));
    }
    protected virtual void Start()
    {
        movementController2D.SetSpeed(velocidad);
        SeleccionarSubtipo();
        
    }

    protected bool desplazamientoInicialCompletado = false;
    protected virtual void Update()
    {
        if (!desplazamientoInicialCompletado)
        {
            ComprobarTarget();
            MoverA(posFuturaInicial);

            if ((distanciaJugador2 < distanciaVision || distanciaJugador1 < distanciaVision) 
                || (transform.position.x > suelo.GetChild(0).position.x && transform.position.x < suelo.GetChild(1).position.x
                && transform.position.y > suelo.GetChild(1).position.y && transform.position.y < suelo.GetChild(0).position.y))
            {
                desplazamientoInicialCompletado = true;
            }
                
        }
        else
            ComprobarTarget();
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
            MoverA(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1.33f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomY = Random.Range(-4, 4);
        float randomX = Random.Range(-4, 4);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y + randomY, transform.position.z + 0.5f);

        if (Physics2D.Raycast(walkPoint, transform.forward, 2f, mascaraSuelo))
        {
            //Debug.Log("Si");
            walkPointSet = true;
        }
        //else
            //Debug.Log("No");
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

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, distanciaVision);
    }
}

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
    [SerializeField] Transform objetivo;
    public float vida;
    public float ataque;
    public float velocidad; // 0.05 es bastantante rapido

    bool walkPointSet = false;
    private Vector3 walkPoint;
    [SerializeField] LayerMask suelo;


    void Start()
    {
        movementController2D.SetSpeed(velocidad); 
    }
    
    void Update()
    {
        //movementController2D.MoveTo(objetivo.position);
        Patrullar();
    }

    protected virtual void Patrullar()
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
        float randomY = UnityEngine.Random.Range(-2, 2);
        float randomX = UnityEngine.Random.Range(-2, 2);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y + randomY, transform.position.z + 0.5f);

        if (Physics.Raycast(walkPoint, transform.forward, 2f, suelo))
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
}

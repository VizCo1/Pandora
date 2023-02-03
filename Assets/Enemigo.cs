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


    
    void Start()
    {
        movementController2D.SetSpeed(velocidad); 
    }
    
    void Update()
    {
        movementController2D.MoveTo(objetivo.position);
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

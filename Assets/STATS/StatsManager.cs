using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

[System.Serializable]
public class Stats
{
    [Range(0, 1)]
    public float ataque;
    //public InputActionReference botonIntercambioAtaque;
    
    [Range(0, 1)]
    public float defensa;
    //public InputActionReference botonIntercambioDefensa;
    [Range(0, 1)]
    public float velocidad;
    //public InputActionReference botonIntercambioVelocidad;

    public PlayerInput playerInput;
}

public enum TipoStat
{
    Ataque, Defensa, Velocidad
} 


public class StatsManager : MonoBehaviour
{
    public Stats statsJugador1; // Objeto principal para los stats del jugador
    [SerializeField] private GameObject statsUI1;
    private Slider ataqueJ1;
    private Slider defensaJ1;
    private Slider velocidadJ1;

    public Stats statsJugador2;
    [SerializeField] private GameObject statsUI2;
    private Slider ataqueJ2;
    private Slider defensaJ2;
    private Slider velocidadJ2;

    [SerializeField] float velocidadReduccion = 0.2f;

    private bool exchangingStats;

    private int statExchangeType = 0;

    void Awake()
    {
        ataqueJ1 = statsUI1.transform.GetChild(0).GetComponent<Slider>();
        defensaJ1 = statsUI1.transform.GetChild(1).GetComponent<Slider>();
        velocidadJ1 = statsUI1.transform.GetChild(2).GetComponent<Slider>();

        ataqueJ2 = statsUI2.transform.GetChild(0).GetComponent<Slider>();
        defensaJ2 = statsUI2.transform.GetChild(1).GetComponent<Slider>();
        velocidadJ2 = statsUI2.transform.GetChild(2).GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        ActualizarUI();

        if(exchangingStats)
        {
            IntercambiarStatParaJ1(statExchangeType);
        }

    }

    public void BotonAtaquePulsado(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Se pulsó intercambiar ataque");
            exchangingStats = true;
            statExchangeType = (int)TipoStat.Ataque;
        }
        else if(context.canceled)
        {
            Debug.Log("Se Deja de intercambiar ataque");
            exchangingStats = false;
        }
        
    }

    public void BotonDefensaPulsado(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            exchangingStats = true;
            statExchangeType = (int)TipoStat.Defensa;
        }
        else if (context.canceled)
        {
            exchangingStats = false;
        }

    }

    public void BotonVelocidadPulsado(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            exchangingStats = true;
            statExchangeType = (int)TipoStat.Velocidad;
        }
        else if (context.canceled)
        {
            exchangingStats = false;
        }

    }

    public void IntercambiarStatParaJ1(int tipo)
    {
        if (tipo == (int)TipoStat.Ataque)
        {
            if (statsJugador1.ataque > 0 && statsJugador1.ataque < 1 && statsJugador2.ataque > 0 && statsJugador2.ataque < 1)
            {
                IncreaseStat(ref statsJugador1.ataque);

                DecreaseStat(ref statsJugador2.ataque);
            }
        } 
        else if(tipo == 1)
        {
            if (statsJugador1.defensa > 0 && statsJugador1.defensa < 1 && statsJugador2.defensa > 0 && statsJugador2.defensa < 1)
            {
                IncreaseStat(ref statsJugador1.defensa);

                DecreaseStat(ref statsJugador2.defensa);
            }
        }
        else
        {
            if (statsJugador1.velocidad > 0 && statsJugador1.velocidad < 1 && statsJugador2.velocidad > 0 && statsJugador2.velocidad < 1)
            {
                IncreaseStat(ref statsJugador1.velocidad);

                DecreaseStat(ref statsJugador2.velocidad);
            }
        }
    }

    private void DecreaseStat(ref float stat)
    {
        stat -= velocidadReduccion * Time.deltaTime;
        stat = Mathf.Clamp01(stat);
       
    }

    private void IncreaseStat(ref float stat)
    {
        stat += velocidadReduccion * Time.deltaTime;
        stat = Mathf.Clamp01(stat);

    }


    // ACTUALIZACIÓN DE UI
    private void ActualizarUI()
    {
        ActualizarAtaque1();
        ActualizarAtaque2();
        ActualizarDefensa1();
        ActualizarDefensa2();
        ActualizarVelocidad1();
        ActualizarVelocidad2();
    }
    private void ActualizarAtaque1()
    {
        ataqueJ1.value = statsJugador1.ataque;
    }

    private void ActualizarAtaque2()
    {
        ataqueJ2.value = statsJugador2.ataque;
    }

    private void ActualizarDefensa1()
    {
        defensaJ1.value = statsJugador1.defensa;
    }

    private void ActualizarDefensa2()
    {
        defensaJ2.value = statsJugador2.defensa;
    }

    private void ActualizarVelocidad1()
    {
        velocidadJ1.value = statsJugador1.velocidad;
    }

    private void ActualizarVelocidad2()
    {
        velocidadJ2.value = statsJugador2.velocidad;
    }
}




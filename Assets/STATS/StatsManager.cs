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
    
    [Range(0, 1)]
    public float defensa;
   
    [Range(0, 1)]
    public float velocidad;
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

    private int statExchangeTypeP1 = 0;
    private int statExchangeTypeP2 = 0;

    private bool exchangingP1;
    private bool exchangingP2;

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
            if(exchangingP1)
                IntercambiarStatParaJ1(statExchangeTypeP1);
            if(exchangingP2)
                IntercambiarStatParaJ2(statExchangeTypeP2);
        }

    }

    public void BotonAtaquePulsadoJ1(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            exchangingStats = true;
            exchangingP1 = true;
            statExchangeTypeP1 = (int)TipoStat.Ataque;
        }
        else if(context.canceled)
        {
            exchangingP1 = false;
            exchangingStats = false;
        }
        
    }

    public void BotonAtaquePulsadoJ2(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            exchangingStats = true;
            exchangingP2 = true;
            statExchangeTypeP2 = (int)TipoStat.Ataque;
        }
        else if (context.canceled)
        {
            exchangingP2 = false;
            exchangingStats = false;
        }

    }

    public void BotonDefensaPulsadoJ1(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            exchangingStats = true;
            exchangingP1 = true;
            statExchangeTypeP1 = (int)TipoStat.Defensa;
        }
        else if (context.canceled)
        {
            exchangingP1 = false;
            exchangingStats = false;
        }

    }

    public void BotonDefensaPulsadoJ2(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            exchangingStats = true;
            exchangingP2 = true;
            statExchangeTypeP2 = (int)TipoStat.Defensa;
        }
        else if (context.canceled)
        {
            exchangingP2 = false;
            exchangingStats = false;
        }

    }

    public void BotonVelocidadPulsadoJ1(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            exchangingStats = true;
            exchangingP1 = true;
            statExchangeTypeP1 = (int)TipoStat.Velocidad;
        }
        else if (context.canceled)
        {
            exchangingStats = false;
            exchangingP1 = false;
        }

    }

    public void BotonVelocidadPulsadoJ2(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            exchangingStats = true;
            exchangingP2 = true;
            statExchangeTypeP2 = (int)TipoStat.Velocidad;
        }
        else if (context.canceled)
        {
            exchangingStats = false;
            exchangingP2 = false;
        }

    }

    public void IntercambiarStatParaJ1(int tipo)
    {
        if (tipo == (int)TipoStat.Ataque)
        {
            if (statsJugador1.ataque >= 0 && statsJugador1.ataque < 1 && statsJugador2.ataque > 0 && statsJugador2.ataque <= 1)
            {
                IncreaseStat(ref statsJugador1.ataque);

                DecreaseStat(ref statsJugador2.ataque);
            }
        } 
        else if (tipo == (int)TipoStat.Defensa)
        {
            if (statsJugador1.defensa >= 0 && statsJugador1.defensa < 1 && statsJugador2.defensa > 0 && statsJugador2.defensa <= 1)
            {
                IncreaseStat(ref statsJugador1.defensa);

                DecreaseStat(ref statsJugador2.defensa);
            }
        }
        else if (tipo == (int)TipoStat.Velocidad)
        {
            if (statsJugador1.velocidad >= 0 && statsJugador1.velocidad < 1 && statsJugador2.velocidad > 0 && statsJugador2.velocidad <= 1)
            {
                IncreaseStat(ref statsJugador1.velocidad);

                DecreaseStat(ref statsJugador2.velocidad);
            }
        }
    }

    public void IntercambiarStatParaJ2(int tipo)
    {
        if (tipo == (int)TipoStat.Ataque)
        {
            if (statsJugador1.ataque > 0 && statsJugador1.ataque <= 1 && statsJugador2.ataque >= 0 && statsJugador2.ataque < 1)
            {
                DecreaseStat(ref statsJugador1.ataque);

                IncreaseStat(ref statsJugador2.ataque);
            }
        }
        else if (tipo == (int)TipoStat.Defensa)
        {
            if (statsJugador1.defensa > 0 && statsJugador1.defensa <= 1 && statsJugador2.defensa >= 0 && statsJugador2.defensa < 1)
            {
                DecreaseStat(ref statsJugador1.defensa);

                IncreaseStat(ref statsJugador2.defensa);
            }
        }
        else if (tipo == (int)TipoStat.Velocidad)
        {
            if (statsJugador1.velocidad > 0 && statsJugador1.velocidad <=  1 && statsJugador2.velocidad >= 0 && statsJugador2.velocidad < 1)
            {
                DecreaseStat(ref statsJugador1.velocidad);

                IncreaseStat(ref statsJugador2.velocidad);
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




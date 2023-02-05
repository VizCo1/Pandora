using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using DG.Tweening;

[System.Serializable]
public class Stats
{
    [Range(0, 1)]
    public float ataque;
    
    [Range(0, 1)]
    public float defensa;
   
    [Range(0, 1)]
    public float velocidad;

    [Range (0, 100)]
    public float humanLife;
}

public enum TipoStat
{
    Ataque, Defensa, Velocidad
} 


public class StatsManager : MonoBehaviour
{
    // JUGADOR 1
    public Stats statsJugador1; // Objeto principal para los stats del jugador
    [SerializeField] private GameObject statsUI1;
    [SerializeField] private Slider humanHealth1Slider;
    private Slider ataqueJ1Slider;
    [SerializeField] private ParticleSystem attackParticles1;
    private Slider defensaJ1Slider;
    [SerializeField] private ParticleSystem defenseParticles1;
    private Slider velocidadJ1Slider;
    [SerializeField] private ParticleSystem velocityParticles1;

    [Space]

    // JUGADOR 2
    public Stats statsJugador2;
    [SerializeField] private GameObject statsUI2;
    [SerializeField] private Slider humanHealth2Slider;
    private Slider ataqueJ2Slider;
    [SerializeField] private ParticleSystem attackParticles2;
    private Slider defensaJ2Slider;
    [SerializeField] private ParticleSystem defenseParticles2;
    private Slider velocidadJ2Slider;
    [SerializeField] private ParticleSystem velocityParticles2;

    [Space]

    // UI
    [SerializeField] float velocidadReduccion = 0.2f;
   // [SerializeField] float shakeDuration = 0.1f;

    private bool exchangingStats;

    private int statExchangeTypeP1 = 0;
    private int statExchangeTypeP2 = 0;

    private bool exchangingP1;
    private bool exchangingP2;

    public static StatsManager instance;
    void Awake()
    {

        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        ataqueJ1Slider = statsUI1.transform.GetChild(0).GetComponent<Slider>();
        defensaJ1Slider = statsUI1.transform.GetChild(1).GetComponent<Slider>();
        velocidadJ1Slider = statsUI1.transform.GetChild(2).GetComponent<Slider>();

        ataqueJ2Slider = statsUI2.transform.GetChild(0).GetComponent<Slider>();
        defensaJ2Slider = statsUI2.transform.GetChild(1).GetComponent<Slider>();
        velocidadJ2Slider = statsUI2.transform.GetChild(2).GetComponent<Slider>();
    }

    private void Start()
    {
        ActualizarUIStart();
    }

    // Update is called once per frame
    void Update()
    {
        if(exchangingStats) // changes when input is detected
        {
            if (exchangingP1) 
            {
                IntercambiarStatParaJ1(statExchangeTypeP1);
                statsJugador1.humanLife -= velocidadReduccion * 5 * Time.deltaTime;
            }
            if (exchangingP2) 
            {
                IntercambiarStatParaJ2(statExchangeTypeP2);
                statsJugador2.humanLife -= velocidadReduccion * 5 * Time.deltaTime;
            }
            ActualizarUI();
        }
        else
        {
            StopAllParticles();
        }
        ActualizarVida();
    }

    private void StopAllParticles()
    {
        attackParticles1.Stop();
        defenseParticles1.Stop();
        velocityParticles1.Stop();
        attackParticles2.Stop();
        defenseParticles2.Stop();
        velocityParticles2.Stop();
    }

    #region InputHandlers
    public void BotonAtaquePulsadoJ1(InputAction.CallbackContext context)
    {
        if (context.performed && !exchangingP1)
        {
            exchangingStats = true;
            exchangingP1 = true;
            statExchangeTypeP1 = (int)TipoStat.Ataque;
        }
        else if (context.canceled)
        {
            exchangingP1 = false;
            if(!exchangingP2)
                exchangingStats = false;
        }

    }

    public void BotonAtaquePulsadoJ2(InputAction.CallbackContext context)
    {
        if (context.performed && !exchangingP2)
        {
            exchangingStats = true;
            exchangingP2 = true;
            statExchangeTypeP2 = (int)TipoStat.Ataque;
        }
        else if (context.canceled)
        {
            exchangingP2 = false;
            if (!exchangingP1)
                exchangingStats = false;
        }

    }

    public void BotonDefensaPulsadoJ1(InputAction.CallbackContext context)
    {
        if (context.performed && !exchangingP1)
        {
            exchangingStats = true;
            exchangingP1 = true;
            statExchangeTypeP1 = (int)TipoStat.Defensa;
        }
        else if (context.canceled)
        {
            exchangingP1 = false;
            if (!exchangingP2)
                exchangingStats = false;
        }

    }

    public void BotonDefensaPulsadoJ2(InputAction.CallbackContext context)
    {
        if (context.performed && !exchangingP2)
        {
            exchangingStats = true;
            exchangingP2 = true;
            statExchangeTypeP2 = (int)TipoStat.Defensa;
        }
        else if (context.canceled)
        {
            exchangingP2 = false;
            if (!exchangingP1)
                exchangingStats = false;
        }

    }

    public void BotonVelocidadPulsadoJ1(InputAction.CallbackContext context)
    {
        if (context.performed && !exchangingP1)
        {
            exchangingStats = true;
            exchangingP1 = true;
            statExchangeTypeP1 = (int)TipoStat.Velocidad;
        }
        else if (context.canceled)
        {
            exchangingP1 = false;
            if (!exchangingP2)
                exchangingStats = false;
        }

    }

    public void BotonVelocidadPulsadoJ2(InputAction.CallbackContext context)
    {
        if (context.performed && !exchangingP2)
        {
            exchangingStats = true;
            exchangingP2 = true;
            statExchangeTypeP2 = (int)TipoStat.Velocidad;
        }
        else if (context.canceled)
        {
            exchangingP2 = false;
            if (!exchangingP1)
                exchangingStats = false;
        }

    } 
    #endregion

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

       // ActualizarUI();
    }

    private void IncreaseStat(ref float stat)
    {
        stat += velocidadReduccion * Time.deltaTime;
        stat = Mathf.Clamp01(stat);

       // ActualizarUI();
    }


    // ACTUALIZACIÓN DE UI
    private void ActualizarUI()
    {
        if (exchangingP1 && statExchangeTypeP1 == (int) TipoStat.Ataque || exchangingP2 && statExchangeTypeP2 == (int)TipoStat.Ataque)
        {
            ActualizarAtaque1();
            ActualizarAtaque2();
        }
        if (exchangingP1 && statExchangeTypeP1 == (int)TipoStat.Defensa || exchangingP2 && statExchangeTypeP2 == (int)TipoStat.Defensa)
        {
            ActualizarDefensa1();
            ActualizarDefensa2();
        }
        if(exchangingP1 && statExchangeTypeP1 == (int)TipoStat.Velocidad || exchangingP2 && statExchangeTypeP2 == (int)TipoStat.Velocidad)
        {
            ActualizarVelocidad1();
            ActualizarVelocidad2();
        }
        ActualizarVida();   
    }

    private void ActualizarUIStart()
    {
        ActualizarAtaque1();
        ActualizarAtaque2();
       
        ActualizarDefensa1();
        ActualizarDefensa2();
        
        ActualizarVelocidad1();
        ActualizarVelocidad2();
       
        ActualizarVida();
    }
    private void ActualizarAtaque1()
    {
        ataqueJ1Slider.value = statsJugador1.ataque;
        attackParticles1.Play();
    }

    private void ActualizarAtaque2()
    {
        ataqueJ2Slider.value = statsJugador2.ataque;
        attackParticles2.Play();
    }

    private void ActualizarDefensa1()
    {
        defensaJ1Slider.value = statsJugador1.defensa;
        defenseParticles1.Play();
    }

    private void ActualizarDefensa2()
    {
        defensaJ2Slider.value = statsJugador2.defensa;
        defenseParticles2.Play();
    }

    private void ActualizarVelocidad1()
    {
        velocidadJ1Slider.value = statsJugador1.velocidad;
        velocityParticles1.Play();
    }

    private void ActualizarVelocidad2()
    {
        velocidadJ2Slider.value = statsJugador2.velocidad;
        velocityParticles2.Play();
    }

    private void ActualizarVida()
    {
        humanHealth1Slider.value = statsJugador1.humanLife / 100;
        humanHealth2Slider.value = statsJugador2.humanLife / 100;
    }

    //private void ShakeSlider(Transform transform)
    //{
    //    Transform aux = transform;
    //    transform.DOShakePosition(shakeDuration, 1, 5, 20, false, true).OnComplete(() => { transform = aux; });
    //}
}




using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Stats
{
    public float ataque;
    public float defensa;
    public float velocidad;
}


public class StatsManager : MonoBehaviour
{
    public Stats statsJugador1;
    [SerializeField] private GameObject statsUI1;
    private Slider ataqueJ1;
    private Slider defensaJ1;
    private Slider velocidadJ1;

    public Stats statsJugador2;
    [SerializeField] private GameObject statsUI2;
    private Slider ataqueJ2;
    private Slider defensaJ2;
    private Slider velocidadJ2;

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
    }

    private void ActualizarUI()
    {
        ataqueJ1.value = statsJugador1.ataque;
        defensaJ1.value = statsJugador1.defensa;
        velocidadJ1.value = statsJugador1.velocidad;

        ataqueJ2.value = statsJugador2.ataque;
        defensaJ2.value = statsJugador2.defensa;
        velocidadJ2.value = statsJugador2.velocidad;

    }
}




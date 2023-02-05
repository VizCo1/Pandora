using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaraManager : MonoBehaviour
{
    [SerializeField] int player;
    [SerializeField] Sprite[] imagenes = new Sprite[2];
    Image image;
    float humanLife;
    float vidaParaCambiar = 66f;
    int indice = 0;
    [SerializeField] StatsManager statsManager;
    

    private void Start()
    {
        image = GetComponent<Image>();
         
    }

    void Update()
    {
        if (indice < imagenes.Length)
        {
            humanLife = GetHumanLife(); 

            if (humanLife <= vidaParaCambiar)
            {
                CambiarImagen(indice++);
                vidaParaCambiar -= 33f;
            }
        }
    }

    void CambiarImagen(int i)
    {
        image.sprite = imagenes[i];
    }

    float GetHumanLife()
    {
        if (player == 1)
             return statsManager.statsJugador1.humanLife;
        
        return statsManager.statsJugador2.humanLife;
    }
}

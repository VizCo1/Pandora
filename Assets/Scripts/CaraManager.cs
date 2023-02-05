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

    Vector2 fuerza;
    int vibrato = 3;
    

    private void Start()
    {
        fuerza = new Vector2(0.5f, 0.1f);
        image = GetComponent<Image>();
        //transform.DOShakePosition(5f, fuerza, vibrato, 90, false, false).SetLoops(-1);
        
    }

    void Update()
    {
        if (indice < imagenes.Length)
        {
            humanLife = GetHumanLife(); 

            if (humanLife <= vidaParaCambiar)
            {
                CambiarImagen(indice++);
                fuerza = new Vector2(fuerza.x + 1.25f, fuerza.y);
                vibrato++;
                transform.DOShakePosition(5f, fuerza, vibrato, 90, false, false).SetLoops(-1);
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

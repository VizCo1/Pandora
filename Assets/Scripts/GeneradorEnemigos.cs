using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GeneradorEnemigos : MonoBehaviour
{
    [SerializeField] GameObject[] enemigos;
    [SerializeField] Transform[] posicionesSpawn;
    [SerializeField] Transform contenedorEnemigos;
    Transform[] posicionesRandom;
    int i = 0;
    bool rondaCompletada = false;
    float tiempo = 0;
    float tiempoMax = 15f;

    void Start()
    {
        ResetPosionesRandom();
    }

    void Update()
    {
        if (i >= posicionesSpawn.Length)
        {
            rondaCompletada = true;
            ResetPosionesRandom();
            i = 0;
        }
        else if (!rondaCompletada)
        {
            SpawnearEnemigosEn(posicionesRandom[i].position);
            i++;
        }
        else
        {
            tiempo += Time.deltaTime;
            if (tiempo >= tiempoMax)
            {
                tiempo = 0;
                rondaCompletada = false;
            }
        }
    }

    void SpawnearEnemigosEn(Vector3 pos)
    {
        Instantiate(EnemigoRandom(), pos, Quaternion.identity, contenedorEnemigos).SetActive(true);
        //if (Random.Range(0, 1f) > 0.7f)
          //  Instantiate(enemigos[1], pos, Quaternion.identity, contenedorEnemigos).SetActive(true);

    }

    GameObject EnemigoRandom()
    {
        return enemigos[Random.Range(0, enemigos.Length)];
    }

    void ResetPosionesRandom()
    {
        posicionesRandom = posicionesSpawn.OrderBy(x => Random.Range(0, posicionesSpawn.Length - 1)).ToArray();
    }
}

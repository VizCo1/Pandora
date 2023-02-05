using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoD : Enemigo
{
    [SerializeField] GameObject balasCirculo;
    [SerializeField] Vector2[] direcciones;
    bool puedeDisparar = true;

    SpriteRenderer spriteRenderer;

    override protected void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void ComprobarFlipX(Vector2 direccion)
    {
        if (direccion.x > 0.5f)
            spriteRenderer.flipX = false;
        else
            spriteRenderer.flipX = true;
    }

    override protected void Update()
    {
        base.Update();

        if (desplazamientoInicialCompletado)
        {
            MoverA(objetivos[jugadorObjetivo].position);

            if (jugadorObjetivo == 0 && distanciaJugador1 < distanciaVision)
            {
                if (puedeDisparar)
                    Ataque();
            }
            else if (distanciaJugador2 < distanciaVision)
            {         
                if (puedeDisparar)        
                    Ataque();
            }

            Vector2 direccion = transform.position - objetivos[jugadorObjetivo].position;
            direccion.Normalize();
            ComprobarFlipX(direccion);
        }

    }

    void Ataque()
    {
        if (!audioSource.isPlaying)
            audioSource.Play();
        anim.SetTrigger("Ataque");
        puedeDisparar = false;
    } 

    public void LanzarProyectiles()
    {
        Vector2 direccion = objetivos[jugadorObjetivo].position - transform.position;
        direccion /= direccion.magnitude;

        GameObject contenedorDeProyectiles = Instantiate(balasCirculo, transform.position, Quaternion.identity); 
        for(int i = 0; i < contenedorDeProyectiles.transform.childCount; i++)
        {
            contenedorDeProyectiles.transform.GetChild(i).GetComponent<Rigidbody2D>().velocity = direcciones[i] * 15;
        }

        StartCoroutine(ResetDisparo(contenedorDeProyectiles));
    }

    IEnumerator ResetDisparo(GameObject contenedor)
    {
        yield return new WaitForSeconds(2.5f);
        puedeDisparar = true;
        Destroy(contenedor);
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }
}

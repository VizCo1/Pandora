using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPerder : MonoBehaviour
{
    public void MenuInicial(string nombre)
    {
        SceneManager.LoadScene(nombre);
    }

    public void Salir()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }


}

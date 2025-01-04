using System.Collections;
using System.Collections.Generic;
using PlasticPipe.PlasticProtocol.Messages;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{

    public GameObject menuPrincipal;
    public GameObject menuNiveles;
    public GameObject menuAjustes;
    public static int nivel;

    public void Start() {
        if (PlayerPrefs.HasKey("nivelActual")) {
            nivel = PlayerPrefs.GetInt("nivelActual");
        } else {
            PlayerPrefs.SetInt("nivelActual",0);
        }
        if (PlayerPrefs.GetInt("nivelActual") == 0) {
            menuAjustes.SetActive(false);
            menuNiveles.SetActive(false);
            menuPrincipal.SetActive(true);
        } else {
            menuAjustes.SetActive(false);
            menuPrincipal.SetActive(false);
            menuNiveles.SetActive(true);
        }
    }
    public void Settings()
    {

    }
    public void Play()
    {
        menuPrincipal.SetActive(false);
        menuNiveles.SetActive(true);
        PlayerPrefs.SetInt("nivelActual",1);
    }

    public void Nivel(int i) {
        Debug.Log("entrar a nivel: " + i + " estando en nivel: " + PlayerPrefs.GetInt("nivelActual"));
        if (i <= nivel) {
            SceneManager.LoadScene("Definitivo");
        }
    }
    public void Volver(int i) {
        //i == 1 volver de menuAjustes, i == 2 volver de menuNiveles
        if (i == 1) {
            menuAjustes.SetActive(false);
        } else if (i == 2) {
            menuNiveles.SetActive(false);
        }
        menuPrincipal.SetActive(true);
    }
}

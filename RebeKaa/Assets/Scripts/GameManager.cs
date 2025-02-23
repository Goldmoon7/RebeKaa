using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public GameObject menuPrincipal;
    public GameObject menuNiveles;
    public GameObject menuAjustes;
    public static int nivel;
    public List<GameObject> botones;

    public void Start() {
        menuAjustes.SetActive(false);
        menuNiveles.SetActive(false);
        menuPrincipal.SetActive(true);
        /*
        if (PlayerPrefs.GetInt("nivelActual") == 0) {
            menuAjustes.SetActive(false);
            menuNiveles.SetActive(false);
            menuPrincipal.SetActive(true);
        } else {
            menuAjustes.SetActive(false);
            menuPrincipal.SetActive(false);
            menuNiveles.SetActive(true);
            PonerBotones();
        }
        */
    }
    public void Settings()
    {

    }
    public void Play()
    {
        menuPrincipal.SetActive(false);
        menuNiveles.SetActive(true);
        if (PlayerPrefs.GetInt("nivelActual") == 0) {
            PlayerPrefs.SetInt("nivelActual",1);
        }
        PonerBotones();
    }

    public void Nivel(int i) {
        Debug.Log("entrar a nivel: " + i + " estando en nivel: " + PlayerPrefs.GetInt("nivelActual"));
        if (i <= PlayerPrefs.GetInt("nivelActual")) {
            if (i == 1 && PlayerPrefs.GetInt("nivelActual") == 1) {
                SceneManager.LoadScene("ViñetasP1");
            }
            else{
                SceneManager.LoadScene("Definitivo");
            }
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

    public void PonerBotones() {
        int nivel = PlayerPrefs.GetInt("nivelActual");
        Color transp = Color.white;
        transp.a = 185;
        for(int i = 0; i < 5; i++) {
            if (i > (nivel-1)) {
                Button boton = botones[i].GetComponent<Button>();
                boton.interactable = false;
            }
        } 
    }
}

using System.Collections;
using System.Collections.Generic;
//using Palmmedia.ReportGenerator.Core;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private AudioClip darboton;
    [SerializeField] private AudioClip abrirmenu;
    public GameObject PauseMenu;  // Referencia al menú de pausa en la UI
    public GameObject SettingsMenu; //Referencia al menu de ajustes
    public GameObject EndMenu;
    //private bool isPaused = false;  // Indica si el juego está en pausa
    public bool PauseActivo = false;
    public bool SettingsActivo = false;
    public bool FinDePartida = false;

    void Start() {
        PauseMenu.SetActive(false);
        SettingsMenu.SetActive(false);
        EndMenu.SetActive(false);
    }

    void Update()
    {
        // Detectar si se presiona la tecla Escape

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!FinDePartida) {
                if (EndMenu.activeSelf) {
                    FinDePartida = true;
                } else {
                    if(PauseActivo && !SettingsActivo){
                        ResumeGame();
                    } else if(!SettingsActivo && !PauseActivo){
                        PauseGame();
                    } else if(SettingsActivo && PauseActivo){
                        ResumeGame();
                    }
                    /*if(AjustesActivo && !MenuActivo){
                        SalirDeAjustes();
                    }*/
                }
            }
        }
    }

    // Método para pausar el juego
    public void PauseGame()
    {
        ControlAudio.Instance.EjecutarSonido(abrirmenu);
        PauseMenu.SetActive(true);  // Mostrar el menú de pausa
        PauseActivo = true;
        Time.timeScale = 0f;   // Detener el tiempo en el juego
        Debug.Log("Juego pausado"); // Mensaje de depuración
    }

    // Método para reanudar el juego
    public void ResumeGame()
    {
        ControlAudio.Instance.EjecutarSonido(darboton);
        PauseMenu.SetActive(false); // Ocultar el menú de pausa
        PauseActivo = false;
        SettingsMenu.SetActive(false);
        SettingsActivo = false;
        Time.timeScale = 1f;   // Reanudar el tiempo en el juego
        Debug.Log("Juego reanudado"); // Mensaje de depuración
    }

    public void SalirDelJuego()
    {
        // Funciona en la versión compilada del juego
        Application.Quit();
        Debug.Log("Juego cerrado"); // Solo para verificar en el editor de Unity
    }

    public void IrAjustes(){
        ControlAudio.Instance.EjecutarSonido(darboton);
        SettingsMenu.SetActive(true);
        PauseMenu.SetActive(false);
        SettingsActivo = true;
        Time.timeScale = 0f;   // Detener el tiempo en el juego
    }

    public void SalirDeAjustes(){
        SettingsMenu.SetActive(false);
        SettingsActivo = false;
        Time.timeScale = 0f;   // Detener el tiempo en el juego
    }

    public void EndGame()
    {
        EndMenu.SetActive(true);  // Mostrar el menú de pausa
        Time.timeScale = 0f;   // Detener el tiempo en el juego

    }

    public void SalirTrasFinDePartida() {
        ControlAudio.Instance.EjecutarSonido(darboton);
        EndMenu.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuInicio");
    }

    public void VolveraJugar(){
        ControlAudio.Instance.EjecutarSonido(darboton);
        EndMenu.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
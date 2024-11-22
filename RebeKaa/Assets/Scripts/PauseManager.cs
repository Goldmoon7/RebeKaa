using System.Collections;
using System.Collections.Generic;
using Palmmedia.ReportGenerator.Core;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public GameObject Menu;  // Referencia al menú de pausa en la UI
    public GameObject AjustesIN; //Referencia al menu de ajustes
    private bool isPaused = false;  // Indica si el juego está en pausa
    private bool MenuActivo = false;
    private bool AjustesActivo = false;

    void Update()
    {
        // Detectar si se presiona la tecla Escape
      if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(MenuActivo && !AjustesActivo){
                ResumeGame();
            }
            /*if(AjustesActivo && !MenuActivo){
                SalirDeAjustes();
            }*/
            if(!AjustesActivo && !MenuActivo){
                PauseGame();
            }
            if(AjustesActivo && MenuActivo){
                ResumeGame();
            }
        }
    }

    // Método para pausar el juego
    public void PauseGame()
    {
        Menu.SetActive(true);  // Mostrar el menú de pausa
        MenuActivo = true;
        Time.timeScale = 0f;   // Detener el tiempo en el juego
        Debug.Log("Juego pausado"); // Mensaje de depuración
    }

    // Método para reanudar el juego
    public void ResumeGame()
    {
        Menu.SetActive(false); // Ocultar el menú de pausa
        MenuActivo = false;
        AjustesIN.SetActive(false);
        AjustesActivo = false;
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
        AjustesIN.SetActive(true);
        AjustesActivo = true;
        Time.timeScale = 0f;   // Detener el tiempo en el juego
    }

    public void SalirDeAjustes(){
        AjustesIN.SetActive(false);
        AjustesActivo = false;
        Time.timeScale = 0f;   // Detener el tiempo en el juego
    }

}
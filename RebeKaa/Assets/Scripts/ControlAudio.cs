using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlAudio : MonoBehaviour
{
    public static ControlAudio Instance;

    // Asignar los diferentes AudioSource en el Inspector
    public AudioSource sonidoAmbiente;
    public AudioSource sonidoFruta, sonidoGreenKaa, sonidoEnemigo, sonidoDaño;

    // Sliders para controlar el volumen
    public Slider volumenFrutasGreenKaa; // Slider para Fruta y GreenKaa
    public Slider volumenEnemigosDaño;   // Slider para Enemigo y Daño
    private void Awake(){
        if(Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(gameObject);
        }
        volumenFrutasGreenKaa.value = 0.359f;
        volumenEnemigosDaño.value = 0.7f;

        volumenFrutasGreenKaa.onValueChanged.AddListener(AjustarVolumenFrutasGreenKaa);
        volumenEnemigosDaño.onValueChanged.AddListener(AjustarVolumenEnemigosDaño);
    }

        // Ajustar el volumen de los sonidos de frutas y GreenKaa
    public void AjustarVolumenFrutasGreenKaa(float volumen){
        sonidoFruta.volume = volumen;
        sonidoGreenKaa.volume = volumen;
    }

    // Ajustar el volumen de los sonidos de enemigos y daño
    public void AjustarVolumenEnemigosDaño(float volumen){
        sonidoEnemigo.volume = volumen;
        sonidoDaño.volume = volumen;
    }

    // Ejecutar sonido de ambiente
    public void EjecutarSonido(AudioClip sonido){
        sonidoAmbiente.PlayOneShot(sonido);
    }

    // Ejecutar sonido de la fruta
    public void EjecutarSonidoFruta(AudioClip sonido){
        sonidoFruta.PlayOneShot(sonido);
    }

    // Ejecutar sonido de GreenKaa
    public void EjecutarSonidoGreenKaa(AudioClip sonido){
        sonidoGreenKaa.PlayOneShot(sonido);
    }

    // Ejecutar sonido de enemigo
    public void EjecutarSonidoEnemigo(AudioClip sonido){
        sonidoEnemigo.PlayOneShot(sonido);
    }

    // Ejecutar sonido de daño
    public void EjecutarSonidoDaño(AudioClip sonido){
        sonidoDaño.PlayOneShot(sonido);
    }
}

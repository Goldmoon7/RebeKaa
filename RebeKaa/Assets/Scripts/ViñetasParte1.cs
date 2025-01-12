using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;  // Para usar el componente Image
using UnityEngine;
using UnityEngine.SceneManagement;

public class ViñetasParte1 : MonoBehaviour
{
    public Image imageUI;  // Referencia al componente Image
    public Sprite[] sprites;  // Array para las imágenes (sprites)
    public Text textUI;  // Referencia al componente Text
    public string[] texts;  // Array de textos
    // public Animator imageAnimator;

    private int currentIndex = 0;  // Índice de la imagen actual

    void Start()
    {
        // Si no se han asignado imágenes en el inspector, las cargamos desde la carpeta "Resources"
        if (sprites.Length == 0)
        {
            sprites = new Sprite[2];
            sprites[0] = Resources.Load<Sprite>("Sprites/Viñeta1.png");
            sprites[1] = Resources.Load<Sprite>("Sprites/Viñeta2.png");
            texts = new string[2];
            texts[0] = "Un día tranquilo en la jungla, Kaa y Rebe se encontraban de paseo cuando de repente, Rebe desaparece a manos de una extraña sombra";
            texts[1] = "En su busca por rescatar a Rebe, Kaa se encuentra en el vasto desierto con animales poco amigables que que quieren enfrentar a ella";
        }
        UpdateImage();  // Actualiza la imagen inicial
        UpdateText();  // Actualiza el texto inicial
    }

    void Update()
    {
        // Detectamos el clic en la pantalla
        if (Input.GetMouseButtonDown(0) && currentIndex < sprites.Length-1)
        {
            // Cambia la imagen al hacer clic
            currentIndex = (currentIndex + 1);  // Cicla a través de las imágenes
            UpdateImage();
            UpdateText();
        }
        else if (Input.GetMouseButtonDown(0) && currentIndex == sprites.Length-1){
            SceneManager.LoadScene("Definitivo");
        }
    }

    void UpdateImage()
    {
        if (imageUI != null && sprites.Length > 0)
        {
            // if(currentIndex == 1){
            //     imageAnimator.SetTrigger("FadeIn");
            // }
            imageUI.sprite = sprites[currentIndex];  // Cambia la imagen del UI
        }
    }
    void UpdateText()
    {
        if (textUI != null && texts.Length > 0)
        {
            textUI.text = texts[currentIndex];  // Cambia el texto del componente UI
        }
    }
}

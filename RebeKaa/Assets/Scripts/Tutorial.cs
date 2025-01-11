using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private Canvas canvas; // Referencia al Canvas
    [SerializeField] private Text texto;

    // Start is called before the first frame update
    void Start()
    {
        canvas.gameObject.SetActive(false);
    }

    public void ShowCanvas()
    {
        canvas.gameObject.SetActive(true); // Activa el Canvas
    }

    // Método para ocultar el Canvas
    public void HideCanvas()
    {
        canvas.gameObject.SetActive(false); // Desactiva el Canvas
    }

     public void UpdateText(string newText)
    {
        if (texto != null)
        {
            texto.text = newText;
        }
        else
        {
            Debug.LogWarning("El componente de texto no está asignado en el CanvasController.");
        }
    }

}

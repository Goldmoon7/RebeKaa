using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reiniciar : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("nivelActual",5);
        SceneManager.LoadScene("MenuInicio");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

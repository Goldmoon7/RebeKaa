using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.WSA;

public class ModoInfinito : MonoBehaviour
{
    static public bool noMorir;
    public Sprite corazonInfinito;
    public Sprite corazonNormal;
    public GameObject prefab_Corazon;
    // Start is called before the first frame update
    void Start()
    {
        noMorir= false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Modo_Infinito(){
        noMorir = !noMorir;
        if(noMorir == false){
            GameObject go = GameObject.FindGameObjectWithTag("Activacion");
            go.GetComponent<Text>().text = "OFF";
            go.GetComponent<Text>().color = Color.red;
        }
        else{
            GameObject go = GameObject.FindGameObjectWithTag("Activacion");
            go.GetComponent<Text>().text = "ON";
            go.GetComponent<Text>().color = new Color(0.0667f, 0.3961f, 0.0157f, 1.0f);
        }
        foreach(GameObject corazon in Snake.hearts){
            if(corazon != null){
                SpriteRenderer spriteRenderer = corazon.GetComponent<SpriteRenderer>();
                if(spriteRenderer != null){
                    spriteRenderer.sprite = noMorir ? corazonInfinito : corazonNormal;
                }
            }
        }
    }
}

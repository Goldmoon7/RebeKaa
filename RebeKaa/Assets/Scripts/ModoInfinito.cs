using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

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

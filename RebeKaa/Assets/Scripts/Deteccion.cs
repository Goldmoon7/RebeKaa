using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deteccion : MonoBehaviour
{ 
   public delegate void ChildEvent();
   public event ChildEvent detEntrada;
   public event ChildEvent detSalida;
   


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    private void OnTriggerEnter2D (Collider2D collider) {
        if (collider.gameObject.CompareTag("Head")) {
            detEntrada?.Invoke();
        }
    }

    private void OnTriggerStay2D (Collider2D collider) {
        if (collider.gameObject.CompareTag("Head")) {
            detEntrada?.Invoke();
        }
    }

    private void OnTriggerExit2D (Collider2D collider) {
        if (collider.gameObject.CompareTag("Head")) {
            detSalida?.Invoke();
        }
    }
    
}

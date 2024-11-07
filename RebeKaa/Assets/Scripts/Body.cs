using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         if (Input.GetKeyDown(KeyCode.M)) {
            Snake.makeSmallerTrigger = 1;
        }
    }

    private void OnTriggerEnter2D (Collider2D collider) {
        if (collider.gameObject.CompareTag("Lagarto") || collider.gameObject.CompareTag("Fenec") || collider.gameObject.CompareTag("Aguila")) {
            Snake.makeSmallerTrigger = 1;
        }
    }
}

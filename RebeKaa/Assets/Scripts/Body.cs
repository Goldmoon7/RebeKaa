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

    }

    private void OnTriggerEnter2D (Collider2D collider) {
        if(Snake.colisionesCuerpo) {
            if (collider.gameObject.CompareTag("Lagarto") || collider.gameObject.CompareTag("Fenec") || collider.gameObject.CompareTag("Aguila")) {
                GestionarColisiones();
                Snake.makeSmallerTrigger = 1;
            }
        }
    }

    private void GestionarColisiones() {
        StartCoroutine(DesactivarColisiones(3));
    }

    private IEnumerator DesactivarColisiones (float tiempo) {
        Snake.colisionesCuerpo = false;
        yield return new WaitForSeconds(tiempo);
        Snake.colisionesCuerpo = true;
    }
}

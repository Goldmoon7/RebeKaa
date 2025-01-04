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
                StartCoroutine(BlinkEnemy(collider.gameObject));
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

    private IEnumerator BlinkEnemy(GameObject enemy) {
        SpriteRenderer sprite = enemy.GetComponent<SpriteRenderer>();
        Color original = sprite.color;
        Color transp = sprite.color;
        transp.a = 0.25f;
        for (int i = 0; i < 3; i++) {
            sprite.color = transp;
            Debug.Log("hemos llegado aqui");
            yield return new WaitForSeconds(0.2f);
            sprite.color = original;
            yield return new WaitForSeconds(0.2f);
        }
    }
}

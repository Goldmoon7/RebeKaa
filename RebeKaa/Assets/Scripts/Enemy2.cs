using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public float speed = 5f;  // Velocidad de movimiento
    public float rotationSpeed = 90f;  // Velocidad de rotación en grados por segundo
    private Vector2 moveDirection;  // Dirección de movimiento en 2D
    private float currentRotation = 0f;  // Ángulo actual de rotación en grados
    private float xBorderLimit, yBorderLimit;
    public float rotationInterval = 2f;  // Intervalo de rotación en segundos
    private float timeSinceLastRotation;
    private float cont = 0; //Contador para cambio de velocidad
    private Deteccion det;
    private int nivelEnemigo = 10;
    private SpriteRenderer sprite;


    void Start()
    {
        // Inicializar la dirección de movimiento 
        moveDirection = Vector2.up;
        // Bordes del mapa
        xBorderLimit = 36;
        yBorderLimit = 17;

        sprite = GetComponent<SpriteRenderer>();

        det = GetComponentInChildren<Deteccion>();
        det.detEntrada += SpriteAColor;
        det.detSalida += SpriteANormal;
        StartCoroutine(Spawn());
    }

    void Update()
    {
        // Mover el enemigo en la dirección de movimiento
        transform.position += (Vector3)moveDirection * speed * Time.deltaTime;
        timeSinceLastRotation += Time.deltaTime;
        if (timeSinceLastRotation >= rotationInterval)
        {
            // Cambio de velocidad
            if(cont%2 == 0){
                speed *= 2;
            }
            else{
                speed /= 2;
            }
            RotateEnemy();
            cont++;
            timeSinceLastRotation = 0f;
        }
        var newPos = transform.position;
        if(newPos.x > xBorderLimit)
        newPos.x = -xBorderLimit+1;
        else if(newPos.x < -xBorderLimit)
        newPos.x = xBorderLimit-1;
        else if(newPos.y > yBorderLimit)
        newPos.y = -yBorderLimit+1;
        else if(newPos.y < -yBorderLimit)
        newPos.y = yBorderLimit-1;
        transform.position = newPos;
    }

    public void ChangeOrientation(int i){
        currentRotation = (i*90f)%360f;
        Vector3 aux = new Vector3(transform.eulerAngles.x,transform.eulerAngles.y, currentRotation);
        Quaternion q = new Quaternion();
        q.eulerAngles = aux;
        transform.rotation = q;
    }
    void RotateEnemy()
    {
        // Rotar al enemigo un ángulo aleatorio
        int randomAngle = UnityEngine.Random.Range(0, 4);
        currentRotation = (randomAngle*90f)%360f;

        // Aplicar la rotación al transform
        ChangeOrientation(randomAngle);

        // Actualizar la dirección de movimiento según el ángulo de rotación actual
        float radians = currentRotation * Mathf.Deg2Rad;
        if(currentRotation == 0){
            moveDirection = new Vector2(0,1);
        }
        else if(currentRotation == 90){
            moveDirection = new Vector2(-1,0);
        }
        else if(currentRotation == 180){
            moveDirection = new Vector2(0,-1);
        }
        else if(currentRotation == 270){
            moveDirection = new Vector2(1,0);
        }
    }

    public void SpriteAColor() {
        //cambiar la animacion a color verde o rojo
        if (Snake.longitud >= nivelEnemigo && Snake.fly) {
            //cambiar a verde
            sprite.color = new Color(0,255,0);
        } else {
            //cambiar a verde
            sprite.color = new Color(255,0,0);
        }
    }

    public void SpriteANormal() {
        //devolver la animacion a color normal
        sprite.color = Color.white;
    }

    private IEnumerator Spawn() {
        this.tag = "Muerto";
        Color original = sprite.color;
        Color transp = sprite.color;
        transp.a = 0.25f;

        for (int i = 0; i < 5; i++) {
            sprite.color = transp;
            yield return new WaitForSeconds(0.2f);
            sprite.color = original;
            yield return new WaitForSeconds(0.2f);
        }
        this.tag = "Aguila";
    }

    public Vector2 GetMoveDirection(){
        return moveDirection;
    }

    public float GetCurrentRotation(){
        return currentRotation;
    }
}

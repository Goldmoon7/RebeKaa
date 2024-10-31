using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : MonoBehaviour
{
    public float speed = 5f;  // Velocidad de movimiento
    public float rotationSpeed = 90f;  // Velocidad de rotación en grados por segundo
    private Vector2 moveDirection;  // Dirección de movimiento en 2D
    private float currentRotation = 0f;  // Ángulo actual de rotación en grados
    private float xBorderLimit, yBorderLimit;
    public float rotationInterval = 2f;  // Intervalo de rotación en segundos
    private float timeSinceLastRotation;
    private SpriteRenderer sprite;
    public Sprite spriteR;
    public Sprite spriteL;


    void Start()
    {
        // Inicializar la dirección de movimiento 
        moveDirection = Vector2.up;
        
        xBorderLimit = 36;
        yBorderLimit = 17;

        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Mover el enemigo en la dirección de movimiento
        transform.position += (Vector3)moveDirection * speed * Time.deltaTime;

        // Ejemplo: rotar el enemigo cuando presionamos la barra espaciadora
        timeSinceLastRotation += Time.deltaTime;
        if (timeSinceLastRotation >= rotationInterval)
        {
            RotateEnemy();
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
    void RotateEnemy()
    {
        // Rotar al enemigo un ángulo aleatorio
        int randomAngle = UnityEngine.Random.Range(0, 3);
        currentRotation = (randomAngle*90f)%360f;

        // Aplicar la rotación al transform

        if (randomAngle == 1){
            sprite.sprite = spriteL;
        }
        if (randomAngle == 3){
            sprite.sprite = spriteR;
        }

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
}

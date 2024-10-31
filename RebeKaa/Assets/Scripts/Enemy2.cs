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


    void Start()
    {
        // Inicializar la dirección de movimiento 
        int rnd = UnityEngine.Random.Range(0, 4);
        switch (rnd){
            case 0:
                moveDirection = Vector2.up;
                changeOrientation(rnd);
                break;
            case 1:
                moveDirection = Vector2.down;
                changeOrientation(rnd);
                break;
            case 2:
                moveDirection = Vector2.left;
                changeOrientation(rnd);
                break;
            case 3:
                moveDirection = Vector2.right;
                changeOrientation(rnd);
                break;
        }

        xBorderLimit = 36;
        yBorderLimit = 17;
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

    void changeOrientation(int i){
        currentRotation = (i*90f)%360f;
        Vector3 aux = new Vector3(transform.eulerAngles.x,transform.eulerAngles.y, currentRotation);
        Quaternion q = new Quaternion();
        q.eulerAngles = aux;
        transform.rotation = q;
    }
    void RotateEnemy()
    {
        // Rotar al enemigo un ángulo aleatorio
        int randomAngle = UnityEngine.Random.Range(0, 3);
        currentRotation = (randomAngle*90f)%360f;
        
        // Mantener la rotación dentro de los 360 grados
        //if (currentRotation >= 360f) currentRotation -= 360f;

        // Aplicar la rotación al transform
        changeOrientation(randomAngle);

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

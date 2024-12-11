using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<<< HEAD:RebeKaa-main/RebeKaa-main/RebeKaa/Assets/Scripts/Enemy3.cs
public class Enemy3 : MonoBehaviour
========
public class Enemy2 : MonoBehaviour
>>>>>>>> 9d011127d844f02c2f9274783f01960ffba2e4c4:RebeKaa-main/RebeKaa-main/RebeKaa/Assets/Scripts/Enemy2.cs
{
    public float speed = 5f;  // Velocidad de movimiento
    public float rotationSpeed = 90f;  // Velocidad de rotación en grados por segundo
    private Vector2 moveDirection;  // Dirección de movimiento en 2D
    private float currentRotation = 0f;  // Ángulo actual de rotación en grados
    private float xBorderLimit, yBorderLimit;
    public float rotationInterval = 2f;  // Intervalo de rotación en segundos
    private float timeSinceLastRotation;
<<<<<<<< HEAD:RebeKaa-main/RebeKaa-main/RebeKaa/Assets/Scripts/Enemy3.cs
    private SpriteRenderer sprite;
    public Sprite spriteR;
    public Sprite spriteL;
========
    private float cont = 0; //Contador para cambio de velocidad
>>>>>>>> 9d011127d844f02c2f9274783f01960ffba2e4c4:RebeKaa-main/RebeKaa-main/RebeKaa/Assets/Scripts/Enemy2.cs


    void Start()
    {
        // Inicializar la dirección de movimiento 
        moveDirection = Vector2.up;
        // Bordes del mapa
        xBorderLimit = 36;
        yBorderLimit = 17;
<<<<<<<< HEAD:RebeKaa-main/RebeKaa-main/RebeKaa/Assets/Scripts/Enemy3.cs
        // Inicializar sprite
        sprite = GetComponent<SpriteRenderer>();
========
>>>>>>>> 9d011127d844f02c2f9274783f01960ffba2e4c4:RebeKaa-main/RebeKaa-main/RebeKaa/Assets/Scripts/Enemy2.cs
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
<<<<<<<< HEAD:RebeKaa-main/RebeKaa-main/RebeKaa/Assets/Scripts/Enemy3.cs
========

    public void ChangeOrientation(int i){
        currentRotation = (i*90f)%360f;
        Vector3 aux = new Vector3(transform.eulerAngles.x,transform.eulerAngles.y, currentRotation);
        Quaternion q = new Quaternion();
        q.eulerAngles = aux;
        transform.rotation = q;
    }
>>>>>>>> 9d011127d844f02c2f9274783f01960ffba2e4c4:RebeKaa-main/RebeKaa-main/RebeKaa/Assets/Scripts/Enemy2.cs
    void RotateEnemy()
    {
        // Rotar al enemigo un ángulo aleatorio
        int randomAngle = UnityEngine.Random.Range(0, 4);
        currentRotation = (randomAngle*90f)%360f;

        // Aplicar la rotación al transform
<<<<<<<< HEAD:RebeKaa-main/RebeKaa-main/RebeKaa/Assets/Scripts/Enemy3.cs
        if (randomAngle == 1){
            sprite.sprite = spriteL;
        }
        if (randomAngle == 3){
            sprite.sprite = spriteR;
        }
========
        ChangeOrientation(randomAngle);
>>>>>>>> 9d011127d844f02c2f9274783f01960ffba2e4c4:RebeKaa-main/RebeKaa-main/RebeKaa/Assets/Scripts/Enemy2.cs

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
<<<<<<<< HEAD:RebeKaa-main/RebeKaa-main/RebeKaa/Assets/Scripts/Enemy3.cs
========
    }

    public Vector2 GetMoveDirection(){
        return moveDirection;
    }

    public float GetCurrentRotation(){
        return currentRotation;
>>>>>>>> 9d011127d844f02c2f9274783f01960ffba2e4c4:RebeKaa-main/RebeKaa-main/RebeKaa/Assets/Scripts/Enemy2.cs
    }
}

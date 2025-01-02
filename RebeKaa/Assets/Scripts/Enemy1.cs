using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy1 : MonoBehaviour{
    public float speed = 5f;  // Velocidad de movimiento
    public GameObject rdPrefab;
    public float rotationSpeed = 90f;  // Velocidad de rotación en grados por segundo
    private Vector2 moveDirection;  // Dirección de movimiento en 2D
    private float currentRotation = 0f;  // Ángulo actual de rotación en grados
    private float xBorderLimit, yBorderLimit;
    public float rotationInterval = 2f;  // Intervalo de rotación en segundos
    private float timeSinceLastRotation;
    private SpriteRenderer sprite;
    private Sprite explosion_muerte;
    public bool muerte_lagarto = false;
    public Animator animator;
    public Sprite spriteR;
    public Sprite spriteL;
    private Deteccion det;
    private int nivelEnemigo = 3;
    //public Sprite normal, verde, rojo;
    //private GameObject Deteccion;


    void Start()
    {
        // Inicializar la dirección de movimiento 
        moveDirection = Vector2.up;
        // Bordes del mapa
        xBorderLimit = 36;
        yBorderLimit = 17;
        //Inicializar el sprite
        sprite = GetComponent<SpriteRenderer>();
        animator= GetComponent<Animator>();

        //Deteccion = Instantiate(rdPrefab,this.transform.position,Quaternion.identity,this.transform);
        det = GetComponentInChildren<Deteccion>();
        det.detEntrada += SpriteAColor;
        det.detSalida += SpriteANormal;
    }

    void Update()
    {
        if (!muerte_lagarto) {
            // Mover el enemigo en la dirección de movimiento
            transform.position += (Vector3)moveDirection * speed * Time.deltaTime;

            // Ejemplo: rotar el enemigo cuando presionamos la barra espaciadora
            timeSinceLastRotation += Time.deltaTime;
            if (timeSinceLastRotation >= rotationInterval)
            {
                RotateEnemy(1);
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
            //radioDeteccion.transform.position = newPos;
            UpdateSpriteDirection();
        }
        animator.SetBool("muerte_lagarto", muerte_lagarto);
    }

    public void RotateEnemy(int spt)
    {
        // Rotar al enemigo un ángulo aleatorio
        int randomAngle = UnityEngine.Random.Range(0, 4);
        currentRotation = (randomAngle*90f)%360f;
        
        //Cambiar sprite dependiendo de la orientación
        if(spt == 1){
            if (randomAngle == 1){
                this.transform.localScale = new Vector3(transform.localScale.x*-1,transform.localScale.y,transform.localScale.z);
                //sprite.sprite = spriteL;
            }
            if (randomAngle == 3){
                this.transform.localScale = new Vector3(Math.Abs(transform.localScale.x),transform.localScale.y,transform.localScale.z);
                //sprite.sprite = spriteR;
            }
        }
        
        // Actualizar la dirección de movimiento según el ángulo de rotación actual
        if(currentRotation == 0){
            moveDirection = new Vector2(0,1);
            UpdateSpriteDirection();
        }
        else if(currentRotation == 90){
            moveDirection = new Vector2(-1,0);
            UpdateSpriteDirection();
        }
        else if(currentRotation == 180){
            moveDirection = new Vector2(0,-1);
            UpdateSpriteDirection();
        }
        else if(currentRotation == 270){
            moveDirection = new Vector2(1,0);
            UpdateSpriteDirection();
        }
    }

    public void SpriteAColor() {
        //cambiar la animacion a color verde o rojo
        if (Snake.longitud < nivelEnemigo) {
            //cambiar a rojo
            animator.SetInteger("color_lagarto", 1);
        } else {
            //cambiar a verde
            animator.SetInteger("color_lagarto", 2);
        }
    }

    public void SpriteANormal() {
        //devolver la animacion a color normal
        animator.SetInteger("color_lagarto", 0);
    }

    public Vector2 GetMoveDirection(){
        return moveDirection;
    }

    public float GetCurrentRotation(){
        return currentRotation;
    }

    private void UpdateSpriteDirection()
    {
        // Determinar el ángulo en función de la dirección de movimiento
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        // Aplicar la rotación al objeto
        if(angle == 0 || angle == -180)
            transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void SetMuerteLagarto(bool estado){
        if(estado){
            this.tag = "Muerto";
            muerte_lagarto = estado;
            speed = 0f;
            Destroy(gameObject,3f);
            animator.SetBool("muerte_lagarto", false);
        }
        /*muerte_lagarto = estado;
        if(muerte_lagarto == true){
            animator.SetBool("muerte_lagarto", true);
            Destroy(gameObject,10f);
        }*/
    }

}

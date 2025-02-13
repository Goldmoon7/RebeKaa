using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float speed = 5f;  // Velocidad de movimiento
    public float rotationSpeed = 90f;  // Velocidad de rotación en grados por segundo
    private Vector2 moveDirection;  // Dirección de movimiento en 2D
    private float currentRotation = 0f;  // Ángulo actual de rotación en grados
    private float xBorderLimit, yBorderLimit;
    public float rotationInterval = 5f;  // Intervalo de rotación en segundos
    public float attackInterval = 3f;  // Intervalo de ataque en segundos

    private float timeSinceLastRotation;
    private float timeSinceLastAttack;

    public static int toques = 10; // Toques necesarios para derrotar al boss
    public Animator animator;
    public bool enLlamas = false;

    public GameObject bolaFuegoPrefab, cola;

    // Start is called before the first frame update
    void Start()
    {
         // Inicializar la dirección de movimiento 
        moveDirection = Vector2.up;
        // Bordes del mapa
        xBorderLimit = 36;
        yBorderLimit = 17;

        animator= GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3)moveDirection * speed * Time.deltaTime;

        // Ejemplo: rotar el enemigo cuando presionamos la barra espaciadora
        timeSinceLastRotation += Time.deltaTime;
        timeSinceLastAttack += Time.deltaTime;

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

        // Cambiar a animacion horizontal
        if (moveDirection.x != 0 && moveDirection.y == 0)
        {
            animator.SetBool("caminando_deLado", true);
        }
        // Cambiar a animacion vertical
        else if (moveDirection.y != 0 && moveDirection.x == 0)
        {
            animator.SetBool("caminando_deLado", false);
        }
            
        // Cambiar a animacion de ataque
        if (timeSinceLastAttack >= attackInterval)
            {
            StartCoroutine(Attack());
            timeSinceLastAttack = 0f;
        }
    }

     private IEnumerator Attack()
    {
        animator.SetBool("isAttacking", true);
        enLlamas = true;

        // Wait for the duration of the attack animation
        yield return new WaitForSeconds(1f); // Adjust this duration as needed

        GameObject bola = Instantiate(bolaFuegoPrefab, cola.transform.position, Quaternion.identity);
        BolaFuego bolaScript = bola.GetComponent<BolaFuego>();
        bolaScript.targetVector = transform.right;

        // Wait for the duration of the attack animation
        yield return new WaitForSeconds(1f); // Adjust this duration as needed

        animator.SetBool("isAttacking", false);
        enLlamas = false;
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

    private void UpdateSpriteDirection()
    {
        // Determinar el ángulo en función de la dirección de movimiento
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        // Aplicar la rotación al objeto
        if(angle == 0 || angle == -180)
            transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void SetMuerteBoss(bool estado){
        if(estado){
            this.tag = "Muerto";
            speed = 0f;
            Destroy(gameObject,3f);
            EnemySpawner.bossMuerto++;
        }
    }
}

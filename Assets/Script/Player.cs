using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidad de movimiento
    private Rigidbody2D rb;
    private float horizontalInput;

    public enum State
    {
        Running,
        Combat,
        Inventory,
    }
    public State stadoPlayer;
    public GameObject canvasInventorio;
    public GameObject canvasRunning;
    public GameObject panelMuerte;
    public GameObject EscenaObj;
    public GameObject EscenaCombate;

    public Image[] vida = new Image[4]; // Array de imágenes de vida

    private int vidaPlayer = 100; // Vida del jugador
    private int vidaMaxima = 100; // Vida máxima del jugador

    void Start()
    {
        // Obtener referencia al componente Rigidbody2D
        rb = GetComponent<Rigidbody2D>();
        stadoPlayer = State.Running; // Inicialmente en estado de carrera
    }

    void Update()
    {
        if (stadoPlayer == State.Running)
        {

            // Captura la entrada horizontal (A/D o Flecha Izquierda/Derecha -> valores entre -1 y 1)
            horizontalInput = Input.GetAxisRaw("Horizontal");
            if (Input.GetKeyDown(KeyCode.I))
            {
                InventoryManager.Instance.MostrarInventario();
                canvasRunning.SetActive(false);
                stadoPlayer = State.Inventory;
            }
        }
        if (stadoPlayer == State.Combat)
        {

            this.GetComponent<Rigidbody2D>().gravityScale = 0; // Detener el movimiento del jugador
            this.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero; // Detener el movimiento del jugador
            this.transform.position = new Vector3(-5.25f, 0, 0); // Mover al jugador a la posición de combate
        }
        if (stadoPlayer == State.Inventory)
        {
            canvasInventorio.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SalirInventario();
            }
        }
    }
    public void SalirInventario()
    {
        stadoPlayer = State.Running;
        canvasInventorio.SetActive(false);
        canvasRunning.SetActive(true);
    }

    void FixedUpdate()
    {
        // Aplicar movimiento físico en el eje X manteniendo la velocidad en Y
        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Colision con objeto: " + other.name);

        if (other.CompareTag("Item"))
        {
            // Obtener el componente Item del objeto que colisionó
            Item item = other.GetComponent<Item>();
            if (item != null)
            {
                // Agregar el objeto al inventario
                InventoryManager.Instance.AgregarItem(item);
                // Destruir el objeto en la escena después de recogerlo
                other.gameObject.SetActive(false); // Desactivar el objeto en lugar de destruirlo
            }

        }
        if (other.CompareTag("Enemy"))
        {
            // Cambiar al estado de combate
            stadoPlayer = State.Combat;

            EscenaCombate.SetActive(true);
            EscenaObj.SetActive(false);
            // Aquí puedes iniciar la lógica de combate, como activar un canvas de combate, etc.
        }
    }
    public void morir()
    {
        panelMuerte.SetActive(true);
        Time.timeScale = 0f; // Pausar el juego
    }

    public void TakeDamage()
    {
        vidaPlayer -= 35;
        if (vidaPlayer <= 0)
        {
            vida[0].enabled = false;
            morir();

            // El jugador ha muerto, puedes manejar la lógica de muerte aquí
            Debug.Log("El jugador ha muerto.");
        }
        else
        {
            if(vidaPlayer > 75)
            {
                vida[0].enabled = true;
                vida[1].enabled = true;
                vida[2].enabled = true;
                vida[3].enabled = true;
            }
            else if (vidaPlayer > 50)
            {
                vida[0].enabled = true;
                vida[1].enabled = true;
                vida[2].enabled = true;
                vida[3].enabled = false;
            }
            else if (vidaPlayer > 25)
            {
                vida[0].enabled = true;
                vida[1].enabled = true;
                vida[2].enabled = false;
                vida[3].enabled = false;
            }
            else
            {
                vida[0].enabled = true;
                vida[1].enabled = false;
                vida[2].enabled = false;
                vida[3].enabled = false;
            }
        }
    }
}

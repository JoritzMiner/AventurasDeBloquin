using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidad de movimiento
    private Rigidbody2D rb;
    private float horizontalInput;

    public enum State
    {
        Running,
        Combat
    }
    public State stadoPlayer;
    void Start()
    {
        // Obtener referencia al componente Rigidbody2D
        rb = GetComponent<Rigidbody2D>();
        stadoPlayer = State.Running; // Inicialmente en estado de carrera
    }

    void Update()
    {
        if(stadoPlayer == State.Running)
        {

            // Captura la entrada horizontal (A/D o Flecha Izquierda/Derecha -> valores entre -1 y 1)
            horizontalInput = Input.GetAxisRaw("Horizontal");
        }
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
                Destroy(other.gameObject);
            }
            other.gameObject.SetActive(false); // Desactivar el objeto en lugar de destruirlo
        }
    }
}

using UnityEngine;

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
            if(Input.GetKeyDown(KeyCode.I))
            {
                InventoryManager.Instance.MostrarInventario();
                canvasRunning.SetActive(false);
                stadoPlayer = State.Inventory;
            }
        }
        if(stadoPlayer == State.Combat)
        {
            // Aquí puedes manejar la lógica de combate
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
    }
}

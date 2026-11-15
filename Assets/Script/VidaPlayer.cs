
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int vida = 200;

    public bool IsAlive
    {
        get { return vida > 0; }
    }

    public void TakeDamage(int amount)
    {
        vida -= amount;

        Debug.Log("El jugador recibe " + amount + " de daño.");

        if (vida <= 0)
        {
            vida = 0;
            Debug.Log("¡El jugador ha sido derrotado!");
        }
    }
}

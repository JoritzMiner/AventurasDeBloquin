
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public string enemyName;
    public int vida = 100;
    public int daño = 10;

    public bool IsAlive
    {
        get { return vida > 0; }
    }

    public void Attack(PlayerHealth player)
    {
        if (!IsAlive) return;

        Debug.Log(enemyName + " está atacando!");

        player.TakeDamage(daño);
    }

    public void TakeDamage(int amount)
    {
        vida -= amount;

        Debug.Log(enemyName + " recibe " + amount + " de daño.");

        if (vida <= 0)
        {
            vida = 0;
            Debug.Log(enemyName + " ha sido derrotado.");
        }
    }
}

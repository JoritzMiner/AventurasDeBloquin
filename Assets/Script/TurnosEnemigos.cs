using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnosEnemigos : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Queue<EnemyController> turnos = new Queue<EnemyController>();
    public Queue<Renderer> colorTurnos = new Queue<Renderer>();

    public EnemyController enemigo1;
    public EnemyController enemigo2;
    public EnemyController enemigo3;
    public Renderer colorEnemigo1;
    public Renderer colorEnemigo2;
    public Renderer colorEnemigo3;
    public float esperaTurnos = 5f;
    public int playerVida;
    public int enemigoVida = 100;
    public PlayerHealth player;

    void Start()
    {
        turnos.Enqueue(enemigo1);
        turnos.Enqueue(enemigo2);
        turnos.Enqueue(enemigo3);

        colorTurnos.Enqueue(colorEnemigo1);
        colorTurnos.Enqueue(colorEnemigo2);
        colorTurnos.Enqueue(colorEnemigo3);

        StartCoroutine(Combate());
    }

    // Update is called once per frame
    IEnumerator Combate ()
    {
        while (colorTurnos.Count > 0 && turnos.Count > 0 && player.IsAlive)
        {
            EnemyController enemigoActual = turnos.Dequeue();
            Renderer enemigoActualColor = colorTurnos.Dequeue();

            enemigoActualColor.material.color = Color.red;
            //enemigoActual.Attack(player);
            yield return new WaitForSeconds(esperaTurnos);
        }
    }
}

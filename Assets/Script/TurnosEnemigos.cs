using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnosEnemigos : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    //public Queue<TurnosEnemigos> turnos = new Queue<TurnosEnemigos>();
    public Queue<Renderer> colorTurnos = new Queue<Renderer>();

    //public TurnosEnemigos enemigo1;
    //public TurnosEnemigos enemigo2;
    //public TurnosEnemigos enemigo3;
    public Renderer colorEnemigo1;
    public Renderer colorEnemigo2;
    public Renderer colorEnemigo3;
    public float esperaTurnos = 5f;
    public Player player;

    void Start()
    {

        //turnos.Enqueue(enemigo1);
        //turnos.Enqueue(enemigo2);
        //turnos.Enqueue(enemigo3);

        colorTurnos.Enqueue(colorEnemigo1);
        colorTurnos.Enqueue(colorEnemigo2);
        colorTurnos.Enqueue(colorEnemigo3);

        StartCoroutine(Combate());
    }
   

    // Update is called once per frame
    IEnumerator Combate ()
    {
        while (colorTurnos.Count > 0)
        {
            Renderer enemigoActual = colorTurnos.Dequeue();
            // Console.WriteLine("enemigo numero");
            enemigoActual.material.color = Color.red;
            HacerDañoAlJugador();

            yield return new WaitForSeconds(esperaTurnos);
            enemigoActual.material.color = Color.white;
        }
       
        
    }

    public void HacerDañoAlJugador()
    {
        if (player != null)
        {
            player.TakeDamage();
        }
    }

}

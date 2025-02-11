using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private float speed = 100f;     //Velocidad de la bala del enemigo

    //Se ejecuta cada frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);  //Mueve la bala hacia adelante según su orientacion y el Time.deltaTime da un movimiento consistente independientemente a los FPS
    }

    //Se ejecuta al crear la bala
    void Start()
    {
        Destroy(gameObject, 5);     //Destruye la bala después de 5 segundos para evitar la acumulación de objetos
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    private float speed = 100f; //Velocidad de la bala

    //Se ejecuta cada frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);  //Mueve la bala hacia adelante según la orientación y Time.deltaTime asegura un movimiento constante independientemente de los FPS
    }

    //Se ejecuta al iniciar el juego
    void Start()
    {
        Destroy(gameObject, 5);     //Destruye la bala después de 5 segundos, evitando la acumulación de objetos
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCanvas : MonoBehaviour
{
   //Se ejecuta cada frame
   void Update()
    {
        transform.LookAt(Camera.main.transform.position);   //Hace que el objeto (en este caso el canvas) siempre mire hacia la cámara principal
    }
}

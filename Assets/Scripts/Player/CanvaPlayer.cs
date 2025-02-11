using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;    //Librería de TextMeshPro

public class CanvaPlayer : MonoBehaviour
{
    //Texto que mostrará el contador de enemigos eliminados
    [SerializeField] 
    private TextMeshProUGUI killCountText;      //Componente de texto usando TextMeshPro
    private int enemiesKilled = 0;              //Variable para llevar la cuenta de enemigos eliminados

    //Se ejecuta al iniciar el juego
    private void Start()
    {
        UpdateDisplay();    //Actualiza el display al inicio
    }

    //Método público para incrementar el contador deenemigos eliminados
    public void AddKill()
    {
        enemiesKilled++;        //Incrementa el contador
        UpdateDisplay();        //Actualiza el display
    }

    //Método para actualizar el texto en pantalla
    private void UpdateDisplay()
    {
        if (killCountText != null)      //Verifica que existe una referencia de texto antes de utilizarlo
        {
            killCountText.text = enemiesKilled.ToString();  //Convierte el número de enemigos eliminados a texto  lo muestra
        }
    }

}

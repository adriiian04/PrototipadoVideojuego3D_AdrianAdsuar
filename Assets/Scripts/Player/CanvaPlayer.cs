using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;    //Librer�a de TextMeshPro

public class CanvaPlayer : MonoBehaviour
{
    //Texto que mostrar� el contador de enemigos eliminados
    [SerializeField] 
    private TextMeshProUGUI killCountText;      //Componente de texto usando TextMeshPro
    private int enemiesKilled = 0;              //Variable para llevar la cuenta de enemigos eliminados

    //Se ejecuta al iniciar el juego
    private void Start()
    {
        UpdateDisplay();    //Actualiza el display al inicio
    }

    //M�todo p�blico para incrementar el contador deenemigos eliminados
    public void AddKill()
    {
        enemiesKilled++;        //Incrementa el contador
        UpdateDisplay();        //Actualiza el display
    }

    //M�todo para actualizar el texto en pantalla
    private void UpdateDisplay()
    {
        if (killCountText != null)      //Verifica que existe una referencia de texto antes de utilizarlo
        {
            killCountText.text = enemiesKilled.ToString();  //Convierte el n�mero de enemigos eliminados a texto  lo muestra
        }
    }

}

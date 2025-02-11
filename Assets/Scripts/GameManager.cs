using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;       //Es necesario para los elementos UI
using UnityEngine.SceneManagement;  //Es necesario para manejar las escenas del juego

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject panelGameOver;       //Panel que se mostrará cuando el jugador pierda
    [SerializeField]
    private SpawnManager enemyManager;      //Referencia al empty donde spawnean los enemigos
   

    public void GameOver()                  //Se muestra cuando el Player es eliminado
    {
        panelGameOver.SetActive(true);              //Muestra el panel de Game Over
        enemyManager.enabled = false;               //Desactiva el spawn de enemigos
        Cursor.lockState = CursorLockMode.Confined; // Libera el cursor del ratón para poder interactuar con la interfaz
    }

    public void LoadSceneLevel()                //Metodo para reiniciar el nivel
    {
        SceneManager.LoadScene("Main");         //Carga la escena principal, es decir reinicia el nivel
    }
}

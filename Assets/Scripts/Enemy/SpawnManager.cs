using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //Variables que están serializadas para poder utilizarlas desde el editor de Unity
    [Header("Posiciones Spawns")]
    [SerializeField]
    private GameObject enemyPrefab;     //Prefab del enemigo que se va a instanciar
    [SerializeField]
    private Transform[] posRotEnemy;    //Array de las posiciones en las que pueden spawnear los enemigos
    [Header("Tiempo entre spawn de enemigos")]  
    [SerializeField]
    private float timeBetweenEnemies = 5.0f;    //Tiempo entre la aparición de cada enemigo
    //Variables de control
    private int enemiesCount = 0;               //Contador de enemigos actuales
    private int maxEnemies = 6;                 //Número máximo de enemigos permitidos
    private float timer = 0f;                   //Temporizador

    void Start()
    {
        //Genera el primer enemigo si no se ha alcanzado el límite
        if (enemiesCount < maxEnemies)
        {
            SpawnEnemy();
        }
    }

    void Update()
    {
        //Incrementa el temporizador
        timer += Time.deltaTime;
        //Verifica si es el momento de generar un enemigo nuevo
        if (timer >= timeBetweenEnemies)
        {
            if (enemiesCount < maxEnemies)
            {
                SpawnEnemy();
            }
            timer = 0f; //Reinicia el temporizador
        }
    }

    private void SpawnEnemy()       //Método para generar un nuevo enemigo
    {
        //Selecciona una posición aleatoria del array de posiciones
        int n = Random.Range(0, posRotEnemy.Length);
        // Instanciamos el enemigo y guardamos la referencia
        GameObject newEnemy = Instantiate(enemyPrefab, posRotEnemy[n].position, posRotEnemy[n].rotation);
        // Obtenemos el componente Enemy y le asignamos este SpawnManager
        Enemy enemyComponent = newEnemy.GetComponent<Enemy>();
        if (enemyComponent != null)
        {
            enemyComponent.SetSpawnManager(this);
        }
        enemiesCount++;     //Incrementa el contador de enemigos
    }

    public void EnemyDestroyed()        //Método que se llama cuando un enemigo es destruido
    {
        enemiesCount--;         //Reduce el contador de enemigos

        if (enemiesCount < maxEnemies)      //Si no se ha alcanzado el límite, genera un nuevo enemigo
        {
            SpawnEnemy();
        }
    }
}

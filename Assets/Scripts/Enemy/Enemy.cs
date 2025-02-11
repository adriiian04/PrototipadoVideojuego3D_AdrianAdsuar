using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("MOVEMENT PLAYER")]
    [SerializeField]
    private int speed = 12;     //Velocidad de movimiento
    [SerializeField]
    private float distanceToPlayer = 6;     //Distancia mínima al jugador
    GameObject player;                      //Referencia al jugador
    [Header("ENEMY ATTACK")]
    [SerializeField]
    private GameObject bulletPrefab;        //Prefab de la bala
    [SerializeField]
    private Transform[] posRotBullet;       //Array con la posición desde donde sale la bala
    [SerializeField]
    private float timeBetweenBullets;       //Tiempo entre disparos
    [SerializeField]
    private AudioSource shootAudio;         //Componente de audio para los disparos
    [Header("HEALTH BAR")]
    private float maxHealth = 100;      //Salud máxima
    private float currentHealth = 100;  //Salud actual
    private float damageBullet = 25;    //Daño causado por el player
    [SerializeField]
    private Image lifeBar;              //Objeto con la barra de salud
    [SerializeField]
    private ParticleSystem smallExplosion, bigExplosion;    //Variables tipo ParticleSystem
    [Header("NUEVO SPAWN ENEMIGOS")]
    private SpawnManager spawnManager;          //Referencia al gestor de spawn

    //Se ejecuta al iniciar el juego
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");        //Busca al player
        shootAudio = GetComponent<AudioSource>();                   //Obtiene el componente de audio
        InvokeRepeating("Attack", 1, timeBetweenBullets);           //Inicia disparos periódicos
        smallExplosion.Stop();                                      //Detiene efectos de partículas
        bigExplosion.Stop();
        currentHealth = maxHealth;                                  //Inicializa la salud
        lifeBar.fillAmount = 1;                                     //Inicializa la barra de salud
    }

    //Sistema de ataque
    private void Attack()
    {
        shootAudio.Play();                                          //Reproduce el sonido de disparo
        for (int i = 0; i < posRotBullet.Length; i++)               //Instancia las balas en la posición configurada
        {
            Instantiate(bulletPrefab, posRotBullet[i].position, posRotBullet[i].rotation);
        }
    }

    //Se ejecuta cada frame
    void Update()
    {
        if (player == null)                                         //Si no hay jugador no hace nada
        {
            return;
        }
        transform.LookAt(player.transform.position);                //Mira hacia el jugador
        FollowPlayer();                                             //Sigue al jugador
    }
    
    //Sistema de persecución
    private void FollowPlayer()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);       //Calcula la distancia al jugador
        if (distance > distanceToPlayer)                                                        //Si está muy lejos, se acerca
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }

    //Detecta colisiones con triggers
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))         //Si colisiona con bala del jugador
        {
            smallExplosion.Play();              //Efecto de impacto
            currentHealth -= damageBullet;      //Reduce salud
            lifeBar.fillAmount = currentHealth / maxHealth;     //Actualiza barra de vida
            Destroy(other.gameObject);          //Destruye la bala
            if (currentHealth <= 0)             //Si no tiene salud
            {
                Death();                        //Muere
            }
        }
    }

    //Sistema de muerte
    private void Death()
    {
        bigExplosion.Play();    //Reproducción partículas
        Destroy(gameObject, 0.5f); //Destruimos al enemigo
    }

    //Configura la referencia al SpawnManager
    public void SetSpawnManager(SpawnManager manager)
    {
        spawnManager = manager;     
    }

    //Se ejecuta cuando el objeto es destruido
    private void OnDestroy()
    {
        if (spawnManager != null)       //Notifica al SpawnManager
        {
            spawnManager.EnemyDestroyed();
        }

        CanvaPlayer counter = FindObjectOfType<CanvaPlayer>();      //Actualiza el contador de enemigos eliminados
        if (counter != null)                
        {
            counter.AddKill();
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //Variables de movimiento
    private int speed = 50;     //Velocidad de movimiento
    private int turnSpeed = 400;    //Velocidad de giro

    //Variables serializadas para poder modificarlas en el inspector
    [SerializeField]
    private GameObject bulletPrefab;    //Prefab de la bala
    [SerializeField]
    private Transform[] posRotBullet; //Array que almacena la posición de spawneo de la bala

    [SerializeField]
    private AudioSource shootAudio; //Componente de audio del disparo

    [Header("Player Health")]
    private float maxHealth = 100;      // Salud máxima
    private float currentHealth = 100;  // Salud actual
    private float damageBullet = 5;     // Daño que provoca la bala

    [SerializeField]
    private Image lifeBar;              //Referencia de la barra de vida en la interfaz


    [Header("FX Damage")]
    [SerializeField]
    private ParticleSystem explosion;   //Sistema de partículas de explosión

    [Header("Game Over")]
    [SerializeField]
    private GameManager gameManager;    // Referencia del GameManager

    //Método que se ejecuta al iniciar el juego, antes que el método Start
    private void Awake()
    {
        shootAudio = GetComponent<AudioSource>();   //Obtiene el componente de audio
        currentHealth = maxHealth;                  //Inicializa la salud
        lifeBar.fillAmount = 1;                     //Inicializa la barra de vida al máximo
        Cursor.lockState = CursorLockMode.Locked;   //Bloquea el cursor en el centro
        explosion.Stop();                           //Detiene el sistema de partículas
    }

    private void Update()
    {
        Attack();       //Maneja los disparos
        Movement();     //Maneja el movimiento
        Turning();      //Maneja el giro
        
        
    }
    
    //Metodo para el disparo
    private void Attack()
    {
        if (Input.GetMouseButtonDown(0))                                    //Si se presiona el botón izquierdo del ratón
        {
            for (int i = 0; i < posRotBullet.Length; i++)                   //Instancia las balas en la posición configurada
            {
                Instantiate(bulletPrefab, posRotBullet[i].position, posRotBullet[i].rotation);  
            }
            
            shootAudio.Play();              //Reproduce el sonido de disparo
        }
        
    }

    //Método para el movimiento
    private void Movement()
    {
        float horizontal = Input.GetAxis("Horizontal");     //Obtiene input horizontal
        float vertical = Input.GetAxis("Vertical");         //Obtiene input vertical
        Vector3 direction = new Vector3(horizontal, 0, vertical);   
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.Self);     //Mueve al jugador en la dirección indicada
    }

    //Método para el giro
    private void Turning()
    {
        //Obtiene el movimiento del ratón
        float xMouse = Input.GetAxis("Mouse X");    
        float yMouse = Input.GetAxis("Mouse Y");
        Vector3 rotation = new Vector3(-yMouse, xMouse, 0);
        transform.Rotate(rotation.normalized * turnSpeed * Time.deltaTime);     //Rota al jugador según el movimiento del ratón
    }

    //Se ejecuta cuando algo entra en el trigger del jugador
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BulletEnemy"))
        {
            currentHealth -= damageBullet;  //Daño causado
            lifeBar.fillAmount = currentHealth / maxHealth; //Cálculo de la relación de salud
            Destroy(other.gameObject);      //Destruimos el proyectil
        }

        if (other.CompareTag("BulletEnemy"))
        {
            explosion.Play();
        }

        if (currentHealth <= 0)         //Comprobamos si hemos muertos
        {
            Death();
        }

    }

    private void Death()
    {
        Camera.main.transform.SetParent(null);      //Antes de destruir el objeto deshacer la jerarquía
        Destroy(gameObject);                        //Camera.main es la cámara con el tag "MainCamera"
        gameManager.GameOver();
    }

}

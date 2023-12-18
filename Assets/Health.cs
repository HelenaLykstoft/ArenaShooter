using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RehtseStudio.SimpleWaveSystem.Managers;


public class Health : MonoBehaviour
{
    private float StartxPosition;
    private float StartyPosition;
    private float StartzPosition;
    public float currentHealth;
    public float maxHealth = 100;

    bool isdie = false;


    //material color
    public Material materialFullHealth;
    public Material materialHalfHealth;
    public Material materialLowHealth;

    private SpawnManager _spawnManager;



    // Start is called before the first frame update
    void Start()
    {
        
        currentHealth = maxHealth;
        StartxPosition = transform.position.x;
        StartyPosition = transform.position.y;
        StartzPosition = transform.position.z;
    }

    private void OnEnable()
    {
        _spawnManager = GameObject.Find("Managers").GetComponent<SpawnManager>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void TakeDamage(float damageTaken)
    {
        currentHealth -= damageTaken;

        if (currentHealth <= 0)
        {
            Die();
        }

    }

    void Die()
    {
        if (gameObject.tag == "Enemy")
        {
            _spawnManager.ObjectWaveCheck();
           
            gameObject.SetActive(false);
        }
        else if (gameObject.tag == "Player")
        {
            if(isdie == false){
            FindObjectOfType<Gameover>().Endgame();
            isdie = true;
            }
            
        }
    }


    //respawn can be as an agument
    public void Respawn()
    {
        currentHealth = maxHealth;
        transform.GetComponent<Renderer>().material = materialFullHealth;
        transform.position = new Vector3(StartxPosition, StartyPosition, StartzPosition);
    }

}

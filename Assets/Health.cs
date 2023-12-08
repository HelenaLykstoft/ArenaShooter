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
    //material color
    public Material materialFullHealth;
    public Material materialHalfHealth;
    public Material materialLowHealth;

    private SpawnManager _spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Health.cs" + gameObject.name);
        currentHealth = maxHealth;
        StartxPosition = transform.position.x;
        StartyPosition = transform.position.y;
        StartzPosition = transform.position.z;
    }

    private void OnEnable()
    {
        _spawnManager = GameObject.Find("Managers").GetComponent<SpawnManager>();
        currentHealth = maxHealth;
        transform.GetComponent<Renderer>().material = materialFullHealth;
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
        //if statment that changes the color of the enemy when it takes damage

        else if (currentHealth <= 50 && currentHealth > 25)
        {
            transform.GetComponent<Renderer>().material = materialHalfHealth;
        }
        else if (currentHealth <= 25 && currentHealth > 0)
        {
            transform.GetComponent<Renderer>().material = materialLowHealth;
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
            
            Respawn();
        }
    }


    //respawn
    public void Respawn()
    {
        currentHealth = maxHealth;
        transform.GetComponent<Renderer>().material = materialFullHealth;
        transform.position = new Vector3(StartxPosition, StartyPosition, StartzPosition);
    }
}

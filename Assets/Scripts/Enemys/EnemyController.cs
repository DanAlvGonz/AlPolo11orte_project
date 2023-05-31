using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    [SerializeField] float health;
    [SerializeField] float maxHealth;
    // Variable para efecto de cambio de color
    [SerializeField] GameObject model;
    //MeshRenderer modelrend;
    [SerializeField] Material original;
    [SerializeField] Material daño;
    [SerializeField] GameObject exploGO;
    [SerializeField] GameObject sparksGO;
    [SerializeField] ParticleSystem explotion;
    [SerializeField] ParticleSystem sparks;
    void Start()
    {
        //modelrend = GetComponent<MeshRenderer>();
        exploGO = GameObject.Find("Explosion");
        sparksGO = GameObject.Find("Sparks");
        explotion = exploGO.GetComponent<ParticleSystem>();
        sparks = sparksGO.GetComponent<ParticleSystem>();
        health = maxHealth;

    }

    // Update is called once per frame
    void Update()
    {
        damaged();
    }

    void damaged()
    {

        if (health <= 0)
        {
            exploGO.transform.position = transform.position;
            explotion.Play();
            gameObject.SetActive(false);

        }
    }

    public void TakeDamage(int damageToTake)
    {
        sparksGO.transform.position = new Vector3(transform.position.x, 2, transform.position.z);
        sparks.Play();
        //Cabe codear cualquier efecto al recibir daño
        health -= damageToTake;
        Invoke("ResetDamage", 0.05f);
    }

    void ResetDamage()
    {
        sparks.Stop();
    }
}


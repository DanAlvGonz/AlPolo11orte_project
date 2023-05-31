using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Codigo_Salud : MonoBehaviour
{
    public float Salud;
    public float SaludMaxima;

    [Header("Interfaz")]
    public Image BarraSalud;
    public Text TextoSalud;
    

    [Header("Muerto")]
    public GameObject Muerto;

    private void Start()
    {
        Salud = SaludMaxima;
    }

    void Update()
    {
        
    }

    public void RecibirCura(float cura)
    {
        Salud += cura;

        if (Salud > SaludMaxima)
        {
            Salud = SaludMaxima;
        }
    }

    public void RecibirDaño(float daño)
    {
       

        if (Salud <= 0)
        {
            Instantiate(Muerto);
            gameObject.SetActive(false);
        }
    }

    void ActualizarInterfaz()
    {
        BarraSalud.fillAmount = Salud / SaludMaxima;
        TextoSalud.text = "+ " + Salud.ToString("f0");
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Bullet")
        {
            Debug.Log("isDestroyed");
            Destroy(this.gameObject);
            SceneManager.LoadScene("Dead Screen");
        }

    }

}


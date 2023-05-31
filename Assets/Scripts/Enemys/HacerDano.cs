using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HacerDano : MonoBehaviour
{
    public float CantidadDaño;
    private void Awake()
    {
        StartCoroutine(waiter());
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<Codigo_Salud>())
        {
            collision.gameObject.GetComponent<Codigo_Salud>().RecibirDaño(CantidadDaño);
            Destroy(gameObject);
        }
    }

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(5);
        Object.Destroy(this.gameObject);
    }
}

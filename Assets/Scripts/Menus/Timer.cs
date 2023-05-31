using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField] int min, seg;
    [SerializeField] Text tiempo;

    private float restante;
    private bool enMarcha;

 


    private void Awake()
    {
        restante = (min * 60) + seg;

        enMarcha = true;
    }



    // Update is called once per frame
    void Update()
    {
        if (enMarcha)
        {
            restante -= Time.deltaTime;


            if (restante < 1)
            {
                enMarcha = false;

                CargaScene();

                Debug.Log("Se acabó el tiempo, has muerto");
                
            }
            int tempMin = Mathf.FloorToInt(restante / 60);
            int tempSeg = Mathf.FloorToInt(restante % 60);
            tiempo.text = string.Format("{00:00}:{01:00}", tempMin, tempSeg);

            void CargaScene()
            {
                SceneManager.LoadScene("Dead Screen");
            }


        }
    }
}

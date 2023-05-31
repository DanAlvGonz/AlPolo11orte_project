using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEffects : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        Invoke("DestroyObject", 2f);

    }

    void DestroyObject()
    {

        gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

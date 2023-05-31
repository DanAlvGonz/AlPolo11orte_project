using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{

    [Header("Camera Settings")]
    public Transform orientation; //Orientación camara
    [SerializeField] float sensibilityX;//Sens. eje X
    [SerializeField] float sensibilityY;//Sens. eje Y
    float rotationX;//Almacén rot. eje
    float rotationY;//Almacén rot. eje
    float inputX;//Almacen input X
    float inputY;//Almacen Input Y


    // Start is called before the first frame update
    void Start()
    {
        //Fijar puntero ratón centro pantalla e invisible
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;


    }

    // Update is called once per frame
    void Update()
    {
        MouseInput();





    }

    void MouseInput()
    {
        //Guarda Input
        inputX = Input.GetAxisRaw("Mouse X") * sensibilityX * Time.deltaTime;
        inputY = Input.GetAxisRaw("Mouse Y") * sensibilityY * Time.deltaTime;

        //Añadir input raton variables rot
        rotationY += inputX;
        rotationX -= inputY;// Ejes X e Y invertidos

        rotationX = Mathf.Clamp(rotationX, -90, 90); //Limita valor Posit. y Neg. del float X

        //Aplicar rotación en si. Se aplica a la camara, y horientación solo arriba y abajo
        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);
        orientation.rotation = Quaternion.Euler(0, rotationY, 0);


    }








}

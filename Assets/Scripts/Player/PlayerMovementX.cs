using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementX : MonoBehaviour
{

    //Variables referencia
    float horInput;
    float verInput;
    Rigidbody rb;
    Vector3 moveDirection;
    public enum MovementState { walking, sprinting, crouching, air }


    [Header("Movement")]
    float speed; //Velocidad normal
    public float walkSpeed; //Vel. andar
    public float sprintSpeed;//Vel. correr
    public float groundDrag;
    [SerializeField] Transform orientation;
    [SerializeField] MovementState currentState; //Estado movim. personaje

    [Header("Jump")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    [SerializeField] bool canJump;

    [Header("Crouching")]
    public float crouchSpeed; //Velocidad agachados
    [SerializeField] float crouchYScale; // Tamaño en Y personaje
    float startYScale; //Tamaño en Y basico

    [Header("Slope Handling")]
    [SerializeField] float slopeDetection; //Medida Raycast rampa
    [SerializeField] float slopeSpeed; //Velocidad en rampa
    RaycastHit slopeHit;
    Vector3 slopeMoveDirection; //Dirección en rampas

    [Header("Ground Check")]
    public float playerHeight;

    [SerializeField] LayerMask groundLayer;
    [SerializeField] bool isGrounded;



    private void Awake()
    {

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        canJump = true;

    }


    // Start is called before the first frame update
    void Start()
    {

        startYScale = transform.localScale.y;



    }

    // Update is called once per frame
    void Update()
    {
        InputPlayer();
        GroundCheck();
        SpeedControl();
        StateHandler();
        slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);
        // En rampa no hay gravedad
        rb.useGravity = !OnSlope();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    void StateHandler()
    {

        //Modo Agacharse
        if (Input.GetKey(KeyCode.LeftControl))
        {

            currentState = MovementState.crouching;
            speed = crouchSpeed;

        }
        //Modo Sprint
        else if (isGrounded && Input.GetKey(KeyCode.LeftShift))
        {
            currentState = MovementState.sprinting;
            speed = walkSpeed;

        }
        //Modo Andar
        else if (isGrounded)
        {
            currentState = MovementState.walking;
            speed = walkSpeed;

        }
        //Modo Aire
        else
        {
            currentState = MovementState.air;
        }

    }


    void InputPlayer()
    {

        //Almacen axis unity
        horInput = Input.GetAxisRaw("Horizontal");
        verInput = Input.GetAxisRaw("Vertical");

        //Input salto
        if (Input.GetKey(KeyCode.Space) && isGrounded && canJump)
        {

            canJump = false;

            //Salto
            Jump();

            Invoke("ResetJump", jumpCooldown);
        }

        //Input crouch
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);

        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
        }


    }

    void GroundCheck()
    {
        //Toca suelo cuando: lanza rayo(origen ray,direc. ray, long. ray, capa toca ray)
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight, groundLayer);
        if (isGrounded)
        {

            rb.drag = groundDrag;

        }
        else
        {

            rb.drag = 0;

        }

    }


    void Movement()
    {

        //Calcular dirección movimiento
        moveDirection = orientation.forward * verInput + orientation.right * horInput;

        //Si estamos en rampa...
        if (isGrounded && !OnSlope())
        {
            //Mov orientado a la rampa
            rb.AddForce(moveDirection.normalized * speed * 10f, ForceMode.Force);

            
        }

        //Si estamos en suelo...
        else if (isGrounded)
        {

            //mov suelo
            rb.AddForce(slopeMoveDirection.normalized * slopeSpeed * 10f, ForceMode.Force);
        }
        else if (!isGrounded)
        {

            //Mov aire

            //Aumentar velocidad
            rb.AddForce(moveDirection.normalized * speed * 10f * airMultiplier, ForceMode.Force);
            //Reducir velocidad


        }

        

    }


    void SpeedControl()
    {

        //Limitar vel rampa
        
        
        
            Vector3 flatVel = new Vector3(rb.velocity.x, 0, rb.velocity.z);

            if (flatVel.magnitude > speed)
            {

                Vector3 limitedVel = flatVel.normalized * speed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);

            }


        


    }



    void Jump()
    {
        
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);


    }

    void ResetJump()
    {

        canJump = true;




    }

    bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight/2 + slopeDetection))
        {
            //Normal no es recta al ser detectado por el Raycast, la normal no es recta, es una rampa
            if (slopeHit.normal != Vector3.up)
            {
                return true;

            }
            else
            {
                // Normal es recta, no es rampa
                return false;
            }
        }
        return false;

    }

  















}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    /*private Vector3 PlayerMovementInput;
    private Vector2 PlayerMouseInput;
    private float xRotation;
    public bool isJumping;

    private readonly string animator_running = "Running";
    private Animation animator;
    private bool isRunning;

    [SerializeField] private Transform playerCamera;
    [SerializeField]private Rigidbody player;
    [Space]
    [SerializeField] private float speed;
    [SerializeField] private float sensitivty;
    [SerializeField] private float JumpForce;

    private void Update()
    {
        PlayerMovementInput=new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        PlayerMouseInput=new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        MovePlayer();
        MovePlayerCamera();
    }
    private void MovePlayer(){
        Vector3 MoveVector= transform.TransformDirection(PlayerMovementInput)*speed;
        player.velocity=new Vector3(MoveVector.x, player.velocity.y, MoveVector.z);

        if(Input.GetButton("Jump"))
        {
            player.AddForce(Vector3.up*JumpForce, ForceMode.Impulse);
        }
    }
    private void MovePlayerCamera(){
        xRotation -= PlayerMovementInput.y * sensitivty;
        transform.Rotate(0f, PlayerMovementInput.x * sensitivty, 0f);
        playerCamera.transform.localRotation=Quaternion.Euler(xRotation,0f,0f); 
    }
    private void UpdateMovement()
    {
        bool running = false;
        
    }
    private void OnCollisionEnter(Collision item)
    {
        if (item.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
            animator.SetBool("IsJumping", false);
            animator.
        }
    }
    private void OnCollisionExit(Collision item)
    {
        if (item.gameObject.CompareTag("Ground"))
        {
            isJumping = true;
            animator.SetBool("IsJumping", true);
        }
    }*/
    /*[SerializeField] private float moveSpeed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float walkSpeed;
    private CharacterController characterController;

    public Transform cameraPosition;
    public float mouseSens;
    public bool invertX;
    public bool invertY;
    private Vector3 moveVector = Vector3.zero;
    public float gravity = 9.8f;


    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }
    /*void Update()
    {
        float moveHr = Input.GetAxis("Horizontal") * moveSpeed;
        float moveVr = Input.GetAxis("Vertical") * moveSpeed;
        moveVector = new Vector3(moveHr, moveVector.y, moveVr);
        if (characterController.isGrounded)
        {
            if (Input.GetButton("Jump"))
            {
                moveVector.y = jumpSpeed;
            }
            else
            {
                moveVector.y = 0f;
            }
        }
        ApplyMovement();

        Vector2 mouseVector = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSens;

        if (invertX)
        {
            mouseVector.x = -mouseVector.x;
        }
        if (invertY)
        {
            mouseVector.y = -mouseVector.y;
        }
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + mouseVector.x, transform.rotation.eulerAngles.z);
        cameraPosition.rotation = Quaternion.Euler(cameraPosition.rotation.eulerAngles + new Vector3(mouseVector.y, 0f, 0f));

    }
    private void ApplyMovement()
    {
        moveVector = transform.TransformDirection(moveVector);
        moveVector.y -= this.gravity * Time.deltaTime;
        characterController.Move(moveVector * Time.deltaTime);
    }*/
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;

    [SerializeField] private bool isGrounded;
    [SerializeField] private float groundCheckDistanse;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravity;

    private CharacterController characterController;
    private Vector3 moveDirection;
    private Vector3 velocity;

    private Animator animator;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }
    private void Update()
    {
        Move();
    }
    private void Move()
    {
        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistanse, groundMask);
        if (isGrounded && velocity.y<0)
        {
            velocity.y = -2f;
        }

        float moveZ = Input.GetAxis("Vertical");

        moveDirection = new Vector3(0, 0, moveZ);
        moveDirection = transform.TransformDirection(moveDirection);

        if (isGrounded)
        {
            if (moveDirection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
            {
                Walk();
            }
            if (moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
            {
                Run();
            }
            else if (moveDirection == Vector3.zero)
            {
                Idle();
            }

            moveDirection *= moveSpeed;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
                animator.SetBool("isJumping", true);
            }
        }
        else OnLanding();

       
        characterController.Move(moveDirection * Time.deltaTime);    
        
        velocity.y+=gravity*Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);    
    }
    void OnLanding()
    {
        animator.SetBool("isJumping", false);
    }
    private void Idle()
    {
        animator.SetFloat("Speed", 0f, 0.1f, Time.deltaTime);
    }
    private void Walk()
    {
        moveSpeed = walkSpeed;
        animator.SetFloat("Speed", 0.5f, 0.1f, Time.deltaTime);
    }
    private void Run()
    {
        moveSpeed = runSpeed;
        animator.SetFloat("Speed", 1, 0.1f, Time.deltaTime);
    }
    private void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
    }
}

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
    private float moveSpeed = 7f;
    private float jumpSpeed = 8f;
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
    void Update()
    {
        //moveVector.x=Input.GetAxis("Horizontal")*moveSpeed * Time.deltaTime;
        //moveVector.z=Input.GetAxis("Vertical")*moveSpeed*Time.deltaTime;

        /*Vector3 moveVr=Input.GetAxis("Vertical")*transform.forward;
        Vector3 moveHr=Input.GetAxis("Horizontal")*transform.right;
        moveVector=moveHr+moveVr;
        moveVector.Normalize();
        moveVector=moveVector * moveSpeed * Time.deltaTime;
        characterController.Move(moveVector);*/
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
    }
}

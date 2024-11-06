using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement Settings")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float sensitivity;
    [SerializeField] private float jumpForce;
    [SerializeField] private GameObject swing;
    [SerializeField] private GameObject LandingSpot;

    [Header("Camera Settings")]
    [SerializeField] private Camera playerCamera;
    [Tooltip("Must be a negative number")]
    [SerializeField] private float cameraMin;
    [Tooltip("Must be a positive number")]
    [SerializeField] private float cameraMax;
    [SerializeField] private Camera secondCamera;

    private float gravity = -9.0f;
    private float cameraRotaion;
    [SerializeField] private bool isSwinging;
    [SerializeField] private bool canSwinging;
    
    private CharacterController playerControler;

    private Vector3 velo;
    private Vector3 playerMoveInput;
    private Vector2 playerMouseInput;

    private void Awake()
    {
        playerControler = GetComponent<CharacterController>();
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        isSwinging = false;
    }
    private void Update()
    {
        if (canSwinging && Input.GetKeyDown(KeyCode.F))
        {
            isSwinging = true;
        }
        if(isSwinging)
        {
            PlayerSwing();
        }
        else
        {
            PlayerMove();
            PlayerCameraMove();
        }
    }
    private void PlayerMove()
    {
        Vector3 moveVec = transform.TransformDirection(playerMoveInput);
        playerMoveInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        if (playerControler.isGrounded)
        {
            velo.y = -1f;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                velo.y = jumpForce;
            }
        }
        else
        {
            velo.y -= gravity * -2f * Time.deltaTime;
        }
        playerControler.Move(moveVec * moveSpeed * Time.deltaTime);
        playerControler.Move(velo * Time.deltaTime);
    }
    private void PlayerSwing()
    {
        float Horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");
        canSwinging = false;

        playerControler.enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;
        playerCamera.enabled = false;
        secondCamera.enabled = true;
        transform.position = LandingSpot.transform.position;

        swing.GetComponent<Rigidbody>().AddForce(transform.forward * Vertical, ForceMode.Acceleration);
        swing.GetComponent<Rigidbody>().AddForce(transform.forward * Horizontal, ForceMode.Acceleration);

        if(isSwinging && Input.GetKeyDown(KeyCode.Space))
        {
            isSwinging = false;
            playerControler.enabled = true;
            GetComponent<MeshRenderer>().enabled = true;
            GetComponent<CapsuleCollider>().enabled = true;
            playerCamera.enabled = true;
            secondCamera.enabled = false;
        }
    }
    private void PlayerCameraMove()
    {
        playerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        cameraRotaion -= playerMouseInput.y * sensitivity;
        cameraRotaion = Mathf.Clamp(cameraRotaion, cameraMin, cameraMax);
        transform.Rotate(0f, playerMouseInput.x * sensitivity, 0f);
        playerCamera.transform.localRotation = Quaternion.Euler(cameraRotaion, 0f, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Swing")
        {
            canSwinging = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Swing")
        {
            canSwinging = false;
        }
    }
}
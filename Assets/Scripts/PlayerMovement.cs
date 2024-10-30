using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement Settings")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float sensitivity;
    [SerializeField] private float jumpForce;

    [Header("Camera Settings")]
    [SerializeField] private Camera playerCamera;
    [Tooltip("Must be a negative number")]
    [SerializeField] private float cameraMin;
    [Tooltip("Must be a positive number")]
    [SerializeField] private float cameraMax;

    private float gravity = -9.0f;
    private float cameraRotaion;
    
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
    }
    private void Update()
    {
        PlayerMove();
        PlayerCameraMove();
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
    private void PlayerCameraMove()
    {
        playerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        cameraRotaion -= playerMouseInput.y * sensitivity;
        cameraRotaion = Mathf.Clamp(cameraRotaion, cameraMin, cameraMax);
        transform.Rotate(0f, playerMouseInput.x * sensitivity, 0f);
        playerCamera.transform.localRotation = Quaternion.Euler(cameraRotaion, 0f, 0f);
    }
}
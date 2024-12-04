using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PickUp : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private Collider objectCollider;
    [SerializeField] private Transform player;
    [SerializeField] private Transform playerCamera;
    [SerializeField] private Transform handContainer;

    [SerializeField] private float pickUpRange;
    [SerializeField] private float dropFowardForce;
    [SerializeField] private float dropUpForce;

    [SerializeField] private bool equipped;
    [SerializeField] private static bool handFull;

    [SerializeField] private TMP_Text pickUpText;
    [SerializeField] private TMP_Text dropText;


    private void Awake()
    {
        pickUpText.enabled = false;
    }

    private void Start()
    {
        if (!equipped)
        {
            rigidBody.isKinematic = false;
            objectCollider.isTrigger = false;
        }
        if (equipped)
        {
            rigidBody.isKinematic = true;
            objectCollider.isTrigger = true;
        }
    }

    private void Update()
    {
        Vector3 distToPlayer = player.position - transform.position;
        if (!equipped && distToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.E) && !handFull)
        {
            PickUpObject();
        }
        if (equipped && Input.GetKeyDown(KeyCode.Q))
        {
            Drop();
        }
    }

    private void PickUpObject()
    {
        equipped = true;
        handFull = true;
        dropText.enabled = true;

        transform.SetParent(handContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        //transform.localScale = Vector3.one;

        rigidBody.isKinematic = true;
        objectCollider.isTrigger = true;
    }
    private void Drop()
    {
        equipped = false;
        handFull = false;
        dropText.enabled = false;

        transform.SetParent(null);

        rigidBody.isKinematic = false;
        objectCollider.isTrigger = false;

        rigidBody.velocity = player.GetComponent<CharacterController>().velocity;

        rigidBody.AddForce(playerCamera.forward * dropFowardForce, ForceMode.Impulse);
        rigidBody.AddForce(playerCamera.up * dropUpForce, ForceMode.Impulse);

        float random = Random.Range(-1f, 1f);
        rigidBody.AddTorque(new Vector3(random, random, random) * 10);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !handFull)
        {
            pickUpText.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            pickUpText.enabled = false;
        }
    }
}

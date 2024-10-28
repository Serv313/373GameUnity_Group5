using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float moveSpeed;
    private CharacterController cc;

    private void Awake()
    {
        cc = gameObject.GetComponent<CharacterController>();
    }

    private void Update()
    {
        
    }

    private void Move()
    {

    }


}

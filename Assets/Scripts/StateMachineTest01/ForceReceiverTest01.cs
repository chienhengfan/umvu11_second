using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceReceiverTest01 : MonoBehaviour
{
    [SerializeField] private CharacterController controller;

    private float verticalVelocity;
    public Vector3 Movement => Vector3.up * verticalVelocity;

    // Update is called once per frame
    void Update()
    {
        if (verticalVelocity < 0f && controller.isGrounded)
        {
            verticalVelocity = Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }
    }
}

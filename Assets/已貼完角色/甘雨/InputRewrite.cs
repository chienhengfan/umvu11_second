using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputRewrite : MonoBehaviour
{
    [Header("Character Input Values")]
    public Vector2 move;
    public Vector2 look;
    public bool jump;
    public bool sprint;

    [Header("Movement Settings")]
    public bool analogMovement = false;

    [Header("Mouse Cursor Settings")]
    public bool cursorLocked = true;
    public bool cursorInputForLook = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }else jump = false;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            sprint = true;
        }else sprint = false;

        float lookX = Input.GetAxis("Mouse X");
        float lookY = -Input.GetAxis("Mouse Y");
        look = new Vector2(lookX, lookY);

        float fMoveY = Input.GetAxis("Vertical");
        float fMoveX = Input.GetAxis("Horizontal");
        move = new Vector2(fMoveX, fMoveY);

        bool newState = cursorLocked;
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    }
}

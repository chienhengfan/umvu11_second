using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPSControl : MonoBehaviour
{
    public CharacterController cc;
    public float moveSpeed = 1.0f;
    public TPSTarget lookTarget;
    public float followDistance;
    public Transform camTransform;
    private Vector3 horizontalDirection;
    private float verticalDegree = 0.0f;
    public float camCtrlSensitive = 1.0f;
    public LayerMask colMasl;
    // Start is called before the first frame update
    void Start()
    {
        horizontalDirection = transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        float fMoveV = Input.GetAxis("Vertical");
        float fMoveH = Input.GetAxis("Horizontal");
        if(fMoveV != 0 || fMoveH != 0)
        {
            Vector3 vCamF = camTransform.forward;
            vCamF.y = 0;
            vCamF.Normalize();

            Vector3 vCamR = camTransform.right;
            Vector3 VecF = fMoveV* vCamF;
            Vector3 VecR = fMoveH* vCamR;
            VecF = VecF + VecR;
            float inputSpeed = VecF.magnitude;
            if(inputSpeed > 1.0)
            {
                inputSpeed = 1.0f;
            }
            transform.forward = Vector3.Lerp(transform.forward, VecF, 1.0f);
            cc.SimpleMove(VecF * inputSpeed * moveSpeed);
        }
        //transform.Rotate(0, fMoveH, 0);
        // cc.SimpleMove(transform.forward*fMoveV * moveSpeed);


        lookTarget.UpdateTarget();
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = -Input.GetAxis("Mouse Y");
        verticalDegree = verticalDegree + mouseY * camCtrlSensitive;
        if (verticalDegree > 60.0f) {
            verticalDegree = 60.0f;
        } else if (verticalDegree < -30.0f) {
            verticalDegree = -30.0f;
        }
        horizontalDirection = Quaternion.Euler(0, mouseX * camCtrlSensitive, 0) * horizontalDirection;
        Vector3 vAxis = Vector3.Cross(Vector3.up, horizontalDirection);
        Vector3 finalVector = Quaternion.AngleAxis(verticalDegree, vAxis) * horizontalDirection;

        RaycastHit rh;
        Vector3 camPos = Vector3.zero;
        if(Physics.SphereCast(lookTarget.transform.position, 0.2f, -finalVector, out rh, followDistance + 0.2f, colMasl))
        //if (Physics.Raycast(lookTarget.transform.position, -finalVector, out rh, followDistance, colMasl))
        {
            camPos = lookTarget.transform.position - finalVector * (rh.distance - 0.1f);
            //camPos = rh.point + finalVector*0.1f;
        } else
        {
            camPos = lookTarget.transform.position - finalVector * followDistance;
        }

        camTransform.position = camPos;
        camTransform.LookAt(lookTarget.transform.position);
    }
}

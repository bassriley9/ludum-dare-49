using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float lookSens;
    public float minLookX;
    public float maxLookX;
    public Transform camAnchor;

    public bool CamInvert;

    private float curXRot;
    //confine cursot to game
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void LateUpdate()
    {
        //mouse x rotation
        float x = Input.GetAxis("Mouse X");
        //mouse y rotation
        float y = Input.GetAxis("Mouse Y");

        //rotate player when moveing mouse on X
        transform.eulerAngles += Vector3.up * x * lookSens;

        //vertical rotation using inverting rotattion
        if (CamInvert)
            curXRot += y * lookSens;
        else
            curXRot -= y * lookSens;

        //clamp curXRot between minx & maxX
        curXRot = Mathf.Clamp(curXRot, minLookX, maxLookX);

        //temp var and assihgn to cur rotation
        Vector3 clampedAng = camAnchor.eulerAngles;
        clampedAng.x = curXRot;
        //up and down rotation
        camAnchor.eulerAngles = clampedAng;
    }
}

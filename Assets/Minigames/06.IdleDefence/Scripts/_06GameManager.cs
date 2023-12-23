using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _06GameManager : MonoBehaviour
{

    public static void MoveCamera(){
        CameraFollow  cam =  FindObjectOfType<CameraFollow>();
        cam.cameraOffset = new Vector3(0,25,10);
        cam.rotationOffsetX = 0;
    }
}

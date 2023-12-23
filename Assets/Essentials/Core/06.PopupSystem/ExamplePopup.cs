using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExamplePopup : MonoBehaviour
{
    PopUpManager man;
    public GameObject capsule;
    private void Awake()
    {
        man = FindObjectOfType<PopUpManager>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        man.TogglePopup("cube fell on me omg");
    }
    public void CapsuleRespawn(){
        capsule.transform.position = new Vector3(capsule.transform.position.x,25,capsule.transform.position.z);
    }
}

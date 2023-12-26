using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _11_SingleObject : MonoBehaviour
{
        public bool getPooled = true;
    public Vector3 destination;
    public float timeTillDestination;
    public float moveSpeed;
    private float defaultSpeed;
    _11_ObjectMover carmoverScript;
    public bool moveRight = true;
    private void Awake() {
        carmoverScript = FindObjectOfType<_11_ObjectMover>();
        defaultSpeed=moveSpeed;
    }
    private void OnEnable() {
        Invoke("ReturnAfterTime", timeTillDestination);
        moveSpeed=defaultSpeed;
        carmoverScript.AddCarToList(this);
    }
    private void OnDisable(){
        carmoverScript.RemoveCarFromList(this);
    }

        private void ReturnAfterTime()
    {
        if (getPooled)
            ObjectPoolManager.ReturnObjectToPool(gameObject);
        else
            Destroy(gameObject);
    }
    public void MoveTowardsDestination()
    {
        // Calculate the direction to move
        Vector3 direction = (destination - transform.position).normalized;
        direction = moveRight? direction : -direction;
        Vector3 translation = new Vector3(direction.x*moveSpeed*Time.deltaTime,0,0);


        // Move the object in the desired direction
        transform.Translate(translation, Space.World);
    }

}

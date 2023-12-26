using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _11_ObjectMover : MonoBehaviour
{
    List<_11_SingleObject> allCars= new List<_11_SingleObject>();
    
    protected virtual void Awake()
    {
        // base.Awake();
            // startPosition = transform.position;
        // StartMovement();
    }

    private void OnDisable() {
        StopAllCoroutines();
    }

    public void AddCarToList(_11_SingleObject car){
        allCars.Add(car);
    }
    public void RemoveCarFromList(_11_SingleObject car){
        allCars.Remove(car);
    }
    private void Update() {
        foreach (_11_SingleObject c in allCars)
        {
            c.MoveTowardsDestination();
        }
        
    }
}

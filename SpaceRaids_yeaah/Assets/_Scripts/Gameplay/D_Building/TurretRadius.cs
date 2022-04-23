using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Donovan Lott

public class TurretRadius : MonoBehaviour
{
    public bool targetInRange;
    public GameObject target;
    public Transform targetTransform;
    
    void Start()
    {
        targetInRange = false;
    }

    private void OnTriggerEnter(Collider c)
    {
        if(gameObject != null)
        {
            if (c.gameObject.tag == "M_BasicEnemy")
            {
                targetInRange = true;
                target = c.gameObject;
                targetInRange = c.gameObject.transform;
            }
        }

    }

    
    void Update()
    {
        
    }
}

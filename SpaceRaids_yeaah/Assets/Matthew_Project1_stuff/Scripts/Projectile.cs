using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //variables set in the inspector

    //speed is magnitude of initial velocity
    public float speed;

    //damage is how much health the bullet takes from its victim
    public float damage;

    public Rigidbody rb;
}

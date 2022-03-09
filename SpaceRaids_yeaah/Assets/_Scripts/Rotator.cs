//Matthew Watson

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [Tooltip("Rotation about the x, y, and z axis.")]
    /// <summary>
    /// Rotation about the x, y, and z axis.
    /// </summary>
    public Vector3 rotationVelocity;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationVelocity * Time.deltaTime);
    }
}

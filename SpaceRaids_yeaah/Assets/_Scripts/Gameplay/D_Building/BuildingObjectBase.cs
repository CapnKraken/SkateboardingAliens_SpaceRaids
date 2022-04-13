using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuildingObjectBase : MonoBehaviour
{
    public abstract float BuildingHealth();
    public abstract float MaterialCost();

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : BuildingObjectBase
{
    public override float BuildingHealth()
    {
        return 100;
    }

    public override float MaterialCost()
    {
        return 10;
    }
}

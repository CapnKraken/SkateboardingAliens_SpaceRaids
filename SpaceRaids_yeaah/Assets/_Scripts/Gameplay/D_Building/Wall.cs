using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : BuildingObjectBase
{
    public override float BuildingHealth()
    {
        return 100;
    }

    public override float MaterialCost()
    {
        return 2;
    }
}

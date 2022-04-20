using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : BuildingObjectBase
{
    public override float MaxHealth()
    {
        return 75;
    }

    public override float BuildingHealth()
    {
        return 75;
    }

    public override float MaterialCost()
    {
        return 2;
    }
}

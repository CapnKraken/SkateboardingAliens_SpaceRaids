using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleObj : ManagedObject
{
    protected override void Initialize()
    {
        //do start method things here
    }

    public override void OnNotify(Category category, string message, string senderData)
    {
        //respond to notifications here
    }

    public override string GetLoggingData()
    {
        //Don't worry about this one. 
        //Theoretically, it has a debugging use
        return name;
    }
}

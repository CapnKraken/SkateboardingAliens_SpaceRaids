//Matthew

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunRotator : ManagedObject
{
    public Vector3 rotationalVelocity;

    public void Update()
    {
        transform.Rotate(rotationalVelocity);
    }

    #region Notifications
    public override string GetLoggingData()
    {
        return name;
    }

    public override void OnNotify(Category category, string message, string senderData)
    {
        
    }
    #endregion

    #region Initialize

    protected override void Initialize()
    {
        
    }

    #endregion
}

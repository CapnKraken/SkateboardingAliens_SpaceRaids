//Matthew

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunRotator : ManagedObject
{
    public float rotationalVelocity;

    //When these numbers are passed
    public float dawnThreshold;
    public float duskThreshold;

    public void Update()
    {
        transform.Rotate(new Vector3(rotationalVelocity * Time.deltaTime, 0, 0));

        float current = transform.rotation.eulerAngles.x;

        //Rollover
        if(current > 360)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }

        if(current > dawnThreshold)
        {
            Notify(Category.GENERAL, "Daybreak");
        }

        if(current > duskThreshold)
        {
            Notify(Category.GENERAL, "Nightfall");
        }
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

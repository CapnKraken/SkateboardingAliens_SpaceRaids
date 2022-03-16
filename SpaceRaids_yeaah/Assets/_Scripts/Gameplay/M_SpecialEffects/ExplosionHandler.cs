using System.Collections;
//Matthew Watson

using System.Collections.Generic;
using UnityEngine;

public class ExplosionHandler : ManagedObject
{
    //The explosion object
    public GameObject explosion;
    protected override void Initialize()
    {

    }

    #region Notifications

    public override void OnNotify(Category category, string message, string senderData)
    {
        if(message.Split()[0] == "SpawnExplosion")
        {
            string[] splitMessage = message.Split();

            //go to the position specified in the message
            Vector3 explosionPos = new Vector3(
                float.Parse(splitMessage[1]),
                float.Parse(splitMessage[2]),
                float.Parse(splitMessage[3])
                );

            GameObject g = Instantiate(explosion, explosionPos, Quaternion.identity);
            Destroy(g, 2f);
        }
    }

    public override string GetLoggingData()
    {
        return name;
    }

    #endregion
}

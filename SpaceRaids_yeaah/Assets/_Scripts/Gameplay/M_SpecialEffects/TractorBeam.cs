//Matthew Watson

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TractorBeam : ManagedObject
{
    private ParticleSystem ps;
    private InputSystem inputs;

    protected override void Initialize()
    {
        ps = GetComponent<ParticleSystem>();
        inputs = GameManager.Instance.inputSystem;
    }

    private void Update()
    {
        //play the particle system when you're harvesting
        if(inputs.harvest == 2)
        {
            ps.Play();
        }

        if(inputs.harvest == 3)
        {
            ps.Stop();
        }
    }

    public override void OnNotify(Category category, string message, string senderData)
    {
        
    }

    public override string GetLoggingData()
    {
        return name;
    }
}

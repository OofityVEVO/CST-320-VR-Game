using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mochi_Standby : StateBase
{
    public override void StartState(MochiManager manager)
    {
        SetPosition(manager.rb);
    }

    public override void UpdateState(MochiManager manager)
    {

    }

    void SetPosition(Rigidbody rb)
    {
        // We can have Mochi rotate so he is looking at the Player
        rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mochi_ChangeBehaviors : MonoBehaviour
{
    public MochiManager mochiManager;

    
    public void Switch_Standby()
    {
        mochiManager.SwitchState(mochiManager.mochiStandby);
    }

    public void Switch_Interact()
    {
        mochiManager.SwitchState(mochiManager.mochiInteract);
    }

    public void Switch_Follow()
    {
        mochiManager.SwitchState(mochiManager.mochiFollow);
    }

}

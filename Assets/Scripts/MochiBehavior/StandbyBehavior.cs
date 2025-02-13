using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

public class StandbyBehavior : MochiBehavior
{
   private Vector3 MochiPosition;
   
    void Start()
    {
        //sets the mochi position to the current position
        SetPosition(transform.position);
        
    }
    void Update()
    {
       
        
        //stops the mochi from moving
        transform.position = MochiPosition;
    }
    public override void SetPosition(Vector3 newPosition)
    {
        MochiPosition = newPosition;
    }

}

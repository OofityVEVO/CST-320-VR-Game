using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onFinish : StateMachineBehaviour
{

   
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //changes the animation to idle after the current animation is finished
        animator.GetComponent<soldierAnimation>().changeAnimation("idle",stateInfo.length);
    }

}

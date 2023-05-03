using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class idleBehavior : StateMachineBehaviour
{
    public float timer;
    public float minTime;
    public float maxTime;
    private int rand;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rand = Random.Range(0, 3);
        timer = Random.Range(minTime, maxTime);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (timer <= 0 && rand == 0)
        {
            animator.SetTrigger("idle");
        }

        else if (timer <= 0 && rand == 1)
        {
            animator.SetTrigger("shoot");
        }
        else if (timer <= 0 && rand == 2)
        {
            animator.SetTrigger("walk");
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }
}

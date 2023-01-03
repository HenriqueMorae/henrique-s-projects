using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bubbleSound : StateMachineBehaviour
{
    public AudioClip bolha1;
    public AudioClip bolha2;
    public AudioClip bolha3;
    public AudioClip bolha4;
    public AudioClip bolha5;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AudioSource bolha = animator.GetComponent<AudioSource>();

        int numero = Random.Range(1, 6);

        switch (numero) {

            case 1: bolha.clip = bolha1; break;
            case 2: bolha.clip = bolha2; break;
            case 3: bolha.clip = bolha3; break;
            case 4: bolha.clip = bolha4; break;
            case 5: bolha.clip = bolha5; break;
        }

        bolha.Play();    
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}

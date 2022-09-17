using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruzMother_IdelBehaviour : StateMachineBehaviour
{

    public GruzMother gruzMother;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        gruzMother = GameObject.FindGameObjectWithTag("GruzMother").GetComponent<GruzMother>();
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        gruzMother.IdelState();
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Change_action : MonoBehaviour
{
    protected Animator animator;
    protected AnimatorOverrideController animatorOverrideController;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
        animatorOverrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = animatorOverrideController;
        animator.runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load(
            "BasicMotions@Walk"
        );
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

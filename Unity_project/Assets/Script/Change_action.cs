using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Change_action : MonoBehaviour
{
    protected Animator animator;
    protected AnimatorOverrideController animatorOverrideController;

    private List<string> animatorsControllers = new List<string>();

    public float RotateSpeed = .5f;
    public float AnimHolding = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        DirectoryInfo direction = new DirectoryInfo("Assets/Resources/Controller");
        FileInfo[] files = direction.GetFiles("*.controller");
        foreach (var file in files) {
            animatorsControllers.Add("Controller/"+file.Name.Split('.')[0]);
        }
        animator = this.GetComponent<Animator>();
        animator.runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load(
            animatorsControllers[0]
        );
        InvokeRepeating("AnimCtrChange", 0f, AnimHolding);
        // // for testing
        // foreach (var j in animatorsControllers){
        //     Debug.Log(j);
        // }
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(Vector3.up * RotateSpeed, Space.Self);
    }

    void AnimCtrChange() {
        string tmp = animatorsControllers[0];
        Debug.Log(tmp);
        animator.runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load(tmp);
        animatorsControllers.RemoveAt(0);
        animatorsControllers.Add(tmp);
    }
}

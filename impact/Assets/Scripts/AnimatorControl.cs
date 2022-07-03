using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorControl : MonoBehaviour
{
    public Animator characterAnimator;


    // Start is called before the first frame update
    void Start()
    {
    
    }

    

    public void playAnimation(string actionName)
    {
        characterAnimator.Play(actionName);
    }
}

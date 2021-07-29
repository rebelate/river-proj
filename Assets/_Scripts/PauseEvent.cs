using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseEvent : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Awake()
    {

        animator = this.gameObject.GetComponent<Animator>();
    }

    public void usePause(int state)
    {
        animator.enabled = state > 0 ? false : true;
    }

}

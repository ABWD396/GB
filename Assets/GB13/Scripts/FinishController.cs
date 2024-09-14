using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishController : MonoBehaviour
{
    Animator animator;
    public LevelController levelController;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Finish()
    {
        animator.SetTrigger("Finish");
    }

    public void EndOfAnimation()
    {
        levelController.NextLevel();
    }
}

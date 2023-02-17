using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterAniamtionController : MonoBehaviour
{
    public bool isSad;
    public bool isRandom;

    public UnityEvent onStart;


    [Space]
    public Animator animator;

    private void Start()
    {
        onStart.Invoke();
    }

    public void ChangeAnimationState(string state)
    {
        animator.SetTrigger(state);
    }

    public void SetPositiveState(bool state)
    {
        GetComponent<CharacterOutfitScript>().isPositive = state;
    }

    public void PlayRandomAnim()
    {
        int value = Random.Range(1, 3);
        animator.SetInteger("value", value);
    }


}

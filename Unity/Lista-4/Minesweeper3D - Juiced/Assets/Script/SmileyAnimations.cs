using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmileyAnimations : MonoBehaviour
{
    private Animator animator;
    public float animationChance = 0.5f;

    public GameHandler gameHandler;

    void Start()
    {
        animator = GetComponent<Animator>();
        
        if (animator == null)
        {
            Debug.LogError("Komponent Animator nie został znaleziony na tym obiekcie.");
        }
    }

    void Update()
    {   
        if(gameHandler != null)
        {
            if(gameHandler.isWin())
            {
                WinAnimation();
                return;
            }
            else if(gameHandler.isLose())
            {
                LoseAnimation();
                return;
            }
        }

        if (Random.Range(0.0f, 100.0f) < animationChance && animator.GetCurrentAnimatorStateInfo(0).IsName("Blinking"))
        {
            PlayRandomAnimation();
        }

        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            IdleAnimation();
            return;
        }

    }

    void IdleAnimation(){
        animator.Play("Blinking");
    }
    
    void WinAnimation(){
        animator.Play("Winning");
    }

    void LoseAnimation(){
        animator.Play("Losing");
    }

    void PlayRandomAnimation()
    {
        string[] otherAnimations = { "Laughing", "LookingAtYou", "Yawing" };
        string randomAnimation = otherAnimations[Random.Range(0, otherAnimations.Length)];
        animator.Play(randomAnimation);
    }
}

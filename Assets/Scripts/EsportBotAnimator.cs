using UnityEngine;

public class EsportBotAnimator : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>(); 
    }

    public void StartTalking()
    {
        animator.SetBool("isTalking", true); 
    }

    public void StopTalking()
    {
        animator.SetBool("isTalking", false); 
    }

    public void PlaySouffleAnimation()
    {
        Debug.Log("Joue l'animation SouffleSheet !"); // Debug
        animator.SetTrigger("Souffle"); // On active le trigger pour lancer l'anim
    }
}
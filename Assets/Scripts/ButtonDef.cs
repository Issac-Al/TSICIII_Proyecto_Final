using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MyButton : Button
{
    private Animator playerAnimator;
    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        if (playerAnimator == null)
            playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        playerAnimator.SetBool("Blocking", true);
        //show text
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        if (playerAnimator == null)
            playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        playerAnimator.SetBool("Blocking", false);
        //hide text
    }
}
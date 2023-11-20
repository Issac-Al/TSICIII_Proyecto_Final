using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private GameObject player;
    public Animator playerAnimator;
    int nextAttack = 0;
    // Start is called before the first frame update
    // Update is called once per frame
    public void Attack()
    {
        if (playerAnimator == null)
            playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        if (nextAttack == 0)
        {
            playerAnimator.SetInteger("Attack", 1);
            nextAttack = 1;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().EnableCollider();
        }
        else
        {
            playerAnimator.SetInteger("Attack", 2);
            nextAttack = 0;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().EnableCollider();

        }
    }

}

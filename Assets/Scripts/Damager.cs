using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    public bool isPlayer;
    public int damageAmount;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Trigger!");
        if(other.gameObject.tag == "Player" && !isPlayer)
        {
            //other.gameObject.GetComponent<PlayerMovement>().GetHurt(damageAmount);
            Debug.Log("PlayerDamaged");
        }
        else
        {
            if(other.gameObject.tag == "Enemy" && isPlayer)
            {
                other.gameObject.GetComponent<BasicEnemy>().GetHurt(damageAmount);
                Debug.Log("EnemyDamaged");
            }
        }
    }
}

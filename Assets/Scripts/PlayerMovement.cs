using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float velocity;
    public Joystick joystick;
    public Animator playerAnimator;
    private float currentVelocity;
    private float smoothTime = 0.05f;
    public Rigidbody playerRb;
    private int currentHP;
    public int totalHP; 
    public Collider weaponCollider;
    // Start is called before the first frame update
    void Start()
    {
        currentHP = totalHP;
        joystick = GameObject.FindGameObjectWithTag("GameController").GetComponent<Joystick>();
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Death();
    }
    private void Movement()
    {
        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;
        Vector3 movementVector = new Vector3(horizontal, transform.position.y, vertical);
        float targetAngle = Mathf.Atan2(horizontal, vertical) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentVelocity, smoothTime);
        playerRb.MovePosition(transform.position + movementVector * velocity * Time.deltaTime);
        if (horizontal == 0 && vertical == 0)
        {
            playerAnimator.SetBool("Running", false);
        }
        else
        {
            playerAnimator.SetBool("Running", true);
            transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
        }
        movementVector = Vector3.zero;
    }

    public void AttackReset()
    {
        playerAnimator.SetInteger("Attack", 0);
    }

    public void DisableCollider()
    {
        weaponCollider.enabled = false;
    }

    public void EnableCollider()
    {
        weaponCollider.enabled = true;
    }

    public void GetHurt(int damage)
    {
        currentHP -= damage;
    }

    private void Death()
    {
        if (currentHP <= 0)
        {
            playerAnimator.SetTrigger("Dead");
        }
    }

}

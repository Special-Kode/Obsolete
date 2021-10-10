﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorPlayerScript: MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;
    Movement movement;
    public bool isMoved;
    public bool isDashed;
    public string whichState;
    private float MouseClickedTime;
    private float touchTime;
    private float ClickDelay;
    private int Clicks;
    private float SecondsToAttack;
    AttackBehaviour attack;
    void Start()
    {
        Clicks = 0;
        ClickDelay = 0.25f;
        animator = GetComponent<Animator>();
        movement = this.GetComponent<Movement>();
        attack = this.GetComponent<AttackBehaviour>();
        Weapon sword = new Weapon("Lapiz", 20, "Sword");
        attack.SetWeapon(sword);
        isMoved = false;
        SecondsToAttack = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if (isDashed == false)
            animator.SetBool("Dash", false);
        if(isMoved==false)
            animator.SetBool("Walking", false);
        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            if (Input.GetMouseButtonDown(0))
            {
                setMovement();
                Clicks++;
                if (Clicks == 1)
                {
                    MouseClickedTime = Time.time;
                    
                }
                    

                else if (Clicks == 2 && (Time.time - MouseClickedTime) < ClickDelay)
                {
                    animator.SetBool("Dash", true);
                    isDashed = true;
                    Clicks = 0;
                    isMoved = false;
                }




            }
            else if (!Input.anyKey)
            {
                if ((Time.time - MouseClickedTime) > ClickDelay && Clicks == 1) { 

                    MouseClickedTime = 0;
                    Clicks = 0;
                    animator.SetBool("Walking", true);
                    isMoved = true;

                }



            }
        }
        else if(SystemInfo.deviceType == DeviceType.Handheld)
        {
            
            if (Input.GetTouch(0).tapCount %2 !=0)
            {
                setMovement();
                touchTime = Time.time;

            }
            else if(Input.GetTouch(0).tapCount % 2 ==0 && (Time.time - touchTime) < 0.25f)
            {
                animator.SetBool("Dash", true);
                isDashed = true;
                Clicks = 0;
                isMoved = false;
            }

            else if(Input.GetTouch(0).phase==TouchPhase.Ended)
            {
                if ((Time.time - touchTime) > 0.25f && Input.GetTouch(0).tapCount % 2 != 0)
                {
                    animator.SetBool("Walking", true);
                    touchTime = 0;
                    isMoved = true;

                }



            }



        }
       

        if (attack.getWeapon().getType() == "Sword")
        {
            
            if (Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Enemie").transform.position) < 1f)
            {
                if (Time.time-SecondsToAttack > 2f || SecondsToAttack == 0)
                {
                    animator.SetBool("Attacking", true);
                    SecondsToAttack = Time.time;
                    isMoved = false;
                    isDashed = false;
                    animator.SetBool("Dash", false);
                }
                    
            }
        }
            





            /*
            if (Input.GetKey(KeyCode.W))
            {

                animator.SetBool("Walking", true);
                animator.SetBool("Up", true);
                animator.SetBool("Down", false);
                isMoved = true;

            }

            if (Input.GetKey(KeyCode.A))
            {
                animator.SetBool("Walking", true);
                animator.SetBool("Left", true);
                animator.SetBool("Right", false);

                isMoved = true;
            }

            if (Input.GetKey(KeyCode.S))
            {
                animator.SetBool("Walking", true);
                animator.SetBool("Down", true);
                animator.SetBool("Up", false);
                isMoved = true;
            }

            if (Input.GetKey(KeyCode.D))
            {

                animator.SetBool("Walking", true);
                animator.SetBool("Right", true);
                animator.SetBool("Left", false);
                isMoved = true;
            }

            if (Input.GetMouseButtonDown(0))
            {
                animator.SetBool("Attacking", true);
                animator.SetBool("Walking", false);
            }
            */
            WhereToLook(movement.screenPos);


        if (this.gameObject.GetComponentInChildren<ExternMechanicsPlayer>().death == true)
        {
            animator.SetBool("Death", true);
            if (animator.GetCurrentAnimatorStateInfo(0).IsTag("DeathTag"))
                UnityEditor.EditorApplication.isPlaying = false;

        }

        
            

        

    }



    void setMovement()
    {
        movement.positionToMove = Input.mousePosition;
        movement.screenPos = Camera.main.ScreenToWorldPoint(new Vector3(movement.positionToMove.x, movement.positionToMove.y, 0));
        movement.direction = movement.screenPos - transform.position;
    }
    void WhereToLook(Vector3 screenPos)
    {

        
        float angle = Mathf.Atan2((screenPos.y - transform.position.y), (screenPos.x - transform.position.x))*Mathf.Rad2Deg;
        if (screenPos.y <= transform.position.y)
        {
            angle += 360;
        }
            
        if (angle <= 22.5f)
        {
            if (animator.GetBool("Walking"))
                animator.SetFloat("BlendWalking", 0.25f);
            else if (!animator.GetBool("Walking") && !animator.GetBool("Attacking"))
                animator.SetFloat("BlendIdle", 0.25f);
            else
                animator.SetFloat("BlendAttacking", 0.25f);
        }
           
        // else if(angle <= 3/8*Mathf.PI && angle >= -Mathf.PI / 8)

        else if(angle<= 112.5f && angle > 67.5f)
        {
            if (animator.GetBool("Walking"))
                animator.SetFloat("BlendWalking", 0f);
            else if (!animator.GetBool("Walking") && !animator.GetBool("Attacking"))
                animator.SetFloat("BlendIdle", 0f);
            else
                animator.SetFloat("BlendAttacking", 0);

        }

        //else if (angle <= 7 / 8 * Mathf.PI && angle > 5 / 8 * Mathf.PI)

        else if (angle <= 202.5f && angle >= 157.5f)
        {
            if (animator.GetBool("Walking"))
                animator.SetFloat("BlendWalking", 0.75f);
            else if (!animator.GetBool("Walking") && !animator.GetBool("Attacking"))
                animator.SetFloat("BlendIdle", 0.75f);
            else
                animator.SetFloat("BlendAttacking", 0.75f);
        }
           
                //else if (angle <= 1 + (3 / 8) * Mathf.PI && angle > 1 + (1 / 8) * Mathf.PI)

        else if (angle <= 292.5f && angle > 247.5f)
        {
            if (animator.GetBool("Walking"))
                animator.SetFloat("BlendWalking", 0.5f);
            else if (!animator.GetBool("Walking") && !animator.GetBool("Attacking"))
                animator.SetFloat("BlendIdle", 0.5f);
            else
                animator.SetFloat("BlendAttacking", 0.5f);
        }
              
        //else if (angle <= 1 + (7 / 8) * Mathf.PI && angle > 1 + (5 / 8) * Mathf.PI)

        //Sea d el segmento ab

        //  if (screenPos.x>0&&screenPos.y>0)
        /*
            this.gameObject.transform.rotation=Quaternion.Euler(0,0,Mathf.Atan2((screenPos.y - transform.position.y), (screenPos.x - transform.position.x)) * Mathf.Rad2Deg);
        */

    }
}

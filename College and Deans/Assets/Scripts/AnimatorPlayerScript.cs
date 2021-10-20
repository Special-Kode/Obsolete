using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorPlayerScript: MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;
    Movement movement;
    GameObject bullet;
    public bool isMoved;
    public bool isDashed;
    private float MouseClickedTime;
    private float touchTime;
    private float ClickDelay;
    private int Clicks;
    private float SecondsToAttack;
    public AttackBehaviour HowToAttack;
    void Start()
    {
        Clicks = 0;
        ClickDelay = 0.25f;
        animator = GetComponent<Animator>();
        movement = this.GetComponent<Movement>();
        bullet = GameObject.FindGameObjectWithTag("Bullet");
        HowToAttack = ScriptableObject.CreateInstance<AttackBehaviour>();
        HowToAttack.init(this,bullet);
        Weapon gun = new Weapon("Lapiz", 20, "Gun");
        HowToAttack.SetWeapon(gun);
        isMoved = false;
        SecondsToAttack = 0;
        
    }

    // Aquí se cambian las variables de estado dependeiendo de el estado al cual se quiere llegar partiendo de un estado específico. También se dispone a ejecutar 
    //las distintas animaciones
    void Update()
    {

        if (isDashed == false)
            animator.SetBool("Dash", false);
        if(isMoved==false)
            animator.SetBool("Walking", false);

        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            HowToAttack.attack(SecondsToAttack, transform.position);
            if(HowToAttack.bulletShooted==false)
                bullet.transform.position = this.transform.position;
            if (Input.GetMouseButtonDown(0))
            {
                if (HowToAttack.ClickedEnemie == false && HowToAttack.bulletShooted == false)
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
                else
                {
                    HowToAttack.ClickedEnemie = false;
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



            



            


        if (this.gameObject.GetComponentInChildren<ExternMechanicsPlayer>().death == true)
        {
            animator.SetBool("Death", true);
           /* if (animator.GetCurrentAnimatorStateInfo(0).IsTag("DeathTag"))
                UnityEditor.EditorApplication.isPlaying = false;*/

        }

        
            

        

    }



    void setMovement()
    {
        movement.positionToMove = Input.mousePosition;
        movement.screenPos = Camera.main.ScreenToWorldPoint(new Vector3(movement.positionToMove.x, movement.positionToMove.y, 0));
        movement.direction = movement.screenPos - transform.position;
    }


    public void SetAttack()
    {
        animator.SetBool("Attacking", true);
        SecondsToAttack = Time.time;
        isMoved = false;
        isDashed = false;
        animator.SetBool("Dash", false);
    }

    public void WhereToLook(Vector3 screenPos)
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

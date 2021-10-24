using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class AttackBehaviour :ScriptableObject
{
    //Aquí están los distintos comportamientos de ataque dependiendo de si el arma es una arma blanca o una arma de fuego.
    private Weapon weapon;
    private AnimatorPlayerScript animatorPlayer;
    private GameObject bullet;
    public bool bulletShooted;
    public bool ClickedEnemy;
    float speedBullet;
    Vector3 EnemyPos;
    public void init(AnimatorPlayerScript animator,GameObject bullet)
    {
        animatorPlayer = animator;
        bulletShooted = false;
        speedBullet = 15f;
        this.bullet = bullet;
        ClickedEnemy = false;
       
    }
    public void SetWeapon(Weapon weapon)
    {
        this.weapon = weapon;
    }
    public Weapon getWeapon()
    {
        return weapon;
    }

    public void attack(float seconds,Vector3 position,Vector3 EnemyPos,int device)
    {
        this.EnemyPos = EnemyPos;
        if (getWeapon().getType() == "Sword")
        {

            if (Vector3.Distance(position, GameObject.FindGameObjectWithTag("Enemy").transform.position) < 1f)
            {
                if (Time.time - seconds > 2f || seconds == 0)
                {
                    animatorPlayer.SetAttack();
                }

            }
        }
        else if (getWeapon().getType() == "Gun")
        {

           bulletShooted = true;
           //animatorPlayer.WhereToLook(Input.mousePosition);
           animatorPlayer.SetAttack();
           shootBullet(position);
                    
           
        }
            
        


    }
    public void moveBullet()
    {
        if (bulletShooted == true)
        {

            bullet.transform.position = Vector2.MoveTowards(new Vector2(bullet.transform.position.x, bullet.transform.position.y),
                new Vector2(EnemyPos.x, EnemyPos.y), speedBullet * Time.deltaTime);


        }
    }

    public void shootBullet(Vector3 playerPos)
    {

        bullet.transform.position = playerPos;

    }
}



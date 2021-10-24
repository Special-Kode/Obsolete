using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collisions : MonoBehaviour
{
    AttackBehaviour attack;
    Movement movement;
    public bool collide = false;
        void OnTriggerEnter2D(Collider2D col)
    {
        if (this.name == "Bullet")
        {
            attack = this.GetComponentInParent<AnimatorPlayerScript>().HowToAttack;
            this.transform.position = GameObject.FindGameObjectWithTag("Player").transform.position;
            attack.bulletShooted = false;
        }
        else if(this.name=="Player" && col.gameObject.tag == "Enemy")
        {
            this.GetComponent<ExternMechanicsPlayer>().damage = true;
        }



    }

   void OnCollisionStay2D(Collision2D other)
    {
        collide = true;


    }


}

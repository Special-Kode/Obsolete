using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collisions : MonoBehaviour
{
    AttackBehaviour attack;

       private void OnTriggerEnter(Collider col)
    {
        if (this.name == "Bullet")
        {
            attack = this.GetComponentInParent<AnimatorPlayerScript>().HowToAttack;
            this.transform.position = GameObject.FindGameObjectWithTag("Player").transform.position;
            attack.bulletShooted = false;
        }
        else if(this.name=="Player" && col.gameObject.name == "Enemie")
        {
            this.GetComponent<ExternMechanicsPlayer>().damage = true;
        }



    }
     void OnCollisionEnter(Collision other)
    {

        if (this.name == "Player" && other.gameObject.name == "Wall")
        {
            this.GetComponent<AnimatorPlayerScript>().isMoved = false;
            this.GetComponent<AnimatorPlayerScript>().isDashed = false;
        }

    }
}

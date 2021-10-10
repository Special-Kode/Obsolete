using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Movement : MonoBehaviour
{
    public int idImage;
    public string movementState;
    private Animator animatorPlayer;
    public bool Up,Right,Left,Down,Attack;
    private Vector3 movement;
    private GameObject player;
    private AnimatorPlayerScript playerScript;
    float offsetx,offsety;
    public Vector3 positionToMove,direction,screenPos;
    float speed;
    float distanceDashed;
    Vector3 InitialPos;
    // Start is called before the first frame update
    void Start()
    {
        player = this.gameObject;
        animatorPlayer = GetComponent<Animator>();
        playerScript = player.GetComponent<AnimatorPlayerScript>();
        movement = new Vector3 (0,0,0);
        offsetx = GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>().size.x/2;
        offsety = GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>().size.y/2 ;
        offsetx=this.gameObject.transform.TransformPoint(offsetx, 0, 0).x;
        offsety=this.gameObject.transform.TransformPoint(offsety, 0, 0).x;
        distanceDashed = 0;
        InitialPos = Vector3.zero;
        //offset = this.gameObject.transform.TransformPoint(offset, 0, 0).x;

    }

    // Update is called once per frame
    void Update()
    {
        if (!animatorPlayer.GetBool("Attacking"))
        {
            Up = animatorPlayer.GetBool("Up");
            Down = animatorPlayer.GetBool("Down");
            Left = animatorPlayer.GetBool("Left");
            Right = animatorPlayer.GetBool("Right");

            if (playerScript.isMoved == true)
                PlayerMoved();
            else if (playerScript.isDashed == true)
                PlayerDashed();
        }
           

        
        
    }


    private void PlayerMoved()
    {
        speed = 5f;
        Vector3 newPosition;
        screenPos.z = 0;
        newPosition=Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), new Vector2(screenPos.x, screenPos.y), speed * Time.deltaTime); ;
        if (applyMovement(newPosition, offsetx, offsety))
            player.transform.position = newPosition;

        /*
        
        if (Up)
        {
            if (Left || Right)
                movement.y = Mathf.Sqrt(0.00125f);
            else
                movement.y = 0.05f;
            player.transform.position=applyMovement(movement, offsetx,offsety);
           

        }
         if (Down)
        {
            if (Left || Right)
                movement.y = -Mathf.Sqrt(0.00125f);
            else
                movement.y = -0.05f;
            player.transform.position = applyMovement(movement, offsetx, offsety);
        }
        if (Left)
        {
            if (Up || Down)
                movement.x = -Mathf.Sqrt(0.00125f);
            else
                 movement.x = -0.05f;
            player.transform.position = applyMovement(movement, offsetx, offsety);

        }
         if (Right)
        {
            if (Up || Down)
                movement.x = Mathf.Sqrt(0.00125f);
            else
                 movement.x = 0.05f;
            player.transform.position = applyMovement(movement, offsetx,offsety);
        }

       */
        if(Vector3.Distance(transform.position,screenPos)<0.05f)
            playerScript.isMoved=false;
    }
    private void PlayerDashed()
    {
       if(InitialPos==Vector3.zero)
            InitialPos = transform.position;
        distanceDashed += Vector3.Distance(player.transform.position, InitialPos);
        speed = 10f;
        Vector3 newPosition;
        screenPos.z = 0;
        newPosition = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), new Vector2(screenPos.x, screenPos.y), speed * Time.deltaTime); ;
        if (applyMovement(newPosition, offsetx, offsety))
            player.transform.position = newPosition;
        if (distanceDashed > 10)
        {
            playerScript.isDashed = false;
            distanceDashed = 0;
            InitialPos = Vector3.zero;
        }
            
    }

    private bool applyMovement(Vector3 newPosition, float offsetx,float offsety)
    {
        if (!this.GetComponent<ExternMechanicsPlayer>().MoveOrNot(newPosition, new Vector3(0, offsety, 0)) &&
            !this.GetComponent<ExternMechanicsPlayer>().MoveOrNot(newPosition, new Vector3(0, -offsety, 0)) &&
            !this.GetComponent<ExternMechanicsPlayer>().MoveOrNot(newPosition, new Vector3(offsetx, 0, 0)) &&
                !this.GetComponent<ExternMechanicsPlayer>().MoveOrNot(newPosition, new Vector3(-offsetx, 0, 0)))
            return true;
        else
            return false;
      
    }
}

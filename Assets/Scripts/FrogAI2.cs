using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogAI2 : Enemy
{
    [SerializeField] private float leftCap;
    [SerializeField] private float rightCap;
    [SerializeField] private float jumpLength = 5f;
    [SerializeField] private float jumpHegiht = 7.5f;
    [SerializeField] private LayerMask ground;
    // private Rigidbody2D rb;
    private Collider2D coll;
    private bool facingLeft = true;
    private enum state {idle,jump};
    

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        coll = GetComponent<Collider2D>();
        // rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetBool("Jumping"))
        {
            if (rb.velocity.y<0.1f)
            {
                anim.SetBool("Falling",true);
                anim.SetBool("Jumping",false);
            }
        }
        else if (rb.IsTouchingLayers(ground)&&anim.GetBool("Falling"))
        {
            anim.SetBool("Falling",false);
            anim.SetBool("Jumping",false);
        }

    }

    private void Move()

    {
        if (facingLeft)
        {
            //make sure sprite is  facing right  locaiton, and if it is not, then face the right direction

            if (transform.position.x > leftCap)
            {
                if (transform.localScale.x != 1)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
                if (coll.IsTouchingLayers(ground))
                {
                    rb.velocity = new Vector2(-jumpLength, jumpHegiht);
                    anim.SetBool("Jumping",true);
                }
            }
            else
            {
                facingLeft = false;
            }
        }
        else
        {
            //make sure sprite is  facing right  locaiton, and if it is not, then face the right direction

            if (transform.position.x < rightCap)
            {
                if (transform.localScale.x != -1)
                {
                    transform.localScale = new Vector3(-1, 1);
                }
                if (coll.IsTouchingLayers(ground))
                {
                    rb.velocity = new Vector2(jumpLength, jumpHegiht);
                    anim.SetBool("Jumping",true);
                }
            }
            else
            {
                facingLeft = true;
            }
        }
    }

    
   
}


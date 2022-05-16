using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator anim;
    
    
    private enum State {idle,running,jumping,falling,hurt};
    private State state = State.idle;
    private Collider2D coll;
    [SerializeField]private int playerSpeed=10;
    [SerializeField]private int playerJumpForce=20;
    [SerializeField]private LayerMask ground;
    [SerializeField]private int gems =0;
    [SerializeField]private Text gemText ;
    [SerializeField]private float hurtForce = 10f;
    [SerializeField]private AudioSource gemSound;   
    [SerializeField]private AudioSource footStep;
    [SerializeField]private AudioSource jump;
    [SerializeField]private AudioSource powerUpSound;
    [SerializeField]private int Health;
    [SerializeField]private Text HealthAmount ; //Can Miktarı


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        HealthAmount.text = Health.ToString();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (state != State.hurt)
        {
             Movment();
            
        }   
       
        AnimationState();
        anim.SetInteger("State", (int)state);
    }

    private void Movment()
    {
        float hDirection = Input.GetAxis("Horizontal");

        if (/*Input.GetKey(KeyCode.A)*/ hDirection < 0)
        {
            rb.velocity = new Vector2(-playerSpeed, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);
            // state = State.running;
            // transform.localScale = new Vector3(-1,1,this.gameObject.transform.position.z+2);
            //transform.position = new Vector3(player.position.x,player.position.y,transform.position.z-10);
            // GetComponent<Animator>();
            // anim.SetBool("Run",true);
        }
        else if (/*Input.GetKey(KeyCode.D)*/ hDirection > 0)
        {
            rb.velocity = new Vector2(playerSpeed * 1, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);
            // state = State.running;
            // transform.localScale = new Vector3(1,1,this.gameObject.transform.position.z+2);
            // anim.SetBool("Run",true);

        }
        // else
        // {
        //     // anim.SetBool("Run",false);
        //     state = State.idle;
        // }
        if (/*Input.GetKeyDown(KeyCode.Space)*/ Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))
        {
            Jump();
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, playerJumpForce);
        state = State.jumping;
        jump.Play();
    }

    

    void AnimationState()
    {
        
        if (state == State.jumping)
        {
            if (rb.velocity.y<0f)
            {
                state = State.falling;
            }
        }
        else if (rb.velocity.y<-2)
        {
            state = State.falling;
        }
        else if (state == State.falling)
        {
            if (coll.IsTouchingLayers(ground))
            {
                state = State.idle;
            }
        }
        else if (state == State.hurt)
        {
            if (Mathf.Abs( rb.velocity.x )< .1f)
            {
                state = State.idle;
            }
        }
        else if (Mathf.Abs(rb.velocity.x)> 2f)
        {
            state = State.running;
        }
        else
        {
            state = State.idle;
        }
        
    }

    private IEnumerator resetPower(){
        yield return new WaitForSeconds(8);
        playerJumpForce = 20;
        GetComponent<SpriteRenderer>().color = Color.white;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        
        if (other.CompareTag("Collectable"))
        {
            gemSound.Play();
           Destroy(other.gameObject); 
           gems++;
           print(gems);
           gemText.text = gems.ToString();
           if (gems>=15&&gems<25)
           {
               gemText.color = Color.yellow;
           }
           else if (gems>=25)
           {
               gemText.color = Color.green;
           }
           
        }

        if (other.CompareTag("PowerUp"))
        {
            Destroy(other.gameObject);
            powerUpSound.Play();
            playerJumpForce = 29;
            GetComponent<SpriteRenderer>().color = Color.yellow;
            
            StartCoroutine(resetPower());
        }
       
        
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Enemy")
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if (state == State.falling)
            {
                // Destroy(other.gameObject);
                // print("Dusman oldu");
                enemy.JumpedOn();
                Jump();
                
            }
            else
            {
                state = State.hurt;
                HandleHealth();
                //düşman sağımda ise
                if (other.gameObject.transform.position.x > transform.position.x)
                {
                    //sola doğru hareket etmem lazım
                    rb.velocity = new Vector2(-hurtForce, rb.velocity.y);
                }
                else //otomatik olarak burasıda düşmanın sağda olduğu durum
                {
                    rb.velocity = new Vector2(hurtForce, rb.velocity.y);
                }
            }

        }
    }

    private void HandleHealth()
    {
        Health--;
        HealthAmount.text = Health.ToString();
        if (Health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void FootStep(){
        footStep.Play();
    }
   

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected AudioSource deathExplosion;
    protected Animator anim;
    protected Rigidbody2D rb;
    
    protected virtual void Start() {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        deathExplosion = GetComponent<AudioSource>();
    }
    public void JumpedOn(){
        anim.SetTrigger("Death");
        rb.velocity = Vector2.zero;  //rb.velocity = new Vector2(0,0); aynı işlevi görüyor
        rb.bodyType = RigidbodyType2D.Static;    
        GetComponent<Collider2D>().enabled = false;
        
    }
     private void Death(){
        Destroy(this.gameObject);
    }

    private void DeathExplosion(){
        deathExplosion.Play();
    }
}

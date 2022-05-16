using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogAI : Enemy
{

    // [SerializeField]private float rightDistance;
    // [SerializeField]private float leftDistance;
    // [SerializeField]private float jumpLength;
    // [SerializeField]private float jumpHegiht;
    // private bool facingLeft = true;
    [SerializeField]private float moveLength;
    
    public int turnDelay;
    private bool facingRight = false;


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        StartCoroutine(SwitchDirections());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right*moveLength*Time.deltaTime);
    }

    IEnumerator SwitchDirections(){
        yield return new WaitForSeconds(turnDelay);
        Switch();
    }
    private void Switch(){
        facingRight =! facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *=-1;
        transform.localScale = scaler;
        moveLength *=-1;
        StartCoroutine(SwitchDirections());
    }
}

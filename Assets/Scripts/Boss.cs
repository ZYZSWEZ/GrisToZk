using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    // Start is called before the first frame update

    private Animator animator;
    private Rigidbody2D rigid2D;
    private Transform grisTrans;
    private float speed;
    private Song song;
    private float timeVal;
    private int hp;
    private LastScript lastScript;
    private bool isDead;
    private SpriteRenderer sr;

    void Start()
    {
        animator = GetComponent<Animator>();
        rigid2D = GetComponent<Rigidbody2D>();
        grisTrans = GameObject.Find("Gris").transform;
        speed = 3;
        song = transform.GetChild(0).GetComponent<Song>();
        //song = transform.GetChild(0).GetComponent<Song>();
        hp = 5;
        lastScript = GameObject.Find("BlackSky").GetComponent<LastScript>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame


    private void FixedUpdate()
    {
        if (isDead)
        {
            return;
        }
        Move();
        timeVal += Time.deltaTime;
    }


    private void Move()
    {
        animator.SetFloat("MoveX",rigid2D.velocity.x);
        animator.SetBool("isGround",true);
        if (hp >= 3)
        {
            animator.SetBool("Walk", true);
        }
        else
        {
            speed = 5;
            animator.SetBool("Walk", false);

        }
        
        float dis = grisTrans.position.x - transform.position.x;
        int dir;
        if (Mathf.Abs(dis) >= (float)10/hp)
        {
            //×·ÖðÖÐ
            if (dis > 0)
            {
                dir = 1;
                sr.flipX = true;
            }
            else
            {
                dir = -1;
                sr.flipY = false;
            }
            Vector2 moveDirection = Vector2.right * dir;

            Vector2 moveVelocity = moveDirection * speed;
            rigid2D.velocity = moveVelocity;
            song.SetSingingState(false);
            animator.SetBool("Sing", false);
            timeVal = 0;
        }
        else 
        {
            if (timeVal >= 0.8f)
            {

                //Ä§Òô¹¥»÷
                song.SetSingingState(true);
                animator.SetBool("Sing", true);
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Song")
        {
            //Debug.Log("ÑªÁ¿-1");
            hp--;

            if (hp <= 0)
            {
                //Debug.Log("BossËÀÍö");
                lastScript.StartLerp(0);
                animator.SetBool("Sing", false);
                animator.Play("Cry");
                isDead = true;
                Destroy(gameObject, 2);
            }
        }
    }


}

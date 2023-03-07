using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gris : MonoBehaviour
{
    // Start is called before the first frame update

    private float moveFactor;
    private float speed ;
    private Rigidbody2D rigidbody2d;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    public float jumpForce;
    private bool isGround;
    private AudioClip jumpClip;
    private float timeVal;
    private float timer;
    private AudioClip moveClip;
    private bool lastIsGround;
    private AudioClip landClip;
    private Song song;
    public List<TearPet> tearList;
    public bool isDead;

    void Start()
    {
        speed = 5;
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        isGround = true;
        jumpForce = 4500;
        jumpClip = Resources.Load<AudioClip>("Gris/Audioclips/Jump");
        moveClip = Resources.Load<AudioClip>("Gris/Audioclips/Move");
        landClip = Resources.Load<AudioClip>("Gris/Audioclips/Land");
        lastIsGround = true;
        song = GetComponentInChildren<Song>();
        tearList = new List<TearPet>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            
            return;
        }
        if (song != null)
        {

            if (Input.GetKey(KeyCode.K) /*&& song.StopSinging*/)
            {
                song.SetSingingState(true);
                moveFactor = 0;
                animator.SetBool("Sing", true);
                return;
                //Debug.LogError("Ssssing");
            }
            else
            {
                //if (Input.GetKeyUp(KeyCode.K))
                
                    
                    
                song.StopSinging = false;
                song.SetSingingState(false);
                animator.SetBool("Sing", false);
                //Debug.LogError("Ssssstop");
            }
           
        }


        moveFactor = Input.GetAxisRaw("Horizontal");


        if (Input.GetButtonDown("Jump")&&isGround)
        {
            rigidbody2d.AddForce(Vector2.up *jumpForce);
            isGround = false;
            lastIsGround = isGround;
            AudioSource.PlayClipAtPoint(jumpClip,transform.position);
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 2;
            timeVal = 1f;
            animator.SetBool("Walk", true);
        }
        else
        {
            speed = 5;
            timeVal = 0.5f;
            animator.SetBool("Walk", false);
        }


    }


    //如果需要持续不断实现某个效果，就得放到fixedUpdate  比如jump就可以放到Update
    private void FixedUpdate()
    {
        if (isDead)
        {
            return;
        }

        //速度小于0，处于在高空的状态
        if (rigidbody2d.velocity.y <= -5f && rigidbody2d.velocity.y >= -7)
        {
            isGround = false;
            lastIsGround = isGround;
        }

        Move();
    }

    private void Move()
    {
        animator.SetBool("isGround",isGround);
        if (moveFactor > 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (moveFactor<0)
        {
            spriteRenderer.flipX = false;
        }
        if (Mathf.Abs(moveFactor) > 0 && isGround)
        {
            if (timer >= timeVal)
            {
                timer = 0;
                AudioSource.PlayClipAtPoint(moveClip, transform.position);
            }
            else
            {
                timer += Time.fixedDeltaTime;
            }
        }
        else
        {
            timer = 0;
        }


        Vector2 moveDirection = Vector2.right* moveFactor;
        Vector2 moveVelocity = moveDirection * speed;
        Vector2 jumpVelocity = new Vector2(0, rigidbody2d.velocity.y);
        animator.SetFloat("MoveY",rigidbody2d.velocity.y);
        rigidbody2d.velocity = moveVelocity+jumpVelocity;
        animator.SetFloat("MoveX",Mathf.Abs(rigidbody2d.velocity.x));


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.ClosestPoint(transform.position).y<transform.position.y)
        {
            //当前碰到了游戏物体，并且是脚步发生了碰撞游戏地面。
            isGround = collision.gameObject.CompareTag("Ground");
            if (isGround != lastIsGround)
            {
                if (isGround)
                {
                    AudioSource.PlayClipAtPoint(landClip, transform.position);
                }
            }

            lastIsGround = isGround;
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "BossSong")
        {
            if (tearList.Count > 0)
            {
                Destroy(tearList[tearList.Count-1].gameObject);
                tearList.RemoveAt(tearList.Count-1);
            }
            else
            {
                animator.Play("Cry");
                isDead = true;
                Invoke("LoadScene",2);


            }
           
        }
        
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(1);
    }

    public void PlaySingAnimation()
    {
        animator.SetBool("Sing", true);
        isDead = true;
        song.SetSingingState(false);
    }


}

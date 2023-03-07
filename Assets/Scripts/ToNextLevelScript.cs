using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToNextLevelScript : MonoBehaviour
{
    // Start is called before the first frame update
    private float speed;
    private Vector3 targetPos;
    private bool startMove;
    private Rigidbody2D rigid2D;
    private Animator animator;

    void Awake()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        
        speed = 2;
        animator = GameObject.Find("Gris").GetComponent<Animator>();
    }

    private void Start()
    {
        GetComponent<SpriteRenderer>().flipX = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (startMove) 
        {
            if (Vector2.Distance(transform.position, targetPos) > 0.01f)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.fixedDeltaTime);
            }
            else
            {
                startMove = false;
            }
        }

    }

    public void StartMove(Vector3 pos)
    {
        startMove = true;
        targetPos = pos;
    }
    


    public void SetRigidBodyType(RigidbodyType2D rigidbodyType2D)
    {
        rigid2D.bodyType = rigidbodyType2D;
    }

    public void PlayAnimation(string stateName)
    {
        animator.Play(stateName);
    }



}

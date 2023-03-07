using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptMove : MonoBehaviour
{
    // Start is called before the first frame update

    private Transform sp1Trans;
    private float moveSpeed = 1.6f;
    private Animator animator;

    void Start()
    {
        sp1Trans = GameObject.Find("Script1").transform;
        animator=GetComponent<Animator>();
        animator.Play("WalkScript");

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position,sp1Trans.position, moveSpeed*Time.deltaTime);
        if (Vector2.Distance(transform.position, sp1Trans.position) <= 0.01f)
        {
            this.enabled = false;
        }
    }
}

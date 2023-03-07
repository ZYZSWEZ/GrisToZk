using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingCarpet : MonoBehaviour
{
    // Start is called before the first frame update

    private bool moveDir;
    private float lerpSpeed;
    private Vector2 startPoint;
    private Vector2 endPoint;
    private SpriteRenderer sr;

    void Start()
    {
        lerpSpeed = 3;
        sr = GetComponent<SpriteRenderer>();
        startPoint = GameObject.Find("startPointt").transform.localPosition;
        endPoint = GameObject.Find("endPointt").transform.localPosition;
        moveDir = true;
        sr.flipX = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (moveDir)
        {
            transform.localPosition = Vector2.MoveTowards(transform.localPosition,endPoint,lerpSpeed*Time.fixedDeltaTime);
            if (Vector2.Distance(transform.localPosition, endPoint) < 0.1f)
            {
                sr.flipX = false;
                moveDir = false;
            }
        }
        else
        {
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, startPoint, lerpSpeed * Time.fixedDeltaTime);
            if (Vector2.Distance(transform.localPosition, startPoint) < 0.1f)
            {
                sr.flipX = true;
                moveDir = true;
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "Gris")
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.name == "Gris")
        {
            collision.transform.SetParent(null);
        }
    }





}

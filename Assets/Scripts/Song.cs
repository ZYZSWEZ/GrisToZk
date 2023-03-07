using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Song : MonoBehaviour
{
    // Start is called before the first frame update
    private float rotateSpeed;
    public bool isSinging;
    private float scaleSpeed;
    private Transform outsideCircle;
    private AudioSource audioSource;
    private CircleCollider2D collider2d;
    private bool stopSinging = true;
    public bool StopSinging { get => stopSinging; set => stopSinging = value; }

    void Start()
    {
        outsideCircle = transform.GetChild(0);
        scaleSpeed = 5;
        rotateSpeed = 120;
        audioSource = GetComponent<AudioSource>();
        collider2d = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotateSpeed * Time.deltaTime * Vector3.forward);
        if (isSinging)
        {
            if (/*!audioSource.isPlaying*/audioSource.volume<=0 || !audioSource.isPlaying)
            {
                audioSource.volume = 1;
                audioSource.Play();
                //Debug.LogError("Sing");
                collider2d.enabled=true;
            }
            if (transform.localScale.x <= 5)
            {
                transform.localScale += scaleSpeed * Time.deltaTime * Vector3.one;
            }
            else
            {
                outsideCircle.gameObject.SetActive(true);
                if (outsideCircle.localScale.x < 1)
                {
                    outsideCircle.localScale += scaleSpeed * 2 * Time.deltaTime * Vector3.one;
                }
            }
        }
        else
        {
            if (collider2d.enabled)
            {
                collider2d.enabled = false;
            }
            
            if (outsideCircle.localScale.x >= 0.8)
            {
                outsideCircle.localScale -= scaleSpeed * 5 * Time.deltaTime * Vector3.one;
            }
            else
            {
                outsideCircle.gameObject.SetActive(false);
                if (transform.localScale.x > 0)
                {
                    audioSource.volume -=  Time.deltaTime;
                    transform.localScale -= scaleSpeed * 0.8f * Time.deltaTime * Vector3.one;
                    if (transform.localScale.x <= 0)
                    {
                        
                        transform.localScale = Vector3.zero ;
                    }
                    if (audioSource.volume > 0)
                    {
                        audioSource.volume -= Time.deltaTime;
                        if (audioSource.volume <= 0)
                        {
                            audioSource.Stop();

                            StopSinging = true;


                        }
                    }

                }
            }
        }

    }

    public void SetSingingState(bool state)
    {
        isSinging = state;
    }

    

}




using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    // Start is called before the first frame update

    public bool bloom;
    private Animator animator;
    private GameObject tearItemGo;
    private AudioClip audioClip;

    void Start()
    {
        tearItemGo = Resources.Load<GameObject>("Prefabs/TearItemPink");
        animator = GetComponent<Animator>();
        audioClip = Resources.Load<AudioClip>("Gris/Audioclips/Bloom");
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Song")
        {
            if (!bloom)
            {
                animator.Play("Bloom");
                bloom = true;
                AudioSource.PlayClipAtPoint(audioClip, transform.position);
                Invoke("CreateTearItem", 1.5f);
            }
        }
        
    }

    private void CreateTearItem() 
    {
        GameObject itemGo =  Instantiate(tearItemGo,transform.position+transform.up,Quaternion.identity);
        itemGo.name = gameObject.name;
        this.enabled = false;
    }

}

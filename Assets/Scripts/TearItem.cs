using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TearItem : MonoBehaviour
{
    // Start is called before the first frame update

    private Transform insideCircleTrans;
    private float rotareSpeed;
    private Transform outsideCircleTrans;
    private float scaleSpeed;
    private SpriteRenderer sr;
    private bool getTear;
    private SpriteRenderer tearSr;
    private Sprite sprite;
    private GameObject tearGo;
    private AudioClip tearClip;
    public bool isPink;

    void Start()
    {
        insideCircleTrans = transform.Find("InsideCircle");
        outsideCircleTrans = transform.Find("OutSideCircle");
        rotareSpeed = 120;
        scaleSpeed = 0.5f;
        sr = outsideCircleTrans.GetComponent<SpriteRenderer>();
        tearSr = GetComponent<SpriteRenderer>();
        sprite = Resources.Load<Sprite>("Gris/Sprites/Item/"+gameObject.name);
        
        if (isPink)
        {
            tearGo = Resources.Load<GameObject>("Prefabs/TearPetPink");
        }
        else
        {
            tearGo = Resources.Load<GameObject>("Prefabs/TearPet");
        }

        tearClip = Resources.Load<AudioClip>("Gris/Audioclips/Tear");
    }

    // Update is called once per frame
    void Update()
    {
        HandleCircle();
        if (getTear)
        {
            tearSr.color -= new Color(0, 0, 0,scaleSpeed/2)*Time.deltaTime;
            if (tearSr.color.a <= 0)
            {
                //收集眼泪完成
                Instantiate(tearGo, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Gris"&&getTear == false)
        {
            Destroy(insideCircleTrans.gameObject);
            getTear = true;
            tearSr.sprite = sprite;
            AudioSource.PlayClipAtPoint(tearClip, transform.position);
        }
    }

    private void HandleCircle()
    {
        if (insideCircleTrans != null)
        {
            insideCircleTrans.Rotate(rotareSpeed * Time.deltaTime * Vector3.forward);
        }
        if (outsideCircleTrans != null)
        {
            outsideCircleTrans.Rotate(rotareSpeed * 2 * Time.deltaTime * Vector3.forward);
            outsideCircleTrans.localScale += scaleSpeed * Time.deltaTime * Vector3.one;
            sr.color -= new Color(0, 0, 0, scaleSpeed) * Time.deltaTime;
            if (sr.color.a <= 0)
            {
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1);

            }
            if (outsideCircleTrans.localScale.x >= 1f)
            {
                outsideCircleTrans.localScale = Vector3.zero;
                if (getTear)
                {
                    Destroy(outsideCircleTrans.gameObject);
                }
            }
        }
    }

}

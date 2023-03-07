using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Red : MonoBehaviour
{
    // Start is called before the first frame update

    private SpriteRenderer[] srs;
    private float scaleSpeed;
    private SpriteRenderer sr;
    

    void Start()
    {
        srs = GetComponentsInChildren<SpriteRenderer>();
        scaleSpeed = 0.3f;
        Vector3 position = transform.position;
        transform.position = new Vector3(311, 3, 0);
        sr = GameObject.Find("GradientBGG").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale += scaleSpeed * Time.deltaTime*Vector3.one;
        for (int i = 0; i < srs.Length; i++)
        {
            srs[i].color -= new Color(0,0,0,scaleSpeed*1.3f)*Time.deltaTime;
        }

        sr.color -= new Color(0, 0, 0, scaleSpeed) * Time.deltaTime;


        if (srs[0].color.a<=0)
        {
            Destroy(gameObject);
        }



    }
}

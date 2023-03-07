using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TearPet : MonoBehaviour
{
    // Start is called before the first frame update

    private Transform targetTran;
    private float speed;
    private float shakeDistance;
    private float timer;
    private float timeVal;
    private bool startShake;

    void Start()
    {
        speed = 3f;
        shakeDistance = 0.02f;
        timeVal = 0.1f;
    Transform grisTran = GameObject.Find("Gris").transform;
        for (int i = 0; i < grisTran.childCount; i++)
        {
            grisTran.GetChild(i);
            //找到没有被占据的位置
            if (grisTran.GetChild(i).childCount <=0)
            {
                targetTran = grisTran.GetChild(i);
                GameObject go = new GameObject(); 
                go.transform.SetParent(targetTran);
                break;
            }

        }

        grisTran.GetComponent<Gris>().tearList.Add(this);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (startShake)
        {
            PetShake();
        }
        else 
        {
            PetMove();
        }
       
        
    }

    private void PetMove()       
    {
        if (Vector2.Distance(transform.position, targetTran.position) > 0.01f)
        {
            transform.position = Vector2.Lerp(transform.position, targetTran.position, Time.fixedDeltaTime * speed);
        }
        else 
        {
            startShake = true; 
        }

    }

    private void PetShake()
    {
        if (Vector2.Distance(transform.position, targetTran.position) > 0.03f)
        {
            startShake = false;
        }
        else
        {
            if (timer >= timeVal)
            {
                timer = 0;
                transform.position = targetTran.position + Mathf.PingPong(Time.time, shakeDistance) * Vector3.one;
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
       
    }



}

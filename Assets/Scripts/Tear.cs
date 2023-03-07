using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tear : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform[] roadTrans;
    private int index;
    public int finalIndex;
    public Stone stone;
    private bool notTargetMove;
    private float moveSpeed;
    private float rotateAngle;

    void Start()
    {
        moveSpeed = 8;
        rotateAngle = 30;
    }



    // Update is called once per frame
    void Update()
    {
        if (notTargetMove)
        {
            //沿着某一个方向移动
            if (stone.stopTearNum >= 5)
            {

                transform.Translate(transform.right * Time.deltaTime * moveSpeed);
            }
        }
        else 
        {
            //朝着某一个固定目标点移动
            if (index == finalIndex)
            {
                if (Vector2.Distance(transform.position, roadTrans[finalIndex].position) <= 0.1f)
                {
                    stone.stopTearNum++;
                    notTargetMove = true;
                    transform.Rotate(new Vector3(0, 0, rotateAngle*finalIndex));
                    Destroy(gameObject, 15);
                    return;
                }
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, roadTrans[index + 1].position, 4f*Time.deltaTime);
                if (Vector2.Distance(transform.position, roadTrans[index + 1].position) <= 0.1f)
                {
                    index++;
                }
            }
        }
    }
}

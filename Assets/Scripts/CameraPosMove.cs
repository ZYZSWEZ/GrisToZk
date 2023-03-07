using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosMove : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 targetPos;
    private bool startPosLerp;
    private float lerpSpeed;

    void Start()
    {
        lerpSpeed = 1f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (startPosLerp)
        {
            if (Vector3.Distance(transform.localPosition, targetPos) > 0.1f)
            {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPos, lerpSpeed*10 * Time.fixedDeltaTime);
            }
            else
            {
                startPosLerp = false;
            }
        }

    }

    public void SetPos(Vector3 pos)
    {
        startPosLerp = true;
        targetPos = pos;
    }

}

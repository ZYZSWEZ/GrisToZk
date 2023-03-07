using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChangeArea : MonoBehaviour
{


    public Vector3 pos;
    public float size;
    public Color color;


    private CameraCtroller cc;


    public bool isFirstLevel;
    private CameraPosMove cpm;


    private void Start()
    {
        cc = Camera.main.GetComponent<CameraCtroller>();
        cpm = GameObject.Find("CameraTargetPos").GetComponent<CameraPosMove>();
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Gris")
        {
            if (pos != Vector3.zero)
            {
                cpm.SetPos(pos);
            }
            if (size != 0)
            {
                cc.SetSize(size);
            }
            if (color != Color.clear)
            {
                cc.SetColor(color);
            }

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Gris")
        {
            if (isFirstLevel)
            {
                //恢复到摄像机默认位置
                cpm.SetPos(new Vector3(-13.4f, 7.2f, 10f));
                cc.SetSize(5);
            }
            else
            {
                //恢复到摄像机默认位置
                cpm.SetPos(new Vector3(14.6f, 7.2f, 10f));
                cc.SetSize(5);
            }
        }
    }




}

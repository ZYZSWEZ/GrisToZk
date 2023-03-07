using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossArea : MonoBehaviour
{
    // Start is called before the first frame update

    private Gris gris;
    private LastScript lastScript;

    private CameraCtroller cc;


    public bool isFirstLevel;
    private CameraPosMove cpm;


    private void Start()
    {
        cc = Camera.main.GetComponent<CameraCtroller>();
        cpm = GameObject.Find("CameraTargetPos").GetComponent<CameraPosMove>();
        gris = GameObject.Find("Gris").GetComponent<Gris>();
        lastScript = GameObject.Find("BlackSky").GetComponent<LastScript>();
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Gris") 
        {
            if (gris.tearList.Count >= 5)
            {
                Debug.Log("aaa");
                lastScript.StartLerp(1);
                this.enabled = false;
            }


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

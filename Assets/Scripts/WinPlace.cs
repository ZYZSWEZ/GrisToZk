using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinPlace : MonoBehaviour
{
    

    private Transform grisTrans;
    private AudioClip audioClipToNextLevel;
    private AudioClip audioClipNormal;
    private AudioClip audioClipJudge;
    private AudioSource audioSource;
    private AsyncOperation ao;
    private CameraCtroller cc;
    private RenderColor rc;
    private Gris gris;
    private ToNextLevelScript LevelScript;
    private GameObject birds;
    private CameraPosMove cpm;

    void Start()
    {

        grisTrans = GameObject.Find("Gris").transform;
        audioClipToNextLevel = Resources.Load<AudioClip>("Gris/Audioclips/BG4");
        audioClipJudge = Resources.Load<AudioClip>("Gris/Audioclips/BG3");
        audioClipNormal = Resources.Load<AudioClip>("Gris/Audioclips/BG2");
        audioSource = GameObject.Find("Evn").GetComponent<AudioSource>();
        cc = Camera.main.GetComponent<CameraCtroller>();
        rc = GameObject.Find("RenderColors").GetComponent<RenderColor>();
        gris =grisTrans.GetComponent<Gris>();
        LevelScript = grisTrans.GetComponent<ToNextLevelScript>();
        ao = SceneManager.LoadSceneAsync(1);
        ao.allowSceneActivation = false;
        birds = Resources.Load<GameObject>("Prefabs/Birds");
        cpm = GameObject.Find("CameraTargetPos").GetComponent<CameraPosMove>();
    }


    void Update()
    {
        
    }


 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Gris")
        {
            if (audioSource.isPlaying)
            {
                if (JudgeTearNumEnough())
                {
                    if (audioSource.clip.name != audioClipToNextLevel.name)
                    {
                        StartCoroutine(ToNextLevel());
                        //Invoke("PlayNormalClip", 24);
                    }
                }
                else
                {
                    if (audioSource.clip.name != audioClipJudge.name)
                    {
                        audioSource.clip = audioClipJudge;
                        audioSource.loop = false;
                        audioSource.Play();
                        Invoke("PlayNormalClip", 30);
                    }
                }
            }


        }

    }


    //????????????????
    private bool JudgeTearNumEnough()
    {
        for (int i = 0; i < grisTrans.childCount-1 ; i++)
        {
            if (grisTrans.GetChild(i).childCount <= 0)
            {
                //????????????????????????????
                return false;
            }
        }
        //????????????????????
        return true;
    }

    private void PlayNormalClip()
    {
        audioSource.clip = audioClipNormal;

        audioSource.Play();

    }


    IEnumerator ToNextLevel()
    {
        //??????????????????
        audioSource.Stop();
        yield return new WaitForSeconds(0.6f);
        //??????????Gris????????
        gris.enabled = false;
        LevelScript.enabled = true;
        LevelScript.SetRigidBodyType(RigidbodyType2D.Kinematic);
        LevelScript.PlayAnimation("Cry");
        yield return new WaitForSeconds(1.167f);

        //????????????????????1
        //???????? ??????????????????2

        LevelScript.StartMove(new Vector3(311,3,0));
        yield return new WaitForSeconds(0.3f);//????????????
        //????????
        audioSource.clip = audioClipToNextLevel;
        audioSource.loop = false;
        audioSource.Play();
        yield return new WaitForSeconds(2.7f);
        LevelScript.PlayAnimation("Fly");
        //????????
        Instantiate(Resources.Load<GameObject>(@"Prefabs/Red"));
        //??????
        Instantiate(birds);
        //???????? ????????????
        cc.SetColor(new Color((float)252/255,(float)236/255,(float)228/255));
        //??????????
        cpm.SetPos(new Vector3(-8.8f, 4.4f, 10));
        cc.SetSize(10);
        //GameObject red = GameObject.Instantiate(Resources.Load("Prefabs/Red")) as GameObject;
        //red.transform.position = new Vector3(311, 3, 0);
        //yield return null;


        yield return new WaitForSeconds(1);
        rc.StartChangeBGAlphaCutoff();
        yield return new WaitForSeconds(3);
        rc.StartChangeColorAlpha();
        yield return new WaitForSeconds(4);
        rc.StartChangeAlphaAndScaleValue();
        //??????????????????????????????????????
        yield return new WaitForSeconds(5);
        //???????? 311 -2.2
        cpm.SetPos(new Vector3(-13.4f, 6.6f, 10f));
        cc.SetSize(5);
        yield return new WaitForSeconds(2);

        LevelScript.StartMove(new Vector3(311, -2.5f, 0));
        yield return new WaitForSeconds(3);
        LevelScript.PlayAnimation("ToIdle");
        yield return new WaitForSeconds(2);
        //cc.SetPos(new Vector3(14.6f, 6.49f, 10));
        //yield return new WaitForSeconds(2);
        //??????????2
        ao.allowSceneActivation = true;

    }


}

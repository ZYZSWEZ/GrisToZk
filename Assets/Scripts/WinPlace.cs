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


    //判断当前是否通关
    private bool JudgeTearNumEnough()
    {
        for (int i = 0; i < grisTrans.childCount-1 ; i++)
        {
            if (grisTrans.GetChild(i).childCount <= 0)
            {
                //有空位置代表眼泪没有收集完毕
                return false;
            }
        }
        //已经没有空位置，通关
        return true;
    }

    private void PlayNormalClip()
    {
        audioSource.clip = audioClipNormal;

        audioSource.Play();

    }


    IEnumerator ToNextLevel()
    {
        //异步加载第二关场景
        audioSource.Stop();
        yield return new WaitForSeconds(0.6f);
        //玩家失去对Gris的控制权
        gris.enabled = false;
        LevelScript.enabled = true;
        LevelScript.SetRigidBodyType(RigidbodyType2D.Kinematic);
        LevelScript.PlayAnimation("Cry");
        yield return new WaitForSeconds(1.167f);

        //人物切换剧情动画状态1
        //人物升空 切换到剧情动画状态2

        LevelScript.StartMove(new Vector3(311,3,0));
        yield return new WaitForSeconds(0.3f);//等待人物升空
        //播放音乐
        audioSource.clip = audioClipToNextLevel;
        audioSource.loop = false;
        audioSource.Play();
        yield return new WaitForSeconds(2.7f);
        LevelScript.PlayAnimation("Fly");
        //生成小花
        Instantiate(Resources.Load<GameObject>(@"Prefabs/Red"));
        //生成鸟
        Instantiate(birds);
        //改变颜色 设置相机位置
        cc.SetColor(new Color((float)252/255,(float)236/255,(float)228/255));
        //摄像机拉远
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
        //播放切换场景前的特效，并拉远摄像机视角
        yield return new WaitForSeconds(5);
        //人物下落 311 -2.2
        cpm.SetPos(new Vector3(-13.4f, 6.6f, 10f));
        cc.SetSize(5);
        yield return new WaitForSeconds(2);

        LevelScript.StartMove(new Vector3(311, -2.5f, 0));
        yield return new WaitForSeconds(3);
        LevelScript.PlayAnimation("ToIdle");
        yield return new WaitForSeconds(2);
        //cc.SetPos(new Vector3(14.6f, 6.49f, 10));
        //yield return new WaitForSeconds(2);
        //切换到场景2
        ao.allowSceneActivation = true;

    }


}

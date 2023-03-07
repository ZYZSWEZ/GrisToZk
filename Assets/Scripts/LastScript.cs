using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LastScript : MonoBehaviour
{
    // Start is called before the first frame update

    private SpriteRenderer sr;
    public bool startLerp;
    public float targetValue;
    private GameObject grisGo;
    private AudioSource audioSource;
    private AudioClip bossAudioClip;
    private AudioClip lastAudioClip;
    private GameObject changeCameraArea;
    private Gris gris;
    private AsyncOperation ao;

    void Start()
    {
        sr = GameObject.Find("BlackSky").GetComponent<SpriteRenderer>();
        grisGo = GameObject.Find("BOSS");
        grisGo.SetActive(false);
        audioSource =  GameObject.Find("Evn").GetComponent<AudioSource>();
        bossAudioClip = Resources.Load<AudioClip>("Gris/Audioclips/Boss");
        lastAudioClip = Resources.Load<AudioClip>("Gris/Audioclips/Sing");
        changeCameraArea = GameObject.Find("ChangeCameraArea");
        gris = GameObject.Find("Gris").GetComponent<Gris>();
        ao = SceneManager.LoadSceneAsync(0);
        ao.allowSceneActivation = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (startLerp)
        {
            if (targetValue > 0)
            {
                sr.color += new Color(0, 0, 0, 0.2f) * Time.deltaTime;
            }
            else
            {
                sr.color -= new Color(0, 0, 0, 0.2f) * Time.deltaTime;
            }
        }
        if (Mathf.Abs(sr.color.a - targetValue) <= 0.05f)
        {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, targetValue);
            
            if (targetValue >= 1.0f)//生成boss
            {
                if (!grisGo.activeInHierarchy)
                {
                    grisGo.SetActive(true);
                    startLerp = false;
                }
            }
        }
    }


    public void StartLerp(float targetVal)
    {
        if (targetVal>=1)
        {
            //开始Boss战
            Debug.LogError("SSS");
            audioSource.clip = bossAudioClip;
            audioSource.volume = 0.3f;
            audioSource.Play();
            changeCameraArea.SetActive(false);
        }
        else 
        {
            //通关
            audioSource.clip = lastAudioClip;
            audioSource.volume = 1f;
            audioSource.Play();
            gris.PlaySingAnimation();
            Invoke("LoadScene",80);
        }
        targetValue = targetVal;
        startLerp = true;

    }

    private void LoadScene()
    {
        ao.allowSceneActivation = true;
    }




}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject[] ViewPorts; //head view ,eye view .....
    public Image[] Icons;
    public Button PreviousButton, NextButton,restartBtn;

    public GameObject[] heads, eyes, mouths, accs, bodys;
    public AudioClip PreviousBtnClip, NextBtnClip;
    public AudioClip[] BGMusic;
    public AudioSource source1, source2;
    public GameObject GamePanel, WinPanel;
    public VideoClip[] videos;
    public VideoPlayer videoPlayer;

    [HideInInspector]
    public int selectedHead, selectedEye, selectedMouth, selectedAcc, selectedBody;
    enum Item
    {
        Head = 0, Eye = 1, Mouth = 2, Acc = 3, Body = 4
    }
    Item currentItem;
    [HideInInspector]
    public GameObject selectedObject;
    public static GameManager instance;
    void Start()
    {


        reset();


    }
    void Update()
    {
        EnableAndDisableBtns();
        if (currentItem == Item.Body && selectedBody == -1)
        {
            NextButton.interactable = false;
            //NextButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Next";
        }
        else if (currentItem == Item.Body && selectedBody != -1)
        {
            NextButton.interactable = true;
            //NextButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Done";
        }


    }
    void reset()
    {
        // GamePanel.SetActive(true);
        // WinPanel.SetActive(false);
        selectedHead = selectedEye = selectedMouth = selectedAcc = selectedBody = -1;
        currentItem = Item.Head;
        instance = this;
        // Camera.main.orthographicSize = 10f;
        // Camera.main.transform.position = new Vector3(0, 0, -10);
        source1.clip = BGMusic[Random.Range(0, BGMusic.Length)];
        // videoPlayer.gameObject.SetActive(false);
        for (int i = 0; i < ViewPorts.Length; i++)
        {
            if (i == (int)currentItem)
            {
                ViewPorts[i].SetActive(true);
            }
            else
            {
                ViewPorts[i].SetActive(false);
            }

        }

        // for (int i = 0; i < heads.Length; i++) { heads[i].SetActive(false); }
        // for (int i = 0; i < eyes.Length; i++) { eyes[i].SetActive(false); }
        // for (int i = 0; i < mouths.Length; i++) { mouths[i].SetActive(false); }
        // for (int i = 0; i < accs.Length; i++) { accs[i].SetActive(false); }
        // for (int i = 0; i < bodys.Length; i++) { bodys[i].SetActive(false); bodys[i].transform.GetChild(0).GetComponent<Animator>().enabled = false;
        // bodys[i].transform.GetChild(0).GetComponent<Animator>().StopPlayback();}

       // NextButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Next";

        if (PlayerPrefs.GetInt("CanPlaySounds", 1) == 0)
        {
            source1.volume = 0; source2.volume = 0;
        }
        else
        {
            source1.volume = 0.7f; source2.volume = 0.7f;
            source1.Play();
        }
    }
    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void RestartBtn()
    {
        ///////////////////////show ads /////////////////////////////////////////
        
      
        SceneManager.LoadScene(1);
    }

void showAds(){
  AdsInitializer.ads.interstitialAdExample.ShowAd();
  restartBtn.enabled=true;

}

    public void Next()
    {


        if (currentItem != Item.Body)
        {



            source2.clip = NextBtnClip;
            source2.Play();

            if (currentItem == Item.Acc)
            {
                Camera.main.orthographicSize = 19.5f;
                Camera.main.transform.position = new Vector3(0, -4, -10);
            }
            currentItem = currentItem + 1;
            for (int i = 0; i < ViewPorts.Length; i++)
            {
                if (i == (int)currentItem)
                {
                    ViewPorts[i].SetActive(true);
                }
                else
                {
                    ViewPorts[i].SetActive(false);
                }

            }
            for (int i = 0; i < Icons.Length; i++)
            {
                if (i == (int)currentItem)
                {
                    Icons[i].GetComponent<Animator>().SetBool("Selected",true);
                }
                else if (i < (int)currentItem)
                {
                    Icons[i].GetComponent<Animator>().SetBool("Selected", false);
                }
                else
                {
                    Icons[i].GetComponent<Animator>().SetBool("Selected", false);
                }
            }
        }
        else
        {

            if (selectedBody != -1)
            {
                // ///////////////// win                    //////                         /////////////////////////
                
                
                restartBtn.enabled=false;
                GamePanel.SetActive(false);
                WinPanel.SetActive(true);
                videoPlayer.clip = videos[Random.Range(0, videos.Length)];
                videoPlayer.gameObject.SetActive(true);
                bodys[selectedBody].transform.GetChild(0).GetComponent<Animator>().enabled = true;
                Invoke("showAds",1f);

            }

        }
    }
    public void Previous()
    {
        source2.clip = PreviousBtnClip;
        source2.Play();
        //NextButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Next";
        Camera.main.orthographicSize = 10;
         Camera.main.transform.position = new Vector3(0, 0, -10);
        if (currentItem != Item.Head)
        {
            currentItem = currentItem - 1;
            for (int i = 0; i < ViewPorts.Length; i++)
            {
                if (i == (int)currentItem)
                {
                    ViewPorts[i].SetActive(true);
                }
                else
                {
                    ViewPorts[i].SetActive(false);
                }

            }
            for (int i = 0; i < Icons.Length; i++)
            {
                if (i == (int)currentItem)
                {
                    Icons[i].GetComponent<Animator>().SetBool("Selected", true);
                }
                else if (i < (int)currentItem)
                {
                    Icons[i].GetComponent<Animator>().SetBool("Selected", false);
                }
                else
                {
                    Icons[i].GetComponent<Animator>().SetBool("Selected", false);
                }
            }
        }
    }

    void EnableAndDisableBtns()
    {

        if (currentItem == Item.Head)
        {
            PreviousButton.interactable = false;

        }
        else
        {
            PreviousButton.interactable = true;

        }
        if (currentItem == Item.Body && selectedBody==-1)
        {
            NextButton.interactable = false;
        }
        else
        {

            switch (currentItem)
            {
                case Item.Head:
                    if (selectedHead == -1) { NextButton.interactable = false; } else { NextButton.interactable = true; }
                    break;
                case Item.Eye:
                    if (selectedEye == -1) { NextButton.interactable = false; } else { NextButton.interactable = true; }
                    break;
                case Item.Mouth:
                    if (selectedMouth == -1) { NextButton.interactable = false; } else { NextButton.interactable = true; }
                    break;
                case Item.Acc:
                    if (selectedAcc == -1) { NextButton.interactable = false; } else { NextButton.interactable = true; }
                    break;
                default:

                    break;
            }


            //  NextButton.interactable = true;

        }
    }


    public void itemButtom(int n)
    {
        switch (currentItem)
        {
            case Item.Head:
                selectedHead = n;
                for (int i = 0; i < heads.Length; i++)
                {
                    if (i == n)
                    {
                        heads[i].SetActive(true);

                    }
                    else
                    {
                        heads[i].SetActive(false);
                    }
                }
                break;
            case Item.Eye:
                selectedEye = n;
                for (int i = 0; i < eyes.Length; i++)
                {
                    if (i == n)
                    {
                        eyes[i].SetActive(true); selectedObject = eyes[i];
                    }
                    else
                    {
                        eyes[i].SetActive(false);
                    }
                }
                break;
            case Item.Mouth:
                selectedMouth = n;
                for (int i = 0; i < mouths.Length; i++)
                {
                    if (i == n)
                    {
                        mouths[i].SetActive(true); selectedObject = mouths[i];
                    }
                    else
                    {
                        mouths[i].SetActive(false);
                    }
                }
                break;
            case Item.Acc:
                selectedAcc = n;
                for (int i = 0; i < accs.Length; i++)
                {
                    if (i == n)
                    {
                        accs[i].SetActive(true); selectedObject = accs[i];
                    }
                    else
                    {
                        accs[i].SetActive(false);
                    }
                }
                break;
            case Item.Body:
                selectedBody = n;
                for (int i = 0; i < bodys.Length; i++)
                {
                    if (i == n)
                    {
                        bodys[i].transform.position = heads[selectedHead].transform.GetChild(0).position;
                        bodys[i].SetActive(true);
                    }
                    else
                    {
                        bodys[i].SetActive(false);
                    }
                }
                //NextButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Done";
                break;
            default:
                break;
        }
    }

}

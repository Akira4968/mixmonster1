using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenuScript : MonoBehaviour
{
    public GameObject[] heads, accs, eyes, mouths;
    public GameObject SettingsPanel;
    public Sprite soundOn, soundOff, VibrationON, VibrationOff;
    public Image SoundBtn, VibrationBtn;
    int n, item;
    float t, timer = .5f;
    private void Start() {
         if (PlayerPrefs.GetInt("CanPlaySounds", 1) == 0){
             SoundBtn.sprite = soundOff;
         }
         
    }
    private void Update()
    {

        if (t < timer)
        {
            t += Time.deltaTime;
        }
        else
        {
            t = 0;
            randomitem();
        }

    }


    public void startBtn()
    {
        SceneManager.LoadScene(1);
    }
    void randomitem()
    {

        item = Random.Range(0, 4);

        if (item == 0)
        {
            n = Random.Range(0, heads.Length);
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
        }
        if (item == 1)
        {
            n = Random.Range(0, accs.Length);
            for (int i = 0; i < accs.Length; i++)
            {
                if (i == n)
                {
                    accs[i].SetActive(true);

                }
                else
                {
                    accs[i].SetActive(false);
                }
            }
        }
        if (item == 2)
        {
            n = Random.Range(0, eyes.Length);
            for (int i = 0; i < eyes.Length; i++)
            {
                if (i == n)
                {
                    eyes[i].SetActive(true);

                }
                else
                {
                    eyes[i].SetActive(false);
                }
            }
        }
        if (item == 3)
        {
            n = Random.Range(0, mouths.Length);
            for (int i = 0; i < mouths.Length; i++)
            {
                if (i == n)
                {
                    mouths[i].SetActive(true);

                }
                else
                {
                    mouths[i].SetActive(false);
                }
            }
        }


    }
    public void SoundButton()
    {
        if (SoundBtn.sprite == soundOn)
        {
            SoundBtn.sprite = soundOff;
            PlayerPrefs.SetInt("CanPlaySounds", 0);
        }
        else
        {
            SoundBtn.sprite = soundOn;
            PlayerPrefs.SetInt("CanPlaySounds", 1);
        }
    }
    public void VibrationButton()
    {
        if (VibrationBtn.sprite == VibrationON)
        {
            VibrationBtn.sprite = VibrationOff;

        }
        else
        {
            VibrationBtn.sprite = VibrationON;

        }
    }
    public void ShowSettings()
    {

        SettingsPanel.SetActive(true);
    }
    public void HideSettings()
    {
        SettingsPanel.SetActive(false);

    }
}

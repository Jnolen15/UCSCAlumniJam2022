using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject playButton;
    public GameObject percentage;
    public GameObject extremeDecor;

    public QTEManager qte;
    public Timer leftHand;
    public Timer rightHand;
    public AudioClip music;
    public AudioSource musicPlayer;
    public AudioSource menuPlayer;

    public void StartGame()
    {
        qte.gameStarted = true;
        leftHand.gameStarted = true;
        rightHand.gameStarted = true;

        mainMenu.SetActive(false);
        percentage.SetActive(true);
        musicPlayer.PlayOneShot(music);
        menuPlayer.mute = true;

    }

    public void RestartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ExtremeMode()
    {
        if (!qte.extremeMode)
        {
            qte.extremeMode = true;
            extremeDecor.SetActive(true);
        }
        else
        {
            qte.extremeMode = false;
            extremeDecor.SetActive(false);
        }
    }
}

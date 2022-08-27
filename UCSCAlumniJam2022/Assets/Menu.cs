using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject playButton;

    public QTEManager qte;
    public Timer leftHand;
    public Timer rightHand;

    public void StartGame()
    {
        qte.gameStarted = true;
        leftHand.gameStarted = true;
        rightHand.gameStarted = true;

        playButton.SetActive(false);
    }
}

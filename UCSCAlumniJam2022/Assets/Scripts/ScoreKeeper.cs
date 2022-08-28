using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreKeeper : MonoBehaviour
{
    public TextMeshProUGUI hitAccuracy;

    public float totalQTEs;
    public float hitQTEs;

    public void UpdateAccuracy(bool hit)
    {
        totalQTEs++;

        if (hit)
        {
            hitQTEs++;
        }

        float percent = (hitQTEs / totalQTEs);
        Debug.Log("hitQTEs / totalQTEs: " + percent);
        percent *= 100f;
        Debug.Log("%" + percent);

        hitAccuracy.text = "Hit Accuracy: " + Mathf.Round(percent) + "%";
    }
}

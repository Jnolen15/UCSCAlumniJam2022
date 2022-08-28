using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTEManager : MonoBehaviour
{
    [Header("Variables")]
    public ScoreKeeper score;
    public GameObject popUp;
    public GameObject badPopUp;
    public GameObject leftHand;
    public GameObject rightHand;
    public Timer timer;
    public string[] keys = {"a", "s", "d", "j", "k", "l"};
    public float cooldown;
    public float hitTime;
    public float numberOfMissesRight = 0;
    public float numberOfMissesLeft = 0;
    public int missCounter = 0;
    public int chooseHand = 0;
    public bool extremeMode = false;
    [Header("Changing values")]
    public bool gameStarted = false;
    public bool inQTE = false;
    public string currentKey;
    public float activeCooldown;

    private Coroutine qte;
    private GameObject currentPopup;
    private GameObject currentBadPopup;

    void Start()
    {
        activeCooldown = cooldown;
    }

    void Update()
    {
        if (timer.timeLapse < timer.endTime && gameStarted)
        {
            // Game gets faster over time
            if (extremeMode)
            {
                float temp = ((timer.endTime - timer.timeLapse) / 20);
                if (temp > 1.5)
                {
                    cooldown = temp;
                    hitTime = temp / 2;
                }
                else
                {
                    cooldown = 1f;
                    hitTime = 0.75f;
                }
            } else
            {
                float temp = ((timer.endTime - timer.timeLapse) / 14);
                if (temp > 2.4)
                {
                    cooldown = temp;
                    hitTime = temp / 2;
                }
                else
                {
                    cooldown = 2.4f;
                    hitTime = 1.2f;
                }
            }

            // QTE spawn timer
            if (activeCooldown > 0) activeCooldown -= Time.deltaTime;
            else if (!inQTE)
            {
                inQTE = true;
                
                currentKey = SelectKey(); // Get random Key

                Vector2 pos = Vector2.zero; // Spawn QTE Popup
                Vector2 otherpos = Vector2.zero; // Spawn Bad QTE Popup
                if (currentKey == "a" || currentKey == "s" || currentKey == "d")
                {
                    pos = new Vector2(leftHand.transform.position.x + Random.Range(-1, 1), leftHand.transform.position.y - 2);
                    otherpos = new Vector2(rightHand.transform.position.x + Random.Range(-1, 1), rightHand.transform.position.y - 2);
                    chooseHand = 0;
                }
                else if (currentKey == "j" || currentKey == "k" || currentKey == "l")
                {
                    pos = new Vector2(rightHand.transform.position.x + Random.Range(-1, 1), rightHand.transform.position.y - 2);
                    otherpos = new Vector2(leftHand.transform.position.x + Random.Range(-1, 1), leftHand.transform.position.y - 2);
                    chooseHand = 1;
                }
                currentPopup = Instantiate(popUp, pos, transform.rotation);
                currentPopup.GetComponent<Popup>().SetUp(currentKey);

                if (extremeMode) // Spawn fake popups in Extreme mode
                {
                    currentBadPopup = Instantiate(badPopUp, otherpos, transform.rotation);
                    currentBadPopup.GetComponent<Popup>().SetUp(SelectKey());
                }

                qte = StartCoroutine(RunQTE()); // Start QTE timer
            }

            // Get input (As long as its not nothing, and QTE is active)
            if (Input.inputString != "" && inQTE)
                InputDetected(Input.inputString);
        }
    }

    // Provides a random key in the list
    public string SelectKey()
    {
        return keys[Random.Range(0, keys.Length)];
    }

    // Testing if Input was correct during QTE
    public void InputDetected(string key)
    {
        if(key == currentKey)
        {
            StopCoroutine(qte);
            score.UpdateAccuracy(true);
            Debug.Log("QTE SUCCESS!");
            EndQTE(true);
        } else
        {
            StopCoroutine(qte);
            score.UpdateAccuracy(false);
            if (chooseHand == 0)
            {
                if (missCounter < 6)
                {
                    missCounter += 1;
                }
                    numberOfMissesLeft = Mathf.Clamp(0.25f * missCounter, 0, 1.7f);
                
            }
            else
            {
                if (missCounter < 6)
                {
                    missCounter += 1;
                }
                numberOfMissesRight = Mathf.Clamp(-0.25f * missCounter, -1.7f, 0);
            }

            Debug.Log("QTE FAILED! (Wrong Key)");
            EndQTE(false);
        }
    }

    // The QTE timer
    IEnumerator RunQTE()
    {
        yield return new WaitForSeconds(hitTime);
        score.UpdateAccuracy(false);
        Debug.Log("QTE FAILED! (OUT OF TIME)");

        if (chooseHand == 0) //Left Hand
        {
            if (missCounter < 6)
            {
                missCounter += 1;
            }
            numberOfMissesLeft = Mathf.Clamp(0.25f * missCounter, 0, 1.7f);

        }
        else //Right Hand
        {
            if (missCounter < 6)
            {
                missCounter += 1;
            }
            numberOfMissesRight = Mathf.Clamp(-0.25f * missCounter, -1.7f, 0);
        }
        EndQTE(false);
    }

    private void EndQTE(bool success)
    {
        inQTE = false;
        currentKey = "";
        activeCooldown = cooldown;

        if (success)
        {
            currentPopup.GetComponent<Popup>().Success();
            if (extremeMode) currentBadPopup.GetComponent<Popup>().Success();
        } else
        {
            currentPopup.GetComponent<Popup>().Fail();
            if (extremeMode) currentBadPopup.GetComponent<Popup>().Fail();
        }
    }
}

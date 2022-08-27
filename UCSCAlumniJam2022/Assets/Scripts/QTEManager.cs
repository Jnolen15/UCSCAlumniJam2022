using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTEManager : MonoBehaviour
{
    [Header ("Variables")]
    public GameObject popUp;
    public GameObject leftHand;
    public GameObject rightHand;
    public Timer timer;
    public string[] keys = {"a", "s", "d", "j", "k", "l"};
    public float cooldown;
    public float hitTime;
    [Header("Changing values")]
    public bool inQTE = false;
    public string currentKey;
    public float activeCooldown;

    private Coroutine qte;
    private GameObject currentPopup;

    void Start()
    {
        activeCooldown = cooldown;
    }

    void Update()
    {
        if (timer.timeLapse < timer.endTime)
        {
            // Game gets faster over time
            float temp = ((timer.endTime - timer.timeLapse) / 10);
            if (temp > 1)
            {
                cooldown = temp;
                hitTime = temp / 2;
            } else
            {
                cooldown = 1;
                hitTime = 0.5f;
            }

            // QTE spawn timer
            if (activeCooldown > 0) activeCooldown -= Time.deltaTime;
            else if (!inQTE)
            {
                inQTE = true;
                
                currentKey = SelectKey(); // Get random Key

                Vector2 pos = Vector2.zero; // Spawn QTE Popup
                if (currentKey == "a" || currentKey == "s" || currentKey == "d")
                    pos = new Vector2(leftHand.transform.position.x + Random.Range(-1, 1), leftHand.transform.position.y - 2);
                else if (currentKey == "j" || currentKey == "k" || currentKey == "l")
                    pos = new Vector2(rightHand.transform.position.x + Random.Range(-1, 1), rightHand.transform.position.y - 2);
                currentPopup = Instantiate(popUp, pos, transform.rotation);
                currentPopup.GetComponent<Popup>().SetUp(currentKey);

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
            Debug.Log("QTE SUCCESS!");
            EndQTE(true);
        } else
        {
            StopCoroutine(qte);
            Debug.Log("QTE FAILED! (Wrong Key)");
            EndQTE(false);
        }
    }

    // The QTE timer
    IEnumerator RunQTE()
    {
        yield return new WaitForSeconds(hitTime);
        Debug.Log("QTE FAILED! (OUT OF TIME)");
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
        } else
        {
            currentPopup.GetComponent<Popup>().Fail();
        }
    }
}

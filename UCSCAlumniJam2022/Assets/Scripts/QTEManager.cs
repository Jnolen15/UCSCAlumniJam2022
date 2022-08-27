using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTEManager : MonoBehaviour
{
    public GameObject popUp;
    public string[] keys = {"w", "a", "s", "d", "i", "j", "k", "l"};
    public bool inQTE = false;
    public string currentKey;
    public float hitTime;
    public float cooldown;
    public float activeCooldown;

    private Coroutine qte;
    private GameObject currentPopup;

    void Start()
    {
        activeCooldown = cooldown;
    }

    void Update()
    {
        // QTE spawn timer
        if (activeCooldown > 0) activeCooldown -= Time.deltaTime;
        else if (!inQTE)
        {
            inQTE = true;
            currentKey = SelectKey();
            Debug.Log(currentKey);
            currentPopup = Instantiate(popUp, transform.position, transform.rotation, transform);
            currentPopup.GetComponent<Popup>().SetUp(currentKey);
            qte = StartCoroutine(RunQTE());
        }

        // Get input (As long as its not nothing, and QTE is active)
        if (Input.inputString != "" && inQTE)
            InputDetected(Input.inputString);
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

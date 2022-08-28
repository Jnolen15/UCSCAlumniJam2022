using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    // Start is called before the first frame update
    // Public Variables
    public float endTime = 60f;
    public float timeLapse;
    public float degradingMissTimer;
    public Transform endSpot;
    public QTEManager QTEManager;
    public GameObject restartButton;
    public float counterOfMisses;
    public float animateSpotX;
    public float valueToLerp;
    public float missQTEMove;
    public bool gameStarted = false;

    //Private Variables
    private Vector3 startingPosition;
    private float changingX;
    private float changingY;
    private bool isClosing = false;

    void Start()
    {
        startingPosition = transform.position;
    }

    void Update()
    {
        if (gameStarted)
        {
            if (timeLapse < endTime)
            {
                
                if (gameObject.tag == "Left Hand")
                {
                    missQTEMove = QTEManager.numberOfMissesLeft;
                    if(missQTEMove > 0)
                    {
                        missQTEMove = QTEManager.numberOfMissesLeft - (1f * degradingMissTimer/endTime);
                        degradingMissTimer += Time.deltaTime;

                    }
                    

                }
                
                if (gameObject.tag == "Right Hand")
                {
                    missQTEMove = QTEManager.numberOfMissesRight;
                    if (missQTEMove < 0)
                    {
                        missQTEMove = QTEManager.numberOfMissesRight + (1f * degradingMissTimer / endTime);
                        degradingMissTimer += Time.deltaTime;

                    }
                }
                
                changingX = Mathf.Lerp(startingPosition.x, endSpot.position.x, timeLapse / endTime);
                transform.position = new Vector3(changingX, missQTEMove, 0);
                timeLapse += Time.deltaTime;

            }
            else if (!isClosing)
            {
                isClosing = true;
                StartCoroutine(AnimateClose());
            }
        }
    }
    IEnumerator AnimateClose()
    {
        float timerOne = 0;
        float timertwo = 0;
        float backTime = 0.5f;
        float forawrdTime = 0.25f;

        Vector2 startPos = transform.position;
        Vector2 animateSpot = new Vector2(animateSpotX, missQTEMove);
        Vector2 endSpot = new Vector2(0, missQTEMove);

        Color startCol = Color.white;

        while (timerOne < backTime)
        {
            float t = timerOne / backTime;
            t = t * t * (3f - 2f * t);
            transform.position = Vector3.Lerp(startPos, animateSpot, t);

            timerOne += Time.deltaTime;
            yield return null;
        }
        
        while (timertwo < forawrdTime)
        {
            float t = timertwo / forawrdTime;
            t = t * t * (3f - 2f * t);
            transform.position = Vector3.Lerp(animateSpot, endSpot, t);

            timertwo += Time.deltaTime;
            yield return null;
        }
        restartButton.SetActive(true);
    }
}

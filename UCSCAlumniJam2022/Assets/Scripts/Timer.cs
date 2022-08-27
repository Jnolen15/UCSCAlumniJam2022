using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    // Start is called before the first frame update
    // Public Variables
    public float endTime = 60f;
    public float timeLapse;
    public Transform endSpot;
    public float animateSpotX;
    public float valueToLerp;
    public float missQTEMove;
    public bool gameStarted = false;

    //Private Variables
    private Vector3 startingPosition;
    private float changingX;
    private bool isClosing = false;

    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStarted)
        {
            if (timeLapse < endTime)
            {
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
    }
}

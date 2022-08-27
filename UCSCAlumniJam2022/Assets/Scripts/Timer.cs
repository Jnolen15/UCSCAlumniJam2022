using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    // Start is called before the first frame update
    // Public Variables
    public float endTime = 60f;
    public float timeLapse;
    public GameObject middleSpot;
    public float valueToLerp;
    public float missQTEMove;

    //Private Variables
    private Vector3 startingPosition;
    private float changingX;
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLapse < endTime)
        {
            changingX = Mathf.Lerp(startingPosition.x, 0, timeLapse / endTime);
            transform.position = new Vector3(changingX, missQTEMove, 0);
            timeLapse += Time.deltaTime;

        }
        Debug.Log(timeLapse);

    }
}

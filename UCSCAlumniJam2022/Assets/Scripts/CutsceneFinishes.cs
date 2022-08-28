using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CutsceneFinishes : MonoBehaviour
{
    public string goToLevel;
    public PlayableDirector director;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (director.state != PlayState.Playing)
        {
            Debug.Log("we finish cutscene");
            SceneManager.LoadScene(goToLevel);
        }
    }
}

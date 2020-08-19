using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RythmGameManager : MonoBehaviour
{
    public AudioSource music;

    public static RythmGameManager manager;

    public bool startPlaying;
    public BeatHolder BS;

    public float score = 0;

    // Start is called before the first frame update
    void Start()
    {
        manager = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (!startPlaying)
        {
            if (Input.anyKeyDown)
            {
                startPlaying = true;
                BS.hasStarted = true;
                music.Play();
            }
        }

        List<NoteObject> notes = new List<NoteObject>(FindObjectsOfType<NoteObject>());
        if (notes.Count == 0)
        {
            InterGameData.data.bonusLvl += 5;
            SceneManager.LoadScene(1);
        }
    }
}

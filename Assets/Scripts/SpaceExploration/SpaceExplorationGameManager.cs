using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SpaceExplorationGameManager : MonoBehaviour
{
    public SpaceExplorationPlayerController shipPrefab;

    public static SpaceExplorationGameManager manager;
    public TextMeshProUGUI winText;
    public TextMeshProUGUI timerText;

    private Vector3 startCamaraPosition;
    private float timeLeft = 0.0f;
    private bool playing = true;

    public void Start()
    {
        startCamaraPosition = Camera.main.transform.position;
        if (manager == null)
        {
            manager = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void Update()
    {
        if (playing)
        {
            timeLeft += Time.deltaTime;
            
            timerText.SetText(timeLeft.ToString("###0.00"));
        }
    }

    IEnumerator createNewShip()
    {
        yield return new WaitForSeconds(5f);

        SpaceExplorationPlayerController ship = Instantiate<SpaceExplorationPlayerController>(shipPrefab);

        Camera.main.transform.SetParent(ship.transform);
        Camera.main.transform.position = startCamaraPosition;
    }

    IEnumerator PostWin()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(1);
    }

    public void GameWon()
    {
        playing = false;
        StartCoroutine(PostWin());
    }

    public void SpawnShip()
    {
        StartCoroutine(createNewShip());
    }
}

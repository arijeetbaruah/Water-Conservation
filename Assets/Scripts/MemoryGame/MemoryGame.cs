using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public static class IListExtensions
{
    /// <summary>
    /// Shuffles the element order of the specified list.
    /// </summary>
    public static void Shuffle<T>(this IList<T> ts)
    {
        var count = ts.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }
    }
}

public class MemoryGame : MonoBehaviour
{
    public GameObject cardHolder;
    public Sprite[] cards;

    public static MemoryGame game;
    public MemoryGameCard selectedCard;

    public bool allowCardSelection = true;

    public float timer = 10;
    private bool runTimer = true;
    public Slider timerUI;

    // Start is called before the first frame update
    void Start()
    {
        if (game == null)
        {
            game = this;
        }else
        {
            Destroy(this);
        }
        timerUI.maxValue = timer;
        timerUI.minValue = 0;

        List<Sprite> list = new List<Sprite>(cards);
        foreach (Sprite l in cards)
        {
            list.Add(l);
        }

        list.Shuffle();

        int i = 0, j = 0;
        foreach (Sprite l in list)
        {
            MemoryGameCard sr = Instantiate(cardHolder, transform).GetComponent<MemoryGameCard>();
            sr.targetImage = l;
            sr.cardID = l.name;

            RectTransform rt = sr.GetComponent<RectTransform>();

            rt.localPosition = new Vector3(-200 + 105 * i, 130 - 105 * j, 0);
            i++;
            if (i == 5)
            {
                i = 0;
                j++;
            }
        }
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(1);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0 && runTimer)
        {
            timer -= Time.deltaTime * 0.05f;
            timerUI.value = timer;
        }

        if (transform.childCount == 0)
        {
            GameManager.manager.bonus += timer;
            runTimer = false;
            StartCoroutine(GameOver());
        }
    }

    private void OnEnable()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void OnDisable()
    {
        Cursor.visible = true;
    }
}

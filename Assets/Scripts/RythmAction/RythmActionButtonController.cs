using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RythmActionButtonController : MonoBehaviour
{
    private SpriteRenderer sprite;
    public Sprite pressedSprite;
    private Sprite defaultSprite;

    private SpriteRenderer hitText;

    public bool arrowInRange;

    public KeyCode keyToPress;

    public Sprite hitSprite;
    public Sprite perfectSprite;
    public Sprite missSprite;
    public float hitError = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        defaultSprite = sprite.sprite;
        hitText = transform.GetChild(0).GetComponent<SpriteRenderer>();
        hitText.sprite = null;
    }

    IEnumerator clearHit()
    {
        yield return new WaitForSeconds(1.0f);
        hitText.sprite = null;
    }

    // Update is called once per frame
    void Update()
    {
//        Debug.Log(Input.GetAxis("Submit"));
        if (Input.GetKeyDown(keyToPress))
        {
            sprite.sprite = pressedSprite;

            if (arrowInRange)
            {
                if (hitError == 0.5)
                {
                    hitText.sprite = perfectSprite;
                    RythmGameManager.manager.score += 1;
                }
                else
                {
                    hitText.sprite = hitSprite;
                    RythmGameManager.manager.score += 0.5f;
                }
                StartCoroutine(clearHit());
            }
            else
            {
                hitText.sprite = missSprite;
                StartCoroutine(clearHit());
            }
        }
        if (Input.GetKeyUp(keyToPress))
        {
            sprite.sprite = defaultSprite;
        }
    }
}

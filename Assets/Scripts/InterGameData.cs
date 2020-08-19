using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterGameData : MonoBehaviour
{
    public float bonusLvl = 0;
    public float timer = 0;

    public static InterGameData data;
    public Vector3 playerPos;
    public Quaternion playerRot;

    // Start is called before the first frame update
    void Start()
    {
        if (data == null)
        {
            data = this;
            DontDestroyOnLoad(this);
        } else
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeControl : MonoBehaviour
{

    public float time;
    public Text timeValue;
    private bool gameActive;
    // Start is called before the first frame update
    void Start()
    {
        gameActive = true;
        timeValue.text = time.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameActive)
        {
        time -= Time.deltaTime;
        timeValue.text = ((int)time).ToString();
        }


        if (time < 0)
        {
            time = 60;
            gameActive = false;
            GetComponent<PlayerController>().Die();
        }
    }
}

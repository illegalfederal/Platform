using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{

    public Text puanText;
    public float coinRotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // transform.Translate(new Vector3(10f*Time.deltaTime,0f,0f));
        //if(Time.timeScale !=0)
        transform.Rotate(new Vector3(0f, coinRotationSpeed, 0f));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            int puan = int.Parse(puanText.text);
            puan += 50;
            puanText.text = puan.ToString();
            Destroy(gameObject);
        }
    }

    
}

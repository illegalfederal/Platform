using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public GameObject effect;


    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (!collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);

            if (collision.gameObject.CompareTag("Enemy") && (collision.gameObject.layer != 8))
            {
                
                GameObject efekt =   Instantiate(effect,collision.gameObject.transform.position,Quaternion.identity);
                efekt.transform.position += new Vector3(0f, 0f, -5f);
                Destroy(collision.gameObject);
            }

        }

        
        
        //else
        //{
        //    Debug.Log(collision.gameObject.name);
        //}

        //if(collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Ground"))
        //{
        //    Destroy(gameObject);
           
        //}
    }

}

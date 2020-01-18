using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public LayerMask engel;
    private bool onGround;
    private float width;
    private Rigidbody2D myBody;
    public float speed;
    public static int totalEnemyNumber;
    // Start is called before the first frame update
    void Start()
    {
        totalEnemyNumber++;
        Debug.Log(totalEnemyNumber);
        width = GetComponent<SpriteRenderer>().bounds.extents.x;
        myBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + (transform.right * width / 2), Vector2.down,2f,engel);
        if(hit.collider != null)
        {
            onGround = true;
        }
        else
        {
            onGround = false;
        }

        Flip();

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + (transform.right * width / 2 ), transform.position + (transform.right * width / 2) + new Vector3(0f, -2f,0f));
    }

    void Flip()
    {
        if (!onGround)
        {
        transform.eulerAngles += new Vector3(0, 180f, 0);
        }
        myBody.velocity = new Vector2(transform.right.x * speed,0f);

    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("metota girdi mi?");
        
       /* if (collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log("kontrol yapısına girdi mi?");
            collision.gameObject.GetComponent<Animator>().SetTrigger("Die");
        }*/
    }

    
}

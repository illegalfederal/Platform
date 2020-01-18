using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    private float speed;
    private float mySpeedX;
    private Vector3 defaultLocalScale;
    Animator myAnimator;
    private Rigidbody2D myBody;
    public float jumpPower;
    public bool onGround;
    private bool canDoubleJump;
    public GameObject arrow;
    public bool attacked;
    public float attackTimer,defaultAttackTimer;
    public float time;
    public float arrowNumber;
    public Text arrowNumberText;
    public Text healthText;
    public float health;
    public float arrowSpeed;
    private GameObject soundController;
    public AudioClip audioClip;
    public GameObject winPanel, losePanel;

    // Start is called before the first frame update
    void Start()
    {
        soundController = GameObject.Find("Sound Controller");
        health = 100;
        arrowNumber = 10;
        myAnimator = GetComponent<Animator>();
        myBody = GetComponent<Rigidbody2D>();
        defaultLocalScale = transform.localScale;
        canDoubleJump = false;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //Debug.Log(GameObject.Find("Enemies").transform.childCount);


        //healthText.text = health.ToString();

        /*if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position +=  new Vector3(0.05f,0f,0f);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position -= new Vector3(0.05f, 0f, 0f);
        }*/

        //if(Input.GetAxis)

        //Debug.Log(Input.GetAxis("Horizontal"));

        //transform.position += new Vector3(Input.GetAxis("Yatay")*Time.deltaTime*speed, 0f, 0f);
        //GetComponent<Transform>().position += new Vector3(Input.GetAxis("Yatay")*Time.deltaTime*speed, 0f, 0f);

        mySpeedX = Input.GetAxis("Yatay");
            speed = mySpeedX * 5f;
            myBody.velocity = new Vector2(speed, myBody.velocity.y);
            myAnimator.SetFloat("Speed", Mathf.Abs(speed));
            myAnimator.SetBool("Ground", onGround);
         

         YonuKontrolEt();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (onGround)
            {
            myBody.velocity = new Vector2(myBody.velocity.x, jumpPower);
            canDoubleJump = true;
            myAnimator.SetTrigger("Jump");
            }
            else
            {
                if (canDoubleJump)
                {
                    myBody.velocity = new Vector2(myBody.velocity.x, jumpPower);
                    myAnimator.SetTrigger("Jump");
                    canDoubleJump = false;
                }
            }
        }


        


        if (Input.GetMouseButtonDown(0) && arrowNumber>0)
        {
            if (!attacked)
            {
                myAnimator.SetTrigger("Attack");
                Invoke("Fire", 0.5f);
                attacked = true;
            }
            
        }

        if (attacked)
        {
            attackTimer -= Time.deltaTime;
        }
        else
        {
            attackTimer = defaultAttackTimer;
        }

        if (attackTimer < 0)
        {
            attacked = false;
        }

    }

    void Fire()
    {
        
        GameObject ok = Instantiate(arrow, transform.position, Quaternion.identity);
        ok.transform.parent = GameObject.Find("Arrows").transform;

        if (transform.localScale.x > 0)
        {
            ok.GetComponent<Rigidbody2D>().velocity = new Vector2(Time.deltaTime * arrowSpeed, 0f);
        }
        else
        {
            Vector3 okScale = ok.transform.localScale;
            ok.transform.localScale = new Vector3(-okScale.x, okScale.y, okScale.z);
            ok.GetComponent<Rigidbody2D>().velocity = new Vector2(-Time.deltaTime*arrowSpeed, 0f);
        }

        arrowNumber -= 1;
        arrowNumberText.text = arrowNumber.ToString();

        
    }

    //Bu metot içerisinde karakterin sağa ve sola dönüşü kontrol edilmektedir.
    void YonuKontrolEt()
    {
        if (mySpeedX > 0)
        {
            transform.localScale = new Vector3(defaultLocalScale.x, defaultLocalScale.y, defaultLocalScale.z);
        }
        else if (mySpeedX < 0)
        {
            transform.localScale = new Vector3(-defaultLocalScale.x, defaultLocalScale.y, defaultLocalScale.z);
        }
    }


    public void Die()
    {
        
        soundController.GetComponent<AudioSource>().clip = null;
        soundController.GetComponent<AudioSource>().PlayOneShot(audioClip);
        myAnimator.SetFloat("Speed", 0);
        myAnimator.SetTrigger("Die");
        GetComponent<CapsuleCollider2D>().sharedMaterial = null;
        // myBody.constraints = RigidbodyConstraints2D.FreezeAll;
        myBody.constraints = RigidbodyConstraints2D.FreezePositionX;
        myBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        //Time.timeScale = 0;
        enabled = false;
        StartCoroutine(Wait(false));


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Die();
        }
        else if (collision.gameObject.CompareTag("Finish"))
        {
            StartCoroutine(Wait(true));
        }
    }

    IEnumerator Wait(bool win)
    {
       
        yield return new WaitForSecondsRealtime(2f);
        Time.timeScale = 0;

        if (win)
        {
            winPanel.SetActive(true);
        }
        else
        {

            losePanel.SetActive(true);
        }

    }

}

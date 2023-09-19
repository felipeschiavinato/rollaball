using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UIElements;



public class PlayerController : MonoBehaviour
{

    public float speed = 0;
    public int health;
    private float t;

    public GameObject heart0, heart1, heart2;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI recordText;
    public float startTime;
    public GameObject winTextObject;
    public GameObject loseTextObject;
    public GameObject endButtons;
    public GameObject Player;

    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;

    private Renderer rend;
    private Color originalColor;

    public AudioSource src, srcAmbient;
    public AudioClip sfx1, sfx2, sfxAmbient;

    




    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        health = 3;



        SetCountText();
        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);
        endButtons.SetActive(false);
        recordText.gameObject.SetActive(false);
        heart0.gameObject.SetActive(true);
        heart1.gameObject.SetActive(true);
        heart2.gameObject.SetActive(true);

        startTime = Time.time;

        rend = GetComponent<Renderer>();
        if (rend == null)
        {

        }
        else
        {
            originalColor = rend.material.color;
        }

        srcAmbient.clip = sfxAmbient;
        srcAmbient.Play();
    }


    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;

    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        if(count >= 9)
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;

            if(RecordController.isFirstTime==true)
            {
                RecordController.FlagFirstTime();
            }
            
            winTextObject.SetActive(true);
            timerText.gameObject.SetActive(false);
            recordText.gameObject.SetActive(true);
            if(t<RecordController.record)
            {
                RecordController.SaveRecord(t);

                recordText.text = "New Record: " + RecordController.record.ToString("f2");
            }
            else
            {
                recordText.text = "Record: " + RecordController.record.ToString("f2");
            }
            endButtons.SetActive(true);
            
            
        }
    }


    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PickUp"))
        {
            src.clip = sfx2;
            src.time = 0.5f;
            src.Play();
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Obstacle"))
        {
            health--;
            src.clip = sfx1;
            src.time = 0.5f;
            src.Play();

            

            StartCoroutine(BlinkRed(0.1f, Color.red));
            if(health == 0)
            {   
                loseTextObject.SetActive(true);
                timerText.gameObject.SetActive(false);
                heart2.gameObject.SetActive(false);
                endButtons.SetActive(true);

                if (RecordController.isFirstTime==false)
                {
                    recordText.gameObject.SetActive(true);
                    recordText.text = "Record: " + RecordController.record.ToString("f2");
                }
            
                rb.constraints = RigidbodyConstraints.FreezeAll;
                
                

                
            }
            else if(health == 2)
            {
                heart0.gameObject.SetActive(false);
            }
            else if(health == 1)
            {
                heart1.gameObject.SetActive(false);
            }
    
            
        }
    }

    IEnumerator BlinkRed(float duration, Color blinkColor)
    {

        rend.material.color = blinkColor;
        yield return new WaitForSeconds(duration);
        rend.material.color = originalColor;

    }

    void Update()
    {
        t = Time.time - startTime;

        string minutes = ((int) t / 60).ToString();
        string seconds = (t % 60).ToString("f2");

        timerText.text = minutes + ":" + seconds;

        //get position y of player
        float y = Player.transform.position.y;
        if (y < -10)
        {
            loseTextObject.SetActive(true);
            timerText.gameObject.SetActive(false);
            endButtons.SetActive(true);
            if (RecordController.isFirstTime==false)
                {
                    recordText.gameObject.SetActive(true);
                    recordText.text = "Record: " + RecordController.record.ToString("f2"); 
                }

            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

}



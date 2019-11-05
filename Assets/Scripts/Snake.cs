using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Snake : MonoBehaviour
{
    public GameObject TailPrefab,Gameover,LifeCanvas,Startc;

    public  List<GameObject> tails = new List<GameObject>();
    private Vector3 direction;
    private float snakeHeadWidth;
    private GameManager gameManager;
    private Vector3 lastTailPosition;
    private int lives = 3;
    public Text life,highscore,scorehs;
    private int score;private int v;
    public Text txt;
    private int defaultTails = 2;


    void Start()
    {
        Startc.SetActive(false);
        var sprite = GetComponent<SpriteRenderer>().sprite;
        snakeHeadWidth = sprite.rect.width / sprite.pixelsPerUnit;

        // by default add two tails
        for (int i=0;i<defaultTails;++i)
        {
            var tail = Instantiate(TailPrefab) as GameObject;

            // disabling colliders for default tails
            tail.GetComponent<BoxCollider2D>().enabled = false;
            tail.transform.position = transform.position - new Vector3(snakeHeadWidth * (i + 1), 0, 0);
            tails.Add(tail);
        }

        direction = Vector2.right;
        InvokeRepeating("Move", 0, 0.1f);
    }

    void Move()
    {
        lastTailPosition = tails[tails.Count - 1].transform.position;

        Vector3 prevPos = transform.position;
        Vector3 currentPos = prevPos + (direction * snakeHeadWidth);
        transform.position = currentPos;

        for (int i=0;i<tails.Count;++i)
        {
            var temp = prevPos;
            prevPos = tails[i].transform.position;
            tails[i].transform.position = temp;
        }
    }

    public void SetGameManager(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            if(Vector3.Dot(direction, Vector3.right) >= 0)
                direction = Vector3.right;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if(Vector3.Dot(direction, Vector3.left) >= 0)
                direction = Vector3.left;
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            if (Vector3.Dot(direction, Vector3.up) >= 0)
                direction = Vector3.up;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (Vector3.Dot(direction, Vector3.down) >= 0)
                direction = Vector3.down;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Boundary"))
        {
            if (lives > 0)
            {
                lives--;
                life.text = lives.ToString();

                // TODO @students [20 marks]
                // Stop the movement immediately (no spawning of food or no input controls)
                // Show reviving in 3 secs (start timer for 3 secs)
                // Move the snake to last 5 positions in reverse direction (Note : while moving it should not collect any existing food)
                // After 5 positions reached, start with the direction it left on 5th position

                // TEMPORARY CODE
                // change position to start x=0,y=0
                transform.position = Vector3.zero;
            }
            else
            {
                Gameover.SetActive(true);
                LifeCanvas.SetActive(false);
                PlayerPrefs.SetInt("HighScore", 4);
                v = PlayerPrefs.GetInt("HighScore");
                if (score > v)
                {
                    PlayerPrefs.SetInt("HighScore", score);

                }
                highscore.text = Convert.ToString(PlayerPrefs.GetInt("HighScore"));
                scorehs.text = Convert.ToString(PlayerPrefs.GetInt("HighScore"));




                gameManager.EndGame(tails.Count - defaultTails);
            }
        }

        

    }

   

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Food"))
        {
            if(Vector3.Distance(collision.gameObject.transform.position, transform.position) <= 0.001f)
            {
                bool isDestroyed = gameManager.foodManager.DestroyFood(collision.gameObject);
                if(isDestroyed)
                {
                    var tail = Instantiate(TailPrefab) as GameObject;
                    tail.transform.position = lastTailPosition;
                    tails.Add(tail);
                    score += 1;

                    txt.text = score.ToString();
                }
            }
        }
        if (collision.gameObject.CompareTag("Tail"))
        {
            // TODO @students [10 marks]
            // Cut the remaining tail including collided block
            if (Vector3.Distance(collision.gameObject.transform.position, transform.position) <= 0.001f)

            {
                int j = tails.IndexOf(collision.gameObject);
                for (int i = j; i <= tails.Count; i++)
                {
                    var gameobject = tails[tails.Count - 1];
                    tails.RemoveAt(tails.Count - 1);
                    //tails[i] = null;
                    Destroy(gameobject);

                    Debug.Log("Destroyed");
                }

            }
        }
    }

    private void OnDestroy()
    {
        // cleanup
        foreach (var tail in tails)
        {
            Destroy(tail);
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    public GameObject FoodPrefab;
    public GameObject Left;
    public GameObject Right;
    public GameObject Top;
    public GameObject Bottom;

    private GameManager gameManager;
    public Snake s;
    public bool coincide;
    private List<GameObject> foodGOs = new List<GameObject>();
    private int minX, maxX, minY, maxY;
    private float spriteWidth;
    private float spawnInterval = 3f;
    
    void Start()
    {
        // use any boundary to get width
        // all boundaries will have equal width
        var sprite = Left.GetComponent<SpriteRenderer>().sprite;
        spriteWidth = sprite.rect.width / sprite.pixelsPerUnit;

        // temp code
        minX = (int)(Left.transform.position.x / spriteWidth) + 1;
        maxX = (int)(Right.transform.position.x / spriteWidth) - 1;
        minY = (int)(Bottom.transform.position.y / spriteWidth) + 1;
        maxY = (int)(Top.transform.position.y / spriteWidth) - 1;

        InvokeRepeating("SpawnFood", 1, spawnInterval);
    }

    public void SetGameManager(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    public void SpawnFood()
    {
        // TODO @students [20 marks]
        // Food should not spawn on snake blocks
        coincide = false;
        var food = Instantiate(FoodPrefab) as GameObject;
        var x = Random.Range(minX, maxX);
        var y = Random.Range(minY, maxY);
        //food.transform.position = new Vector3((x * spriteWidth), (y * spriteWidth), 0);
        if (Vector3.Distance(food.transform.position, s.transform.position) <= 0.5f)
        {
            coincide = true;
        }
        for (int i=0;i<s.tails.Count;i++)
        {
            if (Vector3.Distance(food.transform.position, s.tails[i].transform.position) <= 0.5f)
            {
                coincide = true;
            }
        }

        
            if (coincide == false)
            {

                food.transform.position = new Vector3((x * spriteWidth), (y * spriteWidth) , 0);
            }
           
        
        foodGOs.Add(food);
    }

    public bool DestroyFood(GameObject food)
    {
        bool isRemoved = foodGOs.Remove(food);

        if (isRemoved)
            Destroy(food);

        return isRemoved;
    }

    private void OnDestroy()
    {
        foreach (var food in foodGOs)
            Destroy(food);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Snake snake;
    public FoodManager foodManager;
    
    void Start()
    {
        snake.SetGameManager(this);
        foodManager.SetGameManager(this);
    }

    public void EndGame(int score)
    {
        Destroy(snake.gameObject);
        Destroy(foodManager.gameObject);
    }
}

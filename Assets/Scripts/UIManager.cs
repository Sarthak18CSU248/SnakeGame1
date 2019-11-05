using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject Boundaries, GameManager, FoodManager, Snake,life;
    // Start is called before the first frame update
    void Start()
    {
        // TODO @students [10 marks]
        // Implement UI for lives, game over screen, high score with player prefs, restart, start menu
    }
    public void restart()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void StartGame()
    {
        Boundaries.SetActive(true);
        GameManager.SetActive(true);
        FoodManager.SetActive(true);
        Snake.SetActive(true);
        life.SetActive(true);


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

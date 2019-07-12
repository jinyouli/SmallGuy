using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int level = 1;
    public int food = 100;
    private static GameManager _instance;

    public List<enemy> enemyList = new List<enemy>();
    private Text foodText;
    private Text failText;
    private player player;
    private map mapManager;
    public bool isEnd = false;
    public AudioClip dieclip;

    private Image dayImage;
    private Text dayText;

    public static GameManager Instance {
        get {
            return _instance;
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(gameObject);
        initGame();
    }

    void initGame() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<player>();
        mapManager = GetComponent<map>();
        mapManager.initMap(); 

        foodText = GameObject.Find("foodText").GetComponent<Text>();
        failText = GameObject.Find("gameFailure").GetComponent<Text>();
        failText.enabled = false;
        updateFoodText(0);
        dayImage = GameObject.Find("dayImage").GetComponent<Image>();
        dayText = GameObject.Find("dayText").GetComponent<Text>();
        dayText.text = "Day " + level;
        Invoke("HideBlack",1);

        isEnd = false;
        enemyList.Clear();
        
    }

    void updateFoodText(int foodChange) {
        if (foodChange == 0)
        {
            foodText.text = "Food : " + food;
        }
        else {

            string str = "";
            if (foodChange < 0)
            {
                str = foodChange.ToString();
            }
            else {
                str = "+" + foodChange;
            }
            foodText.text = str + " Food : " + food;
        }
        
    }

    public void ReduceFood(int count) {
        food -= count;

        if (food <= 0 ) {
            failText.enabled = true;
            musicManager.instance.RandomPlay(dieclip);
            musicManager.instance.Stopmusic();
        }

        updateFoodText(-count);
    }

    void HideBlack() {
        dayImage.gameObject.SetActive(false);
    }

    public void AddFood(int count) {
        food += count;
        updateFoodText(count);
    }

    private bool sleepStep = true;

    public void OnPlayerMove() {
        if (sleepStep == true)
        {
            sleepStep = false;
        }
        else {
            foreach (var enemy in enemyList) {
                enemy.Move();
            }
            sleepStep = true;
        }

        if (player.targetPos.x == mapManager.col - 2 && player.targetPos.y == mapManager.row - 2) {
            isEnd = true;
            Application.LoadLevel(Application.loadedLevel);
        }

    }

    private void OnLevelWasLoaded(int screenLevel)
    {
        level++;
        initGame();
    }
}

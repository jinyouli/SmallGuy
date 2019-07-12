using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class map : MonoBehaviour
{
    public GameObject[] wallArray;
    public GameObject[] outWallArray;
    public GameObject[] floorArray;
    public GameObject[] foodArray;
    public GameObject[] enemyArray;
    public GameObject exitPrefab;

    public int row = 10;
    public int col = 10;

    public int minCountWall = 2;
    public int maxCountWall = 9;

    private Transform mapHolder;
    private List<Vector2> positionList = new List<Vector2>();
    private GameManager gameManager;

    // Start is called before the first frame update
    void Awake()
    {
        gameManager = this.GetComponent<GameManager>();
        initMap();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void initMap() {

        mapHolder = new GameObject("Map").transform;
        for (int x = 0; x < col; x++) {
            for (int y = 0; y < row; y++) {

                if (x == 0 || y == 0 || x == col - 1 || y == row - 1) {
                    int index = Random.Range(0, outWallArray.Length);
                    GameObject go = GameObject.Instantiate(outWallArray[index], new Vector3(x, y, 0), Quaternion.identity);
                    go.transform.SetParent(mapHolder);
                }
                else {
                    int index = Random.Range(0, outWallArray.Length);
                    GameObject go = GameObject.Instantiate(floorArray[index], new Vector3(x, y, 0), Quaternion.identity);
                    go.transform.SetParent(mapHolder);
                }

            }
        }

        positionList.Clear();
        for (int x = 2; x < col - 2; x++) {
            for (int y = 2; y < row - 2; y++) {
                positionList.Add(new Vector2(x, y));
            }
        }

        int wallCount = Random.Range(minCountWall, maxCountWall + 1);
        initThing(wallCount,wallArray);

        int foodCount = Random.Range(2, gameManager.level * 2 + 1);
        initThing(foodCount,foodArray);

        int enemyCount = gameManager.level / 2 + 1;
        initThing(enemyCount,enemyArray);

        GameObject go4 = Instantiate(exitPrefab,new Vector2(col - 2,row - 2),Quaternion.identity) as GameObject;
        go4.transform.SetParent(mapHolder);
    }

    private void initThing(int count, GameObject[] array) {
        for (int i = 0; i < count; i++)
        {
            Vector2 pos = RandomPosition();
            GameObject enemyPrefab = RandomPrefab(array);
            GameObject go = Instantiate(enemyPrefab, pos, Quaternion.identity) as GameObject;
            go.transform.SetParent(mapHolder);
        }
    }

    private Vector2 RandomPosition() {
        int positionIndex = Random.Range(0,positionList.Count);
        Vector2 pos = positionList[positionIndex];
        positionList.RemoveAt(positionIndex);
        return pos;
    }

    private GameObject RandomPrefab(GameObject[] prefabs) {
        int index = Random.Range(0,prefabs.Length);
        return prefabs[index];
    }
}

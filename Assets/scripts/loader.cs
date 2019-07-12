using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loader : MonoBehaviour
{
    public GameObject gameManager;
    // Start is called before the first frame update
    void Awake()
    {
        if (GameManager.Instance == null)
            GameObject.Instantiate(gameManager);
    } 

    // Update is called once per frame
    void Update()
    {
        
    }
}

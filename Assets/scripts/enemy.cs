using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    private Vector2 targetPosition;
    private Transform player;
    private Rigidbody2D enemybody;

    private BoxCollider2D colider;
    private Animator animator;
    public int lossFood = 10;

    public float smoothing = 3;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemybody = GetComponent<Rigidbody2D>();
        targetPosition = transform.position;
        GameManager.Instance.enemyList.Add(this);
        colider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        enemybody.MovePosition(Vector2.Lerp(transform.position,targetPosition,smoothing * Time.deltaTime));
    }

    public void Move() {
        Vector2 offset = player.position - transform.position;


        if (offset.magnitude < 1.1f)
        {
            animator.SetTrigger("enemy_attack");
            player.SendMessage("TakeDamage" , lossFood);
        }
        else {

            float x = 0, y = 0;
            Debug.Log("test2 ==  " + offset.x + "  " + offset.y);
            Debug.Log("test3 ==  " + Mathf.Abs(offset.x) + "  " + Mathf.Abs(offset.y));
            if (Mathf.Abs(offset.y) > Mathf.Abs(offset.x))
            {
                Debug.Log("vcbvcxbz == 1");
                if (offset.y < 0)
                {
                    y = -1;
                }
                else
                {
                    y = 1;
                }
            }
            else {
                Debug.Log("vcbvcxbz == 2");
                if (offset.x > 0)
                {
                    x = 1;
                }
                else {
                    x = -1;
                }
            }

            //RaycastHit2D hit = Physics2D.Linecast(targetPosition,targetPosition + new Vector2(x,y));
            targetPosition += new Vector2(x,y);
        }
    }
}

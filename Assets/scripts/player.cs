using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public float smoothing = 1;
    public float restTime = 1;
    public float restTimer = 0;
    public AudioClip audio1clip;
    public AudioClip audio2clip;

    [HideInInspector]public Vector2 targetPos = new Vector2(1,1);
    private Rigidbody2D playerbody;
    private BoxCollider2D colider;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        playerbody = GetComponent<Rigidbody2D>();
        colider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        playerbody.MovePosition(Vector2.Lerp(transform.position, targetPos, smoothing * Time.deltaTime));
        if (GameManager.Instance.food <= 0 || GameManager.Instance.isEnd) return;
        restTimer += Time.deltaTime;

       if (restTimer < restTime) return;

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (h > 0) {
            v = 0;
        }

        if (h != 0 || v != 0)
        {
            GameManager.Instance.ReduceFood(1);
            colider.enabled = false;
            RaycastHit2D hit = Physics2D.Linecast(targetPos, targetPos + new Vector2(h, v));
            //colider.enabled = true;

            if (hit.transform == null)
            {
                targetPos += new Vector2(h, v);
            }
            else
            {
                Debug.Log("vcbvcxbz == " + hit.collider.tag);
                switch (hit.collider.tag) {
                    case "outwall":
                        break;
                    case "wall":
                        // animator.SetTrigger("player_attack");
                        // hit.collider.SendMessage("TakeDamage");
                        break;
                    case "cherry":
                        GameManager.Instance.AddFood(10);
                        targetPos += new Vector2(h,v);
                        Destroy(hit.transform.gameObject);
                        break;
                    case "cola":

                        GameManager.Instance.AddFood(20);
                        targetPos += new Vector2(h, v);
                        Destroy(hit.transform.gameObject);
                        break;
                    case "enemy":
                        targetPos += new Vector2(h, v);
                        break;
                 }
            }
        }

        GameManager.Instance.OnPlayerMove();
        restTimer = 0;
    }

    public void TakeDamage(int lossFood) {
        musicManager.instance.RandomPlay(audio1clip, audio2clip);
        GameManager.Instance.ReduceFood(lossFood);
        animator.SetTrigger("player_beattack");
    }
}

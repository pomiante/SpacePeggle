using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    Wave.Shooting shot;
    ProjectileManager pm;
    int ySign;
    [SerializeField] float speed=1;
    Rigidbody2D rb;
    void Start()
    {
        pm = FindObjectOfType<ProjectileManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shot == Wave.Shooting.straight)
        {
            rb.velocity = new Vector2(0, ySign * speed * Time.deltaTime);
        }
    }

    public void Setup(Wave.Shooting s, Vector3 pos, int ys) 
    {
        shot = s;
        transform.position = pos;
        ySign = ((int)Mathf.Sign(ys));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Top")
        {
            print("top");
            pm.Gather(this);
        }
        else if (collision.tag == "Player")
        {
            print(collision.name);
            StartCoroutine(collision.gameObject.GetComponent<playerShip>().GetHit());
            pm.Gather(this);
        }
    }

}

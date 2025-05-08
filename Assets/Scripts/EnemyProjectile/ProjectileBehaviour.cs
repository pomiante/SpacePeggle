using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    Wave.Shooting shot;
    ProjectileManager pm;
    [SerializeField] playerShip player;
    int ySign;
    [SerializeField] float speed=1;
    Rigidbody2D rb;
    Vector2 dir;
    Vector3 dir3d;
    void Start()
    {
        pm = FindObjectOfType<ProjectileManager>();
        rb = GetComponent<Rigidbody2D>();
        //dir3d = Vector3.zero; dir = Vector2.zero;
    }

    private void Awake()
    {
        player = FindObjectOfType<playerShip>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (shot)
        {
            case Wave.Shooting.straight:
                rb.velocity = new Vector2(0, ySign * speed * Time.deltaTime);
                break;
            case Wave.Shooting.pointed:
                rb.velocity = speed * Time.deltaTime * dir;
                break;
            case Wave.Shooting.shootNum:
                break;
            default:
                break;
        }
    }

    public void Setup(Wave.Shooting s, Vector3 pos, int ys, float ps) 
    {
        speed = ps;
        shot = s;
        transform.position = pos;
        ySign = ((int)Mathf.Sign(ys));
        switch (shot)
        {
            case Wave.Shooting.straight:
                break;
            case Wave.Shooting.pointed:
                dir3d = player.transform.position - pos;
                dir = new Vector2(dir3d.x, dir3d.y).normalized;
                transform.rotation = Quaternion.Euler(0f, 0f, Vector2.SignedAngle(Vector2.up, dir));
                break;
            case Wave.Shooting.shootNum:
                break;
            default:
                break;
        }
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
            //StartCoroutine(collision.gameObject.GetComponent<playerShip>().GetHit());
            print(collision.name);
            pm.Gather(this);
        }
    }

}

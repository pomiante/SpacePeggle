using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLogic : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] BallManager bm;
    [SerializeField] float force=1;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bm = FindObjectOfType<BallManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setup(float angle, Transform origin) 
    {
        Vector2 dir = new Vector2(-1 * Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad)).normalized;
        transform.position = origin.position;
        rb.AddForce(force*dir,ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("trigger " + collision.tag + collision.gameObject.layer);
        if(collision.gameObject.layer == LayerMask.GetMask("Gutter") || collision.tag == "Gutter")
        {
            print("gather start");
            bm.Gather(this);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerShip : MonoBehaviour
{
    internal int Balls;
    [SerializeField]
    float moveSpeed, coolTime;
    [SerializeField] bool invertedVertical, onCooldown, noBalls, canShoot;
    [SerializeField] int verticalMult;
    [SerializeField] Transform arrowT;
    [SerializeField] Gradient red, purple;
    [SerializeField] BallManager ballManager;
    [SerializeField] float angle;
    [SerializeField] LineRenderer lr;
    Rigidbody2D rb;
    internal int healthPoints=3;
    // Start is called before the first frame update
    void Start()
    {
        //print(transform.rotation.x);
        isInverted();
        rb = GetComponent<Rigidbody2D>();
        lr = GetComponentInChildren<LineRenderer>();
        print(lr.colorGradient);
        arrowT = lr.transform;
        arrowT.gameObject.SetActive(false);
        angle = 0;
        onCooldown = false; noBalls = false; canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (invertedVertical) { verticalMult = -1; } else { verticalMult = 1; }
        rb.MovePosition(rb.position + new Vector2(Input.GetAxisRaw("Horizontal"),
        Input.GetAxisRaw("Vertical")).normalized * Time.deltaTime * moveSpeed);
        if (Input.GetKeyDown(KeyCode.Mouse0)) { arrowT.gameObject.SetActive(true); }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Point();//punto pantalla (angulo?) <- point
        }
        if (Input.GetKeyUp(KeyCode.Mouse0)) { 
            if (canShoot) { Shoot(); }
            arrowT.gameObject.SetActive(false);
        }
    }

    void isInverted() 
    {
        if (Mathf.Abs(transform.rotation.z) == 1) { invertedVertical = true; }
        else if(transform.rotation.z == 0) { invertedVertical = false; }
    }

    void Point() 
    {
        Vector3 dir3 =  Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        Vector2 dir = new Vector2(dir3.x, dir3.y).normalized;
        angle = Vector2.SignedAngle(Vector2.up, dir);
        arrowT.rotation = Quaternion.Euler(0f, 0f, angle);
    }
    public void UpdateBalls(int b)
    {
        Balls = b;
        noBalls = b > 0;
        UpdateArrow();
    }
    void UpdateArrow()
    {
        canShoot = noBalls && !onCooldown;
        if (canShoot){ lr.colorGradient = purple; }
        else { lr.colorGradient = red; }
    }
    void Shoot()
    {
        ballManager.Shoot(angle, this.transform);
        StartCoroutine(Cooldown(coolTime));
    }    
    public IEnumerator Cooldown(float t) { 
        if(t < 0) { t = 0.5f; }
        //lr.colorGradient = red;
        onCooldown = true;
        UpdateArrow();
        yield return new WaitForSeconds(t);
        onCooldown = false;
        UpdateArrow();
    }

    public IEnumerator GetHit()
    {
        healthPoints -= 1;
        print("getting hit");
        yield return new WaitForSeconds(0.5f);
        print("got hit");
        if(healthPoints <= 0) { SceneManager.LoadScene("titleScreen"); }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "NMEshoot")
        {
            StartCoroutine(GetHit());

        }
    }
}

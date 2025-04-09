using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerShip : MonoBehaviour
{
    [SerializeField]
    int Balls;
    [SerializeField]
    float moveSpeed, coolTime;
    [SerializeField] bool invertedVertical, onCooldown, noBalls, canShoot;
    [SerializeField] int verticalMult;
    [SerializeField] Transform arrowT;
    [SerializeField] Gradient red, purple;
    [SerializeField] BallManager ballManager;
    [SerializeField] float angle;
    [SerializeField] LineRenderer lr;
    // Start is called before the first frame update
    void Start()
    {
        //print(transform.rotation.x);
        isInverted();
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
        transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal")* verticalMult,
        Input.GetAxisRaw("Vertical") * verticalMult).normalized * Time.deltaTime * moveSpeed);
        if (Input.GetKeyDown(KeyCode.Mouse0)) { arrowT.gameObject.SetActive(true); }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Point();//punto pantalla (angulo?) <- point
        }
        if (Input.GetKeyUp(KeyCode.Mouse0)) { 
            if (Balls > 0) { Shoot(); }
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
        lr.colorGradient = red;
        yield return new WaitForSeconds(t);
        UpdateArrow();
    }
}

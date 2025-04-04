using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerShip : MonoBehaviour
{
    [SerializeField]
    int Balls;
    [SerializeField]
    float moveSpeed;
    [SerializeField] bool invertedVertical;
    [SerializeField] int verticalMult;
    // Start is called before the first frame update
    void Start()
    {
        print(transform.rotation.x);
        isInverted();
    }

    // Update is called once per frame
    void Update()
    {
        if (invertedVertical) { verticalMult = -1; } else { verticalMult = 1; }
        transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal"),
        Input.GetAxisRaw("Vertical") * verticalMult).normalized * Time.deltaTime * moveSpeed);
        if (Input.GetKeyDown(KeyCode.Mouse0) && Balls > 0)
        {
            Fire();//punto pantalla (angulo?)
        }
    }

    void isInverted() 
    {
        if (Mathf.Abs(transform.rotation.x) == 1) { invertedVertical = true; }
        else if(transform.rotation.x == 0) { invertedVertical = false; }
    }

    void Fire() 
    {
        //print(Vector2.Angle(new Vector2(transform.position.x,transform.position.y), new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y)));
        Vector3 dir3 =  Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        Vector2 dir = new Vector2(dir3.x, dir3.y).normalized;
        print("old" + dir);
        float ang = Vector2.Angle(transform.up, dir);
        //print(ang);
        if (ang > 60) { ang = 60; }
        Vector2 nudir = new Vector2(Mathf.Sign(dir.x) * Mathf.Sin(ang*Mathf.Deg2Rad),Mathf.Sign(transform.up.y) * Mathf.Cos(ang*Mathf.Deg2Rad));
        print("new" + nudir);
    }
    
}

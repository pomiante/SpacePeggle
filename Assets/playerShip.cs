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
    [SerializeField] Transform arrowT;
    [SerializeField] Gradient red, purple;
    // Start is called before the first frame update
    void Start()
    {
        //print(transform.rotation.x);
        isInverted();
        var arrLine = GetComponentInChildren<LineRenderer>();
        print(arrLine.colorGradient);
        arrowT = arrLine.transform;
        arrowT.gameObject.SetActive(false);
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
            if (Balls > 0) { /*shoot*/}
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
        float ang= Vector2.SignedAngle(Vector2.up, dir);
        print(ang + "<-old");
        arrowT.rotation = Quaternion.Euler(0f, 0f, ang);
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerShip : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal"),
        Input.GetAxisRaw("Vertical")));
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Fire();//punto pantalla (angulo?)
        }
    }

    void Fire() { }
}

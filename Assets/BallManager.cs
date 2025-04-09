using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    [SerializeField] BallLogic Ball;
    [SerializeField] int BallsAtPlay=0;
    [SerializeField] List<BallLogic> BolaLibre;
    [SerializeField] List<BallLogic> BolaEnJuego;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < BallsAtPlay; i++)
        {
            BallLogic thisBall = Instantiate(Ball,transform);
            BolaLibre.Add(thisBall);
            thisBall.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

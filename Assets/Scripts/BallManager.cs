using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    [SerializeField] playerShip player;
    [SerializeField] BallLogic Ball;
    [SerializeField] int BallsAtPlay=0;
    [SerializeField] List<BallLogic> BolaLibre;
    [SerializeField] List<BallLogic> BolaEnGameSpace;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < BallsAtPlay; i++)
        {
            BallLogic thisBall = Instantiate(Ball,transform);
            BolaLibre.Add(thisBall);
            thisBall.gameObject.SetActive(false);
        }
        player.UpdateBalls(BolaLibre.Count);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot(float angle, Transform origin)
    {
        if(BolaLibre.Count == 0) { return;}
        BallLogic current = BolaLibre[0];
        BolaLibre.RemoveAt(0);
        current.gameObject.SetActive(true);
        current.Setup(angle,origin);
        BolaEnGameSpace.Add(current);
        player.UpdateBalls(BolaLibre.Count);
    }

    public void Gather(BallLogic currentBall)
    {
        BolaEnGameSpace.Remove(currentBall);
        BolaLibre.Add(currentBall);
        currentBall.gameObject.SetActive(false);
        player.UpdateBalls(BolaLibre.Count);
    }
}

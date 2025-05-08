using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRowBehaviour : MonoBehaviour
{
    bool active;
    Wave.Shooting shot;
    Wave.Movement move;
    Wave.Entrance enter;
    [SerializeField]List<EnemyShipBehaviour> ships;
    [SerializeField]EnemyWavesBehaviour waves;
    float columnGap, leftMost, rightMost, leftBoundary = -4, rightBoundary = 4;
    [SerializeField] float speed;
    int xSign;
    Vector2 moveTo;
    private void Awake()
    {
        active = false;
        //print("prewaves");
        waves = FindObjectOfType<EnemyWavesBehaviour>();
        //print(waves.name);
        ships = new List<EnemyShipBehaviour>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(xSign * speed * Time.deltaTime, 0, 0);
        if((xSign < 0 && transform.position.x + leftMost <= leftBoundary) || (xSign > 0 && transform.position.x + rightMost >= rightBoundary))
        {
            xSign *= -1;
        }
        if(active && ships.Count == 0)
        {
            //decomission
        }
    }

    /*public void Setup(Wave.Entrance e, Wave.Movement m, Wave.Shooting s, int numShips, float cg, Vector2 coord)
    {
        active = false;
        shot = s; move = m; enter = e;
        columnGap = cg;
        moveTo = coord;
        transform.Translate(coord.x,coord.y, 0); //cambiar en el futuro
        xSign = 0;
        while(xSign == 0) { xSign =Random.Range(-1, 2); }
        leftMost = -1 * columnGap * numShips / 2;
        rightMost = columnGap * numShips / 2;
        for(int i = 0; i < numShips; i++)
        {
            if(waves == null) { print("shit"); }
            else if(waves.shipLibreList == null) { print("fuck"); }
            else if (waves.shipLibreList.Count == 0) { print("what");}
            else if (waves.shipLibreList[0] == null) { print("really what"); }
            EnemyShipBehaviour thisShip = waves.shipLibreList[0];
            thisShip.gameObject.SetActive(true);
            waves.shipLibreList.Remove(thisShip);
            ships.Add(thisShip);
            thisShip.transform.SetParent(transform);
            thisShip.Setup(s, new Vector2(coord.x + i * cg - numShips * cg /2, coord.y));
        }
        active = true;
    }*/

    public void Setup(Row r, Vector2 coord)
    {
        active = false;
        shot = r.shootPattern; move = r.movePattern; enter = r.enterType;
        columnGap = r.columnGap;
        moveTo = coord;
        transform.Translate(coord.x, coord.y, 0); //cambiar en el futuro
        xSign = 0;
        while (xSign == 0) { xSign = Random.Range(-1, 2); }
        leftMost = -1 * columnGap * r.shipsNum / 2;
        rightMost = columnGap * r.shipsNum / 2;
        for (int i = 0; i < r.shipsNum; i++)
        {
            if (waves == null) { print("shit"); }
            else if (waves.shipLibreList == null) { print("fuck"); }
            else if (waves.shipLibreList.Count == 0) { print("what"); }
            else if (waves.shipLibreList[0] == null) { print("really what"); }
            EnemyShipBehaviour thisShip = waves.shipLibreList[0];
            thisShip.gameObject.SetActive(true);
            waves.shipLibreList.Remove(thisShip);
            ships.Add(thisShip);
            thisShip.transform.SetParent(transform);
            thisShip.Setup(shot, new Vector2(coord.x + i * columnGap - r.shipsNum * columnGap / 2, coord.y), r.projSpeed);
        }
        active = true;
    }
    public void Remove(EnemyShipBehaviour thisShip)
    {
        active = false;
        thisShip.gameObject.SetActive(false);
        ships.Remove(thisShip);
        if (ships.Count == 0) { waves.GatherRow(this); }
    }
}

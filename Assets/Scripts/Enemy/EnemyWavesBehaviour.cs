using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Wave
{
    public enum Movement { horizontal, moveNum }
    public enum Shooting { straight, pointed, shootNum }
    public enum Entrance { bottomStraight, enterNum }
    [SerializeField] internal float delayBefore, delayAfter, timeLimit, rowGap/*, columnGap*/;
    //[SerializeField] internal int rowNum, shipsXRow, healthPoints;
    [SerializeField] internal List<Row> rows;
    //[SerializeField] internal Movement movePattern;
    //[SerializeField] internal Shooting shootPattern;
    //[SerializeField] internal Entrance enterType;
    //[SerializeField] internal bool smartRow /*o adapt Rows*/;
    [SerializeField] internal Vector2 waveCenterPosition;
}
[System.Serializable]
public class Row
{
    [SerializeField] internal float columnGap, projSpeed;
    [SerializeField] internal int shipsNum, healthPoints;
    [SerializeField] internal Wave.Movement movePattern;
    [SerializeField] internal Wave.Shooting shootPattern;
    [SerializeField] internal Wave.Entrance enterType;
    [SerializeField] internal bool smartDistancing /*o adapt Rows*/;
}
public class EnemyWavesBehaviour : MonoBehaviour
{
    [SerializeField] EnemyRowBehaviour enemyRow;
    [SerializeField] EnemyShipBehaviour enemyShip;
    [SerializeField] List<Wave> waves;
    [SerializeField] Wave current;
    public List<EnemyShipBehaviour> shipLibreList;
    [SerializeField] List<EnemyRowBehaviour> rowLibreList;
    [SerializeField] GameObject shipStockpile, rowStockpile;
    [SerializeField] int maxShips = 0, maxRows = 0;
    bool working;
    List<EnemyRowBehaviour> curRows;
    // Start is called before the first frame update
    void Start()
    {
        shipLibreList = new List<EnemyShipBehaviour>();
        curRows = new List<EnemyRowBehaviour>();
        rowLibreList = new List<EnemyRowBehaviour>();
        working = waves.Count > 0 && waves[0].rows.Count > 0;
        for (int i = 0; i < maxShips; i++)
        {
            EnemyShipBehaviour thisShip = Instantiate(enemyShip);
            thisShip.gameObject.SetActive(false);
            shipLibreList.Add(thisShip);
            thisShip.transform.SetParent(shipStockpile.transform);
        }
        for (int i = 0; i < maxRows; i++)
        {
            EnemyRowBehaviour thisRow = Instantiate(enemyRow);
            thisRow.gameObject.SetActive(false);
            rowLibreList.Add(thisRow);
            thisRow.transform.SetParent(rowStockpile.transform);
        }
        
        if (working)
        {
            NextWave();
        }
    }

    // Update is called once per frame
    void Update()
    {    }
    public void GatherShip(EnemyShipBehaviour thisShip)
    {
        thisShip.transform.SetParent(shipStockpile.transform);
        shipLibreList.Add(thisShip);
    }
    public void GatherRow(EnemyRowBehaviour thisRow) {
        thisRow.transform.SetParent(rowStockpile.transform);
        thisRow.gameObject.SetActive(false);
        rowLibreList.Add(thisRow);
        curRows.Remove(thisRow);
        if(curRows.Count == 0) { StartCoroutine(DA(current.delayAfter)); }
    }

    IEnumerator DB(float t)
    {
        //delay before
        yield return new WaitForSeconds(t);
        SetupWave();
    }
    IEnumerator DA(float t)
    {
        //delay after
        yield return new WaitForSeconds(t);
        NextWave();
    }
    void SetupWave()
    {
        for (int i = 0; i < current.rows.Count; i++) {
            EnemyRowBehaviour thisRow = rowLibreList[0];
            thisRow.gameObject.SetActive(true);
            curRows.Add(thisRow);
            rowLibreList.Remove(thisRow);
            thisRow.transform.SetParent(transform);
            /*thisRow.Setup(current.enterType, current.movePattern, current.shootPattern, current.shipsXRow,
                            current.columnGap, current.waveCenterPosition + new Vector2(0,-current.rowGap * current.rowNum/2 + current.rowGap * i));*/
            thisRow.Setup(current.rows[i], current.waveCenterPosition + new Vector2(0, -current.rowGap * current.rows.Count / 2 + current.rowGap * i));
        }

    }
    void NextWave()
    {
        if(waves.Count > 0) {
            current = waves[0];
            waves.Remove(current);
            StartCoroutine(DB(current.delayBefore));
        }
        else
        {
            SceneManager.LoadScene("continuara");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipBehaviour : MonoBehaviour
{
    [SerializeField] int hp=1;
    Wave.Shooting shot;
    [SerializeField] float frequency = 0.5f;
    ProjectileManager pm;
    EnemyWavesBehaviour wav;
    EnemyRowBehaviour row;
    bool isDead = false;
    // Start is called before the first frame update
    void Start()
    {
        pm = FindObjectOfType<ProjectileManager>();
        wav = FindObjectOfType<EnemyWavesBehaviour>();
        row = GetComponentInParent<EnemyRowBehaviour>();
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Setup(Wave.Shooting s, Vector2 pos) {
        shot = s;
        transform.Translate(pos.x,pos.y,0);
        StartCoroutine(Shoot(frequency));
    }
    IEnumerator Shoot(float t)
    {
        yield return new WaitForSeconds(t);
        if(!isDead && pm.ProjLibre.Count > 0) pm.Shoot(shot,transform.position,1);
        StartCoroutine(Shoot(t));
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Canica")
        {
            hp -= 1;
            if(hp == 0)
            {
                StartCoroutine(Die());
            }
        }
    }
    IEnumerator Die()
    {
        isDead = true;
        yield return new WaitForSeconds(0.25f);
        row.Remove(this);
        wav.GatherShip(this);
    }

}

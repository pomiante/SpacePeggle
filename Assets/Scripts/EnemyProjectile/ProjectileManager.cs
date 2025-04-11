using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    [SerializeField] ProjectileBehaviour projectile;
    [SerializeField] int maxProjectiles = 0;
    public List<ProjectileBehaviour> ProjLibre;
    [SerializeField] List<ProjectileBehaviour> ProjBusy;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < maxProjectiles; i++)
        {
            ProjectileBehaviour thisBall = Instantiate(projectile, transform);
            ProjLibre.Add(thisBall);
            thisBall.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot(Wave.Shooting s, Vector3 pos, int ys)
    {
        ProjectileBehaviour thisProj = ProjLibre[0];
        ProjLibre.Remove(thisProj);
        ProjBusy.Add(thisProj);
        thisProj.gameObject.SetActive(true);
        thisProj.Setup(s,pos,ys);
    }

    public void Gather(ProjectileBehaviour p)
    {
        ProjBusy.Remove(p);
        ProjLibre.Add(p);
        p.gameObject.SetActive(false);
    }
}

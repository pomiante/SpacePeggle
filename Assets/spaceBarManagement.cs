using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spaceBarManagement : MonoBehaviour
{
    int maxLoops = 5;
    int currLoops;
    Animator animator;
    [SerializeField] TitleScreenManager tsm;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        currLoops = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("SkipToMenu",true);
        }
    }
    public void LoopDone()
    {
        currLoops += 1;
        if (currLoops >= maxLoops) 
        {
            ToMenu();
        }
    }

    public void ToMenu()
    {
        animator.SetBool("JumpToMenu", true);
        tsm.OpenMenu();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreenManager : MonoBehaviour
{
    [SerializeField] titleTxtManager ttm;
    [SerializeField] GridLayoutGroup grid;
    [SerializeField] Image credPan;
    // Start is called before the first frame update
    void Start()
    {
        grid.gameObject.SetActive(false);
        credPan.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OpenMenu()
    {
        print("managePosting");
        ttm.ToMenu();
        grid.gameObject.SetActive(true);

    }
    public void FirstLevel() 
    {
        SceneManager.LoadScene("SampleScene"); //cambiar al índice, probablemente
    }
    public void OpenCredits()
    {
        credPan.gameObject.SetActive(true);
        grid.gameObject.SetActive(false);
    }
    public void CloseCredits()
    {
        credPan.gameObject.SetActive(false);
        grid.gameObject.SetActive(true);
    }
    public void Quit()
    { 
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }

        
}

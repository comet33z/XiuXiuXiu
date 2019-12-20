using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIRoot : MonoSingleton<UIRoot>
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickRestartBtn()
    {
        SceneManager.LoadScene("Main");
    }

    public void ClickToSelectLevelBtn()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoSingleton<GameMgr>
{
    [Header("LevelSettings")]
    public int SwordCnt;
    public int PerfectSwordCnt;
    public int StandardSwordCnt;

    public int CurrentSwordCount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClearSwordCount()
    {
        CurrentSwordCount = 0;
    }

    public void AddCurrentSwordCount()
    {
        CurrentSwordCount++;
    }

    public FloatingTextType GetFloatType()
    {
        if(CurrentSwordCount <= 1)
        {
            return FloatingTextType.FloatingTextType_Perfect;
        }
        else if(CurrentSwordCount <= 2)
        {
            return FloatingTextType.FloatingTextType_Good;
        }
        else
        {
            return FloatingTextType.FloatingTextType_Bad;
        }
    }

    public void GameOver()
    {

    }
}

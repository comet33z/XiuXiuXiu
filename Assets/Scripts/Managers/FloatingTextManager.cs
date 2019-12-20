using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FloatingTextType
{
    FloatingTextType_Perfect,
    FloatingTextType_Good,
    FloatingTextType_Bad,
}

public class FloatingTextManager : MonoSingleton<FloatingTextManager>
{
    public GameObject PrefabPerfect;
    public GameObject PrefabGood;
    public GameObject PrefabBad;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private GameObject GetPrefab(FloatingTextType floatType)
    {
        switch(floatType)
        {
            case FloatingTextType.FloatingTextType_Perfect:
                return PrefabPerfect;
            case FloatingTextType.FloatingTextType_Good:
                return PrefabGood;
            case FloatingTextType.FloatingTextType_Bad:
                return PrefabBad;

        }

        return null;
    }

    public void NewFloatingText(FloatingTextType floatType, Vector3 pos)
    {
        GameObject prefab = GetPrefab(floatType);
        if (prefab == null)
            return;
        GameObject go = GameObject.Instantiate(prefab);
    }
}

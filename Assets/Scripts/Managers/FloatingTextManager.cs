using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FloatingTextType
{
    FloatingTextType_Perfect,
    FloatingTextType_Good,
    FloatingTextType_Bad,
    FloatingTextType_Miss,
}

public class FloatingTextManager : MonoSingleton<FloatingTextManager>
{
    public GameObject PrefabPerfect;
    public GameObject PrefabGood;
    public GameObject PrefabBad;
    public GameObject PrefabMiss;
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
            case FloatingTextType.FloatingTextType_Miss:
                return PrefabMiss;

        }

        return null;
    }

    public void NewFloatingText(FloatingTextType floatType, Vector3 pos)
    {
        GameObject prefab = GetPrefab(floatType);
        if (prefab == null)
            return;
        GameObject go = GameObject.Instantiate(prefab);

        Vector3 ScreenPos = Camera.main.WorldToScreenPoint(pos);
        Vector2 uiPos = new Vector2();
        RectTransform rtf = go.transform as RectTransform;
        RectTransform parentRtf = UIRoot.Instance.transform as RectTransform;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRtf, ScreenPos,null, out uiPos);
       
        rtf.anchoredPosition = new Vector2(uiPos.x, uiPos.y + 65f);

      
        rtf.SetParent(UIRoot.Instance.transform, false);
    }
}

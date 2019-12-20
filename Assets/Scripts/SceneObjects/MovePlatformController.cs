using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveStatus
{
    MoveStatus_Normal,
    MoveStatus_Up,
    MoveStatus_Down
}
public class MovePlatformController : MonoBehaviour
{
    public MoveStatus CurrentMoveStatus;
    [Header("Speed")]
    public float SpeedY;

    [Header("MoveHeight")]
    public float MaxHeigth;
    public float MinHeight;

    private float NextHeight;
    private Vector3 CurrentDir = Vector3.up;

    // Start is called before the first frame update
    void Start()
    {
        CurrentDir = Vector3.down;
        GenerateNextHeight();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        UpdateHeight();
    }

    private void UpdateHeight()
    {
        Vector3 curScale = transform.localScale;
        curScale += Time.deltaTime * CurrentDir * SpeedY;
        if(CurrentDir == Vector3.down)
        {
            if (curScale.y <= NextHeight)
            {
                curScale = new Vector3(3f, NextHeight, 1f);
                GenerateNextHeight();
            }
        }
        else if(CurrentDir == Vector3.up)
        {
            if (curScale.y >= NextHeight)
            {
                curScale = new Vector3(3f, NextHeight, 1f);
                GenerateNextHeight();
            }
        }
        
        transform.localScale = curScale;
    }

    private void GenerateNextHeight()
    {
        float currentH = this.transform.localScale.y;
        if(CurrentDir == Vector3.down)
        {
            NextHeight = Random.Range(currentH, MaxHeigth);
            CurrentDir = Vector3.up;
        }
        else if(CurrentDir == Vector3.up)
        {
            NextHeight = Random.Range(MinHeight, currentH);
            CurrentDir = Vector3.down;
        }
    }
}

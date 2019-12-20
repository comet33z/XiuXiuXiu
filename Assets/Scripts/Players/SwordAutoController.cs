using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAutoController : MonoBehaviour
{
    [Header("Speed")]
    public float SwordLength = 1;
    public float UpAccSpeed;
    public float UpSpeed;
    public float DownAccSpeed;
    public float DownSpeed;

    public LineRenderer lineRender;
    public Transform SwordTopTrans;

    public float TotalPushTime = 1f;
    private float StartDownLength;
    private float StartUpLength;

    private float pushTime = 0f;
    private float downTime = 0f;

    private Vector3 LineStartPosition;
    private Vector3 LineEndPosition;

    public bool IsPushed = false;
    public SwordStatus CurrentStatus;
    // Start is called before the first frame update
    void Start()
    {
        StartUpLength = SwordLength;
        CurrentStatus = SwordStatus.SwordStatus_Up;
    }

    // Update is called once per frame
    void Update()
    {
        //if (IsPushed == false && Input.GetMouseButtonDown(0))
        //{
        //    IsPushed = true;
        //    pushTime = 0f;
        //    CurrentStatus = SwordStatus.SwordStatus_Up;
        //    StartUpLength = transform.localScale.y;
        //}

        //if (IsPushed)
        //{
        //    pushTime += Time.deltaTime;
        //}
        //else
        //{
        //    if (CurrentStatus == SwordStatus.SwordStatus_Down)
        //        downTime += Time.deltaTime;
        //}

        //if (IsPushed && Input.GetMouseButtonUp(0))
        //{
        //    IsPushed = false;
        //    StartDownLength = transform.localScale.y;

        //    downTime = 0;
        //    CurrentStatus = SwordStatus.SwordStatus_Down;
        //}

        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z);
        Vector3 pos = Camera.main.ScreenToWorldPoint(mousePos);
        DrawAimLine(pos);


        if (Input.GetMouseButtonDown(0))
        {

            Debug.Log("Input.mousePosition " + mousePos + " world click pos " + pos);
            float dis = Mathf.Abs(pos.y - transform.position.y) - 0.5f;
            pushTime = 0f;
            CurrentStatus = SwordStatus.SwordStatus_Up;
            StartUpLength = dis;//transform.localScale.y;

            Debug.Log("StartUpLength " + dis);
        }

        UpdateSword();
    }

    void UpdateSword()
    {
        pushTime += Time.deltaTime;
        if (CurrentStatus == SwordStatus.SwordStatus_Up)
        {
            float l = StartUpLength + pushTime * UpSpeed + pushTime * pushTime * UpAccSpeed / 2;
            UpdateSwordLength(l);
        }
        else if (CurrentStatus == SwordStatus.SwordStatus_Down)
        {
            float l = StartDownLength - downTime * DownSpeed - downTime * downTime * DownAccSpeed / 2;
            if (l <= SwordLength)
            {
                CurrentStatus = SwordStatus.SwordStatus_Normal;
                l = SwordLength;
            }
            UpdateSwordLength(l);
        }
        else
        {
            UpdateSwordLength(SwordLength);
        }
    }

    public void UpdateSwordLength(float l)
    {
        Vector3 swordScale = transform.localScale;
        swordScale.y = l;
        transform.localScale = swordScale;

        UpdateSwordTopPos();
    }

    void UpdateSwordTopPos()
    {
        if(SwordTopTrans != null)
        {
            SwordTopTrans.position = new Vector3(SwordTopTrans.position.x,
                this.transform.position.y + this.transform.localScale.y,
                SwordTopTrans.position.z);
        }
    }
    void DrawAimLine(Vector3 pos)
    {
        if(lineRender)
        {
            lineRender.positionCount = 2;
            LineStartPosition = new Vector3(-30, pos.y, pos.z);
            LineEndPosition = new Vector3(30, pos.y, pos.z);
            lineRender.SetPosition(0, LineStartPosition);
            lineRender.SetPosition(1, LineEndPosition);
        }
    }
}

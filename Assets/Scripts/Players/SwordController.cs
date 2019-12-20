using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SwordStatus
{
    SwordStatus_Normal,
    SwordStatus_Up,
    SwordStatus_Down,
    SwordStatus_GameOver,
}

public class SwordController : MonoBehaviour
{
    [Header("Speed")]
    public float SwordLength = 1;
    public float UpAccSpeed;
    public float UpSpeed;
    public float DownAccSpeed;
    public float DownSpeed;


    public float TotalPushTime = 1f;
    private float StartDownLength;
    private float StartUpLength;

    private float pushTime = 0f;
    private float downTime = 0f;

    public bool IsPushed = false;
    public SwordStatus CurrentStatus;
    // Start is called before the first frame update
    void Start()
    {
        CurrentStatus = SwordStatus.SwordStatus_Normal;
    }

    // Update is called once per frame
    void Update()
    {
        if(IsPushed == false && Input.GetMouseButtonDown(0))
        {
            IsPushed = true;
            pushTime = 0f;
            CurrentStatus = SwordStatus.SwordStatus_Up;
            StartUpLength = transform.localScale.y;
        }

        if(IsPushed)
        {
            pushTime += Time.deltaTime;
        }
        else
        {
            if (CurrentStatus == SwordStatus.SwordStatus_Down)
                downTime += Time.deltaTime;
        }

        if(IsPushed && Input.GetMouseButtonUp(0))
        {
            IsPushed = false;
            StartDownLength = transform.localScale.y;

            downTime = 0;
            CurrentStatus = SwordStatus.SwordStatus_Down;
        }

        UpdateSword();
    }

    void UpdateSword()
    {
        if(CurrentStatus == SwordStatus.SwordStatus_Up)
        {
            float l = StartUpLength + pushTime * UpSpeed + pushTime * pushTime * UpAccSpeed / 2;
            UpdateSwordLength(l);
        }
        else if(CurrentStatus == SwordStatus.SwordStatus_Down )
        {
            float l = StartDownLength - downTime * DownSpeed - downTime * downTime * DownAccSpeed / 2;
            if(l <= SwordLength)
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
    }

}

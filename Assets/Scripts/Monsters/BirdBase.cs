using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BirdStatus
{
    BirdStatus_Normal,
    BirdStatus_FlyAway,
    BirdStatus_ToHotPot
}

public class BirdBase : MonoBehaviour
{
    [Header("Speed")]
    public float SpeedX;
    public float SpeedY;
    public float FlyAwayAngleSpeed = 5;
    public Transform HotPotTrans;

    public bool IsDead = false;

    public BirdStatus CurrentStatus;
    private float flyAwayAngle;


    // Start is called before the first frame update
    void Start()
    {
        CurrentStatus = BirdStatus.BirdStatus_Normal;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    virtual public void Move()
    {
        if(CurrentStatus == BirdStatus.BirdStatus_Normal)
        {
            Vector3 move = Vector3.left * SpeedX * Time.deltaTime;
            this.transform.Translate(move);
        }

        if(CurrentStatus == BirdStatus.BirdStatus_FlyAway)
        {
            this.transform.Rotate(Vector3.forward, FlyAwayAngleSpeed);
   
            Vector3 move = Vector3.right * SpeedX * 3 * Time.deltaTime
                 + Vector3.down * SpeedY * Time.deltaTime;
            this.transform.position += move;
            //flyAwayAngle += FlyAwayAngleSpeed * Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Sword"))
        {
            if(this.CurrentStatus == BirdStatus.BirdStatus_Normal)
                Destroy(this.gameObject);
        }
        else if(collision.gameObject.layer == LayerMask.NameToLayer("SwordInvalid"))
        {
            Debug.Log("Invalid !!!!");
            FlyAway();
        }
    }

    private void OnTriggerEnter2D(Collision2D collision)
    {
        
    }

    virtual public void FlyAway()
    {
        SpeedY = Random.Range(10, 12);
        SpeedX = Random.Range(5, 8);
        CurrentStatus = BirdStatus.BirdStatus_FlyAway;
    }

    virtual public void FlyToHotPot()
    {

    }
}

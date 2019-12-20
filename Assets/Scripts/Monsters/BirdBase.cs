using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BirdStatus
{
    BirdStatus_Normal,
    BirdStatus_FlyAway,
    BirdStatus_ToHotPot,
    BirdStatus_Freeze,
}

public class BirdBase : MonoBehaviour
{
    [Header("Speed")]
    public float SpeedX;
    public float SpeedY;
    public float FlyAwayAngleSpeed = 5;
    public float FlyHotPotSpeedY = 2f;
    public float FlyHotPotTime = 0f;
    public float FlyHotPotAcc = 20f;
    public Transform HotPotTrans;


    public bool IsDead = false;

    public BirdStatus CurrentStatus;
    private float flyAwayAngle;
    private Animator animator;
    protected float CurrentFreezeTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        CurrentStatus = BirdStatus.BirdStatus_Normal;
        animator = this.GetComponent<Animator>();
       
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
        else if(CurrentStatus == BirdStatus.BirdStatus_Freeze)
        {
            CurrentFreezeTime += Time.deltaTime;
            SwordAutoController sac = GameObject.FindObjectOfType<SwordAutoController>();
            if(CurrentFreezeTime >= sac.TotalFreezeTime)
            {
                CurrentStatus = BirdStatus.BirdStatus_Normal;
                CurrentFreezeTime = 0;
            }
        }
        else if(CurrentStatus == BirdStatus.BirdStatus_ToHotPot)
        {

            float GritySpeed = -FlyHotPotAcc * (FlyHotPotTime);

            //SpeedY -= 10  * Time.deltaTime;
            Vector3 move = (SpeedY + GritySpeed) * Time.deltaTime * Vector3.up;
            this.transform.position += move;

            FlyHotPotTime += Time.deltaTime;
        }
        else if(CurrentStatus == BirdStatus.BirdStatus_FlyAway)
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
            if (this.CurrentStatus == BirdStatus.BirdStatus_Normal)
            {
                FlyToHotPot();
            }
        }
        else if(collision.gameObject.layer == LayerMask.NameToLayer("SwordInvalid"))
        {
            Debug.Log("Invalid !!!!");
            if(this.CurrentStatus == BirdStatus.BirdStatus_Normal)
            {
                FlyAway();
            }
            
        }
    }

    virtual public void Die()
    {
        if (animator)
            animator.SetBool("IsDead", true);
    }

    virtual public void FlyAway()
    {
        SpeedY = Random.Range(10, 12);
        SpeedX = Random.Range(5, 8);
        CurrentStatus = BirdStatus.BirdStatus_FlyAway;
    }

    virtual public void FlyToHotPot()
    {
        SpeedY = FlyHotPotSpeedY;
        FlyHotPotTime = 0;
  
        CurrentStatus = BirdStatus.BirdStatus_ToHotPot;

        SwordAutoController sac = GameObject.FindObjectOfType<SwordAutoController>();
        if(sac)
        {
            sac.Freeze();
            BirdBase[] birds = GameObject.FindObjectsOfType<BirdBase>();
            foreach(var bird in birds)
            {
                if(bird && bird != this)
                {
                    bird.Freeze();
                }
            }
        }
        Die();
    }

    virtual public void Freeze()
    {
        CurrentStatus = BirdStatus.BirdStatus_Freeze;
        CurrentFreezeTime = 0;
    }
}

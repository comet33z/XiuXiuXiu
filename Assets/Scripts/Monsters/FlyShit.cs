using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyShit : BirdBase
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentStatus == BirdStatus.BirdStatus_Normal)
        {
            Vector3 move = Vector3.left * SpeedX * Time.deltaTime;
            this.transform.Translate(move);
        }
        else if (CurrentStatus == BirdStatus.BirdStatus_Freeze)
        {
            CurrentFreezeTime += Time.deltaTime;
            SwordAutoController sac = GameObject.FindObjectOfType<SwordAutoController>();
            if (CurrentFreezeTime >= sac.TotalFreezeTime)
            {
                CurrentStatus = BirdStatus.BirdStatus_Normal;
                CurrentFreezeTime = 0;
            }
        }
        else if (CurrentStatus == BirdStatus.BirdStatus_ToHotPot)
        {

            float GritySpeed = -10f * (FlyHotPotTime);

            //SpeedY -= 10  * Time.deltaTime;
            Vector3 move = (SpeedY + GritySpeed) * Time.deltaTime * Vector3.up;
            this.transform.position += move;

            FlyHotPotTime += Time.deltaTime;
        }
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
        else if (collision.gameObject.layer == LayerMask.NameToLayer("SwordInvalid"))
        {
            Debug.Log("Invalid !!!!");
            if (this.CurrentStatus == BirdStatus.BirdStatus_Normal)
            {
                FlyToHotPot();
            }

        }
    }
}

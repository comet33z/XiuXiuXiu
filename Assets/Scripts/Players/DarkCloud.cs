using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkCloud : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Sword"))
        {
            Debug.Log("GameOver");
            SwordAutoController sac = GameObject.FindObjectOfType<SwordAutoController>();
            if(sac)
            {
                sac.CurrentStatus = SwordStatus.SwordStatus_GameOver;
            }
        }
    }
}

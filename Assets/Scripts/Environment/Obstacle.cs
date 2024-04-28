using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] Player player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "ForegroundObject")
        {
            Destroy(this.gameObject);
        }
        else if(collision.tag == "Player")
        {
            player.Health = 0;
        }
    }
}

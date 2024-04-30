using AIBERG.Core;
using UnityEngine;

namespace AIBERG.ParkourMode{
public class Obstacle : MonoBehaviour
{
    [SerializeField] Player player;
    // Start is called before the first frame update
    void Start()
    {
         player = GameObject.Find("Player_Game").GetComponent<Player>();
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

}

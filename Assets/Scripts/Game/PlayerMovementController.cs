using UnityEngine;
using System.Collections;

public class PlayerMovementController : MonoBehaviour
{
    public Player owner;
    public float speed;

    public void  StartMove()
    {

    }
    
    public void EndMove()
    {

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        EventCharacter ch = collision.gameObject.GetComponent<EventCharacter>();

        if (ch != null)
        {
            
        }
    }
}

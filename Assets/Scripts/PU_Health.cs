using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEditor.UI;
using UnityEngine;


[RequireComponent(typeof(Collider2D))]
public class PU_Health : MonoBehaviour
{
    int healing = 1;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            if (collision.GetComponent<Damageable>().Heal(healing) == true)
            {
                //Put some UI feedback for gaining health trigger here
                Debug.Log("Player healed for" + healing.ToString());
                Destroy(gameObject);
            }
            else
            {
                //Maybe indicate that the player cannot collect this with visual feedback
                Debug.Log("Player is at max health");
            }
        }
        else
        {
            Debug.Log("Object colliding with pickup does not have the tag 'Player'");
        }
    }
}

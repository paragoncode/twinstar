using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Attackarea : MonoBehaviour
{
    
    [SerializeField] private BoxCollider2D bc;

    [Header("Stats")]
    [SerializeField] private int attackForce = 20;
    [SerializeField] private Vector2 hitboxSizeHorizontal = new Vector2(2, 1.5f);
    [SerializeField] private Vector2 hitboxSizeVertical = new Vector2(2, 2);
    private Vector2 hitboxSize;
    private Vector2 hitboxOffset;

    void Update()
    {
        Vector2 facingDirection = new Vector2(transform.parent.GetComponent<movement>().facingDirection.x, transform.parent.GetComponent<movement>().facingDirection.y);
        if (facingDirection == new Vector2(0,-1) || facingDirection == new Vector2(0,1))
        {
            hitboxSize = hitboxSizeVertical;
            hitboxOffset = new Vector2(0, hitboxSize.y / 2 * 1.5f * transform.parent.GetComponent<movement>().facingDirection.y );
        }else /*(facingDirection == new Vector2(-1,0) || facingDirection == new Vector2(1,0))
        */{
            hitboxSize = hitboxSizeHorizontal;
            hitboxOffset = new Vector2(hitboxSize.x / 2 * 1.5f * transform.parent.GetComponent<movement>().facingDirection.x, (hitboxSize.y - 1) / 2);
        } 

        bc.size = hitboxSize;
        bc.offset = hitboxOffset;
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        
        //checks if player is facing either left or right, or up or down and assigns propper values to hitbox size and offset
        

        if (collider.GetComponent<Hitable>() != null)
        {
            Debug.Log("hit"); 
            Rigidbody2D colliderRb = collider.GetComponent<Rigidbody2D>();

            colliderRb.AddForce(transform.parent.GetComponent<movement>().facingDirection * attackForce, ForceMode2D.Impulse);
        }
    }
}

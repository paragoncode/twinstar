using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Attackarea : MonoBehaviour
{
    
    [SerializeField] private BoxCollider2D bc;

    [Header("Stats")]
    private int attackForce = 8;
    private int knockbackForce = -4;
    [SerializeField] private Vector2 hitboxSizeHorizontal = new Vector2(2, 1.5f);
    [SerializeField] private Vector2 hitboxSizeVertical = new Vector2(2, 2);
    private Vector2 hitboxSize;
    private Vector2 hitboxOffset;

    private int conditionalKnockbackForce;
    private int conditionalAttackForce;

    void Update()
    {
        Vector2 facingDirection = new Vector2(transform.parent.GetComponent<movement>().facingDirection.x, transform.parent.GetComponent<movement>().facingDirection.y);
        if (facingDirection == new Vector2(0,-1) || facingDirection == new Vector2(0,1)) // is player facing up or down?
        {
            hitboxSize = hitboxSizeVertical;
            hitboxOffset = new Vector2(0, hitboxSize.y / 2 * 1.5f * facingDirection.y );
        }else // is player facing left or right?
        {
            hitboxSize = hitboxSizeHorizontal;
            hitboxOffset = new Vector2(hitboxSize.x / 2 * 1.5f * facingDirection.x, (hitboxSize.y / 2) / 2);
        } 

        bc.size = hitboxSize;
        bc.offset = hitboxOffset;
    }

    private void OnTriggerEnter2D(Collider2D collider) {

        if (collider.GetComponent<Hitable>() != null)
        {
            Rigidbody2D colliderRb = collider.GetComponent<Rigidbody2D>();
            if (transform.parent.GetComponent<Collision>().onGround)
            {
                conditionalKnockbackForce = knockbackForce / 2;
                conditionalAttackForce = attackForce;
            } else
            {
                conditionalAttackForce = attackForce / 2;
                conditionalKnockbackForce = knockbackForce;
            }

            Vector2 totalAttackForce = transform.parent.GetComponent<movement>().facingDirection * conditionalAttackForce;
            Vector2 totalKnockbackForce = transform.parent.GetComponent<movement>().facingDirection * conditionalKnockbackForce;
            
            Debug.Log("force added: " + totalAttackForce);

            colliderRb.AddForce(totalAttackForce, ForceMode2D.Impulse);
            transform.parent.GetComponent<Rigidbody2D>().AddForce(totalKnockbackForce, ForceMode2D.Impulse);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{

    [Header("Layers")]
    public LayerMask groundLayer;
    public LayerMask player1Layer;
    public LayerMask player2Layer;
    

    [Space]
    public bool onGround;
    public bool onPlayer1;
    public bool onPlayer2;
    [Space]

    [Header("Collision")]

    [SerializeField] private Collider2D self;
    public float collisionRadius = 0.25f;
    public Vector2 bottomOffset, rightOffset, leftOffset;
    private Color debugCollisionColor = Color.red;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {  

        onGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, groundLayer);

        onPlayer1 = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius,player1Layer);
        onPlayer2 = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius,player2Layer);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        var positions = new Vector2[] { bottomOffset, rightOffset, leftOffset };

        Gizmos.DrawWireSphere((Vector2)transform.position  + bottomOffset, collisionRadius);
    }
}
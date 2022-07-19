using UnityEngine;

public class Player : MonoBehaviour
{
     [Header("Player Variables")]
     [Tooltip("How fast the player can move")]
     [SerializeField] private float speed = 6f;
     
     [Tooltip("How much force is applied to the player when they jump")]
     [SerializeField] private float jumpForce = 10f;
     
     [Tooltip("Set true if the player not in the air")]
     private bool grounded;

     [Tooltip("Checks if the player is on the ground")]
     public Transform groundCheck;
     
     private Rigidbody2D rb;
     private Animator anim;
     private SpriteRenderer mySpriteRenderer;
     
     private void Start()
     {
          rb = GetComponent<Rigidbody2D>();
          anim = GetComponent<Animator>();
          mySpriteRenderer = GetComponent<SpriteRenderer>();
     }

     private void FixedUpdate()
     {
          Move();
     }

     private void Update()
     {
          Jump();
     }
     
     void Move()
     {
          float translate = Input.GetAxisRaw("Horizontal") * speed * Time.fixedDeltaTime;

          transform.Translate(translate, 0, 0);

          anim.SetBool("Grounded", grounded);
          float move = Input.GetAxis("Horizontal");

          anim.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal")));
               
          if (move < 0)
          {
               mySpriteRenderer.flipX = true;
          }
          else if (move > 0)
          {
               mySpriteRenderer.flipX = false;
          }
     }
     
     void Jump()
     {
          grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

          if (Input.GetButtonDown("Jump") && grounded)
          {
               rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
          }
     }
}

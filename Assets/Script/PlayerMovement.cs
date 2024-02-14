using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update

    public float moveSpeed ;
    public float  jumpForce ;

    public Rigidbody2D rb ;
    private Vector3 velocity = Vector3.zero ; 

   

    public Transform groudCheckLeft;
    public Transform groudCheckRight;
    public Animator animator ;
    public SpriteRenderer spriteRenderer;
   

    

    private bool isJumping ;
    private bool isGrounded ;
   

    
    
    void Start()
    {
        
    }

    // FixedUpdate stabilise les simulations physiques dans Unity en évitant les variations de framerate.
    void FixedUpdate()
    {
        //Cette ligne utilise Physics2D.OverlapArea pour vérifier si une zone entre groudCheckLeft et groudCheckRight touche le sol, et le résultat est stocké dans isGrounded pour indiquer si le joueur est au sol.
        isGrounded = Physics2D.OverlapArea(groudCheckLeft.position, groudCheckRight.position);

        //Cette ligne récupère l'entrée horizontale du joueur à l'aide de Input.GetAxis("Horizontal"), puis multiplie cette valeur par la vitesse de déplacement (moveSpeed) et le temps écoulé depuis la dernière frame (Time.deltaTime). Cela permet d'obtenir un mouvement horizontal lissé indépendamment du framerate.
        float horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime ;

        MovePlayer (horizontalMovement);


        Flip (rb.velocity.x);

        //rb.velocity.x récupère la composante de la vitesse horizontale du Rigidbody (rb). 
        //Mathf.Abs signifie "valeur absolue". Elle prend la valeur de la vitesse horizontale, quel que soit son signe (positif ou négatif), et la rend positive.
        float characterVelocity = Mathf.Abs(rb.velocity.x);
        animator.SetFloat("Speed", characterVelocity);
    

    }

    void Update ()
    {
         if (Input.GetButtonDown("Jump")&& isGrounded)
        {
            isJumping = true;
        }

    }


    void MovePlayer(float horizontalMovement)
    {
        Vector3 targetVelocity = new Vector2 (horizontalMovement, rb.velocity.y) ;
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, 0.05f) ; 

        if(isJumping == true)
        {
            rb.AddForce(new Vector2(0f, jumpForce)); 
            isJumping = false ; 
        }

    }

    void Flip(float _velocity)
    {
        if (_velocity > -0.01f)
        {
            spriteRenderer.flipX = false ;
            
        }
        else if(_velocity < 0.01f)
            {
                spriteRenderer.flipX = true ;
            }

    }


}

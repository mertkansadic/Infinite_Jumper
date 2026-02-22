using UnityEngine;

public class NewEmptyCSharpScript : MonoBehaviour
{
    public float Speed = 1.0f;
    private Rigidbody2D rigidbody2D;
    private Animator animator;
    private Vector3 charPos;
    [SerializeField] private GameObject camera;
    private SpriteRenderer renderer;
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        charPos = transform.position;
        renderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        
        
        //rigidbody2D.linearVelocity = new Vector2(Speed,0f);
        charPos = new Vector3(charPos.x + (Input.GetAxis("Horizontal")*Speed*Time.deltaTime), charPos.y);
        transform.position = charPos;
        if(Input.GetAxis("Horizontal")==0.0f)
        {
            animator.SetFloat("Speed",0.0f);
        }
        else{
        animator.SetFloat("Speed",Speed);
        }
        if(Input.GetAxis("Horizontal")>0.01f)
        {
            renderer.flipX = false;
        }
        else if(Input.GetAxis("Horizontal")<-0.01f)
        {
            renderer.flipX = true;
        }
        
    }

    private void LateUpdate()
    {
        camera.transform.position = new Vector3(charPos.x , charPos.y, charPos.z - 1.0f);
    }

}

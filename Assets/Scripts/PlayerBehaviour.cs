using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [Header("Variables")]
    public bool canMove = true;
    public bool isInteracting = false;
    [SerializeField] private Vector2 inputVector;
    [SerializeField] private float playerSpeed;

    [Header("Components")]
    public Animator _hairAnimator;
    public Animator _clothesAnimator;
    [SerializeField] private Animator _undiesAnimator;
    
    Rigidbody2D _rb;
    Animator _animator;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update() 
    {
        GetInput();
        SetAnimation();
    }

    private void FixedUpdate() 
    {
        if(!canMove) return;
        HandleMovement();    
    }

    private void GetInput()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");


        if(canMove)
            inputVector = new Vector2(horizontal, vertical);
        else
            inputVector = new Vector2(0, 0);

    }

    private void HandleMovement()
    {
        _rb.velocity = inputVector.normalized * playerSpeed;
    }

    private void SetAnimation()
    {
        _animator.SetFloat("horizontal", inputVector.x);
        _animator.SetFloat("vertical", inputVector.y);

        _hairAnimator.SetFloat("horizontal", inputVector.x);
        _hairAnimator.SetFloat("vertical", inputVector.y);
        
        _clothesAnimator.SetFloat("horizontal", inputVector.x);
        _clothesAnimator.SetFloat("vertical", inputVector.y);
        
        _undiesAnimator.SetFloat("horizontal", inputVector.x);
        _undiesAnimator.SetFloat("vertical", inputVector.y);
    }

    private void OnTriggerEnter2D(Collider2D trig) 
    {
        if(trig.gameObject.tag.Equals("Interactable"))
        {
            trig.GetComponent<PropInteract>().canInteract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D trig) 
    {
        if(trig.gameObject.tag.Equals("Interactable"))
        {
            trig.GetComponent<PropInteract>().canInteract = false;
        }  
    }
}

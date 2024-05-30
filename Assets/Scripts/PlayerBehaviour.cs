using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public bool canMove = true;
    public bool isInteracting = false;

    [SerializeField] private Vector2 inputVector;
    [SerializeField] private float playerSpeed;
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
        HandleMovement();    
    }

    private void GetInput()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        inputVector = new Vector2(horizontal, vertical);
    }

    private void HandleMovement()
    {
        if(!canMove) return;
        _rb.velocity = inputVector.normalized * playerSpeed;
    }

    private void SetAnimation()
    {
        _animator.SetFloat("horizontal", inputVector.x);
        _animator.SetFloat("vertical", inputVector.y);
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

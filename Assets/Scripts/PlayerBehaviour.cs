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

    private const string _horizontal = "horizontal";
    private const string _vertical = "vertical";
    private const string _lastHorizontal = "lastHorizontal";
    private const string _lastVertical = "lastVertical";

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
            inputVector.Set(horizontal, vertical);
        else
            inputVector.Set(0, 0);

    }

    private void HandleMovement()
    {
        _rb.velocity = inputVector.normalized * playerSpeed;
    }

    private void SetAnimation()
    {
        _animator.SetFloat(_horizontal, inputVector.x);
        _animator.SetFloat(_vertical, inputVector.y);

        _hairAnimator.SetFloat(_horizontal, inputVector.x);
        _hairAnimator.SetFloat(_vertical, inputVector.y);
        
        _clothesAnimator.SetFloat(_horizontal, inputVector.x);
        _clothesAnimator.SetFloat(_vertical, inputVector.y);
        
        _undiesAnimator.SetFloat(_horizontal, inputVector.x);
        _undiesAnimator.SetFloat(_vertical, inputVector.y);

        if(inputVector != Vector2.zero)
        {
            _animator.SetFloat(_lastHorizontal, inputVector.x);
            _animator.SetFloat(_lastVertical, inputVector.y);

            _hairAnimator.SetFloat(_lastHorizontal, inputVector.x);
            _hairAnimator.SetFloat(_lastVertical, inputVector.y);

            _clothesAnimator.SetFloat(_lastHorizontal, inputVector.x);
            _clothesAnimator.SetFloat(_lastVertical, inputVector.y);

            _undiesAnimator.SetFloat(_lastHorizontal, inputVector.x);
            _undiesAnimator.SetFloat(_lastVertical, inputVector.y);
        }
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

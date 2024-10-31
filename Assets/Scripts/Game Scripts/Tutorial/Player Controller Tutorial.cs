using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerTutorial : MonoBehaviour
{
    Animator myAnimator;
    ItemFactory itemFactory;
    TutorialController tutorialController;

    Vector2 rawInput;
    [SerializeField] float moveSpeed = 5f;

    Interactable currentInteractable;
    bool isInteracting;

    Item heldItem;
    [SerializeField] SpriteRenderer heldItemSpriteRenderer;
    [SerializeField] bool hasStartingItem;
    
    float idleTimeout;

    void Awake()
    {
        myAnimator = GetComponent<Animator>();
        itemFactory = FindObjectOfType<ItemFactory>();
        tutorialController = FindObjectOfType<TutorialController>();
    }
    
    void Start()
    {
        if(hasStartingItem)
            heldItem = itemFactory.MakeGem(4);
        else
            heldItem = itemFactory.MakeNullItem();

        heldItemSpriteRenderer.sprite = heldItem.GetSprite();
    }

    void FixedUpdate()
    {
        Move();
        UpdateAnimator();
    }

    void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
    }

    void Move()
    {
        Vector2 delta = rawInput * moveSpeed * Time.deltaTime;
        Vector2 newPos = new Vector2(transform.position.x + delta.x, transform.position.y + delta.y);
        transform.position = newPos;
    }

    void UpdateAnimator()
    {
        myAnimator.SetBool("isRight", rawInput.x > 0);
        myAnimator.SetBool("isLeft", rawInput.x < 0);
        myAnimator.SetBool("isUp", rawInput.y > 0);
        myAnimator.SetBool("isDown", rawInput.y < 0);

        if(Mathf.Abs(rawInput.x) > 0.1f || Mathf.Abs(rawInput.y) > 0.1f)
            idleTimeout = 0;
        else
            idleTimeout += Time.deltaTime;

        myAnimator.SetFloat("idleTimeout", idleTimeout);
    }

    void OnInteract()
    {
        if(!isInteracting) return;
        
        heldItem = currentInteractable.Interact(heldItem);
        heldItemSpriteRenderer.sprite = heldItem.GetSprite();
        tutorialController.CheckTutorialObjective(heldItem);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Interactable"))
        {
            currentInteractable = other.gameObject.GetComponent<Interactable>();
            isInteracting = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Interactable"))
            isInteracting = false;
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Interactable"))
        {
            currentInteractable = other.gameObject.GetComponent<Interactable>();
            isInteracting = true;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Interactable"))
            isInteracting = false;
    }
}
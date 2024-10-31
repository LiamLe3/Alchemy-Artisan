using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllers : MonoBehaviour
{
    Animator myAnimator;
    OrderManagers orderManager;
    GameManagers gameManager;

    Vector2 rawInput;
    [SerializeField] float moveSpeed = 5f;
    float idleTimeout;

    Interactables currentInteractable;
    Itemss heldItem = GemItem.NO_GEM;

    void Awake()
    {
        myAnimator = GetComponent<Animator>();
        orderManager = FindObjectOfType<OrderManagers>();
        gameManager = FindObjectOfType<GameManagers>();
    }

    void FixedUpdate()
    {
        if(gameManager.IsGameEnded()) return;
        Move();
        UpdateAnimator();
    }

    void Move()
    {
        Vector2 delta = rawInput * moveSpeed * Time.deltaTime;
        Vector2 newPos = new Vector2(transform.position.x + delta.x, transform.position.y + delta.y);
        transform.position = newPos;
    }

    void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
    }

    void UpdateAnimator()
    {
        myAnimator.SetBool("isRight", rawInput.x > 0);
        myAnimator.SetBool("isLeft", rawInput.x < 0);
        myAnimator.SetBool("isUp", rawInput.y > 0);
        myAnimator.SetBool("isDown", rawInput.y < 0);

        if(Mathf.Abs(rawInput.x) > 0.1f || Mathf.Abs(rawInput.y) > 0.1f)
        {
            idleTimeout = 0;
        }
        else
        {
            idleTimeout += Time.deltaTime;
        }
        
        myAnimator.SetFloat("idleTimeout", idleTimeout);
        
    }

    void OnInteract()
    {
        if(currentInteractable == null) return;

        heldItem = currentInteractable.Interact(heldItem);

        orderManager.UpdateHeldItemUI(heldItem);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Interactables"))
        {
            currentInteractable = other.gameObject.GetComponent<Interactables>();
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Interactables"))
        {
            currentInteractable = other.gameObject.GetComponent<Interactables>();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Interactables"))
        {
            currentInteractable = null;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Interactables"))
        {
            currentInteractable = other.gameObject.GetComponent<Interactables>();
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Interactables"))
        {
            currentInteractable = other.gameObject.GetComponent<Interactables>();
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Interactables"))
        {
            currentInteractable = null;
        }
    }
}

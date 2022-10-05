using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Rigidbody2D rigidbody2D;
    public float moveSpeed;
    Vector2 movement;
    private NPC_Controller npc;

    public Animator animator;
    public VectorValue startPos;

    void Start() {
        transform.position = startPos.initialValue;
    }

    void Update() {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        if (Input.GetAxisRaw("Horizontal")==1 || Input.GetAxisRaw("Horizontal")== -1 || Input.GetAxisRaw("Vertical")== 1 || Input.GetAxisRaw("Vertical")== -1) {
            animator.SetFloat("LastHorizontal", Input.GetAxisRaw("Horizontal"));
            animator.SetFloat("LastVertical", Input.GetAxisRaw("Vertical"));
        }

        if (movement.x != 0 ) movement.y = 0;
    }


    private void FixedUpdate() {
        rigidbody2D.MovePosition(rigidbody2D.position + movement * moveSpeed * Time.deltaTime);
    }

    private bool inDialogue()
    {
        if (npc != null)
            return npc.DialogueActive();
        else
            return false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "NPC")
        {
            npc = collision.gameObject.GetComponent<NPC_Controller>();

            if (Input.GetKey(KeyCode.Space))
                npc.ActivateDialogue();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        npc = null;
    }
}

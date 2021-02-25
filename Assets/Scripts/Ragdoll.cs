using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Ragdoll : MonoBehaviour
{
    private Animator animator = null;
    private CharacterController characterController = null;
    private Player player = null;
    public List<Rigidbody> rigidbodies = new List<Rigidbody>();

    public bool RagdollOn
    {
        get { return !animator.enabled; }
        set
        {
            animator.enabled = !value;
            characterController.enabled = !value;
            player.enabled = !value;
            foreach(Rigidbody r in rigidbodies)
            {
                r.isKinematic = !value;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        player = GetComponent<Player>();
        foreach (Rigidbody r in rigidbodies)
        {
            r.isKinematic = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

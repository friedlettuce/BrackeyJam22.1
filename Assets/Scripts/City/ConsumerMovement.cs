using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumerMovement : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private Animator anim;
    private void Awake(){
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }
}

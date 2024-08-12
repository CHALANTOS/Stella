using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stella : MonoBehaviour
{
    [Header("Imports")]
    public Rigidbody2D rig;


    [Header("Attributes Move")]
    public float tempoSemAndarDash;
    public float speed, speedMultiplier = 1f, jumpForce, dashForce;
    private float ultimoInput = 1;
    public float cooldownJump;
    [Range(0,20)]public int quantidadePulos;
    private int podePular;
    public bool podeAndar = true, podeDash = true;

    float MoveX() => Input.GetAxisRaw("Horizontal");
    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        Move();
        if (MoveX() != 0)
        {
            ultimoInput = MoveX();
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Pular();
        }

        if (Input.GetKeyDown(KeyCode.Q) && podeDash)
        {
            Dash();
            podeAndar = false;
        }
    }

#region MovePlayer
    public void Move()
    {
        if (podeAndar)
        {
            rig.velocity = new Vector2(MoveX() * speed * speedMultiplier, rig.velocity.y);
        }
    }

    public void Pular()
    {
        if (podePular > 0)
        {
            rig.velocity = new Vector2(rig.velocity.x, jumpForce);
            podePular--;
        }
    }

    public void Dash()
    {
        rig.velocity = new Vector2(ultimoInput * dashForce, rig.velocity.y);
        StartCoroutine(DashCooldown());
        podeDash = false;
    }

    IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(tempoSemAndarDash);
        podeAndar = true;
    }

#endregion
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.layer == 3)
        {
            podePular = quantidadePulos;
            podeDash = true;
        }
    }
    
}
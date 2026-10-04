using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("���ʳ]�w")]
    [SerializeField] private float moveSpeed = 6f;

    [Header("���D�]�w")]
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private float coyoteTime = 0.15f;
    [SerializeField] private float jumpBufferTime = 0.15f;
    [SerializeField] private float variableJumpCutMultiplier = 0.5f;

    [Header("�a���˴�")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.18f;
    [SerializeField] private LayerMask groundLayer;

    [Header("�¦V")]
    [SerializeField] private GameObject modelObject;
    [SerializeField] private float leftDegreeY = -90;
    [SerializeField] private float rightDegreeY = 90;

    [Header("�ʵe")]
    [SerializeField] private Animator animator;
    [SerializeField] private string AniPar_MoveSpeed = "MoveSpeed";
    [SerializeField] private string AniPar_Jump= "Jump";
    [SerializeField] private string AniPar_IsGrounded = "IsGrounded";


    private Rigidbody2D rb;
    private float horizontalInput;

    private bool isGrounded;
    private bool wasGrounded;
    private float coyoteTimer;
    private float jumpBufferTimer;

    private bool jumpPressed;
    private bool jumpReleased;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Ū����J
        horizontalInput = Input.GetAxisRaw("Horizontal");

        // ���D����U/��}
        if (Input.GetButtonDown("Jump"))
        {
            jumpPressed = true;
            jumpBufferTimer = jumpBufferTime;
        }
        if (Input.GetButtonUp("Jump"))
        {
            jumpReleased = true;
        }

        // �w�ĭp�ɻ���
        if (jumpBufferTimer > 0f)
            jumpBufferTimer -= Time.deltaTime;

        // ��s�a�����A
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Coyote Time
        if (isGrounded)
            coyoteTimer = coyoteTime;
        else
            coyoteTimer -= Time.deltaTime;
        
        HandleFacing();
    }

    private void FixedUpdate()
    {
        HandleMovement();
        HandleJump();
        ApplyVariableJump();

        UpdateAnimation(); // �b���z��s��]�w�ʵe�Ѽ�

        // ���m�@���� flag
        jumpPressed = false;
        jumpReleased = false;
    }

    private void HandleMovement()
    {
        float targetVelX = horizontalInput * moveSpeed;
        rb.linearVelocity = new Vector2(targetVelX, rb.linearVelocity.y);
    }

    private void HandleJump()
    {
        // �i������Gcoyote ������ + �����w��
        if (jumpBufferTimer > 0f && coyoteTimer > 0f)
        {
            Jump();
            jumpBufferTimer = 0f;
            coyoteTimer = 0f;
        }
    }

    private void Jump()
    {
        // �����мg Y �t�סA�T�O���D�@�P��
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        if (animator != null)
        {
            animator.SetTrigger(AniPar_Jump);
        }
    }

    private void ApplyVariableJump()
    {
        // ��}���D��B���V�W�ɡA�d��W�ɳt�׹F��u�u���v
        if (jumpReleased && rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * variableJumpCutMultiplier);
        }
    }

    private void HandleFacing()
    {
        if (horizontalInput == 0f) return;

        if (horizontalInput > 0f)
        {
            modelObject.transform.localRotation = Quaternion.Euler(0f, rightDegreeY, 0f);
        }
        else
        {
            modelObject.transform.localRotation = Quaternion.Euler(0f, leftDegreeY, 0f);
        }
    }

    private void UpdateAnimation()
    {
        if ( animator == null) return;

        // ��¦�s��Ѽ�
        animator.SetFloat(AniPar_MoveSpeed, Mathf.Abs(rb.linearVelocity.x));
        animator.SetBool(AniPar_IsGrounded, isGrounded);


        wasGrounded = isGrounded;
    }

    // ��K�b�s�边����ܦa���˴��d��
    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}

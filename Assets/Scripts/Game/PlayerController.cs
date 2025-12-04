using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("移動設定")]
    [SerializeField] private float moveSpeed = 6f;

    [Header("跳躍設定")]
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private float coyoteTime = 0.15f;
    [SerializeField] private float jumpBufferTime = 0.15f;
    [SerializeField] private float variableJumpCutMultiplier = 0.5f;

    [Header("地面檢測")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.18f;
    [SerializeField] private LayerMask groundLayer;

    [Header("朝向")]
    [SerializeField] private GameObject modelObject;
    [SerializeField] private float leftDegreeY = -90;
    [SerializeField] private float rightDegreeY = 90;

    [Header("動畫")]
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
        // 讀取輸入
        horizontalInput = Input.GetAxisRaw("Horizontal");

        // 跳躍鍵按下/放開
        if (Input.GetButtonDown("Jump"))
        {
            jumpPressed = true;
            jumpBufferTimer = jumpBufferTime;
        }
        if (Input.GetButtonUp("Jump"))
        {
            jumpReleased = true;
        }

        // 緩衝計時遞減
        if (jumpBufferTimer > 0f)
            jumpBufferTimer -= Time.deltaTime;

        // 更新地面狀態
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

        UpdateAnimation(); // 在物理更新後設定動畫參數

        // 重置一次性 flag
        jumpPressed = false;
        jumpReleased = false;
    }

    private void HandleMovement()
    {
        float targetVelX = horizontalInput * moveSpeed;
        rb.velocity = new Vector2(targetVelX, rb.velocity.y);
    }

    private void HandleJump()
    {
        // 可跳條件：coyote 仍有效 + 有跳緩衝
        if (jumpBufferTimer > 0f && coyoteTimer > 0f)
        {
            Jump();
            jumpBufferTimer = 0f;
            coyoteTimer = 0f;
        }
    }

    private void Jump()
    {
        // 直接覆寫 Y 速度，確保跳躍一致性
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        if (animator != null)
        {
            animator.SetTrigger(AniPar_Jump);
        }
    }

    private void ApplyVariableJump()
    {
        // 放開跳躍鍵且仍向上時，削減上升速度達到「短跳」
        if (jumpReleased && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * variableJumpCutMultiplier);
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

        // 基礎連續參數
        animator.SetFloat(AniPar_MoveSpeed, Mathf.Abs(rb.velocity.x));
        animator.SetBool(AniPar_IsGrounded, isGrounded);


        wasGrounded = isGrounded;
    }

    // 方便在編輯器中顯示地面檢測範圍
    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}

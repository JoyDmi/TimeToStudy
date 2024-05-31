using UnityEngine;
using System.Collections;

public class PlayerMovementMus : MonoBehaviour
{
    public float moveSpeed = 10f; // ��������
    public float runSpeed = 15f; // �������� ����
    public float jumpForce = 10f; // ���� ������
    public float rotationSpeed = 10f; // �������� �������� �������
    public float gravity = -9.81f; // ����������
    private float jumpCooldown = 1.5f; // ����� �������� ������

    private float currentSpeed;
    private float verticalVelocity; // ������������ ��������
    private bool canJump = true; // ����, �����������, ����� �� �������� �������

    [SerializeField] private Camera PlayerCamera;
    private Animator animator;
    private CharacterController controller;

    private void Start()
    {
        PlayerCamera = Camera.main;
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        HandleMovementInput();
        if (canJump)
        {
            JumpLogic();
        }
    }

    private void HandleMovementInput()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        currentSpeed = isRunning ? runSpeed : moveSpeed;

        Vector3 forwardDirection = PlayerCamera.transform.forward;
        forwardDirection.y = 0;

        Vector3 rightDirection = PlayerCamera.transform.right;
        rightDirection.y = 0;

        Vector3 moveDirection = forwardDirection.normalized * verticalInput + rightDirection.normalized * horizontalInput;
        moveDirection = moveDirection.normalized;

        if (moveDirection.magnitude > 0.1f)
        {
            Vector3 velocity = moveDirection * currentSpeed;
            velocity.y = verticalVelocity; // ��������� ������������ �������� (���� ������ ��� ����������)

            controller.Move(velocity * Time.deltaTime);

            Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            animator.SetFloat("Speed", moveDirection.magnitude * currentSpeed);
        }
        else
        {
            Vector3 velocity = new Vector3(0, verticalVelocity, 0); // ��������� ������������ �������� (���� ������ ��� ����������)
            controller.Move(velocity * Time.deltaTime);
            animator.SetFloat("Speed", 0);
        }

        // ��������� ����������
        if (controller.isGrounded)
        {
            if (verticalVelocity < 0)
            {
                verticalVelocity = -2f; // ��������� �������� ��� ����, ����� �������� ����� �����������
            }
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }
    }

    private void JumpLogic()
    {
        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            StartCoroutine(JumpRoutine());
        }
    }

    private IEnumerator JumpRoutine()
    {
        canJump = false;
        verticalVelocity = Mathf.Sqrt(jumpForce * -2f * gravity);
        animator.SetBool("IsJumping", true);
        yield return new WaitForSeconds(jumpCooldown);
        canJump = true;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (controller.isGrounded)
        {
            animator.SetBool("IsJumping", false);
        }
    }
}

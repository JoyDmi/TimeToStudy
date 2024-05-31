using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10f; // �������� ��������
    public float maxMoveSpeed = 40f; // ������������ �������� ��������
    public float rotationSpeed = 5f; // �������� �������� �������
    public float acceleration = 1f; // ��������� ��������
    public float shiftBoost = 2f; // ��������� ��� ������� ������� Shift
    private float currentSpeed; // ������� ��������

    [SerializeField] GameObject turboPrefab; // ������ ��� �������� �����

    [SerializeField] AudioSource shuttleSound; // ���� ������
    [SerializeField] AudioSource shiftSound; // ���� ��������� ��� ������� Shift
    [SerializeField] Camera PlayerCamera; // ������ �� ������ ������

    private void Start()
    {
        PlayerCamera = Camera.main; // ��������� �������� ������ ��� ������ ������
    }

    private void FixedUpdate()
    {
        HandleMovementInput(); // ��������� ����������������� ����� ��� ��������
    }

    private void HandleMovementInput()
    {
        float verticalInput = Input.GetAxisRaw("Vertical"); // ��������� ������������� ����� (W/S ��� ������� �����/����)
        float horizontalInput = Input.GetAxisRaw("Horizontal"); // ��������� ��������������� ����� (A/D ��� ������� �����/������)

        // ���� ��� ����� �� ������������, �������� ������� �������� �� ��������� ��������
        if (verticalInput == 0 && horizontalInput == 0)
        {
            currentSpeed = moveSpeed;
            if (shuttleSound.isPlaying)
            {
                shuttleSound.Stop(); // ��������� ��������������� ����� ������
            }
            if (shiftSound.isPlaying)
            {
                shiftSound.Stop(); // ��������� ��������������� ����� ��������� ��� ������� Shift
            }

            // ���������� �������� �����
            turboPrefab.SetActive(false);
        }
        else
        {
            // ��������� ����������� ������, ���� ������� ������
            Vector3 forwardDirection = PlayerCamera.transform.forward;

            // ��������� ����������� ������, ���������������� ����������� ������
            Vector3 rightDirection = PlayerCamera.transform.right;

            // ���������� ������� ��������, ������� ����������� ������ � ������
            Vector3 moveDirection = forwardDirection.normalized * verticalInput +
                                    rightDirection.normalized * horizontalInput;

            // ��������, ������ �� ������� Shift
            if (Input.GetKey(KeyCode.LeftShift))
            {
                currentSpeed = Mathf.Lerp(currentSpeed, maxMoveSpeed * shiftBoost, Time.deltaTime * acceleration); // ��������� �� ������������ �������� ��� ������� Shift

                // ��������������� ����� ��������� ��� ������� Shift, ������ ���� �� ��� �� ������
                if (!shiftSound.isPlaying)
                {
                    shiftSound.Play();
                }

                // ��������� �������� �����
                turboPrefab.SetActive(true);
            }
            else
            {
                currentSpeed = Mathf.Lerp(currentSpeed, maxMoveSpeed, Time.deltaTime * acceleration); // ��������� ������� ��������

                // ��������� ��������������� ����� ��������� ��� ������� Shift, ���� �� ������
                if (shiftSound.isPlaying)
                {
                    shiftSound.Stop();
                }

                // ���������� �������� �����
                turboPrefab.SetActive(false);
            }

            // ����������� ������� � ������������ � ����������� �������� �������� � ���������
            transform.Translate(moveDirection * currentSpeed * Time.deltaTime, Space.World);

            // ������� ������� � ����������� ������
            if (moveDirection.magnitude > 0.1f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }

            // ��������������� ����� ������, ������ ���� �� ��� �� ������
            if (!shuttleSound.isPlaying)
            {
                shuttleSound.Play();
            }
        }
    }
}

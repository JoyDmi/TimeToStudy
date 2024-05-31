using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float sensX; // ���������������� ��������������� ��������
    public float sensY; // ���������������� ������������� ��������

    public Transform playerTransform; // ������ �� ��������� ������

    private float distanceFromPlayer = 4f; // ���������� �� ������ �� ������
    private float heightOffset = 0.5f; // �������� �� ��������� ������������ ������
    private float rotationSmoothTime = 0.12f; // ����� �������� ��������

    private Vector3 rotationSmoothVelocity; // ���������� �������� ��������
    private Vector3 currentRotation; // ������� ��������

    private void Start()
    {
        // ���������� ������� � ������� ���
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void FixedUpdate()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX; // ��������� ��������������� �������� �� ����
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY; // ��������� ������������� �������� �� ����

        // ������� ������ ������������ ������ � ������ ���������������� ����
        currentRotation.x -= mouseY;
        currentRotation.y += mouseX;
        currentRotation.x = Mathf.Clamp(currentRotation.x, -90, 90);

        // ������� �������� ������
        rotationSmoothVelocity = Vector3.zero;
        currentRotation = Vector3.SmoothDamp(currentRotation, currentRotation, ref rotationSmoothVelocity, rotationSmoothTime);

        // ���������������� ������ � ������ �������� � ���������� �� ������
        Vector3 direction = new Vector3(0, 0, -distanceFromPlayer);
        Quaternion rotation = Quaternion.Euler(currentRotation);
        transform.position = playerTransform.position + rotation * direction + Vector3.up * heightOffset;
        transform.rotation = rotation;

        // ������������ ������� ������
        playerTransform.localRotation = Quaternion.Euler(currentRotation.x, playerTransform.localRotation.eulerAngles.y, 0);
    }
}

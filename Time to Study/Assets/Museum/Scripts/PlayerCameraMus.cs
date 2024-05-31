using UnityEngine;

public class PlayerCameraMus : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform playerTransform;

    public float distanceFromPlayer = 40f; // ���������� �� ������ �� ������
    public float heightOffset = 40f; // ������ ������ ������������ ������
    public float horizontalOffset = 10f; // �������������� �������� ������

    private Vector3 currentRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void LateUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensX;
        float mouseY = Input.GetAxis("Mouse Y") * sensY;

        // ������� ������ ������������ ������ � ������ ���������������� ����
        currentRotation.y += mouseX;
        currentRotation.x -= mouseY;

        // ���������������� ������ � ������ �������� � ���������� �� ������
        Quaternion rotation = Quaternion.Euler(currentRotation.x, currentRotation.y, 0);
        Vector3 positionOffset = new Vector3(horizontalOffset, heightOffset, -distanceFromPlayer);
        transform.position = playerTransform.position + rotation * positionOffset;
        transform.LookAt(playerTransform.position + Vector3.up * heightOffset);
    }
}

using UnityEngine;

public class PlayerCameraMus : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform playerTransform;

    public float distanceFromPlayer = 40f; // расстояние от игрока до камеры
    public float heightOffset = 40f; // высота камеры относительно игрока
    public float horizontalOffset = 10f; // горизонтальное смещение камеры
    public PauseMenu pauseMenu;
    private Vector3 currentRotation;

    private void Start()
    {
        // Инициализация состояния курсора в начале игры
        pauseMenu.SetCursorState(CursorLockMode.Locked, false);
    }

    private void LateUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensX;
        float mouseY = Input.GetAxis("Mouse Y") * sensY;

        // Поворот камеры относительно игрока с учетом чувствительности мыши
        currentRotation.y += mouseX;
        currentRotation.x -= mouseY;

        // Позиционирование камеры с учетом поворота и расстояния от игрока
        Quaternion rotation = Quaternion.Euler(currentRotation.x, currentRotation.y, 0);
        Vector3 positionOffset = new Vector3(horizontalOffset, heightOffset, -distanceFromPlayer);
        transform.position = playerTransform.position + rotation * positionOffset;
        transform.LookAt(playerTransform.position + Vector3.up * heightOffset);
    }


}

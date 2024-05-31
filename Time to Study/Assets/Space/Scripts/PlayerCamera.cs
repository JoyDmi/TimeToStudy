using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float sensX; // Чувствительность горизонтального вращения
    public float sensY; // Чувствительность вертикального вращения

    public Transform playerTransform; // Ссылка на трансформ игрока

    private float distanceFromPlayer = 4f; // Расстояние от камеры до игрока
    private float heightOffset = 0.5f; // Смещение по вертикали относительно игрока
    private float rotationSmoothTime = 0.12f; // Время плавного вращения

    private Vector3 rotationSmoothVelocity; // Сглаженная скорость вращения
    private Vector3 currentRotation; // Текущее вращение

    private void Start()
    {
        // Блокировка курсора и скрытие его
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void FixedUpdate()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX; // Получение горизонтального вращения от мыши
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY; // Получение вертикального вращения от мыши

        // Поворот камеры относительно игрока с учетом чувствительности мыши
        currentRotation.x -= mouseY;
        currentRotation.y += mouseX;
        currentRotation.x = Mathf.Clamp(currentRotation.x, -90, 90);

        // Плавное вращение камеры
        rotationSmoothVelocity = Vector3.zero;
        currentRotation = Vector3.SmoothDamp(currentRotation, currentRotation, ref rotationSmoothVelocity, rotationSmoothTime);

        // Позиционирование камеры с учетом поворота и расстояния от игрока
        Vector3 direction = new Vector3(0, 0, -distanceFromPlayer);
        Quaternion rotation = Quaternion.Euler(currentRotation);
        transform.position = playerTransform.position + rotation * direction + Vector3.up * heightOffset;
        transform.rotation = rotation;

        // Вертикальный поворот игрока
        playerTransform.localRotation = Quaternion.Euler(currentRotation.x, playerTransform.localRotation.eulerAngles.y, 0);
    }
}

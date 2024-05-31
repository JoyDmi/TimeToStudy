using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10f; // Скорость движения
    public float maxMoveSpeed = 40f; // Максимальная скорость движения
    public float rotationSpeed = 5f; // Скорость поворота объекта
    public float acceleration = 1f; // Ускорение движения
    public float shiftBoost = 2f; // Ускорение при нажатии клавиши Shift
    private float currentSpeed; // Текущая скорость

    [SerializeField] GameObject turboPrefab; // Префаб для анимации турбо

    [SerializeField] AudioSource shuttleSound; // Звук шаттла
    [SerializeField] AudioSource shiftSound; // Звук ускорения при нажатии Shift
    [SerializeField] Camera PlayerCamera; // Ссылка на камеру игрока

    private void Start()
    {
        PlayerCamera = Camera.main; // Установка основной камеры как камеры игрока
    }

    private void FixedUpdate()
    {
        HandleMovementInput(); // Обработка пользовательского ввода для движения
    }

    private void HandleMovementInput()
    {
        float verticalInput = Input.GetAxisRaw("Vertical"); // Получение вертикального ввода (W/S или стрелки вверх/вниз)
        float horizontalInput = Input.GetAxisRaw("Horizontal"); // Получение горизонтального ввода (A/D или стрелки влево/вправо)

        // Если нет ввода от пользователя, сбросить текущую скорость до начальной скорости
        if (verticalInput == 0 && horizontalInput == 0)
        {
            currentSpeed = moveSpeed;
            if (shuttleSound.isPlaying)
            {
                shuttleSound.Stop(); // Остановка воспроизведения звука шаттла
            }
            if (shiftSound.isPlaying)
            {
                shiftSound.Stop(); // Остановка воспроизведения звука ускорения при нажатии Shift
            }

            // Выключение анимации турбо
            turboPrefab.SetActive(false);
        }
        else
        {
            // Получение направления вперед, куда смотрит камера
            Vector3 forwardDirection = PlayerCamera.transform.forward;

            // Получение направления вправо, перпендикулярное направлению камеры
            Vector3 rightDirection = PlayerCamera.transform.right;

            // Вычисление вектора движения, сочетая направления вперед и вправо
            Vector3 moveDirection = forwardDirection.normalized * verticalInput +
                                    rightDirection.normalized * horizontalInput;

            // Проверка, нажата ли клавиша Shift
            if (Input.GetKey(KeyCode.LeftShift))
            {
                currentSpeed = Mathf.Lerp(currentSpeed, maxMoveSpeed * shiftBoost, Time.deltaTime * acceleration); // Ускорение до максимальной скорости при нажатии Shift

                // Воспроизведение звука ускорения при нажатии Shift, только если он еще не играет
                if (!shiftSound.isPlaying)
                {
                    shiftSound.Play();
                }

                // Включение анимации турбо
                turboPrefab.SetActive(true);
            }
            else
            {
                currentSpeed = Mathf.Lerp(currentSpeed, maxMoveSpeed, Time.deltaTime * acceleration); // Установка обычной скорости

                // Остановка воспроизведения звука ускорения при нажатии Shift, если он играет
                if (shiftSound.isPlaying)
                {
                    shiftSound.Stop();
                }

                // Выключение анимации турбо
                turboPrefab.SetActive(false);
            }

            // Перемещение объекта в соответствии с вычисленным вектором движения и скоростью
            transform.Translate(moveDirection * currentSpeed * Time.deltaTime, Space.World);

            // Поворот объекта в направлении камеры
            if (moveDirection.magnitude > 0.1f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }

            // Воспроизведение звука шаттла, только если он еще не играет
            if (!shuttleSound.isPlaying)
            {
                shuttleSound.Play();
            }
        }
    }
}

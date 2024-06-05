using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProjectileMotion : MonoBehaviour
{
    public float initialSpeed = 10f;
    public float angle = 45f;
    public float mass = 1f;
    public float airResistanceCoefficient = 0.35f;
    public float gravity = 9.81f;

    private Vector3 velocity;
    private Vector3 acceleration;
    private float timeOfFlight;
    private float horizontalRange;
    private float startPositionX;
    private bool isFired = false;

    [SerializeField] TextMeshProUGUI range;
    [SerializeField] TextMeshProUGUI time;
    [SerializeField] GameObject shotExplosion;
    [SerializeField] GameObject explosion;
    [SerializeField] AudioSource shootSound; // Звук выстрела
    [SerializeField] AudioSource bigExpl; // Звук большого взрыва
    [SerializeField] float explosionDuration = 2f; // Длительность анимации взрыва

    void Start()
    {
        startPositionX = transform.position.x;
        float angleInRadians = angle * Mathf.Deg2Rad;
        velocity = new Vector3(initialSpeed * Mathf.Cos(angleInRadians),
            initialSpeed * Mathf.Sin(angleInRadians), 0);
    }

    void FixedUpdate()
    {
        if (Input.GetButtonDown("Fire1") && !isFired) // Проверяем состояние isFired
        {
            isFired = true; // Устанавливаем isFired в true
            GameObject shotExplosionInstance = Instantiate(shotExplosion, transform.position, Quaternion.identity);
            Destroy(shotExplosionInstance, explosionDuration); // Уничтожаем анимацию после её воспроизведения
            shootSound.Play();
        }

        if (isFired) // Если isFired установлено в true, запускаем движение снаряда
        {
            RungeKutta4();
            transform.position += velocity * Time.fixedDeltaTime;
            timeOfFlight += Time.fixedDeltaTime;
        }
    }

    void RungeKutta4()
    {
        Vector3 k1 = GetAcceleration(velocity) * Time.fixedDeltaTime;
        Vector3 k2 = GetAcceleration(velocity + k1 / 2) * Time.fixedDeltaTime;
        Vector3 k3 = GetAcceleration(velocity + k2 / 2) * Time.fixedDeltaTime;
        Vector3 k4 = GetAcceleration(velocity + k3) * Time.fixedDeltaTime;

        velocity += (k1 + 2 * k2 + 2 * k3 + k4) / 6;
    }

    Vector3 GetAcceleration(Vector3 velocity)
    {
        float speed = velocity.magnitude;
        float phi = Mathf.Atan2(airResistanceCoefficient * velocity.y, mass * velocity.x);
        Vector3 drag = airResistanceCoefficient * speed * velocity / mass * Mathf.Cos(phi);
        Vector3 gravityForce = new Vector3(0, -gravity * mass, 0);

        return (gravityForce - drag) / mass;
    }

    // Обработка столкновений
    void OnTriggerEnter(Collider colider)
    {
        if (colider.gameObject.CompareTag("Ground"))
        {
            horizontalRange = transform.position.x - startPositionX;
            range.text = "Дальность полёта: " + horizontalRange + " метров";
            time.text = "Время полёта: " + timeOfFlight + " секунд";
            bigExpl.Play();
            GameObject explosionInstance = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(explosionInstance, explosionDuration); // Уничтожаем анимацию после её воспроизведения
            Destroy(gameObject);
        }
    }
}

using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] Transform FirstPoint; // “очка, куда будет происходить респаун игрока
    [SerializeField] Transform SecondPoint; // “очка, куда будет происходить респаун игрока
    [SerializeField] Transform Player;
    void FixedUpdate()
    {
        // ѕровер€ем, если игрок упал ниже заданной высоты
        if (Player.transform.position.y <= -5f & transform.position.z >= 60f)
        {
            FirstSpawn();
        }
        if (Player.transform.position.y <= -5f & transform.position.z <= 63f)
        {
            SecondSpawn();
        }
    }

    void FirstSpawn()
    {
        // ѕеремещаем игрока к точке респауна
        Player.transform.position = SecondPoint.transform.position;
    }
    void SecondSpawn()
    {
        // ѕеремещаем игрока к точке респауна
        Player.transform.position = FirstPoint.transform.position;
    }
}

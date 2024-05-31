using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] Transform FirstPoint; // �����, ���� ����� ����������� ������� ������
    [SerializeField] Transform SecondPoint; // �����, ���� ����� ����������� ������� ������
    [SerializeField] Transform Player;
    void FixedUpdate()
    {
        // ���������, ���� ����� ���� ���� �������� ������
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
        // ���������� ������ � ����� ��������
        Player.transform.position = SecondPoint.transform.position;
    }
    void SecondSpawn()
    {
        // ���������� ������ � ����� ��������
        Player.transform.position = FirstPoint.transform.position;
    }
}

/// <summary>
/// ������� ���������
/// </summary>
public interface IGameManager
{
    //�������� ��������� ����� ����, �������� �� ������ �������������
    ManagerStatus status { get; }//������������, ������� ����� ����������.

    //������������ ��� ��������� �������� ������������� ����������
    void Startup(NetworkService service);
}

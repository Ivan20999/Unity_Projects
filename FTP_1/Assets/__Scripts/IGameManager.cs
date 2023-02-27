/// <summary>
/// Базовый интерфейс
/// </summary>
public interface IGameManager
{
    //Сообщает остальной части кода, завершил ли модуль инициализацию
    ManagerStatus status { get; }//Перечисление, которое нужно определить.

    //Предназначен для обработки процесса инициализации диспетчера
    void Startup(NetworkService service);
}

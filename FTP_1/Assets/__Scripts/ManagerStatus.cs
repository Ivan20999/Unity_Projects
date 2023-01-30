
/// <summary>
/// Перечилсяем возможные состояния диспетчеров
/// принудительно устанавливая для свойства status
/// одно из указанных значений.
/// </summary>
public enum ManagerStatus
{
    Shutdown,
    Initializing,
    Started
}
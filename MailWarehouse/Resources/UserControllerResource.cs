namespace MailWarehouse.Resources;

public static class UserControllerResource
{
    public static string GetAllUsers { get; } = "Отримати всіх користувачів";
    public static string GetUserById { get; } = "Отримати користувача за ID";
    public static string CreateUser { get; } = "Створити нового користувача";
    public static string UpdateUser { get; } = "Оновити існуючого користувача";
    public static string DeleteUser { get; } = "Видалити користувача";
    public static string UserNotFound { get; } = "Користувача не знайдено.";
    public static string InvalidUserData { get; } = "Некоректні дані користувача.";
    public static string ErrorMessage { get; } = "Помилка: {0}";
    public static string UserCreateSuccess { get; } = "Користувача успішно створено";
    public static string UserCreateTitle { get; } = "Створити користувача";
    public static string UserDeleteSuccess { get; } = "Користувача успішно видалено";
    public static string UserDeleteTitle { get; } = "Видалити користувача";
    public static string UserDetailsTitle { get; } = "Деталі користувача";
    public static string UserEditTitle { get; } = "Редагувати користувача";
    public static string UserIndexTitle { get; } = "Список користувачів";
    public static string UserUpdateSuccess { get; } = "Користувача успішно оновлено";
}
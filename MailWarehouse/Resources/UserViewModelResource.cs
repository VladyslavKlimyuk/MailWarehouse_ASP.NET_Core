namespace MailWarehouse.Resources;

public static class UserViewModelResource
{
    public static string AreYouSure { get; } = "Ви впевнені?";
    public static string BackToList { get; } = "Повернутися до списку";
    public static string Delete { get; } = "Видалити";
    public static string Email { get; } = "Електронна пошта";
    public static string EmailAddressError { get; } = "Некоректний формат електронної пошти";
    public static string FirstName { get; } = "Ім'я";
    public static string LastName { get; } = "Прізвище";
    public static string PhoneError { get; } = "Некоректний формат номеру телефону";
    public static string PhoneNumber { get; } = "Номер телефону";
    public static string RequiredError { get; } = "Поле (0) обов'язкове для заповнення.";
    public static string Save { get; } = "Зберегти";
    public static string StringLengthError { get; } = "Поле не може перевищувати роз...";
    public static string UserDeleteTitle { get; } = "Видалення користувача";
    public static string UserDetailsTitle { get; } = "Деталі про користувача";
    public static string UserEditTitle { get; } = "Редагування користувача";
    public static string UserViewModel { get; } = "Інформація про користувача";
    public static string UserId { get; } = "Ідентифікатор користувача";
    public static string Address { get; } = "Адреса";
    public static string CreatedAt { get; } = "Дата створення";
    public static string UpdatedAt { get; } = "Дата оновлення";
    public static string RangeError { get; } = "Значення виходить за допустимі межі.";
    public static string UserUpdateSuccess { get; } = "Користувача успішно оновлено.";
    public static string UserDeleteSuccess { get; } = "Користувача успішно видалено.";
    public static string RemoveRolesError { get; } = "Помилка видалення ролі";
    public static string AddRolesError { get; } = "Помилка додавання ролі";
    public static string RolesUpdatedSuccess { get; } = "Ролі успішно оновлено";
    public static string EditUserRolesTitle { get; } = "Зміна ролі користувачів";
    public static string SaveRoles { get; } = "Зберегти ролі";
    public static string Roles { get; } = "Ролі";
    public static string Password { get; } = "Пароль";
}
namespace MailWarehouse.Resources;

public static class PackageControllerResource
{
    public static string GetAllPackages { get; } = "Отримати всі посилки";
    public static string GetPackageById { get; } = "Отримати посилку за ID";
    public static string CreatePackage { get; } = "Створити нову посилку";
    public static string UpdatePackage { get; } = "Оновити існуючу посилку";
    public static string DeletePackage { get; } = "Видалити посилку";
    public static string PackageNotFound { get; } = "Посилку не знайдено.";
    public static string InvalidPackageData { get; } = "Некоректні дані посилки.";
    public static string CreateSuccess { get; } = "Посилку успішно створено";
    public static string ErrorMessage { get; } = "Помилка: {0}";
    public static string PackageCreateTitle { get; } = "Створити посилку";
    public static string PackageDeleteSuccess { get; } = "Посилку успішно видалено";
    public static string PackageDeleteTitle { get; } = "Видалити посилку";
    public static string PackageDetailsTitle { get; } = "Деталі посилки";
    public static string PackageEditTitle { get; } = "Редагувати посилку";
    public static string PackageIndexTitle { get; } = "Список посилок";
    public static string UpdateSuccess { get; } = "Посилку успішно оновлено";
}
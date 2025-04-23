namespace MailWarehouse.Resources;

public static class PackageViewModelResource
{
    public static string TrackingNumber { get; } = "Номер відстеження";
    public static string SenderId { get; } = "Ідентифікатор відправника";
    public static string RecipientId { get; } = "Ідентифікатор отримувача";
    public static string SenderAddress { get; } = "Адреса відправника";
    public static string RecipientAddress { get; } = "Адреса отримувача";
    public static string Weight { get; } = "Вага";
    public static string Price { get; } = "Ціна";
    public static string CreatedAt { get; } = "Дата створення";
    public static string DeliveredAt { get; } = "Дата доставки";
    public static string RequiredError { get; } = "Це поле є обов'язковим.";
    public static string RangeError { get; } = "Значення виходить за допустимі межі.";
    public static string Create { get; } = "Поле не може перевищувати максимальну довжину.";
}

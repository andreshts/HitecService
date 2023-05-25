namespace HitecService.Models.Constants;

public static class Constants
{


    public enum AffiliateStatus
    {
        Confirmación_Pendiente,
        Identificación_Pendiente,
        Aprobación_Pendiente,
        Affiliado_Aprobado
    }

    public enum CardIdStatus
    {
        Aprobado,
        Rechazado,
        Pendiente
    }

    public const string UrlEmailConfirm     = "/user_confirm";
    public const string SubjectEmailConfirm = "Email Confirm Notification";
}
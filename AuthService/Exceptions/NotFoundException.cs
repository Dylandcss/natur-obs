namespace AuthService.Exceptions
{
    // Notre exception (Alarme) personnalisé (Ressource introuvable)
    // Elle hérite de Exception : elle fonctionne comme les autres alarmes
    public class NotFoundException : Exception
    {
        //On oblige à donner une raison de cette exception(le message d'erreur)
        public NotFoundException(string message) :base(message) 
        { 
        }
    }
}
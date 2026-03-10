using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NetCoreSeguridadPersonalizada.Filters
{
    public class AuthorizeUsersAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        //ESTE METODO ES EL QUE PERMITIRA IMPEDIR EL ACCESO A LOS ACTION/CONTROLLERS
        //EL FILTER SE ENCARGA DE INTERCEPTAR PETICIONES Y DECIDIR QUE HACER
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //EL USUARIO QUE SE HA VALIDADO EN NUESTRA APP
            //ESTARA DENTRO DE Context Y EN UNA PROPIEDAD LLAMADA User
            //CUALQUIER USER ESTA COMPUESTO POR DOS CARACTERISTICAS:
            //1) Identity: EL NOMBRE DEL USUARIO Y SI ES ACTIVO
            //2) Principal: EL ROLE DEL USUARIO
            var user = context.HttpContext.User;
            //EL FILTRO SOLAMENTE PREGUNTARA SI EXISTE EL USER
            //SOLO ENTRA EN ACCION SI NO EXISTE
            if(user.Identity.IsAuthenticated == false)
            {
                //LO LLEVAMOS A LOGIN SI NO SE HA AUTENTIFICADO
                //A LA RUTA DEBEMOS ENVIARLE UN controller Y UN action
                //TAMBIEN PODRIAMOS ENVIARLE PARAMS...
                //controller=HOME
                //action=Privacy
                //mensaje=Esto es un message
                RouteValueDictionary rutaLogin = 
                    new RouteValueDictionary(
                        new
                        {
                            controller="Managed",
                            action="LogIn"
                        }
                        );
                context.Result =
                    new RedirectToRouteResult(rutaLogin);

            }
        }
    }
}

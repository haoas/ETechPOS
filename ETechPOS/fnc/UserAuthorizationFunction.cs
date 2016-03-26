using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ETech.Views.Forms.Generics;
using ETech.cls;

namespace ETech.fnc
{
    public class UserAuthorizationFunction
    {
        public List<string> UserAuthorizations;

        public UserAuthorizationFunction()
            : this(new List<string>())
        {
        }
        public UserAuthorizationFunction(List<string> userAuthorizations)
        {
            UserAuthorizations = userAuthorizations;
        }

        public bool IsVerifiedAuthorization(string userAuthorization)
        {
            if (UserAuthorizations.Contains("ALL") ||
                UserAuthorizations.Contains(userAuthorization))
                return true;
            UserAuthenticationForm userAuthenticationForm = new UserAuthenticationForm();
            userAuthenticationForm.UserAuthorization = userAuthorization;
            userAuthenticationForm.ShowDialog();
            return userAuthenticationForm.HasAuthorization;
        }
    }
}

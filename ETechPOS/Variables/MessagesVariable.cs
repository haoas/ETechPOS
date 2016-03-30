using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETech.Variables
{
    public static class MessagesVariable
    {
        public const string FailedConnectDatabaseInMySql = "Failed to connect to the MySql database.";
        public const string FailedFetchDataInMySql = "Failed to fetch data.";
        public const string FailedExecuteQueryInMySql = "Failed to execute query.";
        public const string FailedLoadConnectionSettings = "Failed to load connection settings file.";
        public const string FailedLoadSettingsFromDatabase = "Failed to load settings from database.";
        public const string FailedCreateBackupDatabase = "Failed in createing backup database.";

        public const string CancelledCreatingBackupDatabase = "Creating backup database has been cancelled.";

        public const string InvalidUserNameOrPasswod = "Username or password is invalid.";

        public const string MessageYourAccountHasNoAuthorization = "Your account has no authorization.";
        public const string MessageYourAccountNotYetActivated = "Your account is not yet activated.";

        public const string RequiredPassword = "Password is required.";
        public const string RequiredUserName = "Username is required.";

        public const string TaskCreatingBackupDatabase = "Creating backup database.";

        public const string SuccessCreatedBackupDatabase = "Created backup database successfully.";
    }
}

namespace EMS.Data
{
    public static class Constants
    {
        // DB records
        internal const string RoleManager = "manager";
        internal const string RoleOperator = "operator";
        internal const string DefaultManagerUsername = "manager@ems.com";
        internal const string DefaultOperator1Username = "operator1@ems.com";
        internal const string DefaultOperator2Username = "operator2@ems.com";
        internal const string DefaultPassword = "Password-123";

        // Connections
        public const string LocalDBConnection = "LocalConnection";
        public const string AzureDBConnection = "AzureConnection";


        // Messages
        public const string AppCreateSucc = "You have successfully created an application";
        public const string AppValidSucc = "You have approved the application";
        public const string AppInvalidSucc = "You have rejected the application";
        public const string AppNewSucc = "You have marked an appication as New";

        public const string EmailInvalidSucc = "You have marked an email as Invalid";
        public const string EmailNotReviewedSucc = "You have marked an email as Not Reviewed";
        public const string EmailNewSucc = "You have marked an email as New";
        public const string EmailOpenSucc = "You have marked an email as Open";

        public const string UserCreateSucc = "You have successfully created a new user";
        public const string UserWrongPass = "Wrong password";
        public const string UserInvalidPass = "You have entered an invalid password";
        public const string UserPassChangeSucc = "You have successfully changed your password";
        public const string UserSignOutSucc = "You have successfully signed out";
        public const string UserSignInSucc = "You have successfully logged in";
        public const string UserInvalLogin = "Invalid login attempt.";

        public const string UserExists = @"User ""{0}"" already exists";
        public const string ErrorCatch = "Something went wrong";
        public const string NoBody = "No body";





        public const string EmailDisplayName = "Email";

        // Navigation
        public const string PageIndex = "Index";
        public const string PageOpen = "Open";
        public const string PageEmail = "Email";
        public const string PageHome = "Home";
        public const string PageUser = "User";
        public const string PageRegister = "Register";

        public const string PageGetClosedEmails = "GetClosedEmails";
        public const string PageGetOpenEmails = "GetOpenEmails";
        public const string PageGetNewEmails = "GetNewEmails";

        public const string ChangePassRedirect = "/Identity/Account/Login";

        public const string HeaderReferer = "Referer";

        //Tabs
        public const string TabAll = "all";
        public const string TabNew = "new";
        public const string TabOpen = "open";
        public const string TabClosed = "closed";

        // Users
        public const string SelListTextManager = "Manager";
        public const string SelListValueManager = "manager";
        public const string SelListTextOperator = "Operator";
        public const string SelListValueOperator = "operator";

        public const string Manager = "manager";
        public const string Operator = "operator";

        public const string LoginDisplayName = "Remember me?";


        // Misc
        public const string TempDataMsg = "message";
        public const string AuthPolicy = "IsPasswordChanged";

        public const string TimeParser0Min = "0 min";
        public const string TimeParserMin = " min";
        public const string TimeParserHrsMin = " hr, ";
        public const string TimeParserDays = " days, ";

        // Logger
        public const string LogEmailInvalid = "{0} has marked email {1} as invalid";
        public const string LogEmailNotReviewd = "{0} has marked email {1} as not reviewed";
        public const string LogEmailNew = "{0} has marked email {1} as new";
        public const string LogEmailOpen = "{0} has marked email {1} as open";
        public const string LogEmailClosed = "{0} has marked email {1} as closed";

        public const string LogEmailDelete = "{0} has deleted email {1}";
        public const string LogAppDelete = "{0} has deleted application {1}";

        public const string LogAppCreate = "{0} has created application for email {1}";
        public const string LogAppApproved = "{0} has approved application {1}";
        public const string LogAppReject = "{0} has rejected application {1}";

        public const string LogUserCreate = "{0} has created a new {1} with email {2}";
        public const string LogUserPassChange = "{0} has changed their password";
        public const string LogUserSignOut = "{0} has signed out";
        public const string LogUserLogin = "{0} has logged in";
        public const string LogUserLogout = "{0} has logged out";
        public const string LogUserLockedOut = "{0} account locked out";

        // Validations
        public const string EnterName = "You have to enter a name";
        public const string NameTooLong = "Name you have entered is too long";
        public const string EnterEGN = "You have to enter a personal ID/EGN";
        public const string EGNTooLong = "ID/EGN you have entered is not valid";
        public const string EnterPhoneNumber = "You have to enter a phone number";
        public const string PhoneNumberTooLong = "Phone number you have entered is too long";
        public const string PassValidLengthErrMsg = "The {0} must be at least {2} and at max {1} characters long";
        public const string PassValidStateErrMsg = "Passsword must consists of at least one of these: lower case letter, upper case letter, a digit and a symbol";
        public const string Password = "Password";
        public const string ConfurmPassword = "Confirm password";
        public const string PassMatchMsg = "The password and confirmation password do not match";
        public const string RegexExpression = @"^(?=.{1,}$)(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*\W).*$";
        public const string InvalidData = "Data is not valid";
        public const string UnknownPass = "Unknown password";


        // Default records in db
        public const string BlockedContent = "Blocked content";

    }
}

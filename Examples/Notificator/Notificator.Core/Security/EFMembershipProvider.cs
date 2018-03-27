using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Configuration.Provider;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Linq;
using Notificator.Core.Model;

/*
 * Based on Ado.Net Entity Framework Membership Provider 
 * http://efmembership.codeplex.com/
 */

namespace Notificator.Core.Security
{
    public class EFMembershipProvider : MembershipProvider
    {
        private const int PASSWORD_SIZE = 14;
        private const int SALT_SIZE_IN_BYTES = 16;
        private int schemaVersionCheck;
        private bool enablePasswordRetrieval;
        private bool enablePasswordReset;
        private bool requiresQuestionAndAnswer;
        private bool requiresUniqueEmail;
        private int maxInvalidPasswordAttempts;
        private int passwordAttemptWindow;
        private int minRequiredPasswordLength;
        private int minRequiredNonalphanumericCharacters;
        private string passwordStrengthRegularExpression;
        private int commandTimeout;
        private string appName;
        private int applicationId;
        private MembershipPasswordFormat passwordFormat;
        private string sqlConnectionString;
        private string hashAlgorithm;
       
        public User GetDBUser(NotificatorEntities db, string username)
        {
            return db.GetUserByName(username, applicationId).FirstOrDefault();
        }

        public override void Initialize(string name, NameValueCollection config)
        {
            if (config == null)
                throw new ArgumentNullException("config");
            if (String.IsNullOrEmpty(name))
                name = "SqlMembershipProvider";
            if (string.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "MembershipSqlProvider_description");
            }
            base.Initialize(name, config);

            schemaVersionCheck = 0;

            enablePasswordRetrieval = SecurityUtils.GetBooleanValue(config, "enablePasswordRetrieval", false);
            enablePasswordReset = SecurityUtils.GetBooleanValue(config, "enablePasswordReset", true);
            requiresQuestionAndAnswer = SecurityUtils.GetBooleanValue(config, "requiresQuestionAndAnswer", true);
            requiresUniqueEmail = SecurityUtils.GetBooleanValue(config, "requiresUniqueEmail", true);
            maxInvalidPasswordAttempts = SecurityUtils.GetIntValue(config, "maxInvalidPasswordAttempts", 5, false, 0);
            passwordAttemptWindow = SecurityUtils.GetIntValue(config, "passwordAttemptWindow", 10, false, 0);
            minRequiredPasswordLength = SecurityUtils.GetIntValue(config, "minRequiredPasswordLength", 7, false, 128);
            minRequiredNonalphanumericCharacters = SecurityUtils.GetIntValue(config, "minRequiredNonalphanumericCharacters", 1, true, 128);

            passwordStrengthRegularExpression = config["passwordStrengthRegularExpression"];
            if (passwordStrengthRegularExpression != null)
            {
                passwordStrengthRegularExpression = passwordStrengthRegularExpression.Trim();
                if (passwordStrengthRegularExpression.Length != 0)
                {
                    try
                    {
                        Regex regex = new Regex(passwordStrengthRegularExpression);
                    }
                    catch (ArgumentException e)
                    {
                        throw new ProviderException(e.Message, e);
                    }
                }
            }
            else
            {
                passwordStrengthRegularExpression = string.Empty;
            }
            if (minRequiredNonalphanumericCharacters > minRequiredPasswordLength)
                throw new HttpException("MinRequiredNonalphanumericCharacters_can_not_be_more_than_MinRequiredPasswordLength");

            string temp = config["connectionStringName"];
            if (temp == null || temp.Length < 1)
                throw new ProviderException("Connection_name_not_specified");
            sqlConnectionString = temp;
            if (sqlConnectionString == null || sqlConnectionString.Length < 1)
            {
                throw new ProviderException("Connection_string_not_found");
            }

            commandTimeout = SecurityUtils.GetIntValue(config, "commandTimeout", 30, true, 0);
            appName = config["applicationName"];

            if (string.IsNullOrEmpty(appName))
                appName = "/";

            if (appName.Length > 256)
            {
                throw new ProviderException("Provider_application_name_too_long");
            }

            using (var db = new NotificatorEntities())
            {
                applicationId = db.GetApplicationId(appName);
            }

            string strTemp = config["passwordFormat"];
            if (strTemp == null)
                strTemp = "Hashed";

            switch (strTemp)
            {
                case "Clear":
                    passwordFormat = MembershipPasswordFormat.Clear;
                    break;
                case "Encrypted":
                    passwordFormat = MembershipPasswordFormat.Encrypted;
                    break;
                case "Hashed":
                    passwordFormat = MembershipPasswordFormat.Hashed;
                    break;
                default:
                    throw new ProviderException("Provider_bad_password_format");
            }

            if (PasswordFormat == MembershipPasswordFormat.Hashed && EnablePasswordRetrieval)
                throw new ProviderException("Provider_can_not_retrieve_hashed_password");
            //if (_PasswordFormat == MembershipPasswordFormat.Encrypted && MachineKeySection.IsDecryptionKeyAutogenerated)
            //    throw new ProviderException(SR.GetString(SR.Can_not_use_encrypted_passwords_with_autogen_keys)); 

            config.Remove("connectionStringName");
            config.Remove("enablePasswordRetrieval");
            config.Remove("enablePasswordReset");
            config.Remove("requiresQuestionAndAnswer");
            config.Remove("applicationName");
            config.Remove("requiresUniqueEmail");
            config.Remove("maxInvalidPasswordAttempts");
            config.Remove("passwordAttemptWindow");
            config.Remove("commandTimeout");
            config.Remove("passwordFormat");
            config.Remove("name");
            config.Remove("minRequiredPasswordLength");
            config.Remove("minRequiredNonalphanumericCharacters");
            config.Remove("passwordStrengthRegularExpression");
            if (config.Count > 0)
            {
                string attribUnrecognized = config.GetKey(0);
                if (!String.IsNullOrEmpty(attribUnrecognized))
                    throw new ProviderException("Provider_unrecognized_attribute, attribUnrecognized");
            }
        }

        public override string ApplicationName
        {
            get
            {
                return appName;
            }
            set
            {
                appName = value;

                using (var db = new NotificatorEntities())
                    applicationId = db.GetApplicationId(appName);
            }
        }

        private bool CheckPassword(NotificatorEntities db, string username, string password, bool updateLastLoginActivityDate, bool failIfNotApproved, out User usr)
        {
            string salt;
            int passwordFormat;
            return CheckPassword(db, username, password, updateLastLoginActivityDate, failIfNotApproved, out salt, out passwordFormat, out usr);
        }

        private bool CheckPassword(NotificatorEntities db, string username, string password, bool updateLastLoginActivityDate, bool failIfNotApproved, out string salt, out int passwordFormat, out User usr)
        {
            var user = GetDBUser(db, username);

            usr = user;
            if (user == null)
            {
                salt = null;
                passwordFormat = -1;

                return false;
            }

            var enc = EncodePassword(password, user.PasswordFormat, user.PasswordSalt);
            passwordFormat = user.PasswordFormat;
            salt = user.PasswordSalt;
            if (enc == user.Password)
            {
                if (updateLastLoginActivityDate)
                {
                    user.LastActivityDate = DateTime.Now;
                    user.LastLoginDate = DateTime.Now;

                    db.SaveChanges();
                }
                return true;
            }
            else
                return false;
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            var salt = string.Empty;
            var passwordFormat = 1;

            using (var db = new NotificatorEntities())
            {
                var user = default(User);
                if (!CheckPassword(db, username, oldPassword, false, false, out salt, out passwordFormat, out user))
                {
                    return false;
                }

                user.Password = EncodePassword(newPassword, passwordFormat, salt);
                user.LastPasswordChangedDate = DateTime.Now;
                user.FailedPasswordAnswerAttemptCount = 0;
                user.FailedPasswordAttemptCount = 0;

                db.SaveChanges();
            }
            return true;
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            using (var db = new NotificatorEntities())
            {
                var user = default(User);
                string salt; int passwordFormat;
                if (!CheckPassword(db, username, password, false, false, out salt, out passwordFormat, out user))
                {
                    return false;
                }

                user.PasswordQuestion = newPasswordQuestion;
                user.PasswordAnswer = newPasswordAnswer;

                db.SaveChanges();
                return true;
            }
        }

        public override System.Web.Security.MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out System.Web.Security.MembershipCreateStatus status)
        {
            if (!ValidateParameter(ref password, true, true, false, 128))
            {
                status = MembershipCreateStatus.InvalidPassword;
                return null;
            }

            var salt = GenerateSalt();
            var pass = EncodePassword(password, (int)passwordFormat, salt);
            if (pass.Length > 128)
            {
                status = MembershipCreateStatus.InvalidPassword;
                return null;
            }

            string encodedPasswordAnswer;
            if (passwordAnswer != null)
            {
                passwordAnswer = passwordAnswer.Trim();
            }

            if (!string.IsNullOrEmpty(passwordAnswer))
            {
                if (passwordAnswer.Length > 128)
                {
                    status = MembershipCreateStatus.InvalidAnswer;
                    return null;
                }
                encodedPasswordAnswer = EncodePassword(passwordAnswer.ToLower(CultureInfo.InvariantCulture), (int)passwordFormat, salt);
            }
            else
                encodedPasswordAnswer = passwordAnswer;

            if (!ValidateParameter(ref encodedPasswordAnswer, RequiresQuestionAndAnswer, true, false, 128))
            {
                status = MembershipCreateStatus.InvalidAnswer;
                return null;
            }

            if (!ValidateParameter(ref username, true, true, true, 256))
            {
                status = MembershipCreateStatus.InvalidUserName;
                return null;
            }

            if (!ValidateParameter(ref email,
                                               RequiresUniqueEmail,
                                               RequiresUniqueEmail,
                                               false,
                                               256))
            {
                status = MembershipCreateStatus.InvalidEmail;
                return null;
            }

            if (!ValidateParameter(ref passwordQuestion, RequiresQuestionAndAnswer, true, false, 256))
            {
                status = MembershipCreateStatus.InvalidQuestion;
                return null;
            }

            if (providerUserKey != null)
            {
                //if (!(providerUserKey is Guid)) {
                //    status = MembershipCreateStatus.InvalidProviderUserKey;
                //    return null;
                //}
                status = MembershipCreateStatus.InvalidProviderUserKey;
                return null;
            }

            if (password.Length < MinRequiredPasswordLength)
            {
                status = MembershipCreateStatus.InvalidPassword;
                return null;
            }

            int count = 0;

            for (int i = 0; i < password.Length; i++)
            {
                if (!char.IsLetterOrDigit(password, i))
                {
                    count++;
                }
            }

            if (count < MinRequiredNonAlphanumericCharacters)
            {
                status = MembershipCreateStatus.InvalidPassword;
                return null;
            }

            if (PasswordStrengthRegularExpression.Length > 0)
            {
                if (!Regex.IsMatch(password, PasswordStrengthRegularExpression))
                {
                    status = MembershipCreateStatus.InvalidPassword;
                    return null;
                }
            }


            ValidatePasswordEventArgs e = new ValidatePasswordEventArgs(username, password, true);
            OnValidatingPassword(e);

            if (e.Cancel)
            {
                status = MembershipCreateStatus.InvalidPassword;
                return null;
            }

            using (var db = new NotificatorEntities())
            {

                if (RequiresUniqueEmail)
                {
                    if (db.Users.Where(u => u.Email == email && u.Application.Id == applicationId).Any())
                    {
                        status = MembershipCreateStatus.DuplicateEmail;
                        return null;
                    }
                }

                if (db.Users.Where(u => u.Username == username && u.Application.Id == applicationId).Any())
                {
                    status = MembershipCreateStatus.DuplicateUserName;
                    return null;
                }

                var utc = DateTime.UtcNow;
                var user = new User()
                {
                    Comment = "",
                    CreateOn = utc,
                    Email = email,
                    FailedPasswordAnswerAttemptCount = 0,
                    FailedPasswordAnswerAttemptWindowStart = utc,
                    FailedPasswordAttemptCount = 0,
                    FailedPasswordAttemptWindowStart = utc,
                    IsAnonymous = false,
                    IsApproved = isApproved,
                    LastActivityDate = utc,
                    LastLockoutDate = utc,
                    LastLoginDate = utc,
                    LastPasswordChangedDate = utc,
                    Password = pass,
                    PasswordAnswer = encodedPasswordAnswer,
                    PasswordFormat = (int)PasswordFormat,
                    PasswordQuestion = passwordQuestion,
                    PasswordSalt = salt,
                    TimeZone = 0,
                    Username = username,
                    Application = db.GetApplication(applicationId)
                };

                db.Users.AddObject(user);
                try
                {
                    db.SaveChanges();
                }
                catch
                {
                    status = MembershipCreateStatus.UserRejected;
                    return null;
                }

                status = MembershipCreateStatus.Success;
                return UserMapper.Map(this.Name, user);
            }

        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            using (var db = new NotificatorEntities())
            {
                var user = db.Users.FirstOrDefault(u => u.Username == username && u.Application.Id == applicationId);
                if (user == null)
                    return false;

                db.DeleteObject(user);
                db.SaveChanges();

                return true;
            }
        }

        public override bool EnablePasswordReset
        {
            get { return enablePasswordReset; }
        }

        public override bool EnablePasswordRetrieval
        {
            get { return enablePasswordRetrieval; }
        }

        public override System.Web.Security.MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            using (var db = new NotificatorEntities())
            {
                var query = from user in db.Users
                            where user.Email.Contains(emailToMatch) && user.Application.Id == applicationId
                            orderby user.Id
                            select user;

                totalRecords = query.Count();
                var col = new MembershipUserCollection();

                foreach (var item in query.Skip(pageSize * pageIndex).Take(pageSize))
                    col.Add(UserMapper.Map(this.Name, item));

                return col;
            }
        }

        public override System.Web.Security.MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            using (var db = new NotificatorEntities())
            {
                var query = from user in db.Users
                            where user.Username.Contains(usernameToMatch) && user.Application.Id == applicationId
                            orderby user.Id
                            select user;

                totalRecords = query.Count();
                var col = new MembershipUserCollection();

                foreach (var item in query.Skip(pageSize * pageIndex).Take(pageSize))
                    col.Add(UserMapper.Map(this.Name, item));

                return col;
            }
        }

        public override System.Web.Security.MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            using (var db = new NotificatorEntities())
            {
                totalRecords = db.Users.Count();
                var col = new MembershipUserCollection();

                foreach (var item in db.Users.Where(u => u.Application.Id == applicationId).OrderBy(o => o.Id).Skip(pageSize * pageIndex).Take(pageSize))
                    col.Add(UserMapper.Map(this.Name, item));

                return col;
            }
        }

        public override int GetNumberOfUsersOnline()
        {
            using (var db = new NotificatorEntities())
                return db.Users.Where(u => u.LastActivityDate > DateTime.Now.AddMinutes(Membership.UserIsOnlineTimeWindow) && u.Application.Id == applicationId).Count();
        }

        public override string GetPassword(string username, string answer)
        {
            using (var db = new NotificatorEntities())
            {
                var usr = GetDBUser(db, username);
                if (usr == null)
                    return null;

                if (usr.PasswordAnswer == answer)
                {
                    return UnEncodePassword(usr.Password, usr.PasswordFormat);
                }
            }
            return null;
        }

        public override System.Web.Security.MembershipUser GetUser(string username, bool userIsOnline)
        {
            using (var db = new NotificatorEntities())
            {
                var usr = GetDBUser(db, username);
                if (usr == null)
                    return null;
                if (userIsOnline)
                {
                    usr.LastActivityDate = DateTime.UtcNow;
                    db.SaveChanges();
                }
                return UserMapper.Map(this.Name, usr);
            }
        }

        public override System.Web.Security.MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            using (var db = new NotificatorEntities())
            {
                var uid = (int)providerUserKey;
                var usr = db.GetUserById(uid, applicationId).FirstOrDefault();
                if (usr == null) 
                    return null;
                if (userIsOnline)
                {
                    usr.LastActivityDate = DateTime.UtcNow;
                    db.SaveChanges();
                }
                return UserMapper.Map(this.Name, usr);
            }
        }

        public override string GetUserNameByEmail(string email)
        {
            using (var db = new NotificatorEntities())
            {
                var usr = (from u in db.Users where u.Email == email && u.Application.Id == applicationId select u.Username).FirstOrDefault();
                return usr;
            }
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { return maxInvalidPasswordAttempts; }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { return minRequiredNonalphanumericCharacters; }
        }

        public override int MinRequiredPasswordLength
        {
            get { return minRequiredPasswordLength; }
        }

        public override int PasswordAttemptWindow
        {
            get { return passwordAttemptWindow; }
        }

        public override System.Web.Security.MembershipPasswordFormat PasswordFormat
        {
            get { return passwordFormat; }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { return passwordStrengthRegularExpression; }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { return requiresQuestionAndAnswer; }
        }

        public override bool RequiresUniqueEmail
        {
            get { return requiresUniqueEmail; }
        }

        public override string ResetPassword(string username, string answer)
        {
            if (!EnablePasswordReset)
            {
                throw new NotSupportedException("Not_configured_to_support_password_resets");
            }

            SecurityUtils.CheckParameter(ref username, true, true, true, 256, "username");

            using (var db = new NotificatorEntities())
            {
                var user = GetDBUser(db, username);
                var passwordAnswer = user.PasswordAnswer;

                string encodedPasswordAnswer;
                if (passwordAnswer != null)
                {
                    passwordAnswer = passwordAnswer.Trim();
                }
                if (!string.IsNullOrEmpty(passwordAnswer))
                    encodedPasswordAnswer = EncodePassword(passwordAnswer.ToLower(CultureInfo.InvariantCulture), user.PasswordFormat, user.PasswordSalt);
                else
                    encodedPasswordAnswer = passwordAnswer;
                SecurityUtils.CheckParameter(ref encodedPasswordAnswer, RequiresQuestionAndAnswer, RequiresQuestionAndAnswer, false, 128, "passwordAnswer");
                string newPassword = GeneratePassword();

                ValidatePasswordEventArgs e = new ValidatePasswordEventArgs(username, newPassword, false);
                OnValidatingPassword(e);

                if (e.Cancel)
                {
                    if (e.FailureInformation != null)
                    {
                        throw e.FailureInformation;
                    }
                    else
                    {
                        throw new ProviderException("Membership_Custom_Password_Validation_Failure");
                    }
                }

                var utc = DateTime.UtcNow;
                if (encodedPasswordAnswer != user.PasswordAnswer)
                {
                    if (utc > user.FailedPasswordAnswerAttemptWindowStart.AddMinutes(PasswordAttemptWindow))
                    {
                        user.FailedPasswordAnswerAttemptCount = 1;
                    }
                    else
                    {
                        user.FailedPasswordAnswerAttemptCount++;
                    }
                    user.FailedPasswordAnswerAttemptWindowStart = utc;

                    if (user.FailedPasswordAnswerAttemptCount > MaxInvalidPasswordAttempts)
                    {
                        user.LastLockoutDate = DateTime.UtcNow;
                        user.Status = (byte)UserStatus.Locked;
                    }

                    db.SaveChanges();
                    return null;
                }
                else
                {
                    user.FailedPasswordAnswerAttemptCount = 0;
                    user.FailedPasswordAnswerAttemptWindowStart = new DateTime(1754, 01, 01);

                    user.FailedPasswordAttemptCount = 0;
                    user.FailedPasswordAttemptWindowStart = user.FailedPasswordAnswerAttemptWindowStart;
                }

                user.Password = EncodePassword(newPassword, user.PasswordFormat, user.PasswordSalt);
                db.SaveChanges();

                return newPassword;
                //user.FailedPasswordAnswerAttemptCount = 0;
            }

        }

        public override bool UnlockUser(string userName)
        {
            SecurityUtils.CheckParameter(ref userName, true, true, true, 256, "username");
            try
            {
                using (var db = new NotificatorEntities())
                {
                    var user = GetDBUser(db, userName);
                    if (user == null)
                        return false;

                    user.Status = (byte)UserStatus.Approved;
                    user.LastLockoutDate = DateTime.UtcNow;

                    db.SaveChanges();
                    return true;
                }
            }
            catch { return false; }
        }

        public override void UpdateUser(System.Web.Security.MembershipUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            string temp = user.UserName;
            SecurityUtils.CheckParameter(ref temp, true, true, true, 256, "UserName");
            temp = user.Email;
            SecurityUtils.CheckParameter(ref temp,
                                       RequiresUniqueEmail,
                                       RequiresUniqueEmail,
                                       false,
                                       256,
                                       "Email");
            user.Email = temp;

            using (var db = new NotificatorEntities())
            {
                var query = from u in db.Users
                            where u.Id == (int)user.ProviderUserKey && u.Application.Id == applicationId
                            select u;

                var usr = query.FirstOrDefault();
                if (usr == null)
                    throw new ProviderException(GetExceptionText(1));

                if (RequiresUniqueEmail)
                {
                    var q = from u in db.Users
                            where u.Id != (int)user.ProviderUserKey
                               && u.Email == user.Email && u.Application.Id == applicationId
                            select u;

                    if (q.Any())
                        throw new ProviderException(GetExceptionText(7));
                }

                usr.Email = user.Email;
                usr.Comment = user.Comment;
                usr.IsApproved = user.IsApproved;
                usr.LastLoginDate = user.LastLoginDate;

                db.SaveChanges();
            }
        }

        public override bool ValidateUser(string username, string password)
        {
            using (var db = new NotificatorEntities())
            {
                var usr = default(User);
                if (ValidateParameter(ref username, true, true, true, 256) &&
                        ValidateParameter(ref password, true, true, false, 128) &&
                        CheckPassword(db, username, password, true, true, out usr))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private HashAlgorithm GetHashAlgorithm()
        {
            if (this.hashAlgorithm != null)
            {
                return HashAlgorithm.Create(this.hashAlgorithm);
            }
            string hashAlgorithmType = Membership.HashAlgorithmType;
            if (hashAlgorithmType != "MD5")
            {
                hashAlgorithmType = "SHA1";
            }
            HashAlgorithm algorithm = HashAlgorithm.Create(hashAlgorithmType);
            if (algorithm == null)
            {
                throw new ConfigurationErrorsException("Invalid_hash_algorithm_type");
            }
            this.hashAlgorithm = hashAlgorithmType;
            return algorithm;
        }

        internal string EncodePassword(string pass, int passwordFormat, string salt)
        {

            if (passwordFormat == 0)
            {
                return pass;
            }
            byte[] bytes = Encoding.Unicode.GetBytes(pass);
            byte[] src = Convert.FromBase64String(salt);
            byte[] inArray = null;
            if (passwordFormat == 1)
            {
                HashAlgorithm hashAlgorithm = this.GetHashAlgorithm();
                if (hashAlgorithm is KeyedHashAlgorithm)
                {
                    KeyedHashAlgorithm algorithm2 = (KeyedHashAlgorithm)hashAlgorithm;
                    if (algorithm2.Key.Length == src.Length)
                    {
                        algorithm2.Key = src;
                    }
                    else if (algorithm2.Key.Length < src.Length)
                    {
                        byte[] dst = new byte[algorithm2.Key.Length];
                        Buffer.BlockCopy(src, 0, dst, 0, dst.Length);
                        algorithm2.Key = dst;
                    }
                    else
                    {
                        int num2;
                        byte[] buffer5 = new byte[algorithm2.Key.Length];
                        for (int i = 0; i < buffer5.Length; i += num2)
                        {
                            num2 = Math.Min(src.Length, buffer5.Length - i);
                            Buffer.BlockCopy(src, 0, buffer5, i, num2);
                        }
                        algorithm2.Key = buffer5;
                    }
                    inArray = algorithm2.ComputeHash(bytes);
                }
                else
                {
                    byte[] buffer6 = new byte[src.Length + bytes.Length];
                    Buffer.BlockCopy(src, 0, buffer6, 0, src.Length);
                    Buffer.BlockCopy(bytes, 0, buffer6, src.Length, bytes.Length);
                    inArray = hashAlgorithm.ComputeHash(buffer6);
                }
            }
            else
            {
                byte[] buffer7 = new byte[src.Length + bytes.Length];
                Buffer.BlockCopy(src, 0, buffer7, 0, src.Length);
                Buffer.BlockCopy(bytes, 0, buffer7, src.Length, bytes.Length);
                inArray = this.EncryptPassword(buffer7);
            }
            return Convert.ToBase64String(inArray);
        }

        internal string UnEncodePassword(string pass, int passwordFormat)
        {
            switch (passwordFormat)
            {
                case 0:
                    return pass;

                case 1:
                    throw new ProviderException("Provider_can_not_decode_hashed_password");
            }
            byte[] encodedPassword = Convert.FromBase64String(pass);
            byte[] bytes = this.DecryptPassword(encodedPassword);
            if (bytes == null)
            {
                return null;
            }
            return Encoding.Unicode.GetString(bytes, 0x10, bytes.Length - 0x10);

        }

        internal string GenerateSalt()
        {
            byte[] buf = new byte[SALT_SIZE_IN_BYTES];
            (new RNGCryptoServiceProvider()).GetBytes(buf);
            return Convert.ToBase64String(buf);
        }

        public virtual string GeneratePassword()
        {
            return Membership.GeneratePassword(
                      MinRequiredPasswordLength < PASSWORD_SIZE ? PASSWORD_SIZE : MinRequiredPasswordLength,
                      MinRequiredNonAlphanumericCharacters);
        }

        internal static bool ValidateParameter(ref string param, bool checkForNull, bool checkIfEmpty, bool checkForCommas, int maxSize)
        {
            if (param == null)
            {
                return !checkForNull;
            }

            param = param.Trim();
            if ((checkIfEmpty && param.Length < 1) ||
                 (maxSize > 0 && param.Length > maxSize) ||
                 (checkForCommas && param.Contains(",")))
            {
                return false;
            }

            return true;
        }

        private string GetExceptionText(int status)
        {
            string key;
            switch (status)
            {
                case 0:
                    return String.Empty;
                case 1:
                    key = "Membership_UserNotFound";
                    break;
                case 2:
                    key = "Membership_WrongPassword";
                    break;
                case 3:
                    key = "Membership_WrongAnswer";
                    break;
                case 4:
                    key = "Membership_InvalidPassword";
                    break;
                case 5:
                    key = "Membership_InvalidQuestion";
                    break;
                case 6:
                    key = "Membership_InvalidAnswer";
                    break;
                case 7:
                    key = "Membership_InvalidEmail";
                    break;
                case 99:
                    key = "Membership_AccountLockOut";
                    break;
                default:
                    key = "Provider_Error";
                    break;
            }
            return key;
        }
    }
}

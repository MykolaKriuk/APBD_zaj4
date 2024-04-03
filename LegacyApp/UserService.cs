using System;

namespace LegacyApp
{
    public interface IClientRepository
    {
        Client GetById(int clientId);
    }

    public interface IUserCreditService
    {
        int GetCreditLimit(string lastName, DateTime dateOfBirth);
    }
    
    public class UserService
    {
        private IClientRepository _clientRepository;
        private IUserCreditService _userCreditService;

        [Obsolete]
        public UserService()
        {
            _clientRepository = new ClientRepository();
            _userCreditService = new UserCreditService();
        }

        public UserService(IClientRepository clientRepository, IUserCreditService userCreditService)
        {
            _clientRepository = clientRepository;
            _userCreditService = userCreditService;
        }

        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            if (IsFirstNameCorrect(firstName) || IsLastNameCorrect(lastName))
            {
                return false;
            }

            if (IsEmailCorrect(email))
            {
                return false;
            }

            var age = CalculateAgeUsingBirthDate(dateOfBirth);

            if (age < 21)
            {
                return false;
            }

            var client = _clientRepository.GetById(clientId);

            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };

            if (client.Type == "VeryImportantClient")
            {
                user.HasCreditLimit = false;
            }
            else if (client.Type == "ImportantClient")
            {
                user.CreditLimit = SetCreditLimitByImportanceOfTheClient(user, 2);
            }
            else
            {
                user.HasCreditLimit = true;
                user.CreditLimit = SetCreditLimitByImportanceOfTheClient(user, 1);
            }
            
            if (IsUserCreditLimitLessThen500(user))
            {
                return false;
            }

            UserDataAccess.AddUser(user);
            return true;
        }

        private int SetCreditLimitByImportanceOfTheClient(User user, int multiplier)
        {
            var creditLimit = _userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
            return creditLimit * multiplier;
        }

        private static bool IsUserCreditLimitLessThen500(User user)
        {
            return user.HasCreditLimit && user.CreditLimit < 500;
        }

        private static int CalculateAgeUsingBirthDate(DateTime dateOfBirth)
        {
            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            bool isCurrentMonthLessThenBirthMonth = now.Month < dateOfBirth.Month;
            if (isCurrentMonthLessThenBirthMonth || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;
            return age;
        }

        private static bool IsEmailCorrect(string email)
        {
            return !email.Contains('@') && !email.Contains('.');
        }

        private static bool IsLastNameCorrect(string lastName)
        {
            return string.IsNullOrEmpty(lastName);
        }

        private static bool IsFirstNameCorrect(string firstName)
        {
            return string.IsNullOrEmpty(firstName);
        }
    }
}

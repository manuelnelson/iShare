using System;
using Elmah;
using iShare.BusinessLogic.Contracts;
using iShare.DataInterface;
using iShare.Models;

namespace iShare.BusinessLogic
{
    public class UserService : Service<IUserRepository, User>, IUserService
    {
        private IUserRepository UserRepository { get; set; }
        public UserService(IUserRepository repository) : base(repository)
        {
            UserRepository = repository;
        }

        public User CreateOrUpdate(User user)
        {
            try
            {
                //Check if user has an account already created
                var accountUser = UserRepository.GetByUserAuthId(user.UserAuthId);
                if (accountUser != null)
                {
                    accountUser.UserAuthId = user.UserAuthId;
                    UserRepository.Update(accountUser);
                    return accountUser;
                }

                //User has never logged in.  Create user
                UserRepository.Add(user);
                return user;
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception("Unable to retrieve information", ex);
            }

        }

        public User GetByUserAuthId(int userAuthId)
        {
            try
            {
                return UserRepository.GetByUserAuthId(userAuthId);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception("Unable to retrieve information", ex);
            }
        }

        public double UpdateAmount(long userId, double amount)
        {
            try
            {
                //get current amount and update!
                var currentUser = UserRepository.Get(userId);
                currentUser.Donation = currentUser.Donation + amount;
                UserRepository.Update(currentUser);
                return currentUser.Donation;
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception("Unable to retrieve information", ex);
            }
        }
    }
}

using PMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public tblUser Get(int id)
        {
            return _userRepository.Get(id);
        }

        public tblUser Get(string email)
        {
            return _userRepository.Get(email);
        }

        public int Insert(tblUser data)
        {
            return _userRepository.Insert(data);
        }

        public int Update(tblUser data)
        {
            return _userRepository.Update(data);
        }
    }
}
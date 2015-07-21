using Rental.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rental.Service.domain;
namespace Rental.Service
{
    public class UserService
    {
        private UnitOfWork _unitOfWork = null;
        public UserService()
        {
            _unitOfWork = new UnitOfWork();
        }

        public bool Login(UserModel model)
        {
            var user = _unitOfWork.UserRepository.Query().FirstOrDefault(s => s.UserName == model.UserName && s.UserPwd == model.UserPwd);
            if (user != null)
            {
                return true;
            }
            return false;
        }


    }
}

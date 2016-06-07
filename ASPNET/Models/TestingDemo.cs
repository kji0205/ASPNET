using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPNET.TestingDemo.Models
{
    public class TestingDemo
    {
    }

    public class User
    {
        public string LoginName { get; set; }
    }

    public interface IUserRepository
    {
        void Add(User newUser);
        User FetchByLoginName(string loginName);
        void SubmitChanges();
    }

    public class DefaultUserRepository : IUserRepository
    {
        public void Add(User newUser)
        {
            // To do
        }
        public User FetchByLoginName(string loginName)
        {
            // To do
            return new User() { LoginName = loginName };
        }
        public void SubmitChanges()
        {
            // To do
        }
    }
}
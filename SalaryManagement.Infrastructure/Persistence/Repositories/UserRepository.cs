﻿using SalaryManagement.Application.Common.Interfaces.Persistence;
using SalaryManagement.Domain.Entities;
using SalaryManagement.Infrastructure.Models;

namespace SalaryManagement.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SalaryManagementContext _context;

        public UserRepository(SalaryManagementContext context)
        {
            _context = context;
        }

        public bool Add(User user)
        {
            try
            {
                _context.Add(user);
                _context.SaveChanges();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }

        public User? GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(x => x.Email == email);
        }
    }
}
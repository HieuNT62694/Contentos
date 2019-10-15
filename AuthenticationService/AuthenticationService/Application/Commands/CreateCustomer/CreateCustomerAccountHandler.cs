using AuthenticationService.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Text.RegularExpressions;
using AuthenticationService.Common;
using AuthenticationService.Models;

namespace AuthenticationService.Application.Commands.CreateCustomer
{
    public class CreateCustomerAccountHandler : IRequestHandler<CreateCustomerAccountCommads, ListUserModel>
    {
        private readonly ContentoContext _context;
        private readonly IHelperFunction _helper;


        public CreateCustomerAccountHandler(ContentoContext context,IHelperFunction helper)
        {
            _context = context;
            _helper = helper;
        }
        public async Task<ListUserModel> Handle(CreateCustomerAccountCommads request, CancellationToken cancellationToken)
        {
            if (!IsEmailUnique(request.Email))
            {
                string newPassword = _helper.GenerateRandomPassword();            
                var newAccount = new Accounts
                {
                    Email = request.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(newPassword),
                    IdRole = 5,
                    IsActive = true,
                    CreatedDate = DateTime.UtcNow

                };
                var lstAcc = new List<Accounts>();
                lstAcc.Add(newAccount);
                var newUser = new Users
                {
                    IsActive = true,
                    Name = request.FullName,
                    IdOccupation = 1,
                    IdLocation = 1,
                    IdManager = request.IdMarketer,
                    Company = request.CompanyName,
                    Accounts = lstAcc
                };
                _context.Users.Add(newUser);
            }

            await _context.SaveChangesAsync();

            return new ListUserModel { Id = _context.Users.OrderByDescending(x=>x.Id).First().Id, Name = request.FullName};
        }
        public bool IsEmailUnique(string Email)
        {
            return _context.Accounts.Where(x => x.Email == Email).Any();

        }
    }
}

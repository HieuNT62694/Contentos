using AuthenticationService.Common;
using AuthenticationService.Entities;
using AuthenticationService.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AuthenticationService.Application.Queries
{
    public class AuthenticationHandler : IRequestHandler<AuthenticationRequest, LoginSuccessViewModel>
    {
        private readonly IHelperFunction _helper;
        private readonly ContentoContext _context;

        public AuthenticationHandler(IHelperFunction helper, ContentoContext context)
        {

            _helper = helper;
            _context = context;
        }

        public async Task<LoginSuccessViewModel> Handle(AuthenticationRequest request, CancellationToken cancellationToken)
        {
            var accounts = _context.Accounts.AsNoTracking().FirstOrDefault(x => x.Email == request.Email && x.IsActive == true);
            bool checkPassword = false;
            if (accounts != null)
            {
                checkPassword = BCrypt.Net.BCrypt.Verify(request.Password, accounts.Password);
                //var appUser = _userManager.Users.SingleOrDefault(r => r.Email == request.Email);
                //var user = _context.Accounts.FirstOrDefault(u => u.Email == request.Email);
                if (checkPassword)
                {
                    string role = _context.Roles.AsNoTracking().FirstOrDefault(r => r.Id == accounts.IdRole).Role;
                    string fullname = _context.Users.Find(accounts.IdUser).Name;
                    LoginSuccessViewModel resultReturn = new LoginSuccessViewModel
                    {
                        
                        FullName = fullname,
                        Role = role,
                        Token = _helper.GenerateJwtToken(request.Email, accounts, role)
                    };

                    return resultReturn;
                }
            }
            return null;
        }
    }
}

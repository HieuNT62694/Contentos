using AuthenticationService.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AuthenticationService.Application.Commands
{
    public class RegisterAccountHandler : IRequestHandler<RegisterAccountCommands>
    {
        private readonly ContentoContext _context;

        public RegisterAccountHandler(ContentoContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(RegisterAccountCommands request, CancellationToken cancellationToken)
        {

            if (!IsEmailUnique(request.Email))
            {

                var newAccount = new Accounts
                {
                    Email = request.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                    IdRole = 4,
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
                    IdManager = 1,
                    Accounts = lstAcc
                };
                _context.Users.Add(newUser);
                //await _context.SaveChangesAsync(cancellationToken);
            }
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
        public bool IsEmailUnique(string Email)
        {
            return _context.Accounts.Where(x => x.Email == Email).Any();

        }
    }
}

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
        private readonly ContentoDbContext _context;

        public RegisterAccountHandler(ContentoDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(RegisterAccountCommands request, CancellationToken cancellationToken)
        {
            var transaction = _context.Database.BeginTransaction();
            try
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
                        LastName = request.LastName,
                        FirstName = request.FirstName,
                        Age = request.Age,
                        Gender = request.Gender,
                        Phone = request.Phone,
                        Accounts = lstAcc
                    };
                    _context.Users.Add(newUser);
                    //await _context.SaveChangesAsync(cancellationToken);
                }
                await _context.SaveChangesAsync(cancellationToken);
                transaction.Commit();
                return Unit.Value;
            }
            catch(Exception e)
            {
                transaction.Rollback();
                return Unit.Value;

            }
         
        }
        public bool IsEmailUnique(string Email)
        {
            return _context.Accounts.Where(x => x.Email == Email).Any();

        }
    }
}

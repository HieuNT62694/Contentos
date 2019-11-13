using AuthenticationService.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
                    var lstTasacc = new List<Personalizations>();

                    var tags = _context.Tags.AsNoTracking().Select(x=>x.Id).ToList();
                    foreach (var item in tags)
                    {
                        var tskAcc = new Personalizations
                        {
                            IdTag = item,
                            CreatedDate = DateTime.UtcNow,
                            IsChosen = false
                        };
                        lstTasacc.Add(tskAcc);
                    }
                    lstTasacc.Where(x => request.Tags.Contains(x.IdTag)).ToList().ForEach(x => x.IsChosen = true);
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
                        Accounts = lstAcc,
                        Personalizations = lstTasacc
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
            return _context.Accounts.Where(x => x.Email == Email && x.IsActive == true).Any();

        }
    }
}

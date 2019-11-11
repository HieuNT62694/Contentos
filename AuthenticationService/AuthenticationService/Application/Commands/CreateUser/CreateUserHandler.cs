using AuthenticationService.Common;
using AuthenticationService.Entities;
using AuthenticationService.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AuthenticationService.Application.Commands.CreateUser
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommands, UserAdminModels>

    {
        private readonly ContentoDbContext _context;
        private readonly IHelperFunction _helper;


        public CreateUserHandler(ContentoDbContext context, IHelperFunction helper)
        {
            _context = context;
            _helper = helper;
        }

        public async Task<UserAdminModels> Handle(CreateUserCommands request, CancellationToken cancellationToken)
        {
            var transaction = _context.Database.BeginTransaction();
            try
            {
                string newPassword = "";
                if (!IsEmailUnique(request.Email))
                {
                    newPassword = _helper.GenerateRandomPassword();
                    var newAccount = new Accounts
                    {
                        Email = request.Email,
                        Password = BCrypt.Net.BCrypt.HashPassword(newPassword),
                        IdRole = request.Role,
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
                        Gender = request.Gender ?? 1,
                        Phone = request.Phone,
                        Company = request.Company,
                        Accounts = lstAcc
                    };
                    if (request.IdManager == null)
                    {
                        newUser.IdManager = null;
                    }
                    else if (request.IdManager.Count == 1 && (request.Role == 2 || request.Role == 3))
                    {
                        newUser.IdManager = request.IdManager.FirstOrDefault();
                    }
                    _context.Users.Add(newUser);
                    await _context.SaveChangesAsync(cancellationToken);
                    if (request.IdManager != null)
                    {
                        if (request.IdManager.Count >= 1 && request.Role == 1)
                        {
                            foreach (var item in request.IdManager)
                            {
                                var acc = _context.Users.FirstOrDefault(x => x.Id == item);
                                if (acc != null)
                                {
                                    acc.IdManager = newUser.Id;
                                    _context.Users.Update(acc);
                                }
                                await _context.SaveChangesAsync(cancellationToken);
                            }

                        }
                    }
                  
                    transaction.Commit();
                    var returnAcc = new UserAdminModels
                    {
                        Id = newUser.Id,
                        Role = new RoleModel { Id = newAccount.IdRole, Name = _context.Roles.Find(newAccount.IdRole).Role },
                        Age = newUser.Age ,
                        Email = newAccount.Email,
                        CompanyName = newUser.Company,
                        FullName = newUser.FirstName + " " + newUser.LastName,
                        Gender = newUser.Gender,
                        IsActive = newUser.IsActive,
                        Phone = string.IsNullOrEmpty(newUser.Phone) == true ? null : newUser.Phone.Trim(),
                        Password = newPassword
                    };
                    return returnAcc;
                }
                return null;
            }
            catch (Exception e)
            {
                transaction.Rollback();
                return null;

            }
        }
        public bool IsEmailUnique(string Email)
        {
            return _context.Accounts.Where(x => x.Email == Email && x.IsActive == true).Any();

        }
    }
}

using AuthenticationService.Entities;
using AuthenticationService.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AuthenticationService.Application.Commands.DeleteUser
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommands, UserAdminModels>
    {
        private readonly ContentoDbContext _context;


        public DeleteUserHandler(ContentoDbContext context)
        {
            _context = context;
        }

        public async Task<UserAdminModels> Handle(DeleteUserCommands request, CancellationToken cancellationToken)
        {
            var transaction = _context.Database.BeginTransaction();
            try
            {
                var upUser = await _context.Users.Include(x => x.Accounts).FirstOrDefaultAsync(x => x.Id == request.Id);
                upUser.IsActive = request.IsActive;
                upUser.Accounts.FirstOrDefault().IsActive = request.IsActive;
                _context.Attach(upUser);
                _context.Entry(upUser).State = EntityState.Modified;
                _context.Users.Update(upUser);
                await _context.SaveChangesAsync();
                transaction.Commit();
                var returnUser = new UserAdminModels()
                {
                    Id = upUser.Id,
                    Role = new RoleModel { Id = upUser.Accounts.FirstOrDefault().IdRole, Name = _context.Roles.Find(upUser.Accounts.FirstOrDefault().IdRole).Role },
                    Age = upUser.Age,
                    Email = upUser.Accounts.FirstOrDefault().Email,
                    CompanyName = upUser.Company,
                    FullName = upUser.FirstName + " " + upUser.LastName,
                    Gender = upUser.Gender,
                    IsActive = upUser.IsActive,
                    Phone = string.IsNullOrEmpty(upUser.Phone) == true ? null : upUser.Phone.Trim(),
                };
                return returnUser;
            }
            catch (Exception)
            {
                transaction.Rollback();
                return null;
            }
          
        }
    }
}

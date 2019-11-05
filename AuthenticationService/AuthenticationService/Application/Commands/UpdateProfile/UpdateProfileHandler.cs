using AuthenticationService.Entities;
using AuthenticationService.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AuthenticationService.Application.Commands.UpdateProfile
{
    public class UpdateProfileHandler : IRequestHandler<UpdateProfileCommands, UserProfileModels>
    {
        private readonly ContentoDbContext _context;

        public UpdateProfileHandler(ContentoDbContext context)
        {
            _context = context;
        }
        public async Task<UserProfileModels> Handle(UpdateProfileCommands request, CancellationToken cancellationToken)
        {
            var transaction = _context.Database.BeginTransaction();
            try
            {
                var upUser = _context.Accounts.Include(i => i.IdUserNavigation).FirstOrDefault(x => x.IdUserNavigation.Id == request.Id);
                if (upUser != null)
                { 
                    upUser.IdUserNavigation.Company = request.CompanyName;
                    upUser.IdUserNavigation.LastName = request.LastName;
                    upUser.IdUserNavigation.Phone = request.Phone;
                    upUser.IdUserNavigation.FirstName = request.FirstName;
                    upUser.IdUserNavigation.Age = request.Age;
                    upUser.IdUserNavigation.Gender = request.Gender;
                    upUser.ModifiedDate = DateTime.UtcNow;
                    _context.Attach(upUser);
                    _context.Entry(upUser).State = EntityState.Modified;
                    _context.Accounts.Update(upUser);
                    await _context.SaveChangesAsync();
                    var returnResult = new UserProfileModels
                    {
                        CompanyName = upUser.IdUserNavigation.Company,
                        Email = upUser.Email,
                        FirstName = upUser.IdUserNavigation.FirstName,
                        LastName = upUser.IdUserNavigation.LastName,
                        Phone = string.IsNullOrEmpty(upUser.IdUserNavigation.Phone) == true ? null : upUser.IdUserNavigation.Phone.Trim(),
                        Id = upUser.IdUserNavigation.Id,
                        Age = upUser.IdUserNavigation.Age,
                        Gender = upUser.IdUserNavigation.Gender
                    };
                    transaction.Commit();
                    return returnResult;
                }
                transaction.Rollback();
                return null;

            }
            catch (Exception e)
            {
                transaction.Rollback();
                return null;
            }
        }
    }
}

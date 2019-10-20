using AuthenticationService.Entities;
using AuthenticationService.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AuthenticationService.Application.Commands.UpdateCustomer
{
    public class UpdateCustomerAccountHandler : IRequestHandler<UpdateCustomerAccountCommads, CreateUserModel>
    {
        private readonly ContentoContext _context;


        public UpdateCustomerAccountHandler(ContentoContext context)
        {
            _context = context;
        }
        public async Task<CreateUserModel> Handle(UpdateCustomerAccountCommads request, CancellationToken cancellationToken)
        {
            var transaction = _context.Database.BeginTransaction();
            try
            {
                var upUser = _context.Users.Include(i => i.Accounts).FirstOrDefault(x => x.Id == request.Id);
                //var lstAccount = new List<Accounts>();
                if (upUser != null)
                {
                    //var upAccount = new Accounts
                    //{
                    //    Email = request.Email,
                    //    ModifiedDate = DateTime.UtcNow
                    //};
                    //lstAccount.Add(upAccount);
                    upUser.Company = request.CompanyName;
                    upUser.Name = request.FullName;
                    upUser.Accounts.FirstOrDefault().Email = request.Email;
                    upUser.Accounts.FirstOrDefault().ModifiedDate = DateTime.UtcNow;
                    _context.Attach(upUser);
                    _context.Entry(upUser).State = EntityState.Modified;
                    _context.Users.Update(upUser);
                    await _context.SaveChangesAsync();
                    var returnResult = new CreateUserModel
                    {
                        CompanyName = upUser.Company,
                        Email = upUser.Accounts.FirstOrDefault().Email,
                        FullName = upUser.Name,
                        Id = upUser.Id,
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

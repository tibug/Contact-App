using ContactApp.DataAccess.Data;
using ContactApp.DataAccess.IRepository;
using ContactApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;


namespace ContactApp.DataAccess.Repository
{
    public class Authentication : IAuthentication
    {
        private readonly ApplicationDbContext _db;
        public Authentication(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ResponseMessage> LoginUser(LoginViewModel data)
        {
            if (!isValidEmailDomain(data.Email) && !isValidEmailFormat(data.Email))
            {
                return new ResponseMessage
                {
                    IsSuccess = false,
                    Message = "Email is Not Valid"
                };
            }
            var result = await _db.tblUsers.Where(x => x.Email == data.Email && x.password == data.password).FirstOrDefaultAsync();
            if (result == null)
            {
                return new ResponseMessage
                {
                    IsSuccess = false,
                    Message = "Your username or password is incorrect"
                };
            }
            if (result != null && result.isActive == false && result.isAdmin==true)
            {
                return new ResponseMessage
                {
                    IsSuccess = false,
                    Message = "Your Email is Not Activated from Admin Side",
                    data = result

                };
            }

            if (result != null && result.isActive == false && result.isAdmin == false)
            {
                return new ResponseMessage
                {
                    IsSuccess = true,
                    Message = "signup",
                    data = result
                };
            }
            else
            {
                return new ResponseMessage
                {
                    IsSuccess = true,
                    Message = "Successfully Login",
                    data= result
                };
            }
        }
        public async Task<ResponseMessage> RegisterUser(Users data)
        {
            if (!isValidEmailDomain(data.Email) && !isValidEmailFormat(data.Email))
            {
                return new ResponseMessage
                {
                    IsSuccess = false,
                    Message = "Email is Not Valid"
                };
            }
            var result = await _db.tblUsers.Where(x => x.Email == data.Email).FirstOrDefaultAsync();
            if (result != null)
            {
                return new ResponseMessage
                {
                    IsSuccess = false,
                    Message = "Sorry This Email is already Exist"
                };
            }
            _db.tblUsers.Add(data);
            await _db.SaveChangesAsync();
            return new ResponseMessage
            {
                IsSuccess = true,
                Message = " Welcome aboard! Your free trial account is ready to go"
            };
        }
        private bool isValidEmailFormat(string email)
        {
            try
            {
                var result = new MailAddress(email);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool isValidEmailDomain(string email)
        {
            try
            {
                string domain = email.Split('@')[1];
                var host = Dns.GetHostEntry(domain);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

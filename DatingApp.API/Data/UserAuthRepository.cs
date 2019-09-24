using System;
using System.Threading.Tasks;
using DatingApp.API.Model;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class UserAuthRepository : IUserAuthRepository
    {
        private readonly DataContext _context;
        public UserAuthRepository(DataContext context)
        {
            _context = context;

        }
        public async Task<bool> IsUserExists(string username)
        {
            if(await _context.Users.AnyAsync(x => x.Username == username))
               return true;
            return false;
        }

        public async Task<User> Login(string username, string password)
        {
           var user = await _context.Users.Include(p => p.Photos).FirstOrDefaultAsync(x => x.Username == username);
           if(!VerifyPasswordHash(password, user.PasswordHash,user.PasswordSalt)){
               return null;
           }
           return user;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt)){
                 var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                 for(int i=0; i<computedHash.Length;i++){
                     if(passwordHash[i] != computedHash[i])
                        return false;
                 }
                 return true;
           }
        }

        public async Task<User> Register(User user, string password)
        {
           byte[] PasswordHash, PasswordSalt;
            GeneratePasswordHash(password, out PasswordHash, out PasswordSalt);
           user.PasswordHash = PasswordHash;
           user.PasswordSalt = PasswordSalt;
           await _context.Users.AddAsync(user);
           await _context.SaveChangesAsync();
           return user;
        }

        private void GeneratePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
             using(var hmac = new System.Security.Cryptography.HMACSHA512()){
                 passwordSalt = hmac.Key;
                 passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
           }
        }
    }

}
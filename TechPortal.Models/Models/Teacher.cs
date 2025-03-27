using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TechPortal.Models.Models
{
    public class Teacher
    {
        [JsonIgnore]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string UserName { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email address.")]        
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public ICollection<Student>? Students { get; set; }

        public void SetPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                PasswordHash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        public bool ValidatePassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                return hash == PasswordHash;
            }
        }
    }
}

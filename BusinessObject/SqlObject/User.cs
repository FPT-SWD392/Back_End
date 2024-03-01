using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.SqlObject
{
    public class User : ISqlObject
    {
        public required string UserId {  get; set; }
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public required string Address { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Introduction { get; set; }
        public required DateTime CreatedDate { get; set; }
        public required AccountStatus Status {  get; set; }
        public string? ProfilePictureUrl {  get; set; }
        public UserRole Role { get; set; }
    }
    public enum AccountStatus
    {
        Active,
        Inactive,
    }
    public enum UserRole
    {
        User,
        Artist,
        Admin
    }
}

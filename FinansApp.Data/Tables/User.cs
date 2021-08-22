using System;

namespace FinansApp.Data.Tables
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string OneSignalId { get; set; }
        /// <summary>
        /// 1 - Admin , 2 - Standart
        /// </summary>
        public int UserType { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; }
    }
}

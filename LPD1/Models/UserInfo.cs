using LPD1.Database;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LPD1.Models
{
    public class UserInfo
    {
        //Id
        public ObjectId _id { get; set; }
        //LPD1.Database.Helper.InsertObjectIntoTable<UserInfo>(new UserInfo(){Username="vita1ij", Login="vita1ij", LoginType = ""

        //Username
        [Display(Name = "UserName")]
        [UIHint("This is what other people will see")]
        public string Username { get; set; }

        //Login
        [Required]
        [Display(Name = "Login Name")]
        public string Login { get; set; }

        /// <summary>
        /// LoginType(enum local/FB)
        /// 0-local
        /// 1-FB
        /// </summary>
        public int LoginType { get; set; }

        //PassHash
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public bool IsValid(string login, string password)
        {
            return UserDB.Login(login, password);
        }


    }
}
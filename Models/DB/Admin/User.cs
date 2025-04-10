using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DB.Admin
{
    [Table("User")]
    public class User
    {
        [Column("id"), Description("自增主键id")]
        public int Id { get; set; }

        [Column("user_name"), Description("用户名")]
        public required string UserName { get; set; }

        [Column("password_hash"), Description("密码")]
        public required string PasswordHash { get; set; }

        [Column("nick_name"), Description("昵称")]
        public required string NickName { get; set; }

        [Column("email"), Description("邮箱")]
        public required string Email { get; set; }

        [Column("phone_number"), Description("电话号码")]
        public required string PhoneNumber { get; set; }

        [Column("created"), Description("创建时间")]
        public DateTime Created { get; set; }

        [Column("modified"), Description("修改时间")]
        public DateTime Modified { get; set; }

        [Column("last_login"), Description("上次登录时间")]
        public DateTime? LastLogin { get; set; }

        [Column("status"), Description("状态")]
        public UserStatus Status { get; set; }
    }

    public enum UserStatus : byte
    {
        [Description("正常")]
        Active = 0,

        [Description("禁用")]
        Disabled = 1,

        [Description("冻结")]
        Banned = 2,

        [Description("删除")]
        Deleted = 3
    }
}

﻿using GrabServerCore.Common.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrabServerCore.Models
{
    public class Account : ModelBase
    {
        public int Id { get; set; }
        public double Long { get; set; } = 0;
        public double Lat { get; set; } = 0;
        // authen & author
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string UserRole { get; set; } 
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime TokenCreated { get; set; }
        public DateTime TokenExpires { get; set; }
    }
}

﻿using AngularEshop.DataLayer.Entities.Account;
using AngularEshop.DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace AngularEshop.DataLayer.Entities.Access
{
  public class UserRole : BaseEntity
    {
        #region properties

        public long UserId { get; set; }

        public long RoleId { get; set; }

        #endregion

        #region Relations

        public User User { get; set; }

        public Role Role { get; set; }

        #endregion

    }
}

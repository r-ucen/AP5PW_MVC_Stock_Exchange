using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockExchange.Application.ViewModels
{
    public class EditUserRolesViewModel
    {
            public int UserId { get; set; }
            public string? Username { get; set; }
            public IList<RoleSelection> AvailableRoles { get; set; } = new List<RoleSelection>();
    }

    public class RoleSelection
    {
        public string RoleName { get; set; } = string.Empty;
        public bool IsSelected { get; set; }
    }
}

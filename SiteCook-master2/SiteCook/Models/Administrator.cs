using System;
using System.Collections.Generic;

namespace SiteCook.Models;

public partial class Administrator
{
    public int IdAdministrator { get; set; }

    public string Mail { get; set; } = null!;

    public string Password { get; set; } = null!;
}

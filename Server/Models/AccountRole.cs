using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Server.Models;

public partial class AccountRole
{
    public int Id { get; set; }

    public string EmployeeNik { get; set; } = null!;

    public int RoleId { get; set; }

    [JsonIgnore]
    public virtual Account AccountNikNavigation { get; set; } = null!;

    [JsonIgnore]
    public virtual Role Role { get; set; } = null!;
}

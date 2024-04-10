using System;
using System.Collections.Generic;

namespace DataEntity;

public partial class UserInfo
{
    public int Id { get; set; }

    public string? Mailbox { get; set; }

    public string? Name { get; set; }

    public string Phone { get; set; } = null!;

    public string PassWord { get; set; } = null!;

    public int RoleId { get; set; }

    public string? HeadPortrait { get; set; }

    public string? Address { get; set; }

    public string? OpeningBank { get; set; }

    public string? CardNumber { get; set; }

    public string? IdNumber { get; set; }

    public bool Lock { get; set; }
}

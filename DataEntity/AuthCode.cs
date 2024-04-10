using System;
using System.Collections.Generic;

namespace DataEntity;

public partial class AuthCode
{
    public int Id { get; set; }

    public string Phone { get; set; } = null!;

    public string Code { get; set; } = null!;

    public DateTime ExpirationTime { get; set; }
}

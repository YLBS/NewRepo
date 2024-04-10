using System;
using System.Collections.Generic;

namespace DataEntity;

public partial class Route
{
    public int Id { get; set; }

    public int RoleId { get; set; }

    public string RouteName { get; set; } = null!;

    public int ParentRouteName { get; set; }
}

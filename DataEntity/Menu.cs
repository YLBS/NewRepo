using System;
using System.Collections.Generic;

namespace DataEntity;

public partial class Menu
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Icon { get; set; }

    public string? Click { get; set; }

    public int? ParentId { get; set; }
}

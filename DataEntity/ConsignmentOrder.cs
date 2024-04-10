using System;
using System.Collections.Generic;

namespace DataEntity;

public partial class ConsignmentOrder
{
    public int Id { get; set; }

    public DateOnly? CreateTime { get; set; }

    public int CarInfoId { get; set; }

    public int CurrentProcessor { get; set; }

    public string? Condition { get; set; }

    public string State { get; set; } = null!;
}

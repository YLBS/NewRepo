using System;
using System.Collections.Generic;

namespace DataEntity;

public partial class WorkFlow
{
    public int Id { get; set; }

    public string WorkFlowName { get; set; } = null!;

    public int CurrentProcessor { get; set; }

    public int NextProcessor { get; set; }
}

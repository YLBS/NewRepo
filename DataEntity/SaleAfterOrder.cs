using System;
using System.Collections.Generic;

namespace DataEntity;

public partial class SaleAfterOrder
{
    public int Id { get; set; }

    public DateOnly? CreateTime { get; set; }

    public int SalesTicketId { get; set; }

    public int UserId { get; set; }

    public string State { get; set; } = null!;

    public int? ProcessCecordId { get; set; }
}

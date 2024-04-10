using System;
using System.Collections.Generic;

namespace DataEntity;

public partial class NewSalesTicket
{
    public int Id { get; set; }

    public DateOnly? CreateTime { get; set; }

    public bool Submit { get; set; }

    public int BuyerId { get; set; }

    public int SellerId { get; set; }

    public int CarId { get; set; }

    public DateOnly? Time1 { get; set; }

    public string? Reason1 { get; set; }

    public string? Idea1 { get; set; }

    public int UserId { get; set; }

    public DateOnly? Time2 { get; set; }

    public string? Idea2 { get; set; }

    public string? Reason2 { get; set; }

    public string? State { get; set; }

    public DateOnly? EndTime { get; set; }
}

using System;
using System.Collections.Generic;

namespace Cswr.Dal.Models;

public partial class Log
{
    public int? LogId { get; set; }

    public string Level { get; set; } = null!;

    public string? Logger { get; set; }

    public string? CallSite { get; set; }

    public string? ExceptionType { get; set; }

    public string? Message { get; set; }

    public string? StackTrace { get; set; }

    public string? InnerException { get; set; }

    public string? AdditionalInfo { get; set; }

    public DateTime LoggedOnDate { get; set; }

    public string? AuthenticatedAs { get; set; }

    public string? Impersonating { get; set; }

    public string? SessionId { get; set; }

    public string? MachineName { get; set; }
}

using System;
using System.Collections.Generic;

namespace crud.repository.Models;

public partial class UserAudit
{
    public int AuditId { get; set; }

    public int? UserId { get; set; }

    public string? Operation { get; set; }

    public string? Msg { get; set; }
}

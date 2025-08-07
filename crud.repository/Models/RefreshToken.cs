using System;
using System.Collections.Generic;

namespace crud.repository.Models;

public partial class RefreshToken
{
    public int TokenId { get; set; }

    public string? Token { get; set; }

    public string? JwtId { get; set; }

    public bool? IsUsed { get; set; }

    public bool? IsRevoked { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? ExpiresAt { get; set; }

    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;
}

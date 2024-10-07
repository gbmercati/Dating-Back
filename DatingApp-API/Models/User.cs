using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App_Dating.Models;

public partial class User
{
    public long Id { get; set; }

    public string UserName { get; set; } = null!;

    public string? Name { get; set; }

    public string? LastName { get; set; }

    public int? Age { get; set; }

    public string? Gender { get; set; }

    public string? City { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Match> MatchIdUserSourceNavigations { get; set; } = new List<Match>();

    public virtual ICollection<Match> MatchIdUserTargetNavigations { get; set; } = new List<Match>();

    public virtual ICollection<UserPreference> UserPreferences { get; set; } = new List<UserPreference>();
}

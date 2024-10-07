using System;
using System.Collections.Generic;

namespace App_Dating.Models;

public partial class Preference
{
    public long Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<UserPreference> UserPreferences { get; set; } = new List<UserPreference>();
}

using System;
using System.Collections.Generic;

namespace App_Dating.Models;

public partial class UserPreference
{
    public long Id { get; set; }

    public long IdUser { get; set; }

    public long IdPreferences { get; set; }

    public virtual Preference? IdPreferencesNavigation { get; set; }

    public virtual User? IdUserNavigation { get; set; }
}

using System;
using System.Collections.Generic;

namespace App_Dating.Models;

public partial class Match
{
    public long Id { get; set; }

    public long? IdUserSource { get; set; }

    public long? IdUserTarget { get; set; }

    public virtual User? IdUserSourceNavigation { get; set; }

    public virtual User? IdUserTargetNavigation { get; set; }
}

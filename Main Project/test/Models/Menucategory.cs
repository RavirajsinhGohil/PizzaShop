using System;
using System.Collections;
using System.Collections.Generic;

namespace test.Models;

public partial class Menucategory
{
    public int Menucategoryid { get; set; }

    public string? Categoryname { get; set; }

    public string? Description { get; set; }

    public BitArray? Isdeleted { get; set; }

    public DateTime Createdat { get; set; }

    public int? Createdby { get; set; }

    public DateTime? Updatedat { get; set; }

    public int? Updatedby { get; set; }

    public virtual User? CreatedbyNavigation { get; set; }

    public virtual Item? Item { get; set; }

    public virtual ICollection<Modifiergroup> Modifiergroups { get; set; } = new List<Modifiergroup>();

    public virtual User? UpdatedbyNavigation { get; set; }
}

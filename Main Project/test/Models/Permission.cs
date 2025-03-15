using System;
using System.Collections;
using System.Collections.Generic;

namespace test.Models;

public partial class Permission
{
    public int Permissionid { get; set; }

    public string? Permissionname { get; set; }

    public int Roleid { get; set; }

    public int Menuid { get; set; }

    public BitArray? Canview { get; set; }

    public BitArray? Canaddedit { get; set; }

    public BitArray? Candelete { get; set; }

    public BitArray? Isdeleted { get; set; }

    public DateTime Createdat { get; set; }

    public int? Createdby { get; set; }

    public DateTime? Updatedat { get; set; }

    public int? Updatedby { get; set; }

    public virtual User? CreatedbyNavigation { get; set; }

    public virtual Role Role { get; set; } = null!;

    public virtual User? UpdatedbyNavigation { get; set; }
}

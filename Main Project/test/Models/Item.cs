using System;
using System.Collections;
using System.Collections.Generic;

namespace test.Models;

public partial class Item
{
    public int Itemid { get; set; }

    public string? Itemname { get; set; }

    public string? Itemtype { get; set; }

    public int? Rate { get; set; }

    public int? Quantity { get; set; }

    public BitArray? Available { get; set; }

    public int? Categoryid { get; set; }

    public byte[]? Itemimage { get; set; }

    public BitArray? Ismodifiable { get; set; }

    public BitArray? Isdeleted { get; set; }

    public DateTime Createdat { get; set; }

    public int? Createdby { get; set; }

    public DateTime? Updatedat { get; set; }

    public int? Updatedby { get; set; }

    public virtual User? CreatedbyNavigation { get; set; }

    public virtual Menucategory ItemNavigation { get; set; } = null!;

    public virtual ICollection<Orderdetail> Orderdetails { get; set; } = new List<Orderdetail>();

    public virtual ICollection<Ordermodifierdetail> Ordermodifierdetails { get; set; } = new List<Ordermodifierdetail>();

    public virtual User? UpdatedbyNavigation { get; set; }

    public virtual ICollection<Userfavouriteitem> Userfavouriteitems { get; set; } = new List<Userfavouriteitem>();
}

﻿using System;
using System.Collections.Generic;

namespace test.Models;

public partial class City
{
    public int Cityid { get; set; }

    public string? Cityname { get; set; }

    public int? Stateid { get; set; }

    public virtual State? State { get; set; }
}

using System;
using System.Collections.Generic;

namespace Get_data_from_CSV_file.Models;

public partial class Student
{
    public int Rollnumber { get; set; }

    public string? Name { get; set; }

    public string? Mobilenumber { get; set; }

    public string? City { get; set; }

    public string? Address { get; set; }

    public string? Email { get; set; }

    public string? Pincode { get; set; }
}

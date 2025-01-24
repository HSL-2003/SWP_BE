using System;
using System.Collections.Generic;

namespace Data.Models;

public class SkinType
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<string> Characteristics { get; set; }
    public ICollection<Product> SuitableProducts { get; set; }
    public ICollection<SkinRoutine> SkinRoutines { get; set; }
}

﻿using Shop.Core.Interface;

namespace Shop.Core.Entities;

public class Brand : BaseEntities
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }  
    public ICollection<Product> Products { get; set; }
}

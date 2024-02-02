﻿namespace Shop.Core.Entities;

public class BasketProduct
{
    //public int Id { get; set; }
    public int ProductId { get; set; }
    public int BasketID { get; set; }
    public Product Product { get; set; }
    public Basket Basket { get; set; }
}

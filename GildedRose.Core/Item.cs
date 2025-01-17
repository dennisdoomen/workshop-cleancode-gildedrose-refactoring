using System;

namespace GildedRose;

public class Item
{
    private string name;

    public Item(string name, int sellIn, int quality)
    {
        this.name = name;
        Quality = quality;
        SellIn = sellIn;
    }

    public int SellIn { get; set; }

    public int Quality { get; set; }

    public string Name => name;
}
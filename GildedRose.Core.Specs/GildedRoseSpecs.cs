using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace GildedRose.Core.Specs
{
    public class GildedRoseSpecs
    {
        [Theory]
        [InlineData("+5 Dexterity Vest", 10, 20, 9, 19)]
        [InlineData("Aged Brie", 2, 0, 1, 1)]
        [InlineData("Elixir of the Mongoose", 5, 7, 4, 6)]
        [InlineData("Sulfuras, Hand of Ragnaros", 0, 80, 0, 80)]
        [InlineData("Backstage passes to a TAFKAL80ETC concert", 15, 20, 14, 21)]
        [InlineData("Conjured Mana Cake", 3, 6, 2, 5)]
        public void After_one_day(string name, int startingSellIn, int startingQuality, int expectedSellIn, int expectedQuality)
        {
            // Arrange
            GildedRose gildedRose = new();

            Item item = new(name, startingSellIn, startingQuality);
            
            // Act
            gildedRose.UpdateQuality(new List<Item> {item});

            // Assert
            item.SellIn.Should().Be(expectedSellIn);
            item.Quality.Should().Be(expectedQuality);
        }

        [Theory]
        [InlineData("+5 Dexterity Vest", 10, 20, 7, 17)]
        [InlineData("Aged Brie", 2, 0, -1, 4)]
        [InlineData("Elixir of the Mongoose", 5, 7, 2, 4)]
        [InlineData("Sulfuras, Hand of Ragnaros", 0, 80, 0, 80)]
        [InlineData("Backstage passes to a TAFKAL80ETC concert", 15, 20, 12, 23)]
        [InlineData("Conjured Mana Cake", 3, 6, 0, 3)]
        public void After_three_days(string name, int startingSellIn, int startingQuality, int expectedSellIn, int expectedQuality)
        {
            // Arrange
            GildedRose gildedRose = new();

            Item item = new(name, startingSellIn, startingQuality);
            
            // Act
            for (int day = 0; day < 3; day++)
            {
                gildedRose.UpdateQuality(new List<Item> {item});
            }

            // Assert
            item.SellIn.Should().Be(expectedSellIn);
            item.Quality.Should().Be(expectedQuality);
        }

        [Theory]
        [InlineData("+5 Dexterity Vest", 10, 20, 0, 10)]
        [InlineData("Aged Brie", 2, 0, -8, 18)]
        [InlineData("Elixir of the Mongoose", 5, 7, -5, 0)]
        [InlineData("Sulfuras, Hand of Ragnaros", 0, 80, 0, 80)]
        [InlineData("Backstage passes to a TAFKAL80ETC concert", 15, 20, 5, 35)]
        [InlineData("Conjured Mana Cake", 3, 6, -7, 0)]
        public void After_ten_days(string name, int startingSellIn, int startingQuality, int expectedSellIn, int expectedQuality)
        {
            // Arrange
            GildedRose gildedRose = new();

            Item item = new(name, startingSellIn, startingQuality);
            
            // Act
            for (int day = 0; day < 10; day++)
            {
                gildedRose.UpdateQuality(new List<Item> {item});
            }

            // Assert
            item.SellIn.Should().Be(expectedSellIn);
            item.Quality.Should().Be(expectedQuality);
        }

        [Theory]
        [InlineData(1, 11)]
        [InlineData(5, 15)]
        [InlineData(10, 20)]
        [InlineData(11, 22)]
        [InlineData(15, 30)]
        [InlineData(20, 45)]
        [InlineData(21, 0)]
        public void Backstage_passes_increase_in_quality_quickly_until_they_are_due(int daysPassed, int expectedQuality)
        {
            // Arrange
            GildedRose gildedRose = new();

            int nrDaysUntilConcert = 20;

            Item item = new("Backstage passes to a TAFKAL80ETC concert", sellIn: nrDaysUntilConcert, quality: 10);
            
            // Act
            for (int day = 0; day < daysPassed; day++)
            {
                gildedRose.UpdateQuality(new List<Item> {item});
            }

            // Assert
            item.SellIn.Should().Be(nrDaysUntilConcert - daysPassed);
            item.Quality.Should().Be(expectedQuality);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Linq.DataSources;

namespace Linq
{
    /// <summary>
    /// Considers the use quantifier operators (methods 'All', 'Any', 'SequenceEqual' and `Contains`) in LINQ queries.
    /// Quantifier operation definition:
    /// <see cref="IEnumerable{T}"/> → bool
    /// Quantifier operations return a Boolean value that indicates whether some or all of the elements
    /// in a sequence satisfy a condition.
    /// </summary>
    public static class QuantifierOperations
    {
        /// <summary>
        /// Checks if two sequences match on all elements in the same order.
        /// </summary>
        /// <returns>true.</returns>
        public static bool EqualSequence()
        {
            var wordsA = new[] { "cherry", "apple", "blueberry" };
            var wordsB = new[] { "cherry", "apple", "blueberry" };

            bool sequencesMatch = wordsA.SequenceEqual(wordsB);

            return sequencesMatch;
        }

        /// <summary>
        /// Checks if two sequences match on all elements in the same order.
        /// </summary>
        /// <returns>false.</returns>
        public static bool NotEqualSequence()
        {
            var wordsA = new[] { "cherry", "apple", "blueberry" };
            var wordsB = new[] { "apple", "blueberry", "cherry" };

            bool sequencesMatch = wordsA.SequenceEqual(wordsB);

            return sequencesMatch;
        }

        /// <summary>
        /// Determines if there is at least one word in the list containing “ei”.
        /// </summary>
        /// <returns>true.</returns>
        public static bool AnyMatchingElements()
        {
            string[] words = { "believe", "relief", "receipt", "field" };

#pragma warning disable CA1307
            bool containsEi = (from word in words
                               where word.Contains("ei")
                               select word).Any();
#pragma warning restore CA1307

            return containsEi;
        }

        /// <summary>
        /// Creates product categories with zero units in stock.
        /// </summary>
        /// <returns>Grouped product categories with zero units in stock.</returns>
        public static IEnumerable<(string category, IEnumerable<Product> products)> GroupedAnyMatchedElements()
        {
            List<Product> products = Products.ProductList; // Assuming you have a Products class with a ProductList property
            var categoriesWithUnits = products
          .GroupBy(product => product.Category)
          .Where(categoryGroup => categoryGroup.Any(product => product.UnitsInStock == 0))
          .Select(categoryGroup => (categoryGroup.Key, products: categoryGroup.Select(product => product)));

            return categoriesWithUnits;
        }

        /// <summary>
        /// Determines whether all elements are odd.
        /// </summary>
        /// <returns>true.</returns>
        public static bool AllMatchedElements()
        {
            int[] numbers = { 1, 11, 3, 19, 41, 65, 19 };

            bool allOdd = Array.TrueForAll(numbers, delegate(int item)
            {
                return item % 2 != 0;
            });

            return allOdd;
        }

        /// <summary>
        /// Creates product categories with more than zero units in stock.
        /// </summary>
        /// <returns>Grouped product categories with more than zero units in stock.</returns>
        public static IEnumerable<(string category, IEnumerable<Product> products)> GroupedAllMatchedElements()
        {
            List<Product> products = Products.ProductList;
            var categoriesWithUnits = from product in products
                                      group product by product.Category into categoryGroup
                                      where categoryGroup.All(product => product.UnitsInStock > 0)
                                      select (categoryGroup.Key, categoryGroup.Select(product => product));

            return categoriesWithUnits;
        }

        /// <summary>
        /// Determines whether the sequence contains the given element.
        /// </summary>
        /// <returns>true.</returns>
        public static bool HasAThree()
        {
            int[] numbers = { 2, 3, 4 };

            bool containsThree = Array.Exists(numbers, delegate(int item)
            {
                return item == 3;
            });

            return containsThree;
        }
    }
}

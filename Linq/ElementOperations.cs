using System;
using System.Collections.Generic;
using System.Linq;
using Linq.DataSources;

namespace Linq
{
    /// <summary>
    /// Considers how to select a single element by position in LINQ queries.
    /// Methods: 'First', 'FirstOrDefault', 'Last', 'LastOrDefault', 'Single', 'SingleOrDefault', 'ElementAt', and ElementAtOrDefault.
    /// Element operation definition:
    /// <see cref="IEnumerable{TSource}"/> → TSource
    /// Element operations return a single, specific element from a sequence.
    /// </summary>
    public static class ElementOperations
    {
        /// <summary>
        /// Finds the first matching element as a 'Product'.
        /// </summary>
        /// <returns>The first matching element as a 'Product'.</returns>
        public static Product FirstElement()
        {
            List<Product> products = Products.ProductList;
            return products.Find(product => product.ProductId == 11);
        }

        /// <summary>
        /// Finds the first element in the array that starts with 'o'.
        /// </summary>
        /// <returns>The first element in the array that starts with 'o'.</returns>
        public static string FirstMatchingElement()
        {
            string[] strings = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

            var firstElement = Array.Find(strings, str => str.StartsWith("o", StringComparison.OrdinalIgnoreCase));

            return firstElement;
        }

        /// <summary>
        /// Tries to get the first element of the sequence, unless there are no elements,
        /// in which case the default value for that type is returned.
        /// </summary>
        /// <returns>The value 0.</returns>
        public static int MaybeFirstElement()
        {
            int[] numbers = Array.Empty<int>();

            var firstElement = numbers.FirstOrDefault();

            return firstElement;
        }

            /// <summary>
            /// Tries to get the first product whose `ProductId` is `789` as a single `Product` object,
            /// unless there is no match, in which case `null` is returned.
            /// </summary>
            /// <returns>The value null.</returns>
        public static Product MaybeFirstMatchingElement()
        {
            List<Product> products = Products.ProductList;

            var matchingProduct = products.Find(product => product.ProductId == 789);

            return matchingProduct;
        }

        /// <summary>
        /// Finds the second number (second number has index 1 because sequences use 0-based indexing)
        /// greater than 5 from an array.
        /// </summary>
        /// <returns>The second number greater than 5 from an array.</returns>
        public static int ElementAtPosition()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            var secondNumberGreaterThan5 = numbers.Where(number => number > 5).ElementAtOrDefault(1);

            return secondNumberGreaterThan5;
        }

        /// <summary>
        /// Finds the last element in the array that contains symbol 'o'.
        /// </summary>
        /// <returns>The first element in the array that starts with 'o'.</returns>
        public static string LastMatchingElement()
        {
            string[] strings = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

#pragma warning disable CA1307
            var lastElementWithO = strings.LastOrDefault(predicate: str => str.Contains('o'));
#pragma warning restore CA1307

            return lastElementWithO;
        }

        /// <summary>
        /// Tries to get the last element of the sequence, unless there are no elements,
        /// in which case the default value for that type is returned.
        /// </summary>
        /// <returns>The value 0.</returns>
        public static int MaybeLastElement()
        {
            int[] numbers = Array.Empty<int>();

            var lastElement = numbers.LastOrDefault();

            return lastElement;
        }

        /// <summary>
        /// Tries to find the only element of a sequence that contains symbol 'o'
        /// and throws an exception since more than one such element exists.
        /// </summary>
        /// <returns>Throw InvalidOperationException.</returns>
        public static string SingleMoreThanOneMatchingElement()
        {
            string[] strings = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

            try
            {
#pragma warning disable CA1307
                var matchingElement = strings.Single(str => str.Contains('o'));
#pragma warning restore CA1307
                return matchingElement;
            }
            catch (InvalidOperationException ex) when (ex.Message == "Sequence contains more than one matching element")
            {
                throw new InvalidOperationException("More than one matching element exists.");
            }
            catch (InvalidOperationException ex) when (ex.Message == "Sequence contains no matching element")
            {
                throw new InvalidOperationException("No matching element found.");
            }
        }

        /// <summary>
        /// Tries to find the only element of a sequence that contains symbol 'o'
        /// and throws an exception since there is no such element.
        /// </summary>
        /// <returns>Throw InvalidOperationException.</returns>
        public static string SingleNoMatchingElement()
        {
            string[] strings = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

            try
            {
#pragma warning disable CA1307
                var matchingElement = strings.Single(str => str.Contains('o'));
#pragma warning restore CA1307
                return matchingElement;
            }
            catch (InvalidOperationException ex) when (ex.Message == "Sequence contains no matching element")
            {
                throw new InvalidOperationException("No matching element found.");
            }
        }

        /// <summary>
        /// Tries to find the only element of a sequence that contains symbol 'o'
        /// and returns a default value since no such element exists.
        /// </summary>
        /// <returns>The value null.</returns>
        public static string MaybeSingleMatchingElement()
        {
            string[] strings = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

#pragma warning disable CA1307
            var matchingElements = strings.Where(predicate: str => str.Contains('o')).ToList();
#pragma warning restore CA1307

            var singleElement = matchingElements.Count == 1 ? matchingElements[0] : null;

            return singleElement;
        }
    }
}

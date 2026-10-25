using global::ASSLINQ;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    namespace ASSLINQ
    {
        internal class Program
        {
            static void Main(string[] args)
            {
                // =========================================================
                // 1. LINQ - Element Operators
                // =========================================================

                // Q1: Get first Product out of Stock
                var elemQ1 = ListGenerators.ProductList.FirstOrDefault(p => p.UnitsInStock == 0);
                Console.WriteLine($"Element Q1: {elemQ1}\n");

                // Q2: Return first product whose Price > 1000
                var elemQ2 = ListGenerators.ProductList.FirstOrDefault(p => p.UnitPrice > 1000);
                Console.WriteLine($"Element Q2: {elemQ2?.ToString() ?? "Null"}\n");

                // Q3: Retrieve second number greater than 5
                int[] arrElemQ3 = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
                var elemQ3 = arrElemQ3.Where(n => n > 5).ElementAtOrDefault(1);
                Console.WriteLine($"Element Q3: {elemQ3}\n");


                // =========================================================
                // 2. LINQ - Aggregate Operators
                // =========================================================

                // Q1: Count odd numbers in array
                int[] arrAggQ1 = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
                var aggQ1 = arrAggQ1.Count(n => n % 2 != 0);
                Console.WriteLine($"Aggregate Q1: {aggQ1}\n");

                // Q2: Customers and how many orders each has
                var aggQ2 = ListGenerators.CustomerList.Select(c => new { c.CustomerName, OrderCount = c.Orders.Length });

                // Q3: Categories and how many products each has
                var aggQ3 = ListGenerators.ProductList.GroupBy(p => p.Category)
                            .Select(g => new { Category = g.Key, ProductCount = g.Count() });

                // Q4: Total of numbers in an array
                int[] arrAggQ4 = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
                var aggQ4 = arrAggQ4.Sum();
                Console.WriteLine($"Aggregate Q4: {aggQ4}\n");

                // Q5 - Q8: Dictionary file operations (Requires dictionary_english.txt)
                if (File.Exists("dictionary_english.txt"))
                {
                    string[] dict = File.ReadAllLines("dictionary_english.txt");

                    var aggQ5 = dict.Sum(w => w.Length);
                    var aggQ6 = dict.Min(w => w.Length);
                    var aggQ7 = dict.Max(w => w.Length);
                    var aggQ8 = dict.Average(w => w.Length);
                }

                // Q9: Total units in stock for each category
                var aggQ9 = ListGenerators.ProductList.GroupBy(p => p.Category)
                            .Select(g => new { Category = g.Key, TotalUnits = g.Sum(p => p.UnitsInStock) });

                // Q10: Cheapest price among each category's products
                var aggQ10 = ListGenerators.ProductList.GroupBy(p => p.Category)
                             .Select(g => new { Category = g.Key, MinPrice = g.Min(p => p.UnitPrice) });

                // Q11: Products with cheapest price in each category (Use Let)
                var aggQ11 = from p in ListGenerators.ProductList
                             group p by p.Category into g
                             let minPrice = g.Min(x => x.UnitPrice)
                             select new { Category = g.Key, CheapestProducts = g.Where(x => x.UnitPrice == minPrice) };

                // Q12: Most expensive price among each category's products
                var aggQ12 = ListGenerators.ProductList.GroupBy(p => p.Category)
                             .Select(g => new { Category = g.Key, MaxPrice = g.Max(p => p.UnitPrice) });

                // Q13: Products with most expensive price in each category
                var aggQ13 = from p in ListGenerators.ProductList
                             group p by p.Category into g
                             let maxPrice = g.Max(x => x.UnitPrice)
                             select new { Category = g.Key, ExpensiveProducts = g.Where(x => x.UnitPrice == maxPrice) };

                // Q14: Average price of each category's products
                var aggQ14 = ListGenerators.ProductList.GroupBy(p => p.Category)
                             .Select(g => new { Category = g.Key, AvgPrice = g.Average(p => p.UnitPrice) });


                // =========================================================
                // 3. LINQ - Set Operators
                // =========================================================

                // Q1: Unique Category names
                var setQ1 = ListGenerators.ProductList.Select(p => p.Category).Distinct();

                // Q2: Unique first letter from product and customer names
                var prodLetters = ListGenerators.ProductList.Select(p => p.ProductName[0]);
                var custLetters = ListGenerators.CustomerList.Select(c => c.CustomerName[0]);
                var setQ2 = prodLetters.Union(custLetters);

                // Q3: Common first letter from product and customer names
                var setQ3 = prodLetters.Intersect(custLetters);

                // Q4: First letters of product names NOT in customer names
                var setQ4 = prodLetters.Except(custLetters);

                // Q5: Last 3 characters in name of all customers and products
                var setQ5 = ListGenerators.ProductList.Where(p => p.ProductName.Length >= 3)
                            .Select(p => p.ProductName.Substring(p.ProductName.Length - 3))
                            .Concat(ListGenerators.CustomerList.Where(c => c.CustomerName.Length >= 3)
                            .Select(c => c.CustomerName.Substring(c.CustomerName.Length - 3)));


                // =========================================================
                // 4. LINQ - Partitioning Operators
                // =========================================================

                // Q1: First 3 orders from Washington customers
                var partQ1 = ListGenerators.CustomerList.Where(c => c.Region == "WA")
                             .SelectMany(c => c.Orders).Take(3);

                // Q2: All but first 2 orders from Washington customers
                var partQ2 = ListGenerators.CustomerList.Where(c => c.Region == "WA")
                             .SelectMany(c => c.Orders).Skip(2);

                // Q3: Elements until number < position
                int[] numbersPart = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
                var partQ3 = numbersPart.TakeWhile((n, index) => n >= index);

                // Q4: Elements starting from first element divisible by 3
                var partQ4 = numbersPart.SkipWhile(n => n % 3 != 0);

                // Q5: Elements starting from first element less than its position
                var partQ5 = numbersPart.SkipWhile((n, index) => n >= index);


                // =========================================================
                // 5. LINQ - Quantifiers
                // =========================================================

                // Q1: Any words in dictionary contain 'ei'
                if (File.Exists("dictionary_english.txt"))
                {
                    string[] dict = File.ReadAllLines("dictionary_english.txt");
                    bool quantQ1 = dict.Any(w => w.Contains("ei"));
                }

                // Q2: Grouped products for categories with at least one out-of-stock product
                var quantQ2 = ListGenerators.ProductList.GroupBy(p => p.Category)
                              .Where(g => g.Any(p => p.UnitsInStock == 0));

                // Q3: Grouped products for categories with all products in stock
                var quantQ3 = ListGenerators.ProductList.GroupBy(p => p.Category)
                              .Where(g => g.All(p => p.UnitsInStock > 0));


                // =========================================================
                // 6. LINQ – Grouping Operators
                // =========================================================

                // Q1: Partition list of numbers by remainder when divided by 5
                List<int> numbersGroup = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
                var groupQ1 = numbersGroup.GroupBy(n => n % 5);

                foreach (var g in groupQ1)
                {
                    Console.WriteLine($"Numbers with a remainder of {g.Key} when divided by 5:");
                    foreach (var n in g) Console.WriteLine(n);
                }

                // Q2: Partition list of words by first letter
                if (File.Exists("dictionary_english.txt"))
                {
                    string[] dict = File.ReadAllLines("dictionary_english.txt");
                    var groupQ2 = dict.Where(w => !string.IsNullOrWhiteSpace(w)).GroupBy(w => char.ToUpper(w[0]));
                }

                // Q3: Group By with custom comparer for same characters (Anagrams)
                string[] arrGroupQ3 = { "from", "salt", "earn", " last", "near", "form" };
                var groupQ3 = arrGroupQ3.GroupBy(w => w.Trim(), new AnagramComparer());

                foreach (var g in groupQ3)
                {
                    foreach (var w in g) Console.WriteLine(w.Trim());
                    Console.WriteLine("....");
                }
            }
        }

        // Custom Equality Comparer for Anagrams (Grouping Q3)
        class AnagramComparer : IEqualityComparer<string>
        {
            public bool Equals(string x, string y) => GetSortedForm(x) == GetSortedForm(y);
            public int GetHashCode(string obj) => GetSortedForm(obj).GetHashCode();

            private string GetSortedForm(string str)
            {
                char[] chars = str.Trim().ToCharArray();
                Array.Sort(chars);
                return new string(chars);
            }
        }
    }
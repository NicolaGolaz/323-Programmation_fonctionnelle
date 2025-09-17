using IronXL;
using CsvHelper;
using System.Linq;
using System.Threading.Tasks.Dataflow;
using System.Globalization;
using Microsoft.Extensions.Primitives;
using System.Diagnostics;


List<Product> products = new List<Product>
{
    new Product { Location = 1, Producer = "Bornand", ProductName = "Pommes", Quantity = 20,Unit = "kg", PricePerUnit = 5.50 },
    new Product { Location = 1, Producer = "Bornand", ProductName = "Poires", Quantity = 16,Unit = "kg", PricePerUnit = 5.50 },
    new Product { Location = 1, Producer = "Bornand", ProductName = "Pastèques", Quantity = 14,Unit = "pièce", PricePerUnit = 5.50 },
    new Product { Location = 1, Producer = "Bornand", ProductName = "Melons", Quantity = 5,Unit = "kg", PricePerUnit = 5.50 },
    new Product { Location = 2, Producer = "Dumont", ProductName = "Noix", Quantity = 20,Unit = "sac", PricePerUnit = 5.50 },
    new Product { Location = 2, Producer = "Dumont", ProductName = "Raisin", Quantity = 6,Unit = "kg", PricePerUnit = 5.50 },
    new Product { Location = 2, Producer = "Dumont", ProductName = "Pruneaux", Quantity = 13,Unit = "kg", PricePerUnit = 5.50 },
    new Product { Location = 2, Producer = "Dumont", ProductName = "Myrtilles", Quantity = 12,Unit = "kg", PricePerUnit = 5.50 },

    //...
};

var i18n = new Dictionary<string, string>()
{
    { "Pommes","Apples"},
    { "Poires","Pears"},
    { "Pastèques","Watermelons"},
    { "Melons","Melons"},
    { "Noix","Nuts"},
    { "Raisin","Grapes"},
    { "Pruneaux","Plums"},
    { "Myrtilles","Blueberries"},
    { "Groseilles","Berries"},
    { "Tomates","Tomatoes"},
    { "Courges","Pumpkins"},
    { "Pêches","Peaches"},
    { "Haricots","Beans"}
};


List<List<string>> productsTitle = new List<List<string>>
{
    new List<string> { "Seller", "Product", "CA" }
};



    List<List<string>> products1 = products.Select(product => new List<string>
{

    product.Producer.First() + (product.Producer.Count()-2).ToString() + product.Producer.Last(),
    i18n[product.ProductName],
    (product.Quantity*product.PricePerUnit).ToString(),

    product.Quantity < 10 ? "Stock faible" :
    product.Quantity <= 15 ? "Stock normal" :
    "Stock élevé"


}).ToList();


// Affichage dans la console
Console.WriteLine("{0,-10} {1,-15} {2,5} {3,15}", "Seller", "Product", "CA", "Stock");
Console.WriteLine("--------------------------------------------------");
foreach (var p in products1)
{
    Console.WriteLine("{0,-10} {1,-15} {2,5} {3,15}", p[0], p[1], p[2], p[3]);
}

var finalList = productsTitle.Concat(products1).ToList();

// Export du fichier CSV
var filePath = "Products.csv";

using (var writer = new StreamWriter(filePath))
{
    foreach (var row in finalList)
    {
        writer.WriteLine(string.Join(";", row));
    }
}

static (long time, long memory) MesurePerf(Action action, int iterations = 1000)
{
    // Forcer le garbage collection avant mesure
    GC.Collect();
    GC.WaitForPendingFinalizers();
    GC.Collect();

    var memoryBefore = GC.GetTotalMemory(false);
    var stopwatch = Stopwatch.StartNew();

    for (int i = 0; i < iterations; i++)
    {
        action();
    }

    stopwatch.Stop();
    var memoryAfter = GC.GetTotalMemory(false);

    long temps = stopwatch.ElapsedMilliseconds;
    long memoire = memoryAfter - memoryBefore;

    Console.WriteLine($"Temps : {temps} ms");
    Console.WriteLine($"Mémoire utilisée : {memoire} octets");

    return (stopwatch.ElapsedMilliseconds, memoryAfter - memoryBefore);
}

var test = MesurePerf(() =>
{
    List<List<string>> products1 = products.Select(product => new List<string>
{

    product.Producer.First() + (product.Producer.Count()-2).ToString() + product.Producer.Last(),
    i18n[product.ProductName],
    (product.Quantity*product.PricePerUnit).ToString(),

    product.Quantity < 10 ? "Stock faible" :
    product.Quantity <= 15 ? "Stock normal" :
    "Stock élevé"


}).ToList();
}, iterations: 1);



public class Product
{
    public int Location { get; set; }
    public string Producer { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public string Unit { get; set; }
    public double PricePerUnit { get; set; }

}
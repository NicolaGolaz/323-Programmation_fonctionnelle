using ExcelDataReader;
using IronXL;
using System;
using System.IO;
using System.Linq;



class Program
{

    static void Main(string[] args)
    {
        string filePath = @"C:\Users\golaz\OneDrive\Documents\GitHub\323-Programmation_fonctionnelle\exos\marché\Place du marché.xlsx";
        WorkBook workbook = WorkBook.Load(filePath);

        WorkSheet sheet = workbook.WorkSheets[1];

        var dataListColumn = new List<List<string>>();

        foreach (var column in sheet.Columns)
        {
            var columnData = new List<string>();
            foreach (var cell in column)
            {
                columnData.Add(cell.Text);

            }
            dataListColumn.Add(columnData);

        }

        var dataListRow = new List<List<string>>();

        foreach (var row in sheet.Rows)
        {
            var rowData = new List<string>();
            foreach (var cell in row)
            {
                rowData.Add(cell.Text);

            }
            dataListRow.Add(rowData);

        }

        // calcule du nombre de vendeur de peche
        int columnIndex = 2;
        int Index = 0;
        int nbrPeche = 0;
        foreach (var column in dataListColumn)
        {

            if (Index == columnIndex)
            {
                foreach (var cell in column)
                {
                    if (cell == "Pêches")
                    {
                        nbrPeche++;
                    }
                }
            }
            Index++;
        }

        Console.WriteLine("le nombre de vandeur de peche est : " + nbrPeche);

        int cellIndex = 0;
        int plusGrandeValeur = 0;
        string nomVendeur = "";
        string emplacement = "";
        foreach (var row in dataListRow)
        {
            cellIndex = 0;
            foreach (var cell in row)
            {

                if (cell == "Pastèques")
                {


                    int nbrPasteque = int.Parse(row[3]);
                    if (nbrPasteque > plusGrandeValeur)
                    {
                        plusGrandeValeur = nbrPasteque;
                        nomVendeur = row[1];
                        emplacement = row[0];
                    }

                }
                cellIndex++;
            }

        }
        Console.WriteLine(nomVendeur + " vend " + plusGrandeValeur + " pastèques a l'emplacement " + emplacement);




        // Avec linq
        var data = new List<MarketInfo>();
        for (int i = 1; i < dataListRow.Count; i++) // i = 1 pour sauter l'en-tête
        {
            var row = dataListRow[i];
            if (row.Count < 6) continue; // Sécurité si la ligne est incomplète

            data.Add(new MarketInfo(
                row[0], // place
                row[1], // seller
                row[2], // product
                row[3], // quantity
                row[4], // unit
                row[5]  // pricePerUnit
            ));
        }

        int nbPecheVendeurs = data
    .Where(x => x.Product == "Pêches")
    .Select(x => x.Seller)
    .Distinct()
    .Count();

        Console.WriteLine($"[LINQ] Il y a {nbPecheVendeurs} vendeurs de pêches.");

        var maxPastèque = data
    .Where(x => x.Product == "Pastèques")
    .OrderByDescending(x => x.Quantity)
    .FirstOrDefault();

        if (maxPastèque != null)
        {
            Console.WriteLine($"[LINQ] {maxPastèque.Seller} vend le plus de pastèques ({maxPastèque.Quantity}) à l'emplacement {maxPastèque.Place}.");
        }


    }


}

class MarketInfo
{
    public MarketInfo(string place,
        string seller, string product, string quantity, string unit, string pricPerUnit)
    {
        Place = place;
        Seller = seller;
        Product = product;
        Quantity = Convert.ToInt32(quantity);
        PricPerUnit = Convert.ToSingle(pricPerUnit);
    }

    public string Place { get; private set; }
    public string Seller { get; private set; }
    public string Product { get; private set; }
    public int Quantity { get; private set; }

    public float PricPerUnit { get; private set; }
}
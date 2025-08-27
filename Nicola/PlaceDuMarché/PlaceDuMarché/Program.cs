using ExcelDataReader;
using IronXL;
using System.IO;
using System.Linq;



class Program
{

    static void Main(string[] args)
    {
        string filePath = @"C:\Users\po42oio\Documents\GitHub\323-Programmation_fonctionnelle\exos\marché\Place du marché.xlsx";
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
    }

}

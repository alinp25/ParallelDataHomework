using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ParallelDataHomework {
    class Program {
        static void Main(string[] args) {
            string Input;
            Dictionary<string, System.Data.DataTable> Database = new Dictionary<string, System.Data.DataTable>();
            System.Data.DataTable ContentDogs = new System.Data.DataTable("Dogs");
            ContentDogs.Columns.Add("Name");
            ContentDogs.Columns.Add("Breed");
            System.Data.DataRow newDogRow1 = ContentDogs.NewRow();
            System.Data.DataRow newDogRow2 = ContentDogs.NewRow();
            newDogRow1["Name"] = "Lessie";
            newDogRow1["Breed"] = "Collie";
            ContentDogs.Rows.Add(newDogRow1);
            newDogRow2["Name"] = "Red";
            newDogRow2["Breed"] = "Husky";
            ContentDogs.Rows.Add(newDogRow2);
            Database.Add("Dogs", ContentDogs);
            do {
                Input = Console.ReadLine();
                if (Input.Contains("CREATE TABLE")) {
                    var SplittedInput = Input.Split(new string[] { "CREATE TABLE" }, StringSplitOptions.None);  
                    Database.Add(SplittedInput[1], new System.Data.DataTable(SplittedInput[1]));
                } else if (Input.Contains("SELECT")) {
                    var Columns= Input.Split(new string[] { "SELECT" }, StringSplitOptions.None)
                                .Last().Trim(' ').Split(new string[] { "FROM" }, StringSplitOptions.None).First().Trim(' ');
                    var TableToSearch = Input.Split(new string[] { "FROM" }, StringSplitOptions.None).Last().Trim(' ');
                    Console.WriteLine("Table: {0}", TableToSearch);
                    Console.WriteLine("------------------");
                    foreach (var Item in Database[TableToSearch].Columns) {
                        Console.Write("{0} ", Item);
                    }
                    Console.WriteLine();
                    Console.WriteLine("------------------");
                    if (Columns == "*") {
                        foreach (System.Data.DataRow Row in Database[TableToSearch].Rows) {
                            foreach (var Item in Row.ItemArray) {
                                Console.Write("{0} ", Item);
                            }
                            Console.WriteLine();
                        }
                        break;
                    }
                }
            } while (Input != "EXIT");
        }

        private static void ContentDogs_TableNewRow(object sender, System.Data.DataTableNewRowEventArgs e) {
            throw new NotImplementedException();
        }
    }
}
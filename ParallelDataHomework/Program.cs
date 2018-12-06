using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ParallelDataHomework
{
    class Program
    {
        static void Main(string[] args)
        {
            string Input;
            Dictionary<string, System.Data.DataTable> Database = new Dictionary<string, System.Data.DataTable>();  
            do {
                Input = Console.ReadLine();
                #region CREATE TABLE
                if (Input.Contains("CREATE TABLE")) {
                    var SplittedInput = Input.Split(new string[] { "CREATE TABLE" }, StringSplitOptions.None).Last().Split('(');
                    var TableName = SplittedInput[0].Trim(' ');
                    Database.Add(TableName, new System.Data.DataTable(TableName)); 
                    var Columns = SplittedInput[1].Split('(').Last().Split(')').First();
                    var GetColumns = Columns.Split(',');
                    Parallel.ForEach(GetColumns, (x) => {
                        x = x.Trim(' ');
                        Database[TableName].Columns.Add(x);
                    });
                }
                #endregion
                #region SELECT
                else if (Input.Contains("SELECT")) {
                    var Columns = Input.Split(new string[] { "SELECT" }, StringSplitOptions.None)
                                .Last().Trim(' ').Split(new string[] { "FROM" }, StringSplitOptions.None).First().Trim(' ')
                                .Split(new string[] { "WHERE" }, StringSplitOptions.None).First().Trim(' ');
                    var TableToSearch = Input.Split(new string[] { "FROM" }, StringSplitOptions.None).Last().Trim(' ');
                    var Condition = Input.Split(new string[] { "WHERE" }, StringSplitOptions.None).Last().Trim(' ');

                    Console.WriteLine("Table: {0}", TableToSearch);
                    if (Columns == "*") {
                        Console.WriteLine("------------------");
                        foreach (var Item in Database[TableToSearch].Columns) {
                            Console.Write("{0} ", Item);
                        }
                        Console.WriteLine();
                        Console.WriteLine("------------------");
                        foreach (System.Data.DataRow Row in Database[TableToSearch].Rows) {
                            /*var ConditionElement = Row[LeftOperand];*/
                            foreach (System.Data.DataColumn Col in Database[TableToSearch].Columns) {
                                Console.Write("{0} ", Row[Col]);
                            }
                            Console.WriteLine();
                        }
                    }
                    else {
                        var SplittedColumns = Columns.Split(',');
                        foreach (var c in SplittedColumns) {
                            Console.WriteLine(c);
                        }
                        Parallel.For(0, SplittedColumns.Length, (i) => {
                            SplittedColumns[i] = SplittedColumns[i].Trim(' ');
                        });
                        Console.WriteLine("------------------");
                        foreach (var Item in SplittedColumns) {
                            Console.Write("{0} ", Item);
                        }
                        Console.WriteLine();
                        Console.WriteLine("------------------");
                        foreach (System.Data.DataRow Row in Database[TableToSearch].Rows) {
                            foreach (var Col in SplittedColumns) {
                                Console.Write("{0} ", Row[Col]);
                            }
                            Console.WriteLine();
                        }
                    }
                }
                #endregion
                #region INSERT INTO
                else if (Input.Contains("INSERT INTO")) {
                    var ToInsert = Input.Split(new string[] { "INSERT INTO" }, StringSplitOptions.None).Last()
                                    .Split(new String[] { "VALUES" }, StringSplitOptions.None);
                    Parallel.For(0, ToInsert.Length, (i) => {
                        ToInsert[i] = ToInsert[i].Trim(' ');
                    });
                    var TableName = ToInsert[0].Split('(').First().Trim(' ');
                    var Columns = ToInsert[0].Split('(').Last().Split(')').First().Trim(' ').Split(',');
                    Parallel.For(0, Columns.Length, (i) => {
                        Columns[i] = Columns[i].Trim(' ');
                    });
                    var Values = ToInsert[1].Split(',');
                    Parallel.For(0, Values.Length, (i) => {
                        Values[i] = Values[i].Trim(' ');
                    });
                    if (Columns.Length != Values.Length) {
                        Console.WriteLine("ERROR! Number of columns must be equal to number of values.");
                    } else {
                        System.Data.DataRow RowToAdd = Database[TableName].NewRow();
                        Parallel.For(0, Columns.Length, (i) => {
                            RowToAdd[Columns[i]] = Values[i];
                        });
                        Database[TableName].Rows.Add(RowToAdd);
                    }
                }
                #endregion
            } while (Input != "EXIT");
        }

        //private static void ContentDogs_TableNewRow(object sender, System.Data.DataTableNewRowEventArgs e)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
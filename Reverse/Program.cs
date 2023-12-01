using System;
using System.Xml.XPath;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using Reverse.Helper;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

class Program
{
    static void Main()
    {
        using CoreContext context = new CoreContext();
        try
        {
            var data = context.InformationSchemas.FromSqlRaw($"SELECT ORDINAL_POSITION, COLUMN_NAME,TABLE_NAME, TABLE_CATALOG, TABLE_SCHEMA,  COLUMN_DEFAULT, IS_NULLABLE, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH, NUMERIC_PRECISION, NUMERIC_PRECISION_RADIX, NUMERIC_SCALE, DATETIME_PRECISION FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME='tp_batch'").ToList();
            var tables = data.Select(x => x.TableName).Distinct();
            foreach (var table in tables)
            {
                Console.WriteLine("Get Data " + data.Count);
                string namespaces = "using System.ComponentModel.DataAnnotations;\nusing System.ComponentModel.DataAnnotations.Schema;\nusing Microsoft.EntityFrameworkCore;\n\nnamespace Reverse.Objects;" + Environment.NewLine + Environment.NewLine;
                string firstsection = "[Table(\"" + table + "\")]" + Environment.NewLine + "public class " + ConvertToCamelCase(table) + Environment.NewLine;
                string openersection = "{" + Environment.NewLine;
                string closersection = "}" + Environment.NewLine;

                string mainsection = string.Empty;

                foreach (var item in data.Where(x=> x.TableName.Equals(table)))
                {
                    string cSharpType = GetCSharpType(item.DataType);
                    string proper = item.IsNullable.Contains("YES") ? "?" : "";
                    string anotation = item.IsNullable.Contains("NO") ? cSharpType.Equals("string") ? " = null!;" : "" : "";
                    mainsection += "\t[Column(\"" + item.ColumnName + "\")]" + Environment.NewLine + "\tpublic " + cSharpType + proper + " " + ConvertToCamelCase(item.ColumnName!) + " { get; set; }" + anotation + Environment.NewLine;
                }

                var path = Path.Combine("/Users/zakki/Documents/Credentials/Dev/Reverse/Reverse/Reverse/Objects", ConvertToCamelCase(table) + ".cs");
                WriteToFile(filePath: path, content: String.Concat(namespaces, firstsection, openersection, mainsection, closersection));
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    static void WriteToFile(string filePath, string content)
    {
        try
        {
            // Open the file for writing (create if it doesn't exist)
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                // Write content to the file
                writer.WriteLine(content);
            }

            Console.WriteLine($"File '{filePath}' created and written successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    static string ConvertToCamelCase(string input)
    {
        // Split the input string by underscores
        string[] parts = input.Split('_');

        // Capitalize the first letter of each part, except the first one
        for (int i = 1; i < parts.Length; i++)
        {
            parts[i] = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(parts[i]);
        }

        // Join the parts back together
        string camelCaseString = string.Join("", parts);
        var newstring = char.ToUpper(camelCaseString[0]) + camelCaseString.Substring(1);
        Console.WriteLine("CamelString " + newstring);
        return newstring;
    }


    static string GetCSharpType(string sqlType)
    {
        switch (sqlType.ToLower())
        {
            case "bit":
                return "bool";
            case "tinyint":
                return "byte";
            case "smallint":
                return "short";
            case "int":
                return "int";
            case "bigint":
                return "long";
            case "real":
                return "float";
            case "float":
                return "double";
            case "decimal":
            case "numeric":
            case "money":
            case "smallmoney":
                return "decimal";
            case "char":
            case "varchar":
            case "text":
            case "nchar":
            case "nvarchar":
            case "ntext":
                return "string";
            case "date":
            case "datetime":
            case "datetime2":
            case "smalldatetime":
                return "DateTime";
            case "uniqueidentifier":
                return "Guid";
            case "binary":
            case "varbinary":
            case "image":
                return "byte[]";
            case "xml":
                return "System.Xml.XmlDocument";
            case "time":
                return "TimeSpan";
            // Add more cases for other SQL data types as needed

            default:
                return "object"; // Default to System.Object if the mapping is not handled
        }
    }
}
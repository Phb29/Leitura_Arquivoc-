using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace LeituraArquivo
{
    class Program
    {

        static void Main(string[] args)
        {

            Console.Write("Enter full file path: ");
            string path = Console.ReadLine();
            //aqui é onde vai ler e vc vai passar o caminho do arquivo
            List<Product> list = new List<Product>();

            using (StreamReader sr = File.OpenText(path)) //precisa ser usado pra ler arquivo
            {
                while (!sr.EndOfStream) //le ate  o final
                {
                    string[] fields = sr.ReadLine().Split(',');
                    string name = fields[0];
                    double price = double.Parse(fields[1], CultureInfo.InvariantCulture);
                    list.Add(new Product(name, price));
                    //tem que cria ruma classe com name e price
                }
            }

            var avg = list.Select(p => p.Price).DefaultIfEmpty(0.0).Average(); //defaultem´ele retorna vazio se n achar ,average a media
            Console.WriteLine("Average price = " + avg.ToString("F2", CultureInfo.InvariantCulture));

            var names = list.Where(p => p.Price < avg).OrderByDescending(p => p.Name).Select(p => p.Name);
            foreach (string name in names)
            {
                Console.WriteLine(name);
            }
        }
    }
}





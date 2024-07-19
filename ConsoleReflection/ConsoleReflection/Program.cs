using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleReflection
{
    internal class Program
    {
        static void Main(string[] args)
        {
      
            string assemblypath = @"C:\Users\Administrator\source\repos\ConsoleReflection\PrintAll\bin\Debug\net8.0\PrintAll.dll";
           
                var assembly = Assembly.LoadFile(assemblypath);
                var mytype = assembly.GetType("CustomPrint.PrintAll");
            dynamic myobjects = Activator.CreateInstance(mytype);
            Type myparameter = myobjects.GetType();
                //foreach (Type type in myparameter)
                //{
                //    Console.WriteLine("TypeName : " + type.Name);
                //    if (type.BaseType != null)
                //    {
                //        Console.WriteLine(type.BaseType.FullName);
                //    }
                //    else
                //    {
                //        Console.WriteLine("Basetype <none>");
                //    }
                    Console.WriteLine("-------------------------------");
                    foreach (var field in myparameter.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly))
                    {
                        Console.WriteLine("Fields : " + field.Name);
                    }
                    Console.WriteLine("--------------------------------");
                    foreach (var method in myparameter.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
                    {
                        Console.WriteLine("method : " + method.Name);
                    }
                    Console.WriteLine("--------------------------------");
                    foreach (var property in myparameter.GetProperties())
                    {
                        Console.WriteLine("properties : " + property.Name);
                    }
                
            
            //catch(ReflectionTypeLoadException e)
            //{
            //    Console.WriteLine("Error loading from types assembly");
            //    foreach(Exception innerEx in e.LoaderExceptions)
            //    {
            //        Console.WriteLine(innerEx.Message);
            //    }
            //}
            Console.ReadKey();
        }
    }
}
//private string name;
//public void print()
//{
//    Console.WriteLine("Printing from print");
//}
//public string GetName()
//{
//    return name;
//}

//public void Printname()
//{
//    Console.WriteLine("Name set as " + name);
//}
//public void print(string name)
//{
//    Console.WriteLine("Name passed " + name);
//}
//public void printprivate()
//{
//    Console.WriteLine("Printing from private");
//}
//public string Name => name;
//public static string Staticname => "Static property name";

//    }

//}
namespace PrintAll
{
    public class CustomPrint

    {

        private string name;
        public void print()
        {
            Console.WriteLine("Printing from print");
        }
        public string GetName()
        {
            return name;
        }

        public void Printname()
        {
            Console.WriteLine("Name set as " + name);
        }
        public void print(string name)
        {
            Console.WriteLine("Name passed " + name);
        }
        public void printprivate()
        {
            Console.WriteLine("Printing from private");
        }
        public string Name => name;
        public static string Staticname => "Static property name";

    }

}

    
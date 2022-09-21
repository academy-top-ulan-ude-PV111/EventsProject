namespace EventsProject
{
    internal class Program
    {
        class BankClient
        {
            public delegate void ClientHandler(string message);
            public event ClientHandler OnClientAmount;
            public int Amount { private set; get; }
            public BankClient(int amount = 0) => Amount = amount;
            public void Put(int amount)
            {
                Amount += amount;
                OnClientAmount?.Invoke($"puts {amount}, total = {Amount}");
                //Console.WriteLine($"puts {amount}, total = {Amount}");
            }
            public void Take(int amount)
            {
                if (Amount >= amount)
                {
                    Amount -= amount;
                    OnClientAmount?.Invoke($"takes {amount}, total = {Amount}");
                    //Console.WriteLine($"takes {amount}, total = {Amount}");
                }
                else
                    OnClientAmount?.Invoke($"total = {Amount} is less than {amount}");
            }
        }

        static void ConsoleMessage(string s)
        {
            Console.WriteLine(s);
        }

        static void SmsMessage(string s)
        {
            Console.WriteLine("sms send: " + s);
        }
        static void Main(string[] args)
        {
            BankClient client = new(1000);
            client.OnClientAmount += ConsoleMessage;

            client.OnClientAmount += (s) => Console.WriteLine("lambda handler");

            client.Take(900);

            client.OnClientAmount -= (s) => Console.WriteLine("lambda handler");

            client.OnClientAmount += SmsMessage;

            client.Put(500);

            client.OnClientAmount -= ConsoleMessage;

            //Console.WriteLine($"Account amount = {client.Amount}");
            
            //Console.WriteLine($"Account amount = {client.Amount}");
            client.Take(700);
            //Console.WriteLine($"Account amount = {client.Amount}");
        }
    }
}
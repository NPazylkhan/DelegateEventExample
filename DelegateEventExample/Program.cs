class Account
{
    public delegate void AccountHandler(string message);
    public event AccountHandler? Notify;              // 1.Определение события
    public Account(int sum) => Sum = sum;
    public int Sum { get; private set; }
    public void Put(int sum)
    {
        Sum += sum;
        Notify?.Invoke($"На счет поступило: {sum}");   // 2.Вызов события 
    }
    public void Take(int sum)
    {
        if (Sum >= sum)
        {
            Sum -= sum;
            Notify?.Invoke($"Со счета снято: {sum}");   // 2.Вызов события
        }
        else
        {
            Notify?.Invoke($"Недостаточно денег на счете. Текущий баланс: {Sum}"); ;
        }
    }

    //static void Main(string[] args)
    //{
    //    Account account = new Account(100);
    //    account.Notify += DisplayMessage;   // Добавляем обработчик для события Notify
    //    account.Put(20);    // добавляем на счет 20
    //    Console.WriteLine($"Сумма на счете: {account.Sum}");
    //    account.Take(70);   // пытаемся снять со счета 70
    //    Console.WriteLine($"Сумма на счете: {account.Sum}");
    //    account.Take(180);  // пытаемся снять со счета 180
    //    Console.WriteLine($"Сумма на счете: {account.Sum}");

    //    void DisplayMessage(string message) => Console.WriteLine(message);
    //    Console.ReadKey();
    //}
}

class Account2Class
{
    public delegate void AccountHandler(Account2Class sender, AccountEventArgs e);
    public event AccountHandler? Notify;

    public int Sum { get; private set; }

    public Account2Class(int sum) => Sum = sum;

    public void Put(int sum)
    {
        Sum += sum;
        Notify?.Invoke(this, new AccountEventArgs($"На счет поступило {sum}", sum));
    }
    public void Take(int sum)
    {
        if (Sum >= sum)
        {
            Sum -= sum;
            Notify?.Invoke(this, new AccountEventArgs($"Сумма {sum} снята со счета", sum));
        }
        else
        {
            Notify?.Invoke(this, new AccountEventArgs("Недостаточно денег на счете", sum));
        }
    }
}

class AccountEventArgs
{
    // Сообщение
    public string Message{get;}
    // Сумма, на которую изменился счет
    public int Sum {get;}
    public AccountEventArgs(string message, int sum)
    {
        Message = message;
        Sum = sum;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Account2Class acc = new Account2Class(100);
        acc.Notify += DisplayMessage;
        acc.Put(20);
        acc.Take(70);
        acc.Take(150);

        void DisplayMessage(Account2Class sender, AccountEventArgs e)
        {
            Console.WriteLine($"Сумма транзакции: {e.Sum}");
            Console.WriteLine(e.Message);
            Console.WriteLine($"Текущая сумма на счете: {sender.Sum}\n");
        }
    }

}
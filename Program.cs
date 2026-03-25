List<(string name, int pin, decimal balance, List<string> transactions)> accounts = FileHandling.Read("Accounts.txt");

accounts.Add(("Sam", 12345, (decimal)1234536.34, []));

FileHandling.Write("Accounts.txt", accounts);
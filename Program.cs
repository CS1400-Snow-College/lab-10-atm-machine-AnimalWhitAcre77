// Ammon Whitaker 3/25/2026 Lab 10 - ATM Machine

using System.Diagnostics;

const string accountsFilePath = "Accounts.txt";

testProgram(); // Remove this once the code is finished 

void testProgram()
{
    // Tests go here

    // Read Files
    Debug.Assert(FileHandling.Read(accountsFilePath) != null);

    // Write Files
    (string, int, decimal, List<string>) newAccount = ("TestName", 51423, (decimal)100.00, ["transaction1", "transaction2"]);
    
    var accounts = FileHandling.Read(accountsFilePath);
    accounts.Add(newAccount);

    FileHandling.Write(accountsFilePath, accounts);

    (string, int, decimal, List<string>) readAccount = FileHandling.Read(accountsFilePath)[^1];

    Debug.Assert(readAccount.Item1 == newAccount.Item1);
    Debug.Assert(readAccount.Item2 == newAccount.Item2);
    Debug.Assert(readAccount.Item3 == newAccount.Item3);
    Debug.Assert(Enumerable.SequenceEqual(readAccount.Item4, newAccount.Item4));
    accounts = accounts[..^1];
    FileHandling.Write(accountsFilePath, accounts); // Remove the changes made
}

Console.Clear();

// Read the account data
List<(string name, int pin, decimal balance, List<string> transactions)> accounts = FileHandling.Read("Accounts.txt");

accounts[0].transactions.ForEach(Console.WriteLine);

FileHandling.Write("Accounts.txt", accounts);
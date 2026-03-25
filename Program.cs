// Ammon Whitaker 3/25/2026 Lab 10 - ATM Machine

const string accountsFilePath = "Accounts.txt";

testProgram(); // Remove this once the code is finished 

void testProgram()
{
    // Tests go here

    Console.Write("Reading File: ");
    try // Read Files
    {
        FileHandling.Read(accountsFilePath);
        Console.WriteLine("Succeeded");
    }
    catch {Console.WriteLine("Failed");}

    Console.Write("Writing File: ");
    try // Write Files
    {
        (string, int, decimal, List<string>) newAccount = ("TestName", 51423, (decimal)100.00, ["transaction1", "transaction2"]);
        
        var accounts = FileHandling.Read(accountsFilePath);
        accounts.Add(newAccount);

        FileHandling.Write(accountsFilePath, accounts);

        if (FileHandling.Read(accountsFilePath)[^1] == newAccount)
        {
            Console.WriteLine("Succeeded");
            accounts = accounts[..^1];
            FileHandling.Write(accountsFilePath, accounts); // Remove the changes made
        }
        else {Console.WriteLine("Failed");}   
    }
    catch {Console.WriteLine("Failed");}
}

// Console.Clear();

// Read the account data
List<(string name, int pin, decimal balance, List<string> transactions)> accounts = FileHandling.Read("Accounts.txt");

accounts[0].transactions.ForEach(Console.WriteLine);

FileHandling.Write("Accounts.txt", accounts);





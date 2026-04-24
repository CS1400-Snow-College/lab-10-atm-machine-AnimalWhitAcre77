// Ammon Whitaker 3/25/2026 Lab 10 - ATM Machine

using System.Diagnostics;

const string accountsFilePath = "Accounts.txt";

ConsoleKeyInfo input;

testProgram(); // Remove this once the code is finished 

void testProgram()
{
    // Tests go here

    // Read Files
    Debug.Assert(FileHandling.Read(accountsFilePath) != null);
    
    // REST OF TESTS USE THE ADDED DEBUG ACCOUNT

    // Write Files
    (string, int, decimal, List<string>) newAccount = ("TestName", 51423, (decimal)100.00, ["transaction1", "transaction2"]);
    
    var accounts = FileHandling.Read(accountsFilePath);
    accounts.Add(newAccount);

    FileHandling.Write(accountsFilePath, accounts);

    (string name, int pin, decimal balance, List<string> transactions) readAccount = FileHandling.Read(accountsFilePath)[^1];

    Debug.Assert(readAccount.Item1 == newAccount.Item1);
    Debug.Assert(readAccount.Item2 == newAccount.Item2);
    Debug.Assert(readAccount.Item3 == newAccount.Item3);
    Debug.Assert(Enumerable.SequenceEqual(readAccount.Item4, newAccount.Item4));
    accounts = accounts[..^1];

    // Validate Pin
    Debug.Assert(ValidatePin(11111, readAccount) == false);
    Debug.Assert(ValidatePin(51423, readAccount) == true); // correct pin

    // Withdraw
    decimal startBal = readAccount.Item3;

    Debug.Assert(Withdraw((decimal)0.001, ref readAccount) == false);
    Debug.Assert(Withdraw(startBal + 100, ref readAccount) == false);
    Debug.Assert(Withdraw(-10,            ref readAccount) == false);
    Debug.Assert(Withdraw((decimal)50.50, ref readAccount) == true);
    Debug.Assert(startBal == readAccount.balance + (decimal)50.50);

    // Deposit
    Debug.Assert(Deposit((decimal)0.001, ref readAccount) == false);
    Debug.Assert(Deposit(-50,            ref readAccount) == false);
    Debug.Assert(Deposit((decimal)50.50, ref readAccount) == true);
    Debug.Assert(startBal == readAccount.balance);

    FileHandling.Write(accountsFilePath, accounts); // Remove the debug account
}

Console.Clear();

// Read the account data
List<(string name, int pin, decimal balance, List<string> transactions)> accounts = FileHandling.Read("Accounts.txt");

// Login to account
Console.WriteLine("Welcome to the Account-it program!");
Console.WriteLine("Type 1 to log in, or type 2 to create a new account.");
Console.WriteLine();

string? accountName = "";
int pin = 0; // illegal starting value
int accountIndex = -1; // illegal starting value

while (accountIndex < 0) // stay here until an account is selected
{
    input = Console.ReadKey(true);
    if (input.KeyChar == '1') // Login
    {
        do
        {
            do
            {
                Console.WriteLine("Login In");

                Console.Write("Username: ");
                accountName = Console.ReadLine();

                Console.Write("Pin: ");
                Int32.TryParse(Console.ReadLine(), out pin);

                accountIndex = accounts.FindIndex(account => account.name == accountName); // No overlapping names
            }
            while (pin <= 0);
        }
        while (!ValidatePin(pin, accounts[accountIndex]));
    }
    else if (input.KeyChar == '2') // New Account
    {
        Console.WriteLine("Create New Account");

        while (accountName == "" || accountName.Contains(',') || accounts.FindIndex(account => account.name == accountName) != -1)
        {
            Console.Write("Username: ");
            accountName = Console.ReadLine();
        }

        while (pin <= 0 || pin > 99999)
        {
            Console.Write("5 number Pin: ");
            Int32.TryParse(Console.ReadLine(), out pin);
        }

        accounts.Add((accountName, pin, 0, []));
        accountIndex = accounts.Count - 1;
    } 
}

int inputNum;
// Main Loop
do
{
    Console.Clear();
    Console.WriteLine($"Welcome, {accounts[accountIndex].name}");
    Console.Write(@"
    1. Check Balance
    2. Withdraw
    3. Deposit
    4. Display last 5 transactions
    5. Quick Withdraw $40
    6. Quick Withdraw $100
    Escape: Save changes and Leave program
    
    Enter a number to begin command: ");
    input = Console.ReadKey();
    Console.WriteLine();
    
    Int32.TryParse(input.KeyChar.ToString(), out inputNum);
    switch (inputNum)
    {
        case 1: // Check Balance
            Console.WriteLine("Selected: Check Balance");
            Console.WriteLine($"You have a current balance of {accounts[accountIndex].balance:C}");
            break;
        case 2: // Withdraw
            Console.WriteLine("Selected: Withdraw");
            break;
        case 3:
            // code here
            break;
        case 4:
            // code here
            break;
        case 5:
            // code here
            break;
        case 6:
            // code here
            break;
        default:
            Console.WriteLine("Invalid Command Entered");
            break;
    }

    Console.Write("Press any key to continue: ");
    Console.ReadKey(true);
}
while (input.Key != ConsoleKey.Escape);


FileHandling.Write("Accounts.txt", accounts);

bool ValidatePin(int pin, (string name, int pin, decimal balance, List<string> transactions) account)
{
    return pin == account.pin;
}

bool Withdraw(decimal request, ref (string name, int pin, decimal balance, List<string> transactions) account)
{
    // Make sure the transaction is possible
    if (Math.Round(request, 2) != request) {return false;}; // No withdraw smaller than a penny
    if (account.balance < request) {return false;} // Can't go into debt
    if (request < 0) {return false;} // Can't go into debt

    // actual transaction goes here
    account.balance -= request;
    account.transactions.Add($"{DateTime.Now} --- Withdrew ${request:F2}");
    return true;
}

bool Deposit(decimal request, ref (string name, int pin, decimal balance, List<string> transactions) account)
{
    // Make sure the transaction is possible
    if (Math.Round(request, 2) != request) {return false;}; // No withdraw smaller than a penny
    if (request < 0) {return false;} // Can't deposit debt

    // actual transaction goes here
    account.balance += request;
    account.transactions.Add($"{DateTime.Now} --- Deposited ${request:F2}");
    return true;
}
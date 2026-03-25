public static class FileHandling
{
    // Returns a list of tuples containing account data at the given file location.
    public static List<(string name, int pin, decimal balance, List<string> transactions)> Read(string path)
    {
        List<(string name, int pin, decimal balance, List<string> transactions)> accounts = [];

        // If no file is found, create an empty one
        string[] text;
        try {text = File.ReadAllLines(path);}
        catch
        {
            File.Create(path);
            text = File.ReadAllLines(path);
        }

        // Convert file text to tuple data
        foreach (string entry in text)
        {
            string[] account = entry.Split(","); // split entry into individual fields

            string name = account[0]; // Shouldn't need type validation

            int pin = Convert.ToInt32(account[1]);

            decimal balance = Convert.ToDecimal(account[2]);

            List<string> transactions = [.. account[3].Split(':')];
            
            accounts.Add((name, pin, balance, transactions));
        }

        return accounts;
    }

    // Creates a list of accounts and then writes it to the file path
    public static void Write(string path, List<(string name, int pin, decimal balance, List<string> transactions)> accounts)
    {
        List<string> text = [];

        foreach ((string name, int pin, decimal balance, List<string> transactions) account in accounts)
        {
            text.Add($"{account.name},{account.pin},{account.balance},{string.Join(":", account.transactions)}");
        }
        File.WriteAllLines(path, text);
    }
}
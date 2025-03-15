class Expense
{
    public string Description { get; set; }
    public double Amount { get; set; }
    public string Category { get; set; }

    public Expense(string description, double amount, string category)
    {
        Description = description;
        Amount = amount;
        Category = category;
    }
}

class ExpenseManager
{
    private List<Expense> expenses = new List<Expense>();

    public void AddExpense()
    {
        Console.Write("Enter description: ");
        string? description = Console.ReadLine();

        Console.Write("Enter amount: ");
        if (!double.TryParse(Console.ReadLine(), out double amount))
        {
            Console.WriteLine("Invalid amount!");
            return;
        }

        Console.Write("Enter category: ");
        string? category = Console.ReadLine();

        expenses.Add(new Expense(description, amount, category));
        Console.WriteLine("Expense added successfully!\n");
    }

    public void ViewExpenses()
    {
        if (expenses.Count == 0)
        {
            Console.WriteLine("No expenses recorded.\n");
            return;
        }

        Console.WriteLine("All Expenses:");
        foreach (var expense in expenses)
        {
            Console.WriteLine($"{expense.Description} - Rs. {expense.Amount} (Category:{expense.Category})");
        }
        Console.WriteLine();
    }

    public void ExportExpenses(string format)
    {
        string fileName = "ExportedExpence." + format;

        using (StreamWriter writer = new StreamWriter(fileName))
        {
            if (format == "txt")
            {
                foreach (var expense in expenses)
                {
                    writer.WriteLine($"Description: {expense.Description}");
                    writer.WriteLine($"Amount: ${expense.Amount}");
                    writer.WriteLine($"Category: {expense.Category}\n");
                }
            }
            else if (format == "csv")
            {
                writer.WriteLine("Description,Amount,Category");
                foreach (var expense in expenses)
                {
                    writer.WriteLine($"{expense.Description},{expense.Amount},{expense.Category}");
                }
            }
            else if (format == "xlsm")
            {
                writer.WriteLine("Description,Amount,Category");
                foreach (var expense in expenses)
                {
                    writer.WriteLine($"{expense.Description},{expense.Amount},{expense.Category}");
                }
            }
        }
        Console.WriteLine($"Expenses exported to {fileName}\n");
    }

    public void CalculateTotalSpent()
    {
        double total = expenses.Sum(e => e.Amount);
        Console.WriteLine($"Total Amount Spent: {total:C}\n");
    }

    public void ViewExpensesByCategory()
    {
        var categories = expenses.Select(e => e.Category).Distinct().ToList();

        if (categories.Count == 0)
        {
            Console.WriteLine("No categories found.\n");
            return;
        }

        Console.WriteLine("Categories:");
        for (int i = 0; i < categories.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {categories[i]}");
        }

        Console.Write("Select category number: ");
        if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= categories.Count)
        {
            string selectedCategory = categories[choice - 1];
            var filteredExpenses = expenses.Where(e => e.Category == selectedCategory);

            Console.WriteLine($"Expenses in {selectedCategory}:");
            foreach (var expense in filteredExpenses)
            {
                Console.WriteLine($"{expense.Description} - {expense.Amount:C}");
            }
            double totalSpent = filteredExpenses.Sum(e => e.Amount);
            Console.WriteLine($"Total Spent in {selectedCategory}: {totalSpent:C}\n");
        }
        else
        {
            Console.WriteLine("Invalid selection!\n");
        }
    }
}
class Program
{
    static void Main()
    {
        ExpenseManager manager = new ExpenseManager();
        while (true)
        {
            Console.WriteLine("Expense Tracker Menu:");
            Console.WriteLine("1. Add Expense");
            Console.WriteLine("2. View All Expenses");
            Console.WriteLine("3. Export Expenses");
            Console.WriteLine("4. Check Total Spent");
            Console.WriteLine("5. View Expenses by Category");
            Console.WriteLine("6. Exit");

            Console.Write("Choose an option: ");
            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    manager.AddExpense();
                    break;
                case "2":
                    manager.ViewExpenses();
                    break;
                case "3":
                    manager.ViewExpensesByCategory();
                    break;
                case "4":
                    manager.CalculateTotalSpent();
                    break;
                case "5":
                    Console.Write("Enter export format (txt/csv/xlsm): ");
                    string format = Console.ReadLine().ToLower();
                    if (format == "txt" || format == "csv" || format == "xlsm" || format == "xls")
                        manager.ExportExpenses(format);
                    else
                        Console.WriteLine("Invalid format!\n");
                    break;
                case "6":
                    Console.WriteLine("Exiting... Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid choice! Try again.\n");
                    break;
            }
        }
    }
}



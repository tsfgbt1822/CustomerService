using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions; //for validation

// Customer Class
public class Customer
{
    //customer fields
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string ID { get; set; } //using phone number

    //constructor
    public Customer(string firstName, string lastName, string email, string id)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        ID = id;
    }

    //returns string with customer information
    public string GetDisplayText()
    {
        return $"{FirstName} {LastName} - Email: {Email}, ID: {ID}";
    }
}

// CustomerList Class
public class CustomerList
{
    //list of customers
    private List<Customer> customers = new List<Customer>();

    //get count of customers
    public int Count => customers.Count;

    //delegate
    public delegate void ChangeHandler(CustomerList customers);
    //event is triggered when customer list changes
    public event ChangeHandler? Changed;

    //adds customer to list
    public void Add(Customer customer)
    {
        customers.Add(customer);
        Changed?.Invoke(this);
    }

    //removes customer from list
    public void Remove(Customer customer)
    {
        customers.Remove(customer);
        Changed?.Invoke(this);
    }

    // Allows iteration using foreach
    public IEnumerator<Customer> GetEnumerator()
    {
        return customers.GetEnumerator();
    }
}

// Main Program
class Program
{
    //create instance of customer list
    static CustomerList customerList = new CustomerList();

    static void Main()
    {
        //subscribes displaycustomers method to changed event
        customerList.Changed += DisplayCustomers;

        while (true)
        {
            //display choices and read input
            Console.WriteLine("\nCustomer Management System");
            Console.WriteLine("1. Add a new customer");
            Console.WriteLine("2. Delete a customer");
            Console.WriteLine("3. Exit");
            Console.Write("Select an option: ");
            string choice = Console.ReadLine();

            //trigger correct action for each choice
            switch (choice)
            {
                case "1":
                    AddCustomer();
                    break;
                case "2":
                    RemoveCustomer();
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    //display the list of customers
    static void DisplayCustomers(CustomerList list)
    {
        Console.WriteLine("\nCurrent Customers (" + list.Count + ") :");
        foreach (var customer in list)
        {
            Console.WriteLine(customer.GetDisplayText());
        }
    }

    //adds a customer
    static void AddCustomer()
    {
        //read information
        Console.Write("Enter First Name: ");
        string firstName = Console.ReadLine();
        Console.Write("Enter Last Name: ");
        string lastName = Console.ReadLine();
        Console.Write("Enter Email: ");
        string email = Console.ReadLine();
        Console.Write("Enter ID (Phone Number): ");
        string id = Console.ReadLine();

        //check validation
        if (!ValidateCustomer(firstName, lastName, email, id))
            return;

        //add if validation passed
        customerList.Add(new Customer(firstName, lastName, email, id));
    }

    //remove a customer
    static void RemoveCustomer()
    {
        //retrieve id for removal
        Console.Write("Enter Customer ID to remove: ");
        string id = Console.ReadLine();

        //search for customer and remove if not found
        foreach (Customer customer in customerList)
        {
            if (customer.ID == id)
            {
                customerList.Remove(customer);
                Console.WriteLine("\nCustomer removed successfully.");
                return;
            }
        }

        //otherwise tell user customer was not found
        Console.WriteLine("\nCustomer not found.");
    }

    //validate input
    static bool ValidateCustomer(string firstName, string lastName, string email, string id)
    {
        //if name is too long, return false
        if (firstName.Length > 25 || lastName.Length > 25)
        {
            Console.WriteLine("Error: customer name too long");
            return false;
        }

        //if email has a username ([^\s@]+) and at least one @
        //and has valid "domain" ([^\s@]+) and a . and a domain sfter ([^\s@]+)
        if (!Regex.IsMatch(email, @"^[^\s@]+@[^\s@]+\.[^\s@]+$"))
        {
            Console.WriteLine("Error: customer email not valid input");
            return false;
        }

        //make sure phone number is 10 digits
        if (!Regex.IsMatch(id, @"^\d{10}$"))
        {
            Console.WriteLine("Error: customer ID not valid input");
            return false;
        }
            
        //check for duplicate ID
        foreach (Customer customer in customerList)
        {
            if (customer.ID == id)
            {
                Console.WriteLine("Error: customer ID already exists");
                return false;
            }
        }

        return true;
    }
}

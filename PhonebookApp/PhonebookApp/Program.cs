using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PhonebookApp
{
    class Program
    {
        private const string FilePath = @"/Users/account2/Documents/school/school/School/PhonebookApp/contacts.csv";
        private static Dictionary<string, string> contacts = new Dictionary<string, string>();

        static void Main(string[] args)
        {
            LoadContacts();

            while (true)
            {
                Console.WriteLine("\nPhonebook Options:");
                Console.WriteLine("1. Add Contact");
                Console.WriteLine("2. Search Contact");
                Console.WriteLine("3. Delete Contact");
                Console.WriteLine("4. List All Contacts");
                Console.WriteLine("5. Exit");
                Console.Write("Enter your choice: ");

                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        AddContact();
                        break;
                    case 2:
                        SearchContact();
                        break;
                    case 3:
                        DeleteContact();
                        break;
                    case 4:
                        ListAllContacts();
                        break;
                    case 5:
                        SaveContacts();
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        static void LoadContacts()
        {
            if (File.Exists(FilePath))
            {
                var lines = File.ReadAllLines(FilePath);
                foreach (var line in lines)
                {
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        var parts = line.Split(',');
                        if (parts.Length >= 2)
                        {
                            contacts[parts[0]] = parts[1];
                        }
                    }
                }
            }
        }


        static void SaveContacts()
        {
            var lines = contacts.Select(kvp => $"{kvp.Key},{kvp.Value}").ToArray();
            File.WriteAllLines(FilePath, lines);
        }

        static void AddContact()
        {
            Console.Write("Enter Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Phone Number: ");
            string phoneNumber = Console.ReadLine();

            if (contacts.ContainsKey(name))
            {
                Console.WriteLine("Contact already exists. Overwriting...");
            }
            contacts[name] = phoneNumber;

            SaveContacts();
        }

        static void SearchContact()
        {
            Console.Write("Enter Name: ");
            string name = Console.ReadLine();

            if (contacts.TryGetValue(name, out string phoneNumber))
            {
                Console.WriteLine($"Found: {name} -> {phoneNumber}");
            }
            else
            {
                Console.WriteLine("Contact not found.");
            }
        }

        static void DeleteContact()
        {
            Console.Write("Enter Name: ");
            string name = Console.ReadLine();

            if (contacts.Remove(name))
            {
                Console.WriteLine("Contact removed.");
                SaveContacts();
            }
            else
            {
                Console.WriteLine("Contact not found.");
            }
        }

        static void ListAllContacts()
        {
            foreach (var contact in contacts)
            {
                Console.WriteLine($"{contact.Key} -> {contact.Value}");
            }
        }
    }
}

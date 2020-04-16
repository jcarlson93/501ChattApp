using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JsonTesting
{
    public class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, User> users = new Dictionary<string, User>();
            User u1 = new User("Jake", "1234");
            u1.Online = true;
            User u2 = new User("Katie", "5678");
            User u3 = new User("Dude", "1014");
            u1.AddUserToContacts(u2);
            u1.AddUserToContacts(u3);
            u2.AddUserToContacts(u1);
            u3.AddUserToContacts(u2);

            users.Add("Jake", u1);
            users.Add("Katie", u2);
            users.Add(u3.Name, u3);

            Console.Write("Enter your name: ");
            string name = Console.ReadLine();
            Console.WriteLine();
            if (users.ContainsKey(name))
            {
                Console.Write("That name is already taken, please choose another: ");
                name = Console.ReadLine();
                Console.WriteLine();
            }
            Console.Write("Please enter a password: ");
            string pass = Console.ReadLine();

            User u4 = new User(name, pass);

            users.Add(u4.Name, u4);

            foreach (KeyValuePair<string, User> u in users)
            {
                User temp = u.Value;
                Console.WriteLine($"{temp.Name} has {temp.Contacts.Count} contacts.");
                Console.Write($"{temp.Name}'s contacts: ");
                foreach (User uc in u.Value.Contacts)
                {
                    Console.Write($"{uc.Name} ");
                }
                Console.WriteLine();
            }

            u1.RemoveUserFromContacts(u3);

            string json = JsonConvert.SerializeObject(users, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            Console.WriteLine(json);

            Dictionary<string, User> users2 = JsonConvert.DeserializeObject<Dictionary<string, User>>(json, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            foreach (KeyValuePair<string, User> u in users2)
            {
                User temp = u.Value;
                Console.WriteLine($"{temp.Name} has {temp.Contacts.Count} contacts.");
                Console.Write($"{temp.Name}'s contacts: ");
                foreach (User uc in u.Value.Contacts)
                {
                    Console.Write($"{uc.Name} ");
                }
                Console.WriteLine();
            }
            Console.ReadLine();

        }
    }

    public class User
    {
        private string name;
        private string password;
        private bool online = false;
        private List<User> contacts = new List<User>();

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public string Password
        {
            get
            {
                return password;
            }
            protected set
            {
                password = value;
            }
        }

        public bool Online
        {
            get
            {
                return online;
            }
            set
            {
                online = value;
            }
        }

        public List<User> Contacts
        {
            get
            {
                return contacts;
            }
        }

        public User(string n, string p)
        {
            name = n;
            password = p;
        }

        public void AddUserToContacts(User user)
        {
            contacts.Add(user);
        }

        public void RemoveUserFromContacts(User user)
        {
            contacts.Remove(user);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _TestSW_2
{

    class Person
    {
        public string Name;
        public string Surname;
        public DateTime DOB;
        public string Address;
    }

    static class Storage
    {

        public static List<Person> Records = new List<Person>();

        public static void AddPerson(string Name, string Surname, DateTime DOB, string Address)
        {
            Person person = new Person();
            person.Name = Name;
            person.Surname = Surname;
            person.DOB = DOB;
            person.Address = Address;
            Records.Add(person);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redis
{
    internal class Employee
    {
            public string Id { get; set; } = "1";
            public string FirstName { get; set; } = string.Empty;
            public string LastName { get; set; } = string.Empty;
            public string HireDate { get; set; } = string.Empty;

            public Employee()
            {
            }
            public Employee(string id, string firstName, string lastName, string hireDate)
            {
                this.Id = id;
                this.FirstName = firstName;
                this.LastName = lastName;
                this.HireDate = hireDate;
            }
            public override string ToString()
            {
                return $"ID: {Id} \n First Name: {FirstName} \n Last Name : {LastName} \n Hire Year: {HireDate} \n";
            }

            public string editedEmployee()
            {
                return $"{Id}, {FirstName}, {LastName}, {HireDate} ";
            }
        }
    }
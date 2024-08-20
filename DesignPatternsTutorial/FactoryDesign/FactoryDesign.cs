using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsTutorial.FactoryDesign
{
    // 1- permanent
    // 2- contract
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int employee_type { get; set; }
        public int salary { get; set; }
        public int Hourly_Pay { get; set; }
        public int Bonus { get; set; }
    }

    public interface IEmployee
    {
        public int GetSalary();
        public int GetBonus();
    }

    public class PermanentEmployee : IEmployee
    {
        public int GetSalary()
        {
            return 20;
        }

        public int GetBonus()
        {
            return 10;
        }
    }

    public class ContractEmployee : IEmployee
    {
        public int GetSalary()
        {
            return 10;
        }

        public int GetBonus()
        {
            return 5;
        }
    }

    public class EmployeeFactory
    {
        public IEmployee getEmployee(int type)
        {
            IEmployee employee = null;
            if(type == 1)
            {
                employee = new PermanentEmployee();
            }
            else if(type == 2)
            {
                employee = new ContractEmployee();
            }

            return employee;
        }

    }

    public class ClientCode
    {
        public void someMethod(Employee emp)
        {
            EmployeeFactory factory = new EmployeeFactory();
            IEmployee employee = factory.getEmployee(emp.employee_type);
            employee.GetBonus();
            employee.GetSalary();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsTutorial.Test
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int employee_type { get; set; }
        public int salary { get; set; }
        public int Hourly_Pay { get; set; }
        public int Bonus { get; set; }
        public int HouseAllowance { get; set; }
        public int MedicalAllowance { get; set; }
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

        public int GetHouseAllowance()
        {
            return 150;
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

        public int GetMedicalAllowance()
        {
            return 100;
        }
    }

    public class EmployeeFactory
    {
        public BaseEmployeeFactory SomeMethod(Employee emp)
        {
            BaseEmployeeFactory returnValue = null;
            if(emp.employee_type == 1)
            {
                returnValue = new PermanentEmployeeFactory(emp);
            }
            else if(emp.employee_type == 2)
            {
                returnValue = new ContractEmployeeFactory(emp);
            }

            return returnValue;
        }
    }

    public abstract class BaseEmployeeFactory
    {
        protected Employee _emp;
        public BaseEmployeeFactory(Employee emp)
        {
            _emp = emp;
        }

        public Employee ApplySalary()
        {
            IEmployee employee = this.Create();
            _emp.Hourly_Pay = employee.GetSalary();
            _emp.Bonus = employee.GetBonus();
            return _emp;
        }
        public abstract IEmployee Create();
    }

    public class PermanentEmployeeFactory : BaseEmployeeFactory
    {
        public PermanentEmployeeFactory(Employee emp) : base(emp)
        {

        }
        public override IEmployee Create()
        {
            PermanentEmployee permanentEmployee = new PermanentEmployee();
            _emp.HouseAllowance = permanentEmployee.GetHouseAllowance();
            return permanentEmployee;
        }
    }

    public class ContractEmployeeFactory : BaseEmployeeFactory
    {
        public ContractEmployeeFactory(Employee emp) : base(emp)
        {

        }
        public override IEmployee Create()
        {
            ContractEmployee contractEmployee = new ContractEmployee();
            _emp.HouseAllowance = contractEmployee.GetMedicalAllowance();
            return contractEmployee;
        }
    }

    public class ClientCode
    {
        public void SomeMethod1(Employee emp)
        {
            emp.employee_type = 1;
            BaseEmployeeFactory baseEmployee = new EmployeeFactory().SomeMethod(emp);
            baseEmployee.ApplySalary();
        }
    }
}

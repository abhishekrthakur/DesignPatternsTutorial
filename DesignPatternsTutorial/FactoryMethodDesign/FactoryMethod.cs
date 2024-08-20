using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsTutorial.FactoryMethodDesign
{
    //1 - permanant
    //2 - contract
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
        public int GetBonus();
        public int GetHourlyPay();
    }

    public class PermanentEmployee : IEmployee
    {
        public int GetBonus()
        {
            return 10;
        }

        public int GetHourlyPay()
        {
            return 20;
        }

        public int GetHouseAllowance()
        {
            return 150;
        }
    }

    public class ContractEmployee : IEmployee
    {
        public int GetBonus()
        {
            return 5;
        }

        public int GetHourlyPay()
        {
            return 10;
        }

        public int GetMedicalAllowance()
        {
            return 100;
        }
    }

    public class EmployeeFactory
    {
        public BaseEmployeeFactory GetEmployee(Employee emp)
        {
            BaseEmployeeFactory instance = null;
            if (emp.employee_type == 1)
            {
                instance = new PermanentEmployeeFactory(emp);
            }
            else if (emp.employee_type == 2)
            {
                instance = new ContractEmployeeFactory(emp);
            }

            return instance;
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
            IEmployee emp = this.Create();
            _emp.Hourly_Pay = emp.GetHourlyPay();
            _emp.Bonus = emp.GetBonus();
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
}

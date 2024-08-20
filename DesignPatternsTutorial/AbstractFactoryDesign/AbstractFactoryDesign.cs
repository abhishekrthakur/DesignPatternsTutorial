using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DesignPatternsTutorial.AbstractFactoryDesign.AbstractFactoryDesign.Enumerations;

namespace DesignPatternsTutorial.AbstractFactoryDesign
{
    public class AbstractFactoryDesign
    {
        //1 - permanant
        //2 - contract
        public class Employee
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int employee_type { get; set; }
            public string JobDescription { get; set; }
            public int salary { get; set; }
            public int Hourly_Pay { get; set; }
            public int Bonus { get; set; }
            public int HouseAllowance { get; set; }
            public int MedicalAllowance { get; set; }
            public string ComputerDetails { get; set; }
        }

        public class Enumerations
        {
            public enum ComputerTypes
            {
                LAPTOP,
                DESKTOP
            }

            public enum Brands
            {
                MAC,
                DELL
            }

            public enum Processors
            {
                I3,
                I5,
                I7
            }
        }

        public interface IBrand
        {
            string GetBrand();
        }

        public interface ISystemType
        {
            string GetSystemType();
        }

        public interface IProcessor
        {
            string GetProcessor();
        }

        public class Mac : IBrand
        {
            public string GetBrand()
            {
                return Brands.MAC.ToString();
            }
        }

        public class Dell : IBrand
        {
            public string GetBrand()
            {
                return Brands.DELL.ToString();
            }
        }

        public class I3 : IProcessor
        {
            public string GetProcessor()
            {
                return Processors.I3.ToString();
            }
        }

        public class I5 : IProcessor
        {
            public string GetProcessor()
            {
                return Processors.I5.ToString();
            }
        }

        public class I7 : IProcessor
        {
            public string GetProcessor()
            {
                return Processors.I7.ToString();
            }
        }

        public class Laptop : ISystemType
        {
            public string GetSystemType()
            {
                return ComputerTypes.LAPTOP.ToString();
            }
        }

        public class Desktop : ISystemType
        {
            public string GetSystemType()
            {
                return ComputerTypes.DESKTOP.ToString();
            }
        }

        public interface IComputerFactory
        {
            IProcessor Processor();
            IBrand Brand();
            ISystemType SystemType();
        }

        public class MacFactory : IComputerFactory
        {
            public IProcessor Processor()
            {
                return new I7();
            }

            public IBrand Brand()
            {
                return new Mac();
            }

            public virtual ISystemType SystemType()
            {
                return new Desktop();
            }
        }

        public class MacLaptopFactory : MacFactory
        {
            public override ISystemType SystemType()
            {
                return new Laptop();
            }
        }

        public class DellFactory : IComputerFactory
        {
            public IProcessor Processor()
            {
                return new I5();
            }

            public IBrand Brand()
            {
                return new Dell();
            }

            public virtual ISystemType SystemType()
            {
                return new Desktop();
            }
        }

        public class DellLaptopFactory : DellFactory
        {
            public override ISystemType SystemType()
            {
                return new Laptop();
            }
        }

        public class EmployeeFactory
        {
            public IComputerFactory Create(Employee emp)
            {
                IComputerFactory returnValue = null;
                if(emp.employee_type == 1)
                {
                    if(emp.JobDescription.ToLower() == "manager")
                    {
                        returnValue = new MacLaptopFactory();
                    }
                    else
                    {
                        returnValue = new MacFactory();
                    }
                }
                else if(emp.employee_type == 2)
                {
                    if(emp.JobDescription.ToLower() == "manager")
                    {
                        returnValue = new DellLaptopFactory();
                    }
                    else
                    {
                        returnValue = new DellFactory();
                    }
                }

                return returnValue;
            }
        }

        public class EmployeeManagerFactory
        {
            public IComputerFactory _computerFactory;
            public EmployeeManagerFactory(IComputerFactory computerFactory)
            {
                _computerFactory = computerFactory;
            }

            public string SystemDetails()
            {
                IBrand brand = _computerFactory.Brand();
                IProcessor processor = _computerFactory.Processor();
                ISystemType systemType = _computerFactory.SystemType();

                string returnValue = string.Format("{0} {1} {2}", brand.GetBrand(),
                    processor.GetProcessor(), systemType.GetSystemType());

                return returnValue;
            }
        }

        public class ClientCode
        {
            public void Create(Employee emp)
            {
                emp.JobDescription = "manager";
                emp.employee_type = 1;
                IComputerFactory factory = new EmployeeFactory().Create(emp);
                EmployeeManagerFactory managerFactory = new EmployeeManagerFactory(factory);
                emp.ComputerDetails = managerFactory.SystemDetails();
            }
        }
    }
}

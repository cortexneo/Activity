using System;
using System.Collections.Generic;
using Day3Database.Models;
using Day3Database.Repositories;

namespace Day3Database
{
    class Program
    {
        static void Main(string[] args)
        {
            var registrar = CreateDepartment("Registrar", true);
            var emp = CreateEmployee("Ron", "Agravante", "Natividad", registrar.DepartmentID, null);
            CreateEmployee("Renz", "Agravante", "Natividad", registrar.DepartmentID, null);
            CreateEmployee("Winkle", "Agravante", "Natividad", registrar.DepartmentID, null);
            var employee = RetrieveEmployee(emp.EmployeeID);
            UpdateEmployee(employee);
            var allEmployee = RetrieveAllEmployees();
            allEmployee.ForEach((d) => DeleteEmployee(d.EmployeeID));
            Console.ReadLine();
        }

        //Department
        /*
        var dept = CreateDepartment("Registrar", true);
        CreateDepartment("Accounting", false);
        CreateDepartment("Cashier", true);
        var department = RetrieveDepartment(dept.DepartmentID);
        UpdateDepartment(department);
        var allDepartment = RetrieveAllDepartments();
        allDepartment.ForEach((d) => DeleteDepartment(d.DepartmentID));
        Console.ReadLine();
        */

        //Applicant
        /*
        var ron = CreateApplicant("Ron", "Agravante", "Natividad", DateTime.Parse("1993-07-02"));
        CreateApplicant("Ron", "Agravante", "Natividad", DateTime.Parse("1993-07-02"));
        CreateApplicant("Ron", "Agravante", "Natividad", DateTime.Parse("1993-07-02"));

        var applicant = RetrieveApplicant(ron.ApplicantId);
        UpdateApplicant(applicant);
        var allApplicants = RetrieveAllApplicants();
        allApplicants.ForEach((a) => DeleteApplicant(a.ApplicantId));
        Console.ReadLine();
        */

        private static List<Employee> RetrieveAllEmployees()
        {
            var repository = new EmployeeRepository();
            return repository.Retrieve();
        }

        private static Employee RetrieveEmployee(Guid employeeID)
        {
            var repository = new EmployeeRepository();
            return repository.Retrieve(employeeID);
        }

        static Employee CreateEmployee(string firstName, string middleName, string lastName, Guid departmentID, DateTime? hireDate)
        {
            var employee = new Employee
            {
                EmployeeID = Guid.NewGuid(),
                FirstName = firstName,
                MiddleName = middleName,
                LastName = lastName,
                DepartmentID = departmentID,
                HireDate = hireDate

            };
            var repository = new EmployeeRepository();
            var newEmployee = repository.Create(employee);
            return newEmployee;
        }
        static void DeleteEmployee(Guid employeeID)
        {
            var repository = new EmployeeRepository();
            repository.Delete(employeeID);
        }

        private static void UpdateEmployee(Employee employee)
        {
            employee.FirstName = "Test";
            employee.MiddleName = "Test";
            employee.LastName = "Test";
            employee.DepartmentID = new Guid("0fa7b858-9640-415b-8e7b-6a6cbdbfe934");
            employee.HireDate = DateTime.Now;
            var repository = new EmployeeRepository();
            var newEmployee = repository.Update(employee);
        }

        //Department

        private static List<Department> RetrieveAllDepartments()
        {
            var repository = new DepartmentRepository();
            return repository.Retrieve();
        }

        private static Department RetrieveDepartment(Guid departmentID)
        {
            var repository = new DepartmentRepository();
            return repository.Retrieve(departmentID);
        }

        static Department CreateDepartment(string departmentName, bool isActive)
        {
            var department = new Department
            {
                DepartmentID = Guid.NewGuid(),
                DepartmentName = departmentName,
                IsActive = isActive
            };
            var repository = new DepartmentRepository();
            var newDepartment = repository.Create(department);
            return newDepartment;
        }
        static void DeleteDepartment(Guid departmentID)
        {
            var repository = new DepartmentRepository();
            repository.Delete(departmentID);
        }

        private static void UpdateDepartment(Department department)
        {
            department.DepartmentName = "Accounting";
            department.IsActive = false;
            var repository = new DepartmentRepository();
            var newDepartment = repository.Update(department);
        }


        //Applicants
        /*
        private static List<Applicant> RetrieveAllApplicants()
        {
            var repository = new ApplicantRepository();
            return repository.Retrieve();
        }

        private static Applicant RetrieveApplicant(Guid applicantId)
        {
            var repository = new ApplicantRepository();
            return repository.Retrieve(applicantId);
        }

        static Applicant CreateApplicant(string firstName, string lastName, string middleName, DateTime birthDate)
        {
            var applicant = new Applicant
            {
                ApplicantId = Guid.NewGuid(),
                FirstName = firstName,
                MiddleName = middleName,
                LastName = lastName,
                BirthDate = birthDate
            };
            var repository = new ApplicantRepository();
            var newApplicant = repository.Create(applicant);
            return newApplicant;
        }
        static void DeleteApplicant(Guid applicantID)
        {
            var repository = new ApplicantRepository();
            repository.Delete(applicantID);
        }

        private static void UpdateApplicant(Applicant applicant)
        {
            applicant.FirstName = "Alfred";
            var repository = new ApplicantRepository();
            var newApplicant = repository.Update(applicant);
        }
        */
    }
}


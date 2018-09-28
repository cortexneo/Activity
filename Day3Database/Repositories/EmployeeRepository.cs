using Day3Database.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Day3Database.Repositories
{
    public class EmployeeRepository : RepositoryBase<Employee>
    {
        private readonly string insertStatement = @"Insert into [Employee] (EmployeeID, FirstName, MiddleName, LastName, DepartmentID )
                                                    VALUES
                                                ( @employeeID, @firstName, @middleName, @lastName, @departmentID)";
        private readonly string deleteStatement = @"Delete from [Employee] Where EmployeeID = @employeeID";

        private readonly string updateStatement = @"Update [Employee] SET
                                                    FirstName = @firstName, 
                                                    MiddleName = @middleName,
                                                    LastName = @lastName,
                                                    DepartmentID = @departmentID
                                                    Where
                                                    EmployeeID = @employeeID";

        private readonly string retrieveStatement = @"Select EmployeeID, FirstName, MiddleName, LastName, DEPARTMENT.DepartmentID
                                                    FROM [Employee] INNER JOIN DEPARTMENT ON EMPLOYEE.DepartmentID = DEPARTMENT.DepartmentID ";
        private readonly string retrieveFilter = @"Where EmployeeID = @employeeID";

        public EmployeeRepository()
        {
            base.InsertStatement = this.insertStatement;
            base.DeleteStatement = this.deleteStatement;
            base.UpdateStatement = this.updateStatement;
            base.RetrieveStatement = this.retrieveStatement + this.retrieveFilter;
            base.RetrieveAllStatement = this.retrieveStatement;
        }

        protected override void LoadDeleteParameters(SqlCommand command, Guid id)
        {
            command.Parameters.Add("@employeeID", SqlDbType.UniqueIdentifier).Value = id;
        }

        protected override Employee LoadEntity(SqlDataReader reader)
        {
            var employee = new Employee();
            employee.EmployeeID = reader.GetGuid(0);
            employee.FirstName = reader.GetString(1);
            employee.MiddleName = reader.GetString(2);
            employee.LastName = reader.GetString(3);
            employee.DepartmentID = reader.GetGuid(4);
            return employee;
        }

        protected override void LoadInsertParameters(SqlCommand command, Employee newEntity)
        {
            command.Parameters.Add("@employeeID", SqlDbType.UniqueIdentifier)
                        .Value = newEntity.EmployeeID;
            command.Parameters.Add("@firstName", SqlDbType.NVarChar, 50)
                            .Value = newEntity.FirstName;
            command.Parameters.Add("@middleName", SqlDbType.NVarChar, 50)
                            .Value = newEntity.MiddleName;
            command.Parameters.Add("@lastName", SqlDbType.NVarChar, 50)
                            .Value = newEntity.LastName;
            command.Parameters.Add("@departmentID", SqlDbType.UniqueIdentifier)
                            .Value = newEntity.DepartmentID;
        }

        protected override void LoadRetrieveParameter(SqlCommand command, Guid id)
        {
            command.Parameters.Add("@employeeID", SqlDbType.UniqueIdentifier).Value = id;
        }

        protected override void LoadUpdateParameters(SqlCommand command, Employee entity)
        {
            command.Parameters.Add("@employeeID", SqlDbType.UniqueIdentifier)
            .Value = entity.EmployeeID;
            command.Parameters.Add("@firstName", SqlDbType.NVarChar, 50)
                            .Value = entity.FirstName;
            command.Parameters.Add("@middleName", SqlDbType.NVarChar, 50)
                            .Value = entity.MiddleName;
            command.Parameters.Add("@lastName", SqlDbType.NVarChar, 50)
                            .Value = entity.LastName;
            command.Parameters.Add("@departmentID", SqlDbType.UniqueIdentifier)
                            .Value = entity.DepartmentID;
        }
    }
}

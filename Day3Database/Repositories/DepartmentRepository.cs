using Day3Database.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Day3Database.Repositories
{
    public class DepartmentRepository : RepositoryBase<Department>
    {
        private readonly string insertStatement = @"Insert into [Department] (DepartmentID, DepartmentName, IsActive )
                                                    VALUES
                                                ( @departmentID, @departmentName, @isActive)";
        private readonly string deleteStatement = @"Delete from [Department] Where DepartmentID = @departmentID";

        private readonly string updateStatement = @"Update [Department] SET
                                                    DepartmentName = @departmentName, 
                                                    IsActive = @isActive 
                                                    Where
                                                    DepartmentID = @departmentID";

        private readonly string retrieveStatement = @"Select DepartmentID, DepartmentName, IsActive
                                                    from [Department] ";
        private readonly string retrieveFilter = @"Where DepartmentID = @departmentID";

        public DepartmentRepository()
        {
            base.InsertStatement = this.insertStatement;
            base.DeleteStatement = this.deleteStatement;
            base.UpdateStatement = this.updateStatement;
            base.RetrieveStatement = this.retrieveStatement + this.retrieveFilter;
            base.RetrieveAllStatement = this.retrieveStatement;
        }
        protected override void LoadInsertParameters(SqlCommand command, Department newDepartment)
        {
            command.Parameters.Add("@departmentID", SqlDbType.UniqueIdentifier)
                                    .Value = newDepartment.DepartmentID;
            command.Parameters.Add("@departmentName", SqlDbType.NVarChar, 50)
                            .Value = newDepartment.DepartmentName;
            command.Parameters.Add("@isActive", SqlDbType.NVarChar, 50)
                            .Value = newDepartment.IsActive;
        }
        protected override void LoadDeleteParameters(SqlCommand command, Guid id)
        {
            command.Parameters.Add("@departmentID", SqlDbType.UniqueIdentifier).Value = id;
        }

        protected override void LoadUpdateParameters(SqlCommand command, Department department)
        {
            command.Parameters.Add("@departmentID", SqlDbType.UniqueIdentifier)
                                     .Value = department.DepartmentID;
            command.Parameters.Add("@departmentName", SqlDbType.NVarChar, 50)
                            .Value = department.DepartmentName;
            command.Parameters.Add("@isActive", SqlDbType.Bit)
                            .Value = department.IsActive;
        }

        protected override Department LoadEntity(SqlDataReader reader)
        {
            var department = new Department();
            department.DepartmentID = reader.GetGuid(0);
            department.DepartmentName = reader.GetString(1);
            department.IsActive = reader.GetBoolean(2);
            return department;
        }

        protected override void LoadRetrieveParameter(SqlCommand command, Guid id)
        {
            command.Parameters.Add("@departmentID", SqlDbType.UniqueIdentifier).Value = id;
        }
    }
}

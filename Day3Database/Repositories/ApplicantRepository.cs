using Day3Database.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Day3Database.Repositories
{
    public class ApplicantRepository : RepositoryBase<Applicant>
    {


        protected string insertStatement = @"Insert into [Applicant] (ApplicantID, FirstName, MiddleName, LastName, BirthDate )
                                                    VALUES
                            ( @applicantID, @firstName, @lastName, @middleName, @birthDate)";

        private readonly string deleteStatement = @"Delete from [Applicant] Where ApplicantID = @ApplicantID";

        private readonly string updateStatement = @"Update [Applicant] SET
                                                    FirstName = @firstName, 
                                                    MiddleName = @middleName, 
                                                    LastName = @lastName, 
                                                    BirthDate = @birthDate
                                                    Where
                                                    ApplicantID = @applicantID";

        private readonly string retrieveStatement = @"Select ApplicantID, FirstName, MiddleName, LastName, BirthDate
                                                    from [Applicant] ";
        private readonly string retrieveFilter = @"Where ApplicantID = @applicantID";

        public ApplicantRepository()
        {
            base.InsertStatement = this.insertStatement;
            base.DeleteStatement = this.deleteStatement;
            base.UpdateStatement = this.updateStatement;
            base.RetrieveStatement = this.retrieveStatement + this.retrieveFilter;
            base.RetrieveAllStatement = this.retrieveStatement;
        }

        protected override void LoadInsertParameters(SqlCommand command, Applicant newApplicant)
        {
            command.Parameters.Add("@applicantID", SqlDbType.UniqueIdentifier)
                                    .Value = newApplicant.ApplicantId;
            command.Parameters.Add("@firstName", SqlDbType.NVarChar, 50)
                            .Value = newApplicant.FirstName;
            command.Parameters.Add("@lastName", SqlDbType.NVarChar, 50)
                            .Value = newApplicant.LastName;
            command.Parameters.Add("@middleName", SqlDbType.NVarChar, 50)
                            .Value = newApplicant.MiddleName;
            command.Parameters.Add("@birthDate", SqlDbType.DateTime)
                            .Value = newApplicant.BirthDate;
        }

        protected override void LoadDeleteParameters(SqlCommand command, Guid id)
        {
            command.Parameters.Add("@applicantID", SqlDbType.UniqueIdentifier).Value = id;
        }


        protected override void LoadUpdateParameters(SqlCommand command, Applicant applicant)
        {
            command.Parameters.Add("@applicantID", SqlDbType.UniqueIdentifier)
                    .Value = applicant.ApplicantId;
            command.Parameters.Add("@firstName", SqlDbType.NVarChar, 50)
                            .Value = applicant.FirstName;
            command.Parameters.Add("@lastName", SqlDbType.NVarChar, 50)
                            .Value = applicant.LastName;
            command.Parameters.Add("@middleName", SqlDbType.NVarChar, 50)
                            .Value = applicant.MiddleName;
            command.Parameters.Add("@birthDate", SqlDbType.DateTime)
                            .Value = applicant.BirthDate;
        }

        protected override Applicant LoadEntity(SqlDataReader reader)
        {
            var applicant = new Applicant();
            applicant.ApplicantId = reader.GetGuid(0);
            applicant.FirstName = reader.GetString(1);
            applicant.MiddleName = reader.GetString(2);
            applicant.LastName = reader.GetString(3);
            applicant.BirthDate = reader.GetDateTime(4);
            return applicant;
        }
        protected override void LoadRetrieveParameter(SqlCommand command, Guid id)
        {
            command.Parameters.Add("@applicantID", SqlDbType.UniqueIdentifier).Value = id;
        }
    }
}

using C1_Blazor_Demo_DotNet8.Database;
using Dapper;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace C1_Blazor_Demo_DotNet8.Repository
{
    public class DashboardRepository
    {
 
            private readonly IConfiguration _configuration;

            public DashboardRepository(IConfiguration configuration)
            {
                _configuration = configuration;
            }


            public async Task<object> GetReworkdata(string UserID, string BranchID)
            {
                object result = null;

                try
                {
                    var Param = new OracleConfig();

                    Param.Add("p_user_id", OracleDbType.NVarchar2, ParameterDirection.Input, UserID.ToString());
                    Param.Add("p_branch_id", OracleDbType.NVarchar2, ParameterDirection.Input, BranchID.ToString());

                    Param.Add("p_result", OracleDbType.RefCursor, ParameterDirection.Output);

                    var conn = this.GetConnection();
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }

                    if (conn.State == ConnectionState.Open)
                    {
                        var query = "Abs_Auth_Skb.Fetch_Rework_Data";

                        result =  SqlMapper.Query(conn, query, param: Param, commandType: CommandType.StoredProcedure);
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return result;
            }

            public async Task<object> getUsers(string UserID, string BranchID)
            {
                object result = null;
                try
                {
                    var Param = new OracleConfig();

                    Param.Add("p_user_id", OracleDbType.NVarchar2, ParameterDirection.Input, UserID.ToString());
                    Param.Add("p_branch_id", OracleDbType.NVarchar2, ParameterDirection.Input, BranchID.ToString());

                    Param.Add("p_result", OracleDbType.RefCursor, ParameterDirection.Output);

                    var conn = this.GetConnection();
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }

                    if (conn.State == ConnectionState.Open)
                    {
                        var query = "Abs_Auth_Skb.FetchUserDetails";

                        result = SqlMapper.Query(conn, query, param: Param, commandType: CommandType.StoredProcedure);
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return result;

            }
        public async Task<object> AddCustomer(int CustomerID, string CustomerSrtName, string CustomerSalutation, 
                                                string CustomerFirstName, string CustomerMiddleName, string CustomerLastName )
        {
            object result = null;
            var status = string.Empty;
            try
            {
                var Param = new OracleConfig();

                Param.Add("pcustomer_id", OracleDbType.NVarchar2, ParameterDirection.Output, CustomerID);
                Param.Add("pcustomer_short_nm", OracleDbType.NVarchar2, ParameterDirection.Input, CustomerSrtName);
                Param.Add("pcustomer_salutation", OracleDbType.NVarchar2, ParameterDirection.Input, CustomerSalutation);
                Param.Add("pcustomer_first_nm", OracleDbType.NVarchar2, ParameterDirection.Input, CustomerFirstName);
                Param.Add("pcustomer_middle_nm", OracleDbType.NVarchar2, ParameterDirection.Input, CustomerMiddleName);
                Param.Add("pcustomer_last_nm", OracleDbType.NVarchar2, ParameterDirection.Input, CustomerLastName);

                var conn = this.GetConnection();
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                if (conn.State == ConnectionState.Open)
                {
                    var query = "ABS_auth_skb.cor_cus_profile_i";

                    SqlMapper.Query(conn, query, param: Param, commandType: CommandType.StoredProcedure).FirstOrDefault();


                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
        public async Task<object> AddAccounts(int branchId, int accno, int productId, string currId, long? custId,
                                                int GrpId, decimal Balance, string makeBy)
        {
            object result = null;
            var status = string.Empty;
            try
            {
                var Param = new OracleConfig();

                Param.Add("pbranch_id", OracleDbType.NVarchar2, ParameterDirection.Output, branchId);
                Param.Add("paccount_number", OracleDbType.NVarchar2, ParameterDirection.Input, accno);
                Param.Add("pproduct_id", OracleDbType.NVarchar2, ParameterDirection.Input, productId);
                Param.Add("pcurr_id", OracleDbType.NVarchar2, ParameterDirection.Input, currId);
                Param.Add("pcustomer_id", OracleDbType.NVarchar2, ParameterDirection.Input, custId);
                Param.Add("pgroup_id", OracleDbType.NVarchar2, ParameterDirection.Input, GrpId);
                Param.Add("pbalance_lcy", OracleDbType.NVarchar2, ParameterDirection.Input, Balance);
                Param.Add("puser_id", OracleDbType.NVarchar2, ParameterDirection.Input, makeBy);

                var conn = this.GetConnection();
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                if (conn.State == ConnectionState.Open)
                {
                    var query = "ABS_auth_skb.rtl_demand_ac_mast_i";

                    SqlMapper.Query(conn, query, param: Param, commandType: CommandType.StoredProcedure).FirstOrDefault();


                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
        public IDbConnection GetConnection()
            {
                var connectionString = _configuration.GetSection("ConnectionStrings").GetSection("Database").Value;
                var conn = new OracleConnection(connectionString);
                return conn;
            }
        
    }
}

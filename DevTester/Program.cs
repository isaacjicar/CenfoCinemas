using DataAccess.DAO;


public class program
{
    public static void Main(string[] args)
    {
        var sqlOperation = new SqlOperations();

        sqlOperation.ProcedureName = "CRE_USER_PR";

        sqlOperation.AddStringParameter("P_UserCode", "ismael");
        sqlOperation.AddStringParameter("P_Name", "Isaac");
        sqlOperation.AddStringParameter("P_Email", "Newton");
        sqlOperation.AddStringParameter("P_Password", "12345678");
        sqlOperation.AddStringParameter("P_Status", "AC");
        sqlOperation.AddStringParameter("P_BirthDate", DateTime.Now.ToString("yyyy-MM-dd"));

        var sqlDao = SqlDao.GetInstance();  
        sqlDao.ExecuteProcedure(sqlOperation);

    }
}
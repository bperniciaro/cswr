using System.Data;
using System.Data.Common;

/// <summary>
/// Summary description for DataAccess
/// </summary>

namespace BP.CheatSheetWarRoom.DAL
{
  public abstract class DataAccess
  {
    
    //Connection String
    private string _connectionString = "";
    protected string ConnectionString
    {
      get { return _connectionString; }
      set { _connectionString = value; }
    }


    //Execute Non-Query
    protected int ExecuteNonQuery(DbCommand cmd)
    {
        return cmd.ExecuteNonQuery();
    }

    //Execute Reader
    protected IDataReader ExecuteReader(DbCommand cmd)
    {
      return ExecuteReader(cmd, CommandBehavior.Default);
    }

    //Execute Reader /w Behavior
    protected IDataReader ExecuteReader(DbCommand cmd, CommandBehavior behavior)
    {
      return cmd.ExecuteReader(behavior);
    }

    //Execute Scalar
    protected object ExecuteScalar(DbCommand cmd)
    {
      return cmd.ExecuteScalar();
    }


  }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for SqlSheetsProvider
/// </summary>
namespace BP.CheatSheetWarRoom.DAL.SqlClient
{
  public class SqlForumProvider : ForumProvider
  {
    public SqlForumProvider() { }

    public override List<ForumMemberDetails> GetForumMembers()
    {
      List<ForumMemberDetails> allMembers = new List<ForumMemberDetails>();

      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {

        string query = "SELECT * FROM pgd_members";
        SqlCommand selectCommand = new SqlCommand(query, cn);

        cn.Open();
        try
        {

          IDataReader reader = selectCommand.ExecuteReader();

          while (reader.Read())
          {
            int memberID = 0;
            int.TryParse(reader["Mem"].ToString(), out memberID);
            allMembers.Add(new ForumMemberDetails(memberID, reader["Login"].ToString()));
          }
        }
        catch (Exception ex)
        {
          int i = 0;
        }
        cn.Close();

        return allMembers;
      }
    }

    public override int GetMemberID(string userName)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        int memberID = 0;

        SqlParameter userNameParam = new SqlParameter("UserName", userName);
        string query = "SELECT * FROM pgd_members WHERE Login=@UserName";

        SqlCommand selectCommand = new SqlCommand(query, cn);
        selectCommand.Parameters.Add(userNameParam);


        cn.Open();
        try
        {

          IDataReader reader = selectCommand.ExecuteReader(System.Data.CommandBehavior.SingleRow);
          if (reader.Read())
          {
            memberID = int.Parse(reader["Mem"].ToString());
          }
        }
        catch (Exception ex)
        {
          int i = 0;
        }
        cn.Close();

        return memberID;
      }
    }
  }
}
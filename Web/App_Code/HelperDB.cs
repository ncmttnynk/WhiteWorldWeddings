using System;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;

public class HelperDB
{
    private string connStr = string.Empty;

    public HelperDB()
    {
        connStr = ConfigurationManager.ConnectionStrings["WhiteWorldConnection"].ToString();
    }

    public HelperDB(string connStr)
    {
        this.connStr = connStr;
    }
    public int ExecuteStoreProcedure(string spName, params MySqlParameter[] param)
    {
        MySqlConnection conn = new MySqlConnection(connStr);
        try
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = spName;
            conn.Open();
            SqlCommandParam(cmd, param);
            return cmd.ExecuteNonQuery();
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }
    }
    public int ExecuteNonQuery(string cmdText, params MySqlParameter[] param)
    {
        MySqlConnection conn = new MySqlConnection(connStr);
        try
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            conn.Open();
            cmd.CommandText = cmdText;
            SqlCommandParam(cmd, param);

            return cmd.ExecuteNonQuery();
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }
    }

    public int ExecuteNonQuery(out long SonId, string cmdText, params MySqlParameter[] param)
    {
        MySqlConnection conn = new MySqlConnection(connStr);
        try
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            conn.Open();
            cmd.CommandText = cmdText;
            SqlCommandParam(cmd, param);

            int x = cmd.ExecuteNonQuery();
            SonId = cmd.LastInsertedId;
            return x;
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }
    }

    public MySqlDataReader ExecuteReader(string cmdText, params MySqlParameter[] param)
    {
        MySqlConnection conn = new MySqlConnection(connStr);
        try
        {
            MySqlCommand cmd = new MySqlCommand(cmdText, conn);
            SqlCommandParam(cmd, param);
            conn.Open();
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }
        catch (Exception ex)
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
            throw ex;
        }
    }

    public DataSet ExecuteDataSet(string cmdText, params MySqlParameter[] param)
    {
        MySqlConnection conn = new MySqlConnection(connStr);
        try
        {
            MySqlCommand cmd = new MySqlCommand(cmdText, conn);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            SqlCommandParam(cmd, param);
            conn.Open();
            da.Fill(ds);
            return ds;
        }
        catch (Exception ex)
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
            throw ex;
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }
    }

    public DataTable ExecuteDataTable(string cmdText, params MySqlParameter[] param)
    {
        MySqlConnection conn = new MySqlConnection(connStr);
        try
        {
            MySqlCommand cmd = new MySqlCommand(cmdText, conn);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            SqlCommandParam(cmd, param);
            conn.Open();
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
            throw ex;
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }
    }

    public object ExecuteScalar(string cmdText, params MySqlParameter[] param)
    {
        MySqlConnection conn = new MySqlConnection(connStr);
        try
        {
            MySqlCommand cmd = new MySqlCommand(cmdText, conn);
            SqlCommandParam(cmd, param);
            conn.Open();
            return cmd.ExecuteScalar();
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }
    }

    private void SqlCommandParam(MySqlCommand cmd, params MySqlParameter[] param)
    {
        for (int i = 0; i < param.Length; i++)
        {
            cmd.Parameters.Add(param[i]);
        }
    }
}

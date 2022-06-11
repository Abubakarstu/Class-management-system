
using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

public class Main
{
    private string connection;

    public Main()
    {

    }

    public void ExecuteQuery(string Query)
    {
        MySqlConnection mySqlConnection = new MySqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
        MySqlCommand mySqlCommand = new MySqlCommand(Query, mySqlConnection);
        mySqlConnection.Open();
        try
        {
            try
            {
                mySqlCommand.ExecuteNonQuery().ToString();
            }
            finally
            {
                mySqlConnection.Close();
                mySqlCommand.Dispose();
                mySqlConnection.Dispose();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataSet GetDataSet(string Query)
    {
        DataSet dataset;
        MySqlConnection mysqlconnection = new MySqlConnection("Server=sql208.epizy.com;database=epiz_31699193_curdapp;port=3306;user=epiz_31699193;pwd=abubakar@667");
        MySqlCommand command = new MySqlCommand(Query, mysqlconnection);
        MySqlDataAdapter adapter = new MySqlDataAdapter();
        DataSet ds = new DataSet();
        mysqlconnection.Open();
        try
        {
            adapter.SelectCommand = command;
            adapter.Fill(ds);
            mysqlconnection.Close();
            dataset = ds;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dataset;
    }
    //*****************************************************************************
    public DataSet SerachName(string Query)
    {
        DataSet dataset;
        MySqlConnection mysqlconnection = new MySqlConnection("Server=sql208.epizy.com;database=epiz_31699193_curdapp;port=3306;user=epiz_31699193;pwd=abubakar@667");
        MySqlCommand command = new MySqlCommand(Query, mysqlconnection);
        MySqlDataAdapter adapter = new MySqlDataAdapter();
        DataSet ds = new DataSet();
        mysqlconnection.Open();
        try
        {
            adapter.SelectCommand = command;
            adapter.Fill(ds);
            mysqlconnection.Close();
            dataset = ds;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dataset;
    }
    //*****************************************************************************
    public Image ConvertBinaryToImage(byte[] data)
    {
        Image image;
        using (MemoryStream memoryStream = new MemoryStream(data))
        {
            image = Image.FromStream(memoryStream);
        }
        return image;
    }

    public byte[] ConvertImageToBinary(Image img)
    {
        byte[] array;
        using (MemoryStream memoryStream = new MemoryStream())
        {
            img.Save(memoryStream, ImageFormat.Jpeg);
            array = memoryStream.ToArray();
        }
        return array;
    }

    internal string ConvertBinaryToImage(Image image)
    {
        throw new NotImplementedException();
    }
}
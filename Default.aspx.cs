using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    string prisonPort;
    string dbPassword;
    string connectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (ConfigurationManager.ConnectionStrings.Count > 0)
            {
                Dictionary<String, String> connectionDict = new Dictionary<String, String>();
                string connectionString = ConfigurationManager.ConnectionStrings["myConnectionString1"].ConnectionString;
                string[] connectionParams = connectionString.Split(';');

                foreach (string param in connectionParams)
                {
                    string key = param.Split('=')[0].Trim();
                    string val = param.Split('=')[1];
                    connectionDict.Add(key, val);
                }

                prisonPort = connectionDict["Server"].Split(',')[1];
                SQLPrisonPort.Text = prisonPort;

                dbPassword = connectionDict["Password"];
                SQLPrisonPass.Text = dbPassword;

                /*
                string portFromConfig;
                string passFromConfig;
                string connFromConfig = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;

                string portStart = connFromConfig.Substring(connFromConfig.IndexOf(",") + 1);
                if (portStart.IndexOf(";") < 3)
                {
                    portFromConfig = portStart.Substring(0, 5);
                }
                else
                {
                    portFromConfig = portStart.Substring(0, 1);
                }

                string passStart = connFromConfig.Substring(connFromConfig.IndexOf("d") + 2);
                passFromConfig = passStart.Substring(0, 14);

                SQLPrisonPort.Text = portFromConfig;
                SQLPrisonPass.Text = passFromConfig;
                */
            }
        }
    }

    protected void runQuery(object sender, EventArgs e)
    {
        // Clear Previous Query
        queryResultsDisplay.Text = "";

        // Establish Database Connection
        prisonPort = SQLPrisonPort.Text;
        dbPassword = SQLPrisonPass.Text;
        connectionString = string.Format(@"server=127.0.0.1,{0}; User Id=sa; Password={1}; Database=master; Connection Timeout=30", prisonPort, dbPassword);

        SqlConnection myConnection = new SqlConnection(connectionString);

        try
        {
            myConnection.Open();
        }
        catch (Exception ex)
        {
            queryResultsDisplay.Text = ex.ToString();
            return;
        }


        // Try query, display results if successful
        try
        {
            SqlDataReader myReader = null;
            SqlCommand myCommand = new SqlCommand(SQLQuery.Text, myConnection);
            myReader = myCommand.ExecuteReader();

            StringBuilder results = new StringBuilder("<table><tr>");
            List<string> columnNames = GetColumnNames(myReader);

            // Title returned columns
            foreach (string colName in columnNames)
                results.Append(string.Format("<th>{0}</th>", colName));
            results.Append("</tr>");

            // Populate returned columns with fields
            while (myReader.Read())
            {
                results.Append("<tr>");
                for (int i = 0; i < columnNames.Count; i++)
                    results.Append(string.Format("<td>{0}</td>", myReader[i].ToString()));
                results.Append("</tr>");
            }

            queryResultsDisplay.Text = results.ToString();
        }
        catch (Exception ex)
        {
            queryResultsDisplay.Text = ex.ToString();
        }

        myConnection.Close();
        myConnection.Dispose();
    }


    protected static List<string> GetColumnNames(SqlDataReader myReader)
    {
        List<string> columnNames = new List<string>();

        for (int i = 0; i < myReader.FieldCount; i++)
            columnNames.Add(myReader.GetName(i));

        return columnNames;
    }

    protected void SQLQuery_TextChanged(object sender, EventArgs e)
    {

    }
}
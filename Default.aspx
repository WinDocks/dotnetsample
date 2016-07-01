<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<%@ Import namespace="System.Data.SqlClient"%>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>WinDocks Demo</title>
    <link href="main.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="container">
	<br/><br/>
	<img src="windockslogo.png">
	<br/><br/>
        <h1>WinDocks .NET + SQL Server Container Demo</h1>

        <div id="form-controls">
            <section id="creds">
                <h2>SQL Server Credentials</h2>
            
                <small>PRISON PORT</small> <br />
                <asp:TextBox ID="SQLPrisonPort" runat="server"></asp:TextBox><br />

                <small>DB PASSWORD</small> <br />
                <asp:TextBox ID="SQLPrisonPass" runat="server"></asp:TextBox><br />
            </section>

            <section id="query">  
                <h2>Database Query</h2>
                <asp:TextBox ID="SQLQuery" runat="server" Height="16px" Width="391px" >use customerdata; SELECT * FROM customers</asp:TextBox><br />
                <asp:Button ID="submitQuery" text="Submit Query" onclick="runQuery" runat="server"/>
            </section>
        </div>

        <div id="results">
            <h2>Query Results</h2>
            <asp:Label ID="queryResultsDisplay" runat="server" />
        </div>

    </div>
    </form>
</body>
</html>

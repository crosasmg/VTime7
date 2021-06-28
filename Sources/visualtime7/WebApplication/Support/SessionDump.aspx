<%@ page language="C#" autoeventwireup="true" codefile="SessionDump.aspx.cs" inherits="SessionDump" enableviewstate="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Session Dump</title>
    <style type="text/css">
        table {
            border-collapse: collapse;
        }

        td {
            border: 1px solid black;
            text-align: left;
            vertical-align: top;
        }
    </style>
</head>
<body>
    <h1 id="h1Title" runat="server">Session</h1>
    <form id="form1" runat="server">
        <asp:HyperLink ID="hplSeccionTrace" runat="server">HyperLink</asp:HyperLink>
        <br />
        <asp:HyperLink ID="hplSeccionTraceParameter" runat="server">HyperLink</asp:HyperLink>
        <p style="text-align: right; position: absolute; top: 2em; right: 1em;" id="pFullSessionLink" runat="server" visible="false"><a href="SessionDump.aspx">All Session Items</a></p>
        <p><strong>Estimated Size:</strong>
            <asp:Label ID="lblEstimatedSize" runat="server"></asp:Label></p>
        <div id="divContainer" runat="server"></div>
    </form>
</body>
</html>
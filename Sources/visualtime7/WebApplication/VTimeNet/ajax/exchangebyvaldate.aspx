<%@ Page Language="VB" explicit="true" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneral" %>

<!-- #INCLUDE VIRTUAL="~/VTimeNet/ajax/ajxfunctions.aspx" -->
<script language="VB" runat="Server">
    Private Function GetExchangeInfo() As String
        Dim fnc as New efunctions.Values
        Dim rv As String = "{"
        Dim exc As New eGeneral.Exchange
        Dim dValDate as Date = fnc.StringToDate(Request.QueryString("dValDate"))
        If exc.Find(Request.QueryString("nCurrency"), dValDate) Then
            rv &= AddJsonEntry("sExchange",  fnc.TypeToString(exc.nExchange,eFunctions.Values.eTypeData.etdDouble, False, 2), True)
        End If
        rv &= "}"
        Return rv
    End Function
    </script>
<% 
    Response.Expires = -1441
    Response.Write(GetExchangeInfo())
%>
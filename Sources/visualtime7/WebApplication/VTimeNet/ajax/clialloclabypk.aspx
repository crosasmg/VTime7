<%@ Page Language="VB" explicit="true" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="eProduct" %>

<!-- #INCLUDE VIRTUAL="~/VTimeNet/ajax/ajxfunctions.aspx" -->
<script language="VB" runat="Server">
    Private Function GetCliAlloClaInfo() As String
        Dim fnc as New efunctions.Values
        Dim rv As String = "{"
        Dim roles As New eProduct.Clialloclas
        Dim rol As New eProduct.Cliallocla

        If roles.Find(Request.QueryString("nBranch"), Request.QueryString("nProduct")) Then
            For Each rol In roles
                If rol.nRole = Request.QueryString("nrole") Then
                    rv &= AddJsonEntry("SDEFAULT_CLA_IND", rol.SDEFAULT_CLA_IND, True)
                    Exit For
                End If
            Next
        End If
        rv &= "}"
        Return rv
    End Function
 </script>
<% 
    Response.Expires = -1441
    Response.Write(GetCliAlloClaInfo())
%>
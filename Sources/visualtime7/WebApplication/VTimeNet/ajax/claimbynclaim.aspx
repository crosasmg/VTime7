<%@ Page Language="VB" explicit="true" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClaim" %>

<!-- #INCLUDE VIRTUAL="~/VTimeNet/ajax/ajxfunctions.aspx" -->
<script language="VB" runat="Server">
    Private Function GetClaimInfo() As String
        Dim rv As String = "{"
        Dim clm As New eClaim.Claim
        Dim tab As New eFunctions.Tables
        Dim prd As New eProduct.Product
        If clm.Find(Request.QueryString("nClaim")) Then
            If tab.GetDescription("TABLE10", clm.nBranch) Then
                rv &= AddJsonEntry("sBranch", tab.Descript)
            End If
            If prd.Find(clm.nBranch, clm.nProduct, Date.Today) Then
                rv &= AddJsonEntry("sProduct", prd.sDescript)
            End If
            rv &= AddJsonEntry("nPolicy", clm.nPolicy, True)
        End If
        rv &= "}"
        Return rv
    End Function
    </script>
<% 
    Response.Expires = -1441
    Response.Write(GetClaimInfo())
%>
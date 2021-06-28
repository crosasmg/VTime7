<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim lstrIntermed As String


'---------------------------------------------------------------------------
Private Sub insPreAGC002_k()
	'---------------------------------------------------------------------------
	
Response.Write("" & vbCrLf)
Response.Write("	<BR>" & vbCrLf)
Response.Write("	<TABLE WIDTH =100% COLSPAN=""4"">	" & vbCrLf)
Response.Write("		<TR>			" & vbCrLf)
Response.Write("			<TD ><LABEL ID=8023>" & GetLocalResourceObject("tcnIntermedCaption") & "</LABEL></TD>			" & vbCrLf)
Response.Write("			<TD >")


        Response.Write(mobjValues.PossiblesValues("tcnIntermed", "tabintermedia", 2, mobjValues.StringToType(lstrIntermed, eFunctions.Values.eTypeData.etdDouble), , , , , , , True,10 , GetLocalResourceObject("tcnIntermedToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD COLSPAN=""1"">&nbsp;</TD>        " & vbCrLf)
Response.Write("            <TD COLSPAN=""1"">&nbsp;</TD>        " & vbCrLf)
Response.Write("        </TR>  			" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("tcdStardateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.DateControl("tcdStardate", Session("dStardate"),  , GetLocalResourceObject("tcdStardateToolTip"),  ,  ,  ,  , True))


Response.Write("</TD>						" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("tcdEnddateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.DateControl("tcdEnddate", Session("dEnddate"),  , GetLocalResourceObject("tcdEnddateToolTip"),  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("cbeStatloanCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.PossiblesValues("cbeStatloan", "table191", eFunctions.Values.eValuesType.clngComboType, Session("sStatloan"),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeStatloanToolTip")))


Response.Write("</TD>					" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("cbeBranchCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")

	Response.Write(mobjValues.PossiblesValues("cbeBranch", "Table10", 1, Session("nBranch"),  ,  ,  ,  ,  , "ChangeBranch(this);", True))
Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("valProductCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		    ")

	With mobjValues.Parameters
		.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	End With
Response.Write("" & vbCrLf)
Response.Write("		    <TD>")


Response.Write(mobjValues.PossiblesValues("valProduct", "tabProdmaster1", 2, Session("nProduct"), True,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("valProductToolTip")))


Response.Write("</TD>			" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("tcnPolicyCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.NumericControl("tcnPolicy", 8, Session("nPolicy"),  , "",  , 0,  ,  ,  ,  , True))


Response.Write("</TD>             						" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>        " & vbCrLf)
Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("tcnLoanCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        	<TD>")


Response.Write(mobjValues.NumericControl("tcnLoan", 5, Session("nLoan"),  , GetLocalResourceObject("tcnLoanToolTip"),  , 0,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD COLSPAN=""1"">&nbsp;</TD>        " & vbCrLf)
Response.Write("            <TD COLSPAN=""1"">&nbsp;</TD>        " & vbCrLf)
Response.Write("        </TR>         		  " & vbCrLf)
Response.Write("	</TABLE>")

	
End Sub

</script>
<%Response.Expires = -1

mobjMenu = New eFunctions.Menues
mobjValues = New eFunctions.Values

If CStr(Session("tcnIntermed")) <> vbNullString Then
	lstrIntermed = Trim(Session("tcnIntermed"))
ElseIf CStr(Session("nIntermed")) <> vbNullString Then 
	lstrIntermed = Trim(Session("nIntermed"))
End If

%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>


<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
    <SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 13.15 $"        

//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
<%Session("tcnIntermed") = vbNullString%>
	return true;
}

//------------------------------------------------------------------------------------------
function  insStateZone()
//------------------------------------------------------------------------------------------
{
    var lintIndex = 0;
    for (lintIndex=0;lintIndex<document.forms[0].length;lintIndex++)
        document.forms[0].elements[lintIndex].disabled = false;

   document.images["btntcnIntermed"].disabled = false;
   document.images["btn_tcdStardate"].disabled = false;
   document.images["btn_tcdEnddate"].disabled = false;
}
//--------------------------------------------------------------------------------------------
function ChangeBranch(Field)
//--------------------------------------------------------------------------------------------
{
	if(typeof(document.forms[0].valProduct)!='undefined')
	{
		self.document.forms[0].valProduct.Parameters.Param1.sValue=Field.value;
		self.document.forms[0].valProduct.disabled=false;
		self.document.forms[0].btnvalProduct.disabled=false;
		self.document.forms[0].tcnPolicy.disabled=false;
	}
}

</SCRIPT>
<%

With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjValues.WindowsTitle("AGC002"))
	.Write(mobjMenu.setZone(1, Request.QueryString.Item("sCodispl"), "AGC002_K.aspx"))
	.Write(mobjMenu.MakeMenu("AGC002", "AGC002_k.aspx", 1, ""))
	.Write("<br>")
End With
mobjMenu = Nothing
%>    

</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmQIntermLoans" ACTION="ValAgent.aspx?Zone=1">
<%
Call insPreAGC002_k()
%>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing

%>





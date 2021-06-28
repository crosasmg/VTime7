<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneral" %>
<script language="VB" runat="Server">

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mobjNetFrameWork As eNetFrameWork.Layout


'%insPreCR769: Se cargan los controles de la ventana.
'--------------------------------------------------------------------------------------------
Private Sub insPreCR769()
	'--------------------------------------------------------------------------------------------
	Dim lclsCtrol_date As eGeneral.Ctrol_date
	lclsCtrol_date = New eGeneral.Ctrol_date
	
	Dim lintYear As Short
	Dim lintMonth As Short
	Dim ldtmInit_date As Object
	
	lintYear = Year(Today)
	lintMonth = Month(Today)
	
	If lclsCtrol_date.Find(80) Then
'UPGRADE_NOTE: Date operands have a different behavior in arithmetical operations. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1023.htm
		ldtmInit_date = System.Date.FromOADate(lclsCtrol_date.dEffecdate.ToOADate + 1)
		lintYear = Year(ldtmInit_date)
		lintMonth = Month(ldtmInit_date)
	Else
		ldtmInit_date = DateSerial(Year(Today), Month(Today), 1)
	End If
	
	
	
Response.Write("" & vbCrLf)
Response.Write("		<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD><LABEL>" & GetLocalResourceObject("tcdDateToCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD> ")


Response.Write(mobjValues.DateControl("tcdDateTo", ldtmInit_date,  , GetLocalResourceObject("tcdDateToToolTip"),  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD><LABEL>" & GetLocalResourceObject("tcdEnd_dateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.DateControl("tcdEnd_date", CStr(Today),  , GetLocalResourceObject("tcdEnd_dateToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("      	</TR>			" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=100642>" & GetLocalResourceObject("cboContraTypeCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")

	Response.Write(mobjValues.PossiblesValues("cboContraType", "table173", eFunctions.Values.eValuesType.clngComboType, Session("nType_rein"),  ,  ,  ,  ,  ,  , True,  , ""))
Response.Write("</TD>                   " & vbCrLf)
Response.Write("            <TD><LABEL ID=100643>" & GetLocalResourceObject("cboBranchCaption") & "</LABEL></TD>            " & vbCrLf)
Response.Write("            <TD>")

	With mobjValues
		.Parameters.Add("nType", Session("nType_rein"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		Response.Write(.PossiblesValues("cboBranch", "REACONTRNPR_BRANCH", eFunctions.Values.eValuesType.clngComboType,  , True,  ,  ,  ,  , "FindContratnp(this.value);",  ,  , ""))
	End With
Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		" & vbCrLf)
Response.Write("      	<TR>					" & vbCrLf)
Response.Write("		<TD><LABEL>" & GetLocalResourceObject("tcnNumberCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")

	With mobjValues
		.Parameters.Add("nBranch", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nType", Session("nType_rein"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		Response.Write(.PossiblesValues("tcnNumber", "reacontr_nproc", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  ,  ,  ,  , ""))
	End With
Response.Write("</TD>" & vbCrLf)
Response.Write("	    <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("       	</TR>			" & vbCrLf)
Response.Write("		" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("		    <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("		    <TD>")

	Response.Write(mobjValues.OptionControl(0, "optEjecucion", GetLocalResourceObject("optEjecucion_2Caption"), "1", "2"))
Response.Write("</TD>" & vbCrLf)
Response.Write("   		    <TD>")

	Response.Write(mobjValues.OptionControl(0, "optEjecucion", GetLocalResourceObject("optEjecucion_1Caption"),  , "1"))
Response.Write("" & vbCrLf)
Response.Write("   		    <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("		" & vbCrLf)
Response.Write("		" & vbCrLf)
Response.Write("	</TABLE>")

	
	lclsCtrol_date = Nothing
End Sub

</script>
<%Response.Expires = -1441

mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("CR769_K")

Response.Cache.SetCacheability(HttpCacheability.NoCache)

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
mobjValues.sCodisplPage = "CR769_K"
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
Session("nType_rein") = 685
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>



<SCRIPT LANGUAGE="JavaScript">
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 3 $|$$Date: 5/07/06 22:35 $|$$Author: Vvera $"

//%insStateZone: Se habilita/deshabilita los campos de la ventana.
//-------------------------------------------------------------------------------------------
function insStateZone(){
//-------------------------------------------------------------------------------------------
}

//%insCancel: Acciones a efectuar al cancelar la transacción.
//-------------------------------------------------------------------------------------------
function insCancel(){
//-------------------------------------------------------------------------------------------
	return true;
}

//%insFinish: Acciones a efectuar al finalizar la transacción.
//-------------------------------------------------------------------------------------------
function insFinish(){
//-------------------------------------------------------------------------------------------
	return true;
}

//ChangeCompany: acciones para cambio de compañía
function FindBranch_Rei(nField)
//-------------------------------------------------------------------------------------------
{
 if (nField != 0) 
     self.document.forms[0].cboBranch.Parameters.Param1.sValue =  nField
}

function FindContratnp(nField)
//-------------------------------------------------------------------------------------------
{
 if (nField != 0) 
     self.document.forms[0].tcnNumber.Parameters.Param1.sValue =  nField
}
</SCRIPT>	
<SCRIPT>	
//% ShowData: Se cargan los valores de acuerdo al número de contrato, si éste está previamente registrado en el sistema 
//--------------------------------------------------------------------------------------------------------------------
function ShowData(sField){
//--------------------------------------------------------------------------------------------------------------------
	if(self.document.forms[0].tcnNumber.value!='')
		ShowPopUp("/VTimeNet/CoReinsuran/CoReinsurantra/ShowDefValues.aspx?Field=" + sField  + "&nNumber=" + self.document.forms[0].tcnNumber.value + 
		                                                            "&dEffecdate=" + self.document.forms[0].tcdEnd_date.value, "ShowDefValuesNumberContr_np", 1, 1,"no","no",2000,2000);						        			   
}   	
</SCRIPT>	
<%With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "CR769_K.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
End With
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="CR769" ACTION="valCoReinsurantra.aspx?sMode=1">
<BR><BR><BR>
<%
Call insPreCR769()
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>
<%
Call mobjNetFrameWork.FinishPage("CR769_K")
mobjNetFrameWork = Nothing
%>





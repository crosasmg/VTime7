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
Private Sub insPreCRL893()
	'--------------------------------------------------------------------------------------------
	Dim lclsCtrol_date As eGeneral.Ctrol_date
	lclsCtrol_date = New eGeneral.Ctrol_date
	
	Dim lintYear As Short
	Dim lintMonth As Short
	Dim ldtmInit_date As Date
	
	lintYear = Year(Today)
	lintMonth = Month(Today)
	
	If lclsCtrol_date.Find(81) Then
		ldtmInit_date = lclsCtrol_date.dEffecdate
		lintYear = Year(ldtmInit_date)
		lintMonth = Month(ldtmInit_date)
	Else
		ldtmInit_date = DateSerial(Year(Today), Month(Today), 1)
	End If
	
	
	
Response.Write("" & vbCrLf)
Response.Write("		<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("		" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("		   <TD><LABEL>" & GetLocalResourceObject("valCompanyCaption") & "</LABEL>" & vbCrLf)
Response.Write("		       <TD>")


Response.Write(mobjValues.PossiblesValues("valCompany", "reacompany_contrexst", 1,  ,  ,  ,  ,  ,  , "ChangeCompany(this.value);", False,  , GetLocalResourceObject("valCompanyToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("		   </TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD><LABEL>" & GetLocalResourceObject("tcdDateToCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD> ")


Response.Write(mobjValues.DateControl("tcdDateTo", CStr(Today),  , GetLocalResourceObject("tcdDateToToolTip"),  ,  ,  , "ChangeDateIni(this.value);"))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD><LABEL>" & GetLocalResourceObject("tcdEnd_dateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.DateControl("tcdEnd_date", CStr(Today),  , GetLocalResourceObject("tcdEnd_dateToolTip"),  ,  ,  , "ChangeDateEnd(this.value);"))


Response.Write("</TD>" & vbCrLf)
Response.Write("      	</TR>			" & vbCrLf)
Response.Write("      	<TR>			" & vbCrLf)
Response.Write("           	<TD><LABEL>" & GetLocalResourceObject("tcnNumberCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")

	With mobjValues
		.Parameters.Add("nCompany", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("dDateIni", Today, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("dDateEnd", Today, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.ReturnValue("nType_rel",  ,  , True)
		Response.Write(.PossiblesValues("tcnNumber", "reacontr_percom", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  , "FindContrat(this.value)",  ,  , ""))
	End With
Response.Write("</TD>" & vbCrLf)
Response.Write("		    <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=100642>" & GetLocalResourceObject("cbeContraTypeCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")

	mobjValues.TypeOrder = 1
	mobjValues.BlankPosition = True
	Response.Write(mobjValues.PossiblesValues("cbeContraType", "table173", eFunctions.Values.eValuesType.clngComboType, Session("nType"),  ,  ,  ,  ,  ,  , True,  , ""))
Response.Write("</TD>                   " & vbCrLf)
Response.Write("            <TD><LABEL ID=100643>" & GetLocalResourceObject("cbeBranch_reiCaption") & "</LABEL></TD>            " & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.PossiblesValues("cbeBranch_rei", "table5000", 1, Session("nBranch_rei"),  ,  ,  ,  ,  ,  , True,  , ""))


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("		    <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("		    <TD>")

	Response.Write(mobjValues.OptionControl(0, "optEjecucion", GetLocalResourceObject("optEjecucion_2Caption"), "1", "2"))
Response.Write("</TD>" & vbCrLf)
Response.Write("   		    <TD>")

	Response.Write(mobjValues.OptionControl(0, "optEjecucion", GetLocalResourceObject("optEjecucion_1Caption"),  , "1"))
Response.Write("" & vbCrLf)
Response.Write("   		     ")

	Response.Write(mobjValues.HiddenControl("hddnType_rel", vbNullString))
Response.Write(" " & vbCrLf)
Response.Write("   		    <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		" & vbCrLf)
Response.Write("	</TABLE>")

	
	lclsCtrol_date = Nothing
End Sub

</script>
<%Response.Expires = -1441

mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("CRL893_K")

'Response.CacheControl = False

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
mobjValues.sCodisplPage = "CRL893_K"
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>



<SCRIPT LANGUAGE="JavaScript">
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 20/04/06 18:28 $|$$Author: Vvera $"

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
function ChangeCompany(nField)
//-------------------------------------------------------------------------------------------
{
 if (nField != 0) 
 
     self.document.forms[0].tcnNumber.value =''
     self.document.forms[0].cbeContraType.value =''
     self.document.forms[0].cbeBranch_rei.value=''
     UpdateDiv('tcnNumberDesc', '')
     
     self.document.forms[0].tcnNumber.Parameters.Param1.sValue =  nField
}


//ChangeDateIni: acciones para cambio de fecha inicial
function ChangeDateIni(nField)
//-------------------------------------------------------------------------------------------
{
 if (nField != 0)       
     self.document.forms[0].tcnNumber.Parameters.Param2.sValue =  nField
}

//ChangeDateEnd: acciones para cambio de fecha inicial
function ChangeDateEnd(nField)
//-------------------------------------------------------------------------------------------
{
 if (nField != 0)       
     self.document.forms[0].tcnNumber.Parameters.Param3.sValue =  nField
}

function FindContrat(sField)
//-------------------------------------------------------------------------------------------
{
  var nType_contrat = self.document.forms[0].tcnNumber_nType_rel.value

  self.document.forms[0].hddnType_rel.value = nType_contrat
  self.document.forms[0].cbeContraType.value =''
  self.document.forms[0].cbeBranch_rei.value=''
 // UpdateDiv('tcnNumberDesc', '')

  if  (nType_contrat == 1)
  {
//Busqueda de contratos proporcionales 
     sField='NumberContr'
     ShowPopUp("/VTimeNet/CoReinsuran/CoReinsuranrep/ShowDefValues.aspx?Field=" + sField  + "&nNumber=" + self.document.forms[0].tcnNumber.value + "&dEffecdate=" + self.document.forms[0].tcdEnd_date.value,"ShowDefValuesNumberContr", 1, 1,"no","no",2000,2000);
  }
  else
  {
//Busqueda de contratos no proporcionales  
   sField='NumberContr_np' 
   if(self.document.forms[0].tcnNumber.value!='')
	  ShowPopUp("/VTimeNet/CoReinsuran/CoReinsuranrep/ShowDefValues.aspx?Field=" + sField  + "&nNumber=" + self.document.forms[0].tcnNumber.value + 
	                                                            "&dEffecdate=" + self.document.forms[0].tcdEnd_date.value, "ShowDefValuesNumberContr_np", 1, 1,"no","no",2000,2000);						        			   
  } 
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
<FORM METHOD="post" ID="FORM" NAME="CR769" ACTION="valCoReinsuranrep.aspx?sMode=1">
<BR><BR><BR>
<%
Call insPreCRL893()
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>
<%
Call mobjNetFrameWork.FinishPage("CR769_K")
mobjNetFrameWork = Nothing
%>





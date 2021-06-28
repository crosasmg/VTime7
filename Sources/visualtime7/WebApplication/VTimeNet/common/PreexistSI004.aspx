<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para menjo de grid
Dim mobjGrid As eFunctions.Grid

'- Variables para almacenar parametros de pagina
Dim mstrCertype As String


'% insDefineHeader: se definen los campos del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	mobjGrid.sCodisplPage = "poldata"
	
	With mobjGrid
		.Codispl = "SCA003"
		.AddButton = False
		.DeleteButton = False
		.sEditRecordParam = "nTariff=0" & "&sInsured=" & Request.QueryString.Item("sInsured")
		
	End With
	With mobjGrid.Columns
		.AddPossiblesColumn(0, GetLocalResourceObject("cbeIllnessColumnCaption"), "cbeIllness", "Tab_am_ill", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  ,  , 8, GetLocalResourceObject("cbeIllnessColumnToolTip"))
		.AddDateColumn(0, GetLocalResourceObject("tcdDateIniColumnCaption"), "tcdDateIni", CStr(Today),  , GetLocalResourceObject("tcdDateIniColumnToolTip"))
		
	End With
	
	
	With mobjGrid.Columns("cbeIllness")
		.Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nCertif", Session("nCertif"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("sClient", Request.QueryString.Item("sInsured"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	End With
	mobjGrid.Columns("Sel").GridVisible = False
	mobjGrid.Columns.AddHiddenColumn("sParam", vbNullString)
	
End Sub

'% insPreSCA003: se cargan los controles de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreSCA003()
	'--------------------------------------------------------------------------------------------
	Dim lcolTab_Am_Excs As ePolicy.Tab_am_excs
	Dim lclsTab_Am_Exc As ePolicy.Tab_am_exc
	Dim llngIndex As Integer
	
	lclsTab_Am_Exc = New ePolicy.Tab_am_exc
	lcolTab_Am_Excs = New ePolicy.Tab_am_excs
	
	If lcolTab_Am_Excs.Find(mstrCertype, mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdLong), 0, Session("dEffecdate"), Request.QueryString.Item("sInsured"), "2") Then
		For llngIndex = 1 To lcolTab_Am_Excs.Count
        'For llngIndex = 0 To lcolTab_Am_Excs.Count -1
			With mobjGrid
				.Columns("cbeIllness").DefValue = lcolTab_Am_Excs.Item(llngIndex).sIllness
				
				.Columns("tcdDateIni").DefValue = CStr(lcolTab_Am_Excs.Item(llngIndex).dInit_date)
				
				.Columns("sParam").DefValue = "nIllness=" & lcolTab_Am_Excs.Item(llngIndex).sIllness
				
				response.Write(.DoRow)
			End With
		Next 
	End If
	response.Write(mobjGrid.closeTable())
	lcolTab_Am_Excs = Nothing
	lclsTab_Am_Exc = Nothing
End Sub

</script>
<%response.Expires = -1


mobjValues = New eFunctions.Values

'+ Se deja la pagina en modo consulta     
mobjValues.ActionQuery = True

'+ Se asignan valores de parámetros     
mstrCertype = Request.QueryString.Item("sCertype")


mobjValues.sCodisplPage = "poldata"
%>
<HTML>
<HEAD>
<META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT>
//+ Variable para el control de versiones
	document.VssVersion="$$Revision: 2 $|$$Date: 9/02/04 18:14 $"
</SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


    <%=mobjValues.StyleSheet()%>
    <%=mobjValues.WindowsTitle("SCA850")%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmPolData" ACTION="PolData.aspx">

<%With response
	.Write(mobjValues.ShowWindowsName("SCA850"))
	.Write("<BR>")
End With
%>

<%

Call insDefineHeader()
Call insPreSCA003()

With response
	
	.Write(mobjGrid.closeTable())
	.Write("<P ALIGN=""RIGHT"">")
	mobjValues.ActionQuery = False
	.Write(mobjValues.ButtonAcceptCancel("window.close();",  ,  ,  , eFunctions.Values.eButtonsToShow.OnlyCancel))
	.Write("</P>")
	
End With

mobjGrid = Nothing
mobjValues = Nothing

%>
</FORM>
</BODY>
</HTML>







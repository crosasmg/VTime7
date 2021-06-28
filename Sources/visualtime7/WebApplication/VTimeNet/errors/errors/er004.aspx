<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eErrors" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

Dim mobjMenu As eFunctions.Menues

Dim mobjError As eErrors.ErrorTyp

'- Objeto para el manejo del grid    
Dim mobjGrid As eFunctions.Grid


'% insDefineHeader: se definen los campos del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid.sCodisplPage = "er004"
	
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		Call .AddPossiblesColumn(6764,"Estado", "sStat_error", "Table999", eFunctions.Values.eValuesType.clngWindowType, CStr(eRemoteDB.Constants.strnull),,,,,,,,vbNullString)
		Call .AddDateColumn(6765,"Fecha de Actual", "dDate", vbNullString,,vbNullString)
		Call .AddTextColumn(6766,"Hora de Actual", "sHour", 6, vbNullString,,vbNullString)
		Call .AddTextColumn(6767,"Días Utilizados", "nDays_user", 3, vbNullString,,vbNullString)
		Call .AddTextColumn(6768,"Horas Utilizadas", "sHour_user", 6, vbNullString,,vbNullString)
		Call .AddTextColumn(6769,"Responsable", "sUser", 8, vbNullString,,vbNullString)
		Call .AddTextColumn(6770,"Dias con ese Estado", "sNumDays", 4, vbNullString,,vbNullString)
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "ER004"
		.Codisp = "ER004"
		.DeleteButton = False
		.AddButton = False
		.Columns("Sel").GridVisible = False
	End With
	
End Sub

'% insPreER004: Se carga el Grid con la Información
'--------------------------------------------------------------------------------------------
Private Sub insPreER004()
	'--------------------------------------------------------------------------------------------
	Dim lcolErroquers As eErrors.Err_Histors
	Dim lclsErroquer As eErrors.err_histor
	Dim lIndex As Integer
	Dim nType_mov As Object
	Dim nEndBalance As Object
	Dim nIniBalance As Object
	Dim ldtmAuxDate As Date
	
	lcolErroquers = New eErrors.Err_Histors
	lclsErroquer = New eErrors.err_histor
	
	If lcolErroquers.Find(mobjValues.StringToType(Session("nErrorNum"), eFunctions.Values.eTypeData.etdLong)) Then
		ldtmAuxDate = Today
		For lIndex = 1 To lcolErroquers.Count
			lclsErroquer = lcolErroquers.Item(lIndex)
			With lclsErroquer
				mobjGrid.Columns("sNumDays").DefValue = CStr(System.Date.FromOADate(.dDate.ToOADate - ldtmAuxDate.ToOADate))
				If lIndex <> 1 Then
					Response.Write(mobjGrid.DoRow())
				End If
				mobjGrid.Columns("sStat_error").DefValue = .sStat_error
				mobjGrid.Columns("dDate").DefValue = CStr(.dDate)
				mobjGrid.Columns("sHour").DefValue = .sHour
				If (mobjValues.StringToType(CStr(.nDays_user), eFunctions.Values.eTypeData.etdLong) = eRemoteDB.Constants.intNull) Then
					mobjGrid.Columns("nDays_user").DefValue = CStr(0)
				Else
					mobjGrid.Columns("nDays_user").DefValue = mobjValues.StringToType(CStr(.nDays_user), eFunctions.Values.eTypeData.etdLong)
				End If
				mobjGrid.Columns("sHour_user").DefValue = .sHour_user
				mobjGrid.Columns("sUser").DefValue = .sUser
				ldtmAuxDate = .dDate
			End With
		Next 
            mobjGrid.Columns("sNumDays").DefValue = CStr(System.DateTime.FromOADate(Today.ToOADate - ldtmAuxDate.ToOADate))
		Response.Write(mobjGrid.DoRow())
	End If
	Response.Write(mobjGrid.closeTable)
	
	lcolErroquers = Nothing
	lclsErroquer = Nothing
	
End Sub

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mobjError = New eErrors.ErrorTyp
mobjGrid = New eFunctions.Grid

mobjValues.sCodisplPage = "er004"
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
	<META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">

	<%=mobjValues.StyleSheet()%>
    <%=mobjMenu.setZone(2, "ER004", "ER004.aspx")%>

</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmErroUpd" ACTION="valerrors.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%Response.Write(mobjValues.ShowWindowsName("ER004"))%>
</FORM>
</BODY>
</HTML>

<%
Call insDefineHeader()
Call insPreER004()
mobjError.Find(mobjValues.StringToType(Session("nErrorNum"), eFunctions.Values.eTypeData.etdLong))
With Response
	.Write("<SCRIPT>")
	.Write("with(top.document.frames['fraHeader']){")
	.Write("UpdateDiv('tctCodisp','" & mobjError.sCodisp & "','Normal');")
	.Write("}")
	.Write("</SCRIPT>")
End With

mobjMenu = Nothing
mobjError = Nothing
mobjValues = Nothing
mobjGrid = Nothing
%>












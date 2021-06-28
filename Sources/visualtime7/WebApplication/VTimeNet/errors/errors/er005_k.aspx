<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eErrors" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo del grid    
Dim mobjGrid As eFunctions.Grid

Dim mstrError As String
Dim mstrId As String
Dim mstrType As String
Dim mstrAction As String


'% insDefineHeader: se definen los campos del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	
	If Request.QueryString.Item("Type") <> "PopUp" Then
		Response.Write("<TABLE WIDTH='100%'><TR><TD WIDTH='10%'><LABEL ID=6773>Error</LABEL></TD><TD>")
		Response.Write(mobjValues.NumericControl("tcnError", 5, mstrError,  ,"Código de error",  ,  ,  ,  ,  ,  , mstrError <> vbNullString))
		Response.Write("</TD></TR></TABLE>")
	Else
		Response.Write(mobjValues.HiddenControl("tcnError", mstrError))
	End If
	
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		Call .AddNumericColumn(6774,"Id", "tcnId", 5, "",  ,vbNullString,  ,  ,  ,  ,  , True)
		Call .AddPossiblesColumn(6775,"Tipo de componente", "cbeCompType", "Table998", eFunctions.Values.eValuesType.clngComboType, "",  ,  ,  ,  ,  ,  ,  ,"Tipo de componente")
		Call .AddTextColumn(6776,"Componente", "tctName", 25, "", True,"Nombre del componente")
		Call .AddTextColumn(6777,"Ubicación", "tctPath", 50, vbNullString,,vbNullString)
		Call .AddNumericColumn(6778,"Versión", "tcnVersion", 5, vbNullString,  ,"Versión generada en SourceSafe")
		Call .AddDateColumn(6779,"Traspaso QC", "tcdToQC",  ,  ,"Fecha en que componente fue traspasado a calidad",  ,  ,  , True)
		Call .AddDateColumn(6780,"Traspaso Amauta", "tcdToQA",  ,  ,"Fecha en que componente fue traspasado a Amauta",  ,  ,  , True)
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "ER005"
		.Codisp = "ER005"
		.Width = 500
		.Height = 360
		.ActionQuery = mobjValues.ActionQuery
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("tcnId").EditRecord = True
		.sEditRecordParam = "nError=" & mstrError
		.sDelRecordParam = "nError=" & mstrError & "&nId='+ marrArray[lintIndex].tcnId + '"
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
	
End Sub

'% insPreER005: Se carga el Grid con la Información
'--------------------------------------------------------------------------------------------
Private Sub insPreER005()
	'--------------------------------------------------------------------------------------------
	Dim lclsErr_Comp As Object
	Dim lcolErr_Comps As eErrors.Err_Comps
	
	
	Response.Write("<DIV ID='Scroll' STYLE='WIDTH:710;HEIGHT:300;OVERFLOW:auto;outset gray'>" & vbCrLf)
	
	lcolErr_Comps = New eErrors.Err_Comps
	If lcolErr_Comps.Find(mobjValues.StringToType(mstrError, eFunctions.Values.eTypeData.etdLong)) Then
		For	Each lclsErr_Comp In lcolErr_Comps
			With lclsErr_Comp
				mobjGrid.Columns("tcnId").DefValue = .nId
				mobjGrid.Columns("cbeCompType").DefValue = .nCompType
				mobjGrid.Columns("tctName").DefValue = .sCompName
				mobjGrid.Columns("tctPath").DefValue = .sCompPath
				mobjGrid.Columns("tcnVersion").DefValue = .nCompVers
				mobjGrid.Columns("tcdToQC").DefValue = .dToQC
				mobjGrid.Columns("tcdToQA").DefValue = .dToQA
			End With
			Response.Write(mobjGrid.DoRow())
		Next lclsErr_Comp
	End If
	Response.Write(mobjGrid.closeTable)
	
	lcolErr_Comps = Nothing
	lclsErr_Comp = Nothing
	
	Response.Write("</DIV>" & vbCrLf)
	Response.Write("<TABLE>")
	Response.Write("<TR>")
	Response.Write("<TD>" & mobjValues.ButtonAcceptCancel("insAccept()", "top.close();") & "</TD>")
	Response.Write("</TR>")
	Response.Write("</TABLE>")
	
	
Response.Write("" & vbCrLf)
Response.Write("<SCRIPT LANGUAGE=javascript>" & vbCrLf)
Response.Write("<!--" & vbCrLf)
Response.Write("//%insAccept: Acepta la ventana" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------" & vbCrLf)
Response.Write("function insAccept(){" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------" & vbCrLf)
Response.Write("//+Se agrega indicador que se ya se realizó validacion" & vbCrLf)
Response.Write("    opener.top.fraFolder.document.forms[0].action += '&IsValid=1';" & vbCrLf)
Response.Write("//+Se ejecuta nuevamente envío de datos" & vbCrLf)
Response.Write("    opener.top.fraFolder.document.forms[0].submit();" & vbCrLf)
Response.Write("//+Se cierra popup" & vbCrLf)
Response.Write("    top.close();" & vbCrLf)
Response.Write("}" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("//%CancelErrors: Reactiva menu al cerrar la ventana" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------" & vbCrLf)
Response.Write("function CancelErrors(bClose) {" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------    " & vbCrLf)
Response.Write("    with (opener.top.fraHeader){" & vbCrLf)
Response.Write("        insHandImage(""A390"", false);" & vbCrLf)
Response.Write("        insHandImage(""A301"", false);" & vbCrLf)
Response.Write("	    insHandImage(""A302"", false);" & vbCrLf)
Response.Write("	    insHandImage(""A303"", false);" & vbCrLf)
Response.Write("	    insHandImage(""A304"", false);" & vbCrLf)
Response.Write("	    insHandImage(""A401"", false);" & vbCrLf)
Response.Write("	    insHandImage(""A402"", false);" & vbCrLf)
Response.Write("	    insHandImage(""A392"", true);" & vbCrLf)
Response.Write("	    insHandImage(""A393"", true);" & vbCrLf)
Response.Write("	    insHandImage(""A391"", true);" & vbCrLf)
Response.Write("	    setPointer(""default"");" & vbCrLf)
Response.Write("	}" & vbCrLf)
Response.Write("} " & vbCrLf)
Response.Write("//-->" & vbCrLf)
Response.Write("</" & "SCRIPT>")

	
End Sub

'%inspreER005Upd: Se Actualiza el registro seleccionado en el Grid
'-------------------------------------------------------------------------------------------
Private Sub inspreER005Upd()
	'-------------------------------------------------------------------------------------------
	
Response.Write("" & vbCrLf)
Response.Write("<SCRIPT LANGUAGE=javascript>" & vbCrLf)
Response.Write("<!--" & vbCrLf)
Response.Write("//%insMaxId: Calcula el siguiente correlativo de componente a asignar" & vbCrLf)
Response.Write("//--------------------------------------------------------------------" & vbCrLf)
Response.Write("function insMaxId(){" & vbCrLf)
Response.Write("//--------------------------------------------------------------------" & vbCrLf)
Response.Write("    with (top.opener){" & vbCrLf)
Response.Write("        if (mintArrayCount>=0)" & vbCrLf)
Response.Write("            return parseInt(marrArray[mintArrayCount].tcnId) + 1;" & vbCrLf)
Response.Write("        else" & vbCrLf)
Response.Write("            return 1;" & vbCrLf)
Response.Write("    }" & vbCrLf)
Response.Write("}    " & vbCrLf)
Response.Write("//-->" & vbCrLf)
Response.Write("</" & "SCRIPT>" & vbCrLf)
Response.Write("")

	
	Dim lclsErr_Comp As eErrors.Err_Comp
	lclsErr_Comp = New eErrors.Err_Comp
	
	With Request
		If Request.QueryString.Item("Action") = "Del" Then
			Response.Write("<SCRIPT>top.opener.DeleteRecord(" & .QueryString.Item("Index") & ");</" & "Script>")
			Response.Write(mobjValues.ConfirmDelete(False))
			Response.Write(mobjValues.ConfirmDelete(True, "insConfirmDelete();top.close();"))
			If lclsErr_Comp.InsPostER005("Del", mobjValues.StringToType(mstrError, eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(mstrId, eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType("", eFunctions.Values.eTypeData.etdLong), "", "", mobjValues.StringToType("", eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType("", eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType("", eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdLong)) Then
			End If
		Else
			Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "ValErrors.aspx", "ER005", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
			
			'+Al agregar se calcula el siguiente correlativo
			If .QueryString.Item("Action") = "Add" Then
				Response.Write("<SCRIPT>self.document.forms[0].tcnId.value = insMaxId();</" & "Script>")
			End If
		End If
	End With
	
End Sub

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mobjGrid = New eFunctions.Grid

With Request
	mstrError = .QueryString.Item("nError") & ""
	mstrId = .QueryString.Item("nId") & ""
	mstrType = .QueryString.Item("nType") & ""
	mstrAction = .QueryString.Item("nMainAction")
End With

mobjValues.ActionQuery = Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery)

%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
	<META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">

<SCRIPT>
    //+ Variable para el control de versiones
    document.VssVersion = "$$Revision: 4 $|$$Date: 7/08/04 11:45a $|$$Author: Nsoler $"  
</SCRIPT>	
	
	<%Response.Write(mobjValues.StyleSheet())
Response.Write("<SCRIPT>var nMainAction = '" & mstrAction & "'</SCRIPT>")
%>
</HEAD>
<%If Request.QueryString.Item("Type") <> "PopUp" Then%>
<BODY ONUNLOAD="CancelErrors(true)">
<%Else%>
<BODY ONUNLOAD="closeWindows();">
<%End If%>

<FORM METHOD="post" ID="FORM" NAME="frmErroUpd" ACTION="valErrors.aspx?x=1">
<%Response.Write(mobjValues.ShowWindowsName("ER005"))

Call insDefineHeader()

If Request.QueryString.Item("Type") <> "PopUp" Then
	insPreER005()
Else
	inspreER005Upd()
End If

mobjMenu = Nothing
mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>












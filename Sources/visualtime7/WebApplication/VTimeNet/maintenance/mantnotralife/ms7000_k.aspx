<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eBranches" %>
<script language="VB" runat="Server">

'- Objetos genericos para manejo de valores, menu y grilla

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mobjGrid As eFunctions.Grid

'- Nombre de tabla general

Dim mstrTable As Object


'%insDefineHeader: Definición de columnas del Grid
'-----------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'-----------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	'+ Se definen las columnas del Grid
	
	With mobjGrid.Columns
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnInstitutionColumnCaption"), "tcnInstitution", 4, vbNullString, False, GetLocalResourceObject("tcnInstitutionColumnToolTip"))
		Call .AddTextColumn(0, GetLocalResourceObject("tctNameColumnCaption"), "tctName", 30, vbNullString, False, GetLocalResourceObject("tctNameColumnToolTip"))
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeTypeInstituColumnCaption"), "cbeTypeInstitu", "Table5634", eFunctions.Values.eValuesType.clngComboType, vbNullString, False,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeTypeInstituColumnToolTip"))
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeStatregtColumnCaption"), "cbeStatregt", "Table26", eFunctions.Values.eValuesType.clngComboType, vbNullString, False,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeStatregtColumnToolTip"))
		
		mobjGrid.Columns("cbeStatregt").TypeList = 2
		mobjGrid.Columns("cbeStatregt").List = "2"
	End With
	
	With mobjGrid
		.Columns("tcnInstitution").Disabled = Not (Request.QueryString.Item("Action") = "Add")
		
		If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Or Request.QueryString.Item("nMainAction") = vbNullString Then
			.Columns("Sel").GridVisible = False
			.ActionQuery = True
		End If
		
		.Codispl = Request.QueryString.Item("sCodispl")
		.Codisp = "MS7000"
		.sCodisplPage = "MS7000"
		.Columns("tctName").EditRecord = True
		.AddButton = True
		.DeleteButton = True
		.Height = 240
		.Width = 450
		.sDelRecordParam = "nInstitution=' + marrArray[lintIndex].tcnInstitution + '"
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% insPreMS7000: Muestra la grilla con datos
'--------------------------------------------------------------------------------------------------------------------
Private Sub insPreMS7000()
	'--------------------------------------------------------------------------------------------------------------------
	
Response.Write("" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("<SCRIPT>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("//% insPreZone: Define ubicacion de documento" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("function insPreZone(llngAction){" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("	switch (llngAction){" & vbCrLf)
Response.Write("	    case 301:" & vbCrLf)
Response.Write("	    case 302:" & vbCrLf)
Response.Write("	    case 401:" & vbCrLf)
Response.Write("	        document.location.href = document.location.href.replace(/&nMainAction.*/,'') + '&nMainAction=' + llngAction" & vbCrLf)
Response.Write("	        break;" & vbCrLf)
Response.Write("	}" & vbCrLf)
Response.Write("}" & vbCrLf)
Response.Write("</" & "SCRIPT>")

	
	Dim lintCount As Short
	Dim lobjObject As Object
	Dim lcolTab_Fn_Insts As eBranches.Tab_Fn_Insts
	
	lcolTab_Fn_Insts = New eBranches.Tab_Fn_Insts
	
	If lcolTab_Fn_Insts.Find() Then
		
		lintCount = 0
		
		For	Each lobjObject In lcolTab_Fn_Insts
			With lobjObject
				mobjGrid.Columns("tcnInstitution").DefValue = .nInstitution
				mobjGrid.Columns("tctName").DefValue = .sName
				mobjGrid.Columns("cbeTypeInstitu").DefValue = .nTypeInstitu
				mobjGrid.Columns("cbeStatregt").DefValue = .sStatregt
				
				Response.Write(mobjGrid.DoRow())
			End With
			
			lintCount = lintCount + 1
			
			If lintCount = 1000 Then
				Exit For
			End If
		Next lobjObject
	End If
	
	Response.Write(mobjGrid.closeTable())
	
	lcolTab_Fn_Insts = Nothing
	lobjObject = Nothing
End Sub

'% insPreMS7000Upd: Muestra ventana para actualizar registros
'-----------------------------------------------------------------------------------------
Private Sub insPreMS7000Upd()
	'-----------------------------------------------------------------------------------------
	Dim lclsTab_Fn_Inst As eBranches.Tab_Fn_Inst
	
        If Request.QueryString.Item("Action") = "Del" Then
		
            lclsTab_Fn_Inst = New eBranches.Tab_Fn_Inst
		
            If lclsTab_Fn_Inst.InsPostMS7000Upd("Del", CInt(Request.QueryString.Item("nInstitution")), eRemoteDB.Constants.intNull, " ", " ", mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), String.Empty, String.Empty, String.Empty) Then  'Argumentos agregados.
			
                Response.Write(mobjValues.ConfirmDelete())
                Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valMantNoTraLife.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("nMainAction"), , CShort(Request.QueryString.Item("Index"))))
            End If
            lclsTab_Fn_Inst = Nothing
        Else
            Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valMantNoTraLife.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("nMainAction"), , CShort(Request.QueryString.Item("Index"))))
        End If
    End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "MS7000"
%>

<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>

<%If Request.QueryString.Item("Type") <> "PopUp" Then%>
        <%	'$$EWI_1012:D:\VisualTIMEChile\Result\VTimeStep1\maintenance\mantnotralife\Vtime\Scripts\tMenu.js#%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
<%End If%>

<%=mobjValues.WindowsTitle(Request.QueryString.Item("sCodispl"))%>





<%
With Response
	.Write(mobjValues.StyleSheet())
	
	If Request.QueryString.Item("Type") <> "PopUp" Then
		mobjMenu = New eFunctions.Menues
		
		.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
		.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "MS7000.aspx", 1, ""))
		
		mobjMenu = Nothing
	End If
End With
%>

<SCRIPT>

//- Variable para el control de versiones

    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16:10 $|$$Author: Nvaplat61 $"

//% insCancel: Eejcuta accion de boton cancelar
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}

//%insStateZone: Activa controles
//------------------------------------------------------------------------------------------
function  insStateZone(){
//------------------------------------------------------------------------------------------
}

</SCRIPT>
</HEAD>

<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="MS7000" ACTION="valMantNoTraLife.aspx?mode=1">
<%
Call insDefineHeader()

If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<BR><BR>" & mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
	
	Call insPreMS7000()
Else
	Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
	
	Call insPreMS7000Upd()
End If

mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>







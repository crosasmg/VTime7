<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eBranches" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del grid de la página
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues


'% insDefineHeader : Configura las columnas del grid
'------------------------------------------------------------------------
Private Function insDefineHeader() As Object
	'------------------------------------------------------------------------
	'+ Se definen las columns del Grid
	With mobjGrid
		.Columns.AddNumericColumn(0, GetLocalResourceObject("nDetailArtColumnCaption"), "nDetailArt", 5, vbNullString,  , GetLocalResourceObject("nDetailArtColumnToolTip"))
		.Columns.AddTextColumn(0, GetLocalResourceObject("sDescriptColumnCaption"), "sDescript", 30, vbNullString,  , GetLocalResourceObject("sDescriptColumnToolTip"))
		.Columns.AddTextColumn(0, GetLocalResourceObject("sShort_desColumnCaption"), "sShort_des", 12, vbNullString,  , GetLocalResourceObject("sShort_desColumnToolTip"))
		.Columns.AddPossiblesColumn(0, GetLocalResourceObject("nActivityTypeColumnCaption"), "nActivityType", "Table7045", eFunctions.Values.eValuesType.clngComboType, CStr(eRemoteDB.Constants.intNull),  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("nActivityTypeColumnToolTip"))
		.Columns.AddPossiblesColumn(0, GetLocalResourceObject("nFamilyColumnCaption"), "nFamily", "Table7046", eFunctions.Values.eValuesType.clngComboType, CStr(eRemoteDB.Constants.intNull),  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("nFamilyColumnToolTip"))
		.Columns.AddPossiblesColumn(0, GetLocalResourceObject("sStatregtColumnCaption"), "sStatregt", "Table26", eFunctions.Values.eValuesType.clngComboType, CStr(eRemoteDB.Constants.intNull),  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("sStatregtColumnToolTip"))
		.Columns.AddButtonColumn(0, GetLocalResourceObject("SCA2-BColumnCaption"), "SCA2-B", eRemoteDB.Constants.intNull, True)
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 301 Then
			.AddButton = True
		Else
			.AddButton = False
		End If
		.Codispl = "MIN001"
		.sCodisplPage = "MIN001"
		.Columns("nDetailArt").EditRecord = True
		If Request.QueryString.Item("Type") = "PopUp" Then
			.Columns("btnNoteNum").bQuery = False
		End If
		If Request.QueryString.Item("Action") <> "Add" Then
			.Columns("nDetailArt").Disabled = True
		End If
		.Width = 700
		.Height = 250
		.FieldsByRow = 2
		.Top = 60
		.Left = 50
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.ActionQuery = Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery)
            .sEditRecordParam = "nActivity=" & Request.QueryString.Item("nActivity") 
		
            .sDelRecordParam = "nActivity=" & Request.QueryString.Item("nActivity") & "&nDetailArt=' + marrArray[lintIndex].nDetailArt + '"
	End With
	
	'+ Permite continuar si el check está marcado        
	If Request.QueryString.Item("Reload") = "1" Then
		mobjGrid.sReloadIndex = Request.QueryString.Item("ReloadIndex")
	End If
End Function

'%insPreMIN001Upd: Actualiza un Registro del Grid
'---------------------------------------------------------------------------------------
Private Sub inspreMIN001Upd()
	'---------------------------------------------------------------------------------------
	If Request.QueryString.Item("Action") = "Del" Then
		insDelItem()
	End If
	
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "ValMantFire.aspx", "MIN001", Request.QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(Request.QueryString.Item("Index"))))
	
	If Request.QueryString.Item("Action") = "Add" Then
		mobjGrid.Columns("sStatregt").DefValue = CStr(1)
		Response.Write("<SCRIPT>")
		Response.Write("insDefaultValues();")
		Response.Write("</" & "Script>")
	End If
	
	If Request.QueryString.Item("Action") <> "Del" Then
            Response.Write(mobjValues.HiddenControl("nActivity", Request.QueryString.Item("nActivity")))
            Response.Write("<SCRIPT>")
            Response.Write("self.document.forms[0].tcnNotenum.value=" & Request.QueryString.Item("nNotenum"))
            Response.Write("</" & "Script>")
	End If
End Sub
'%insPreMIN001: Muestra todos los registros asociados en el Grid
'---------------------------------------------------------------------------------------
Private Sub inspreMIN001()
	'---------------------------------------------------------------------------------------
	Dim lobjTab_in_bus As Object
	Dim lobjTab_in_buss As eBranches.tab_in_buss
	
	lobjTab_in_buss = New eBranches.tab_in_buss
	
        If lobjTab_in_buss.Find(CInt(Request.QueryString.Item("nActivity"))) Then
            Dim lintIndex As Integer = 0
            For Each lobjTab_in_bus In lobjTab_in_buss
                With mobjGrid
                    .Columns("nDetailArt").DefValue = lobjTab_in_bus.nDetailArt
                    .Columns("sDescript").DefValue = lobjTab_in_bus.sDescript
                    '+ Obtiene el valor de las notas..                    
                    .Columns("btnNotenum").nNotenum = lobjTab_in_bus.nNoteNum

                    .Columns("sShort_des").DefValue = lobjTab_in_bus.sShort_des
                    .Columns("sStatregt").DefValue = lobjTab_in_bus.sStatregt
                    .Columns("nFamily").DefValue = lobjTab_in_bus.nFamily
                    .Columns("nActivityType").DefValue = lobjTab_in_bus.nActivityType
                    '.Columns("tcnNotenum").DefValue = lobjTab_in_bus.nNoteNum
                    .sEditRecordParam = .sEditRecordParam & "&nNotenum=' + marrArray[" & lintIndex & "].btnNotenum + '"

                    Response.Write(.DoRow)
                End With
                lintIndex += 1
            Next lobjTab_in_bus
        End If
	Response.Write(mobjGrid.CloseTable())
	
	lobjTab_in_bus = Nothing
	lobjTab_in_buss = Nothing
End Sub
'%insDelItem: Elimina un Registro en particular
'---------------------------------------------------------------------------------------
Public Sub insDelItem()
	'---------------------------------------------------------------------------------------
	Dim lobjTab_in_bus As eBranches.Tab_in_bus
	lobjTab_in_bus = New eBranches.Tab_in_bus
	
        Response.Write(mobjValues.ConfirmDelete())
	
	Call lobjTab_in_bus.insPostMIN001("MIN001", Request.QueryString.Item("Action"), CInt(Request.QueryString.Item("nActivity")), CInt(Request.QueryString.Item("nDetailArt")), CStr(eRemoteDB.Constants.StrNull), eRemoteDB.Constants.intNull, CStr(eRemoteDB.Constants.StrNull), CStr(eRemoteDB.Constants.StrNull), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull)
	
	lobjTab_in_bus = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mobjGrid = New eFunctions.Grid

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
mobjValues.sCodisplPage = "MIN001"
%> 

<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


<SCRIPT LANGUAGE="JavaScript">
// insDefaultValues: muestra los valores por defecto
//--------------------------------------------------------------------------------------------
function insDefaultValues(){
//--------------------------------------------------------------------------------------------
//+ Se define la variable para almacenar el consecutivo más alto existente en el grid
    var llngMax = 0

//+ Se genera el número consecutivo de la imagen (el Nº consecutivo más alto +1)
	for(var llngIndex = 0;llngIndex<top.opener.marrArray.length;llngIndex++)
	    if(eval(top.opener.marrArray[llngIndex].nDetailArt)>llngMax)
	        llngMax = top.opener.marrArray[llngIndex].nDetailArt

//+ Se asignan los valores a los campos de la página	
	with (self.document.forms[0]){
		nDetailArt.value = ++llngMax;
	}
}
</SCRIPT>

<HTML>
<HEAD>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:03 $|$$Author: Iusr_llanquihue $"
</SCRIPT>
<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="frmMIN001" ACTION="valMantFire.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%
With Response
	.Write(mobjValues.WindowsTitle(Request.QueryString.Item("sCodispl")))
	.Write(mobjValues.StyleSheet())
End With

If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "MIN001", "MIN001.aspx"))
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
mobjMenu = Nothing

Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))

insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	inspreMIN001()
Else
	inspreMIN001Upd()
End If
mobjGrid = Nothing
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>






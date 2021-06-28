<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eAgent" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Se define la variable mobjGrid para el manejo del Grid de la ventana
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo de las zonas de la página
Dim mobjMenu As eFunctions.Menues

Dim mintCodeTab_Goals As Byte


'%insDefineHeader. Definición de columnas del GRID
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------        
	mobjGrid = New eFunctions.Grid
	
	'+Se definen todas las columnas del Grid
	With mobjGrid.Columns
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnCodeColumnCaption"), "tcnCode", 5, "", True, GetLocalResourceObject("tcnCodeColumnToolTip"), False, 0,  ,  ,  , True)
		Call .AddTextColumn(0, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", 30, "", True, GetLocalResourceObject("tctDescriptColumnToolTip"),  ,  ,  , False)
		Call .AddTextColumn(0, GetLocalResourceObject("tctshort_desColumnCaption"), "tctshort_des", 12, "", True, GetLocalResourceObject("tctshort_desColumnToolTip"),  ,  ,  , False)
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeStatregtColumnCaption"), "cbeStatregt", "Table26", eFunctions.Values.eValuesType.clngComboType, CStr(1),  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeStatregtColumnToolTip"))
	End With
	
	With mobjGrid
		.Codispl = Request.QueryString.Item("sCodispl")
		.Codisp = "MAG7780_k"
		.sCodisplPage = "MAG7780"
		.Top = 100
		.Height = 234
		.Width = 410
		.bOnlyForQuery = Request.QueryString.Item("nMainAction") = "401"
		.ActionQuery = (Request.QueryString.Item("nMainAction") = "401" Or IsNothing(Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = Not .ActionQuery
		.Columns("tcnCode").EditRecord = True
		'.Columns("tcnCode").Disabled = Request.querystring("Action") = "Update"
		.sDelRecordParam = "pnCode='+ marrArray[lintIndex].tcnCode + '"
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'%insPreMAG7780: Esta función se encarga de cargar los datos en la forma "Folder" 
'--------------------------------------------------------------------------------------------
Private Sub insPreMAG7780()
	'--------------------------------------------------------------------------------------------
	Dim lcoltab_goalss As eAgent.tab_goalss
	Dim lclstab_goals As Object
	
	lcoltab_goalss = New eAgent.tab_goalss
	With mobjGrid
		If lcoltab_goalss.Find() Then
			For	Each lclstab_goals In lcoltab_goalss
				.Columns("tctDescript").DefValue = lclstab_goals.sDescript
				.Columns("tcnCode").DefValue = lclstab_goals.nCode
				.Columns("tctshort_des").DefValue = lclstab_goals.sshort_des
				.Columns("cbeStatregt").DefValue = lclstab_goals.sStatRegt
				mintCodeTab_Goals = lclstab_goals.nCode
				Response.Write(mobjGrid.DoRow())
			Next lclstab_goals
		End If
	End With
	
	
Response.Write("" & vbCrLf)
Response.Write("		<TD>")


Response.Write(mobjValues.HiddenControl("tcnCodeTab_Goals", CStr(mintCodeTab_Goals + 1)))


Response.Write("</TD>")

	
	Response.Write(mobjGrid.CloseTable())
	Response.Write(mobjValues.BeginPageButton)
	lclstab_goals = Nothing
	lcoltab_goalss = Nothing
End Sub

'% insPreMAG7780Upd. Se define esta funcion para contruir el contenido de la ventana UPD de los archivos de datos particulares
'----------------------------------------------------------------------------------------------------------------------------
Private Sub insPreMAG7780Upd()
	'----------------------------------------------------------------------------------------------------------------------------
	Dim lclstab_goals As eAgent.tab_goals
	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			lclstab_goals = New eAgent.tab_goals
			Call lclstab_goals.insPostMAG7780_K(False, .QueryString.Item("sCodispl"), CInt(.QueryString.Item("nMainAction")), .QueryString.Item("Action"), Session("nUsercode"), "", mobjValues.StringToType(.QueryString.Item("pnCode"), eFunctions.Values.eTypeData.etdDouble), "", "")
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valMantAgent.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"),  , CShort(.QueryString.Item("Index"))))
		
Response.Write("		" & vbCrLf)
Response.Write("	<SCRIPT>" & vbCrLf)
Response.Write("		if(top.opener.document.forms[0].elements[""tcnCodeTab_Goals""].value!=0)" & vbCrLf)
Response.Write("			self.document.forms[0].tcnCode.value=(top.opener.document.forms[0].tcnCodeTab_Goals.value);" & vbCrLf)
Response.Write("	</" & "SCRIPT>")

		
	End With
	lclstab_goals = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = "MAG7780"
%>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:02 $"

//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
    return true;
}
//------------------------------------------------------------------------------------------
function insStateZone(){
//------------------------------------------------------------------------------------------      
    return true;
}
//-------------------------------------------------------------------------------------------------------------------
function insPreZone(llngAction){
//-------------------------------------------------------------------------------------------------------------------
    switch (llngAction){
        case 302:
        case 305:
        case 401:
	        document.location.href = document.location.href.replace(/&nMainAction.*/,'') + '&nMainAction=' + llngAction            
            break;
    }
}
</SCRIPT> 
<HTML>
<HEAD>
    <META NAME="GENERATOR" Content="eTransaction Designer for Visual TIME">
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<SCRIPT>var nMainAction = " & CShort("0" & Request.QueryString.Item("nMainAction")) & "</SCRIPT>")
End If
With Response
	.Write(mobjValues.StyleSheet())
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write("<SCRIPT LANGUAGE=""JavaScript"" SRC=""/VTimeNet/Scripts/tmenu.js""></SCRIPT>")
		mobjMenu = New eFunctions.Menues
		.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "MAG7780_k.aspx", 1, ""))
		mobjMenu = Nothing
	End If
End With
%>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<BR><BR>")
End If
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
%>
<FORM METHOD="post" ID="FORM" NAME="frmMAG7780" ACTION="valMantAgent.aspx?sTime=1">
<%
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMAG7780()
Else
	Call insPreMAG7780Upd()
End If
mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>







<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCoReinsuran" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues

'- Se define la variable mobjGrid para el manejo del Grid de la ventana
Dim mobjGrid As eFunctions.Grid

'- Se define la variable para la carga del Grid de la ventana 'CR760'
Dim mobjRetentionzones As eCoReinsuran.Retentionzones
'- Se define la variable en que se carga la colección
Dim mclsRetentionzone As eCoReinsuran.Retentionzone


'%insDefineHeader. Definición de columnas del GRID
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------	
	mobjGrid.sCodisplPage = "cr760"
	
	With mobjGrid
		.Codispl = "CR760"
		.Width = 320
		.Height = 200
		.Top = 170
	End With
	
	'+     
	'+Se definen todas las columnas del Grid
	With mobjGrid.Columns
		Call .AddPossiblesColumn(0, GetLocalResourceObject("tcnSeismiczoneColumnCaption"), "tcnSeismiczone", "Table7047", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , Request.QueryString.Item("Action") <> "Add",  , GetLocalResourceObject("tcnSeismiczoneColumnToolTip"))
		Call .AddNumericColumn(100657, GetLocalResourceObject("tcnRetentionColumnCaption"), "tcnRetention", 18,  ,  , GetLocalResourceObject("tcnRetentionColumnToolTip"), True, 6)
		
	End With
	
	With mobjGrid
		.Columns("tcnSeismiczone").BlankPosition = False
		.DeleteButton = True
		.AddButton = True
		.Columns("tcnSeismiczone").EditRecord = True
		If session("bQuery") Then
			.DeleteButton = False
			.AddButton = False
			.Columns("Sel").GridVisible = False
			.bOnlyForQuery = True
		End If
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		.sDelRecordParam = "tcnSeismiczone='+ marrArray[lintIndex].tcnSeismiczone + '&tcnRetention='+ marrArray[lintIndex].tcnRetention  + '"
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		
	End With
End Sub

'%insPreCR007: Esta función se encarga de cargar los datos en la forma "Folder" 
'--------------------------------------------------------------------------------------------
Private Sub insPreCR760()
	'--------------------------------------------------------------------------------------------
	
	Dim lblnFind As Boolean
	Dim lintCount As Object
	
	With mobjValues
		lblnFind = mobjRetentionzones.Find(.StringToType(session("nNumber"), eFunctions.Values.eTypeData.etdDouble), .StringToType(session("nBranch_rei"), eFunctions.Values.eTypeData.etdDouble), .StringToType(session("nType"), eFunctions.Values.eTypeData.etdDouble), .StringToType(session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
	End With
	For	Each mclsRetentionzone In mobjRetentionzones
		With mobjGrid
			.Columns("tcnSeismiczone").DefValue = CStr(mclsRetentionzone.nSeismiczone)
			.Columns("tcnRetention").DefValue = CStr(mclsRetentionzone.nRetention)
		End With
		Response.Write(mobjGrid.DoRow())
	Next mclsRetentionzone
	
	'Se llama a la propiedad CloseTable, para dar por finalizada la creación de la tabla (GRID)            	
	Response.Write(mobjGrid.CloseTable())
End Sub

'% insPreCR760Upd. Se define esta funcion para contruir el contenido de la ventana UPD de las Compañías partocipantes
'--------------------------------------------------------------------------------------------------------------------
Private Sub insPreCR760Upd()
	'--------------------------------------------------------------------------------------------------------------------		
	Dim lblnPost As Object
	Dim lintSel As Byte
	If Request.QueryString.Item("Action") = "Del" Then
		lintSel = 2
		Response.Write(mobjValues.ConfirmDelete())
		With Request
			Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valCoReinsuran.aspx", "CR760", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
		End With
		With Request
			lblnPost = mclsRetentionzone.InspostCR760Upd(.QueryString.Item("Action"), mobjValues.StringToType(.QueryString.Item("tcnSeismicZone"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("tcnRetention"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(session("nNumber"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(session("nBranch_rei"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(session("nType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
		End With
	Else
		With Request
			Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valCoReinsuran.aspx", "CR760", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
		End With
	End If
End Sub

</script>
<%Response.Expires = 0
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mobjGrid = New eFunctions.Grid
mobjRetentionzones = New eCoReinsuran.Retentionzones
mclsRetentionzone = New eCoReinsuran.Retentionzone

If Request.QueryString.Item("Type") <> "PopUp" Then
	With Response
		.Write(mobjMenu.setZone(2, "CR760", "CR760.aspx"))
		.Write("<SCRIPT>var nMainAction = top.frames['fraSequence'].plngMainAction</SCRIPT>")
	End With
	mobjGrid.ActionQuery = session("bQuery")
	mobjMenu = Nothing
End If

mobjValues.sCodisplPage = "cr760"

%>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
    <%=mobjValues.StyleSheet()%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmCR760" ACTION="valCoReinsuran.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%
Response.Write(mobjValues.ShowWindowsName("CR760"))

Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<TD><BR></TD>")
	Call insPreCR760()
Else
	Call insPreCR760Upd()
End If
%>
<SCRIPT>    
//+ Esta línea guarda la versión procedente de VSS 
   document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16.59 $"     
</SCRIPT>
</FORM>
</BODY>
</HTML>







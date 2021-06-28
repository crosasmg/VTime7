<%@ Page Language="VB" explicit="true"  Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false" %>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">

'^Begin Header Block VisualTimer Utility 1.1 27/05/2003 07:39:47 a.m.
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Se define la variable mobjGrid para el manejo del Grid de la ventana
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo de las zonas de la página
Dim mobjMenu As eFunctions.Menues


'%insDefineHeader: Se definen las columnas del grid
'------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	'+Se definen todas las columnas del Grid
	With mobjGrid.Columns
		Call .AddNumericColumn(17684,"Transporte", "tcnWay", 4, "", True,"Código del transporte amparado bajo la póliza", False, 0,  ,  ,  , True)
		Call .AddTextColumn(17685,"Matrícula/Nombre", "tctName_licen", 20, "", False,"Identifica completamente al medio de transporte",  ,  , "ChangeCase(this);", False)
		Call .AddTextColumn(17686,"Descripción", "tctDescript", 30, "", False,"Descripción breve del medio de transporte",  ,  ,  , False)
		If Request.QueryString.Item("Type") = "PopUp" Then
			Call .AddButtonColumn(17687,"Notas", "SCA2-T", 0, True, False)
		Else
			Call .AddButtonColumn(17687,"Notas", "SCA2-T", 0, True, True)
		End If
	End With
	
	With mobjGrid
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Codispl = "TR004"
		.Codisp = "TR004"
		.Top = 100
		.Height = 256
		.Width = 625
		.ActionQuery = mobjValues.ActionQuery
		.bOnlyForQuery = Request.QueryString.Item("nMainAction") = "401"
		.Columns("Sel").GridVisible = Not .ActionQuery
		.Columns("tcnWay").EditRecord = True
		'.Columns("tcnWay").Disabled = Request.querystring("Action") = "Update"
		.sDelRecordParam = "nWay='+ marrArray[lintIndex].tcnWay + '"
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'%insPreTR004. Se crea la ventana madre (Principal)
'------------------------------------------------------------------------------
Private Sub insPreTR004()
	'------------------------------------------------------------------------------
	Dim lcoltran_ways As ePolicy.tran_ways
	Dim lclstran_way As Object
	
	Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
	lcoltran_ways = New ePolicy.tran_ways
	With mobjGrid
		If lcoltran_ways.Find(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate")) Then
			For	Each lclstran_way In lcoltran_ways
				.Columns("tcnWay").DefValue = lclstran_way.nWay
				.Columns("tctName_licen").DefValue = lclstran_way.sName_licen
				.Columns("tctDescript").DefValue = lclstran_way.sDescript
				.Columns("btnNoteNum").nNotenum = lclstran_way.nNotenum
				Response.Write(mobjGrid.DoRow())
			Next lclstran_way
		End If
	End With
	
	Response.Write(mobjGrid.CloseTable())
	Response.Write(mobjValues.BeginPageButton)
	
	lclstran_way = Nothing
	lcoltran_ways = Nothing
End Sub

'% insPreTR004Upd. Se define esta funcion para contruir el contenido de la 
'%                     ventana UPD de los archivos de datos particulares
'------------------------------------------------------------------------------
Private Sub insPreTR004Upd()
	'------------------------------------------------------------------------------
	Dim lclstran_way As ePolicy.tran_way
	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
			Response.Write(mobjValues.ConfirmDelete())
			lclstran_way = New ePolicy.tran_way
			Call lclstran_way.InsPostTR004(False, .QueryString.Item("sCodispl"), CInt(.QueryString.Item("nMainAction")), .QueryString.Item("Action"), Session("nUsercode"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), mobjValues.StringToType(.QueryString.Item("nWay"), eFunctions.Values.eTypeData.etdInteger), "", "", 0)
		Else
			Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")) & "<BR>")
		End If
		If Request.QueryString.Item("Action") <> "Add" Then
			Response.Write("<SCRIPT>top.opener.top.fraSequence.UpdContent('" & Request.QueryString.Item("sCodispl") & "','1');</" & "Script>")
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valpolicyseq.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"),  , CShort(.QueryString.Item("Index"))))
	End With
	lclstran_way = Nothing
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
Call mobjNetFrameWork.BeginPage("TR004")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 27/05/2003 07:39:47 a.m.
mobjValues.sSessionID = Session.SessionID
mobjValues.sCodisplPage = "TR004"
'~End Body Block VisualTimer Utility

mobjValues.ActionQuery = (Request.QueryString.Item("nMainAction") = "401")
%> 

<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
  <HEAD>
	<META NAME="GENERATOR" Content="eTransaction Designer for Visual TIME">
<Script>
//-------------------------------------------------------------------------------------------
function ChangeCase(Field){
//-------------------------------------------------------------------------------------------
	with(self.document.forms[0]){
		tctName_licen.value = Field.value.toUpperCase();
	}
}
</Script>
<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write("<SCRIPT>var	nMainAction	= " & CShort("0" & Request.QueryString.Item("nMainAction")) & "</SCRIPT>")
	
	If Request.QueryString.Item("Type") <> "PopUp" Then
		mobjMenu = New eFunctions.Menues
		.Write(mobjMenu.setZone(2, "TR004", "TR004.aspx"))
		mobjMenu = Nothing
	End If
End With
%>
</HEAD>	  
<BODY ONUNLOAD="closeWindows();">      
 <FORM METHOD="POST"	ID="FORM" NAME="frmTR004" ACTION="valpolicyseq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreTR004()
Else
	Call insPreTR004Upd()
End If
mobjValues = Nothing
mobjGrid = Nothing
%>	  
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 27/05/2003 07:39:47 a.m.
Call mobjNetFrameWork.FinishPage("TR004")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>










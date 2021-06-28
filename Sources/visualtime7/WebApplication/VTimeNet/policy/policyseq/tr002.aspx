<%@ Page Language="VB" explicit="true"  Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false" %>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 31/3/03 19.42.05
Dim mobjNetFrameWork As eNetFrameWork.Layout

Dim mintCodeRoute As Object
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues
'~End Body Block VisualTimer Utility

'- Se define la variable mobjGrid para el manejo del Grid de la ventana
Dim mobjGrid As eFunctions.Grid


'%insDefineHeader. Definición de columnas del GRID
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------	
	With mobjGrid
		.Codispl = Request.QueryString.Item("sCodispl")
		.Width = 350
		.Height = 245
	End With
	
	'+Se definen todas las columnas del Grid
        With mobjGrid.Columns
            
            Call .AddNumericColumn(2567, GetLocalResourceObject("tcnRouteColumnCaption"), "tcnRoute", 4, "", True, GetLocalResourceObject("tcnRouteColumnToolTip"), False, 0, , , , True)
            Call .AddPossiblesColumn(2571, GetLocalResourceObject("cbeTypRouteColumnCaption"), "cbeTypRoute", "Table8003", eFunctions.Values.eValuesType.clngComboType, , , , , , , , 5, GetLocalResourceObject("cbeTypRouteColumnToolTip"), eFunctions.Values.eTypeCode.eNumeric)
            Call .AddPossiblesColumn(2572, GetLocalResourceObject("cbeTranspTypeColumnCaption"), "cbeTranspType", "Table6031", eFunctions.Values.eValuesType.clngComboType, , , , , , , , 2, GetLocalResourceObject("cbeTranspTypeColumnToolTip"), eFunctions.Values.eTypeCode.eNumeric)
		
            If Request.QueryString.Item("Type") = "PopUp" Then
                
                If CInt(Request.QueryString.Item("nNotenum")) <> eRemoteDB.Constants.intNull And Request.QueryString.Item("Action") <> "Add" Then
                    .AddButtonColumn(2568, GetLocalResourceObject("btnNotenumColumnCaption"), "SCA2-T", CDbl(Request.QueryString.Item("nNotenum")), True, False, , , , , "btnNotenum")
                Else
				
                    .AddButtonColumn(2568, GetLocalResourceObject("btnNotenumColumnCaption"), "SCA2-T", 0, True, False, , , , , "btnNotenum")
                End If
            Else
                .AddButtonColumn(2568, GetLocalResourceObject("btnNotenumColumnCaption"), "SCA2-T", 0, True, True)
            End If
		
            Call .AddHiddenColumn("tcnSel", CStr(0))
            Call .AddHiddenColumn("tctExist", "1")
            Call .AddHiddenColumn("sParam", vbNullString)
        End With
	
	With mobjGrid
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Codispl = "TR002"
		.Codisp = "TR002"
		.Top = 100
		.Height = 320
		.Width = 640
		.ActionQuery = mobjValues.ActionQuery
		.bOnlyForQuery = Request.QueryString.Item("nMainAction") = "401"
		.Columns("Sel").GridVisible = Not .ActionQuery
		.Columns("tcnRoute").EditRecord = True
		'.Columns("tcnRoute").Disabled = Request.querystring("Action") = "Update"
		.sDelRecordParam = "nRoute='+ marrArray[lintIndex].tcnRoute + '"
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		
		.Columns("Sel").Title = "Sel"
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
	End With
End Sub

'%insPreTR002: Esta función se encarga de cargar los datos en la forma "Folder" 
'--------------------------------------------------------------------------------------------
Private Sub insPreTR002()
	'--------------------------------------------------------------------------------------------
	Dim lcoltran_routes As ePolicy.tran_routes
	Dim lclstran_route As Object
	
	lcoltran_routes = New ePolicy.tran_routes
	
	If lcoltran_routes.Find(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
		For	Each lclstran_route In lcoltran_routes
			With mobjGrid
				.Columns("tcnRoute").DefValue = lclstran_route.nRoute
				.Columns("cbeTypRoute").DefValue = lclstran_route.nTypRoute
				.Columns("cbeTranspType").DefValue = lclstran_route.nTranspType
				.Columns("btnNoteNum").nNoteNum = lclstran_route.nNoteNum
				.sEditRecordParam = "nNotenum=" & lclstran_route.nNoteNum
				Response.Write(mobjGrid.DoRow())
			End With
		Next lclstran_route
	End If
	
	'Se llama a la propiedad CloseTable, para dar por finalizada la creación de la tabla (GRID)            	
	Response.Write(mobjGrid.CloseTable())
	Response.Write(mobjValues.BeginPageButton)
	
	
Response.Write("" & vbCrLf)
Response.Write("		<TD>")


Response.Write(mobjValues.HiddenControl("tcnCodeRoute", mintCodeRoute + 1))


Response.Write("</TD>")

	
	
	lclstran_route = Nothing
	lcoltran_routes = Nothing
End Sub

'% insPreTR002Upd. Se define esta funcion para contruir el contenido de la ventana UPD de los abonos de anticipos
'------------------------------------------------------------------------------------------------------------------
Private Sub insPreTR002Upd()
	'------------------------------------------------------------------------------------------------------------------		
	Dim lclstran_route As ePolicy.tran_route
	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			lclstran_route = New ePolicy.tran_route
			Call lclstran_route.InsPostTR002(False, .QueryString.Item("sCodispl"), CInt(.QueryString.Item("nMainAction")), .QueryString.Item("Action"), Session("nUsercode"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), mobjValues.StringToType(.QueryString.Item("nRoute"), eFunctions.Values.eTypeData.etdInteger), 0, 0, 0)
		End If
		If Request.QueryString.Item("Action") <> "Add" Then
			Response.Write("<SCRIPT>top.opener.top.fraSequence.UpdContent('" & Request.QueryString.Item("sCodispl") & "','1');</" & "Script>")
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valPolicyseq.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"),  , CShort(.QueryString.Item("Index"))))
	End With
	lclstran_route = Nothing
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("TR002")
'~End Header Block VisualTimer Utility
Response.CacheControl = "private"
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.05
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.05
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
mobjGrid = New eFunctions.Grid
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.05
mobjGrid.sSessionID = Session.SessionID
mobjGrid.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjGrid.sCodisplPage = Request.QueryString.Item("sCodispl")
Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))


If Request.QueryString.Item("Type") <> "PopUp" Then
	With Response
		.Write(mobjMenu.setZone(2, "TR002", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
		.Write("<SCRIPT>var nMainAction = top.frames['fraSequence'].plngMainAction</SCRIPT>")
	End With
	mobjGrid.ActionQuery = Session("bQuery")
	mobjMenu = Nothing
End If

%>

<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
    <%=mobjValues.StyleSheet()%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmTR002" ACTION="ValPolicySeq.aspx?x=1">
<%
Response.Write(mobjValues.ShowWindowsName("TR002", Request.QueryString.Item("sWindowDescript")))

Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreTR002()
Else
	Call insPreTR002Upd()
End If

mobjValues = Nothing
mobjGrid = Nothing

%>
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 27/05/2003 07:39:47 a.m.
Call mobjNetFrameWork.FinishPage("TR002")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>









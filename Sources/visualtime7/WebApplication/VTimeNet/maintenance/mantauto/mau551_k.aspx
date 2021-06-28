<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eBranches" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Se define la variable mobjGrid para el manejo del Grid de la ventana
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo de las zonas de la página
Dim mobjMenu As eFunctions.Menues


'%insDefineHeader. Definición de columnas del GRID
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------        
	mobjGrid = New eFunctions.Grid
	
	'+Se definen todas las columnas del Grid
	With mobjGrid.Columns
		Call .AddTextColumn(0, GetLocalResourceObject("tctSerieColumnCaption"), "tctSerie", 4, "", True, GetLocalResourceObject("tctSerieColumnToolTip"),  ,  ,  , False)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnDigit7ColumnCaption"), "tcnDigit7", 5, "", True, GetLocalResourceObject("tcnDigit7ColumnToolTip"), False, 0,  ,  ,  , False)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnDigit6ColumnCaption"), "tcnDigit6", 5, "", True, GetLocalResourceObject("tcnDigit6ColumnToolTip"), False, 0,  ,  ,  , False)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnDigit5ColumnCaption"), "tcnDigit5", 5, "", True, GetLocalResourceObject("tcnDigit5ColumnToolTip"), False, 0,  ,  ,  , False)
	End With
	
	With mobjGrid
		.Codispl = "MAU551"
		.Codisp = "MAU551_K"
		.sCodisplPage = "MAU551"
		.Top = 100
		.Height = 256
		.Width = 300
		.bOnlyForQuery = Request.QueryString.Item("nMainAction") = "401"
		.ActionQuery = (Request.QueryString.Item("nMainAction") = "401" Or IsNothing(Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = Not .ActionQuery
		.Columns("tctSerie").EditRecord = True
		.Columns("tctSerie").TypeList = CShort("1")
		.Columns("tctSerie").List = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,Ñ,O,P,Q,R,S,T,V,U,W,X,Y,Z,a,b,c,d,e,f,g,h,i,j,k,l,m,n,ñ,o,p,q,r,s,t,u,v,w,x,y,z"
		.Columns("tctSerie").Disabled = Request.QueryString.Item("Action") = "Update"
		.sDelRecordParam = "sSerie=' + marrArray[lintIndex].tctSerie + '"
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'%insPreMAU551: Esta función se encarga de cargar los datos en la forma "Folder" 
'--------------------------------------------------------------------------------------------
Private Sub insPreMAU551()
	'--------------------------------------------------------------------------------------------
	Dim lcolSeriess As eBranches.Seriess
	Dim lclsSeries As Object
	
	lcolSeriess = New eBranches.Seriess
	With mobjGrid
		If lcolSeriess.Find() Then
			For	Each lclsSeries In lcolSeriess
				.Columns("tctSerie").DefValue = lclsSeries.sSerie
				.Columns("tcnDigit7").DefValue = lclsSeries.ndigit7
				.Columns("tcnDigit6").DefValue = lclsSeries.ndigit6
				.Columns("tcnDigit5").DefValue = lclsSeries.ndigit5
				Response.Write(mobjGrid.DoRow())
			Next lclsSeries
		End If
	End With
	
	Response.Write(mobjGrid.CloseTable())
	lclsSeries = Nothing
	lcolSeriess = Nothing
End Sub

'% insPreMAU551Upd. Se define esta funcion para contruir el contenido de la ventana UPD de los archivos de datos particulares
'----------------------------------------------------------------------------------------------------------------------------
Private Sub insPreMAU551Upd()
	'----------------------------------------------------------------------------------------------------------------------------
	Dim lclsSeries As eBranches.Series
	With Request
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			lclsSeries = New eBranches.Series
			Call lclsSeries.InsPostMAU551(.QueryString.Item("Action"), .QueryString.Item("sSerie"), 0, 0, 0, Session("nUsercode"))
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valMantAuto.aspx", "MAU551", .QueryString.Item("nMainAction"),  , CShort(.QueryString.Item("Index"))))
	End With
	lclsSeries = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
mobjValues.sCodisplPage = "MAU551"
%>
<HTML>
<HEAD>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:02 $|$$Author: Iusr_llanquihue $"
</SCRIPT>
<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<%If Request.QueryString.Item("Type") <> "PopUp" Then%>
    <%	'$$EWI_1012:D:\VisualTIMEChile\Result\VTimeStep1\maintenance\mantauto\Vtime\Scripts\tMenu.js#%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
<%End If%>    
<SCRIPT LANGUAGE=JavaScript>
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
//% insPreZone: Modifica variable nMainAction en href
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
<%
Response.Write(mobjValues.StyleSheet())

If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.MakeMenu("MAU551", "MAU551_K.aspx", 1, vbNullString))
	
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If

%>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<%If Request.QueryString.Item("Type") <> "PopUp" Then%>
<BR><BR>
<%End If%>
<FORM METHOD="post" ID="FORM" NAME="frmMAU551" ACTION="valMantAuto.aspx?sTime=1">
<%
Response.Write(mobjValues.ShowWindowsName("MAU551"))
Call insDefineHeader()

If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMAU551()
Else
	Call insPreMAU551Upd()
End If
mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>







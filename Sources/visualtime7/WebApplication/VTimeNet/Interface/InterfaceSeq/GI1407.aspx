<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eSchedule" %>
<script language="VB" runat="Server"> 

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del grid de la página
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo particular de los datos de la página
Dim mcolClass As Object


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		.AddHiddenColumn("hddKey", "")
		.AddTextColumn(0, GetLocalResourceObject("tctSheetColumnCaption"), "tctSheet", 50, "",  , GetLocalResourceObject("tctSheetColumnToolTip"))
		.AddTextColumn(0, GetLocalResourceObject("tctStatusColumnCaption"), "tctStatus", 50, "",  , GetLocalResourceObject("tctStatusColumnToolTip"))
		.AddTextColumn(0, GetLocalResourceObject("tctOutputFileColumnCaption"), "tctOutputFile", 100, "",  , GetLocalResourceObject("tctOutputFileColumnToolTip"))
		.AddTextColumn(0, GetLocalResourceObject("tctViewInterfaceColumnCaption"), "tctViewInterface", 10, GetLocalResourceObject("tctViewInterfaceColumnCaption"),  , GetLocalResourceObject("tctViewInterfaceColumnToolTip"))
	End With
	
	'+ Se definen las propiedades generales del grid
	
	With mobjGrid
		.Codispl = "GI1407"
		.sCodisplPage = "GI1407"
		.AddButton = False
		.DeleteButton = False
		.ActionQuery = mobjValues.ActionQuery
		.Height = 500
		.Width = 1000
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = False
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
	
End Sub

'% insPreCodispl: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreGI1407()
	'--------------------------------------------------------------------------------------------
	Dim lclsJobs As eSchedule.Batch_job
	Dim lintCount As Double
	
	lclsJobs = New eSchedule.Batch_job
	
	If lclsJobs.Find_Interface_Batch_Job(Session("sKey")) Then

		lintCount = 1
		While lclsJobs.ItemBatchJob(lintCount)
                Session("sdirout")= lclsJobs.sDirOut
			With mobjGrid
				.Columns("hddKey").DefValue = lclsJobs.sKey
				.Columns("tctSheet").DefValue = lclsJobs.sSheet
				.Columns("tctOutputFile").DefValue = lclsJobs.sOutputFile
				.Columns("tctStatus").DefValue = lclsJobs.sStatus
				If lclsJobs.sView_Interface = "1" And lclsJobs.sOutputFile <> "" Then
					.Columns("tctViewInterface").DefValue = "Ver Archivo"
					.Columns("tctViewInterface").HRefScript = "showFile('" & lclsJobs.sDirOut & "', '" & lclsJobs.sOutputFile & "')"
                                        
				Else
					.Columns("tctViewInterface").DefValue = ""
					.Columns("tctViewInterface").HRefScript = ""
                    End If
				Response.Write(.DoRow)
			End With
			lintCount = lintCount + 1
		End While
	End If
	lclsJobs = Nothing
	
	Response.Write(mobjGrid.closeTable())
End Sub

</script>
<%Response.Expires = 0

Response.Buffer = False
Server.ScriptTimeOut = 900

mobjValues = New eFunctions.Values

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401

%>
<html>
<head>
	<meta NAME="GENERATOR" Content="Microsoft FrontPage 5.0">
<%Response.Write(mobjValues.StyleSheet())%>	




<script LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></script>

<script LANGUAGE="JavaScript">
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 6 $|$$Date: 21-09-09 0:15 $|$$Author: Mpalleres $"
    
    function showFile(sDirOut, sOutputFile)
    {
		var arrFile = sOutputFile.split(".");
		//var url    = sDirOut + sOutputFile;
                var url    = sOutputFile;
		if (arrFile[1] == 'xls')  url = 'getdata.aspx?file='+ url;
		if (arrFile[1] == 'xlsx') url = 'getdata.aspx?file=' + url;
		if (arrFile[1] == 'txt') url = 'getdata.aspx?file=' + url;
		if (arrFile[1] == 'pdf') url = 'getdata.aspx?file=' + url;
		if (arrFile[1] == 'xml') url = 'getdata.aspx?file=' + url;
		ShowPopUp(url,  arrFile[0], 500 , 500, 'Yes', 'Yes', 200, 100, 'no', 'Yes');

    }
    </script>
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	mobjMenu = New eFunctions.Menues
	Response.Write(mobjMenu.setZone(2, "GI1407", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
	mobjMenu = Nothing
End If
%>
</head>
<body ONUNLOAD="closeWindows();">
<%Response.Write(mobjValues.ShowWindowsName("GI1407", Request.QueryString.Item("sWindowDescript")))%>
<form METHOD="POST" NAME="GI1407" ACTION="valBatch.aspx?sMode=2">
<%
Call insDefineHeader()
Call insPreGI1407()
%>
</form> 
</body>
</html>






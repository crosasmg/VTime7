<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eLetter" %>
<%@ Import namespace="eRemoteDB" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 09/05/2003 10:50:01 a.m.
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'**- Object for the handling of the general functions of load of values.	
'- Objeto para el manejo de	las	funciones generales	de carga de	valores.

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mobjGrid As eFunctions.Grid


'**% insDefineHeader: The columns of the grid of the "Parameters" section are defined.
'%insDefineHeader: Se definen las columnas del grid de la sección "Parameters" .
'---------------------------------------------------------------------------
Private Sub insDefineHeader()
	'---------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 09/05/2003 10:50:01 a.m.
	mobjGrid.sSessionID = Session.SessionID
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = "MLT001"
	
	
	With mobjGrid.Columns
		Call .AddTextColumn(7340,"Descripción", "tctDescript", 30, vbNullString,,"Descripción del contenido de la variable")
		Call .AddHiddenColumn("tcnParam", CStr(0))
		Call .AddHiddenColumn("tctCheck", "2")
	End With
	
	With mobjGrid
		.sArrayName = "marrArray"
		.AddButton = False
		.DeleteButton = False
		.Columns("Sel").OnClick = "MarkRecordMLT001(this)"
		
		If Request.QueryString.Item("nMainAction") = "401" Then
			.bOnlyForQuery = True
			.Columns("Sel").Disabled = True
		End If
		Call .SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	End With
End Sub

'**% insPreMLT001: This procedure to show in the "Parameters" section the read information.
'% insPreMLT001: Esta procedimiento permite mostrar en la sección "Parameters"
'% la información leída.
'---------------------------------------------------------------------------
Private Sub insPreMLT001()
	'---------------------------------------------------------------------------
	Dim lobjQuery As eRemoteDB.Query
	Dim lclsGroupParams As eLetter.GroupParams
	Dim lstrParameters As String
	
	lclsGroupParams = New eLetter.GroupParams
	Call lclsGroupParams.Find(Session("nGroup"))
	
	
Response.Write("" & vbCrLf)
Response.Write("<SCRIPT>" & vbCrLf)
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
Response.Write("" & vbCrLf)
Response.Write("//**% MarkRecordMLT001: This function selected or deselected the records " & vbCrLf)
Response.Write("//**% in the ""Parameters"" section." & vbCrLf)
Response.Write("//% MarkRecordMLT001: Esta función permite marcar o desmarcar los registros" & vbCrLf)
Response.Write("//% en la sección ""Parameters""." & vbCrLf)
Response.Write("//--------------------------------------------------------------------" & vbCrLf)
Response.Write("function MarkRecordMLT001(Field){" & vbCrLf)
Response.Write("//--------------------------------------------------------------------" & vbCrLf)
Response.Write("    var lintIndex=0" & vbCrLf)
Response.Write("    with (document.forms[0]){" & vbCrLf)
Response.Write("		if (tctCheck.length>0){" & vbCrLf)
Response.Write("			tctCheck[Field.value].value=(Field.checked?""1"":""2"")" & vbCrLf)
Response.Write("			tctParameters.value = """"" & vbCrLf)
Response.Write("		    for (lintIndex=0;lintIndex<tctCheck.length;lintIndex++)" & vbCrLf)
Response.Write("				tctParameters.value = tctParameters.value + (Sel[lintIndex].checked?""1"":""2"")" & vbCrLf)
Response.Write("		        " & vbCrLf)
Response.Write("		}" & vbCrLf)
Response.Write("		else " & vbCrLf)
Response.Write("		{" & vbCrLf)
Response.Write("			tctCheck.value=(Field.checked?""1"":""2"")" & vbCrLf)
Response.Write("			tctParameters.value = (Field.checked?""1"":""2"")" & vbCrLf)
Response.Write("		}" & vbCrLf)
Response.Write("	}" & vbCrLf)
Response.Write("}" & vbCrLf)
Response.Write("</" & "SCRIPT>" & vbCrLf)
Response.Write("  <P ALIGN=""CENTER"">" & vbCrLf)
Response.Write("	<LABEL ID=7341><a HREF=""#Parámetros"">Parámetros</a></LABEL><LABEL ID=0> |</LABEL>" & vbCrLf)
Response.Write("	<LABEL ID=7342><a HREF=""#Variables"">Variables</a></LABEL>" & vbCrLf)
Response.Write("  </P>" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=""2"" CLASS=""HighLighted""><LABEL ID=7343><A NAME=""Parámetros"">Parámetros</A></LABEL></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("		  <TD COLSPAN=2><HR></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("        <TD WIDTH=150pcx><LABEL ID=7344>Descripción del grupo</LABEL></TD>" & vbCrLf)
Response.Write("        <TD> ")


Response.Write(mobjValues.TextControl("tctDescript", 30, lclsGroupParams.sDescript, False,"Descripción del grupo",  ,  ,  ,  , Request.QueryString.Item("nMainAction") = "401"))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("        <TD COLSPAN=2>")

	
	lobjQuery = New eRemoteDB.Query
	
	'**+ The table is read with identificativo 622, to obtain the parameters of the group 
	'**+ of correspondence.
	'+ Se lee la tabla con identificativo 622, para conseguir los Parámetros del grupo 
	'+ de correspondencia
	
	If lobjQuery.OpenQuery("Table622",  ,  , "nParameters") Then
		lstrParameters = vbNullString
		
		'**+ All the elements of recordset are readed to show the associate groups.
		'+ Se leen todos los elementos del recordset, para mostrar los grupos asociados
		
		Do While Not lobjQuery.EndQuery
			With mobjGrid
				.Columns("tctDescript").defvalue = lobjQuery.FieldToClass("sDescript")
				.Columns("tcnParam").defvalue = lobjQuery.FieldToClass("nParameters")
				
				'Response.Write "<NOTSCRIPT>alert('sDescript " & lobjQuery.FieldToClass("sDescript") & lobjQuery.FieldToClass("nParameters") & " ')</" & "Script>"
				
				If lclsGroupParams.ExistsParam(lobjQuery.FieldToClass("nParameters")) Then
					.Columns("Sel").checked = Cstr(1)
					lstrParameters = lstrParameters & "1"
				Else
					.Columns("Sel").checked = Cstr(2)
					lstrParameters = lstrParameters & "2"
				End If
				
				Response.Write(.doRow)
			End With
			
			lobjQuery.NextRecord()
		Loop
		lobjQuery.CloseQuery() 
	End If
	
	Response.Write(mobjGrid.closeTable() & "<BR> " & mobjValues.HiddenControl("tctParameters", lstrParameters))
	lobjQuery = Nothing
	
	
Response.Write("" & vbCrLf)
Response.Write("		</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=""2"" CLASS=""HighLighted""><LABEL ID=7345><A NAME=""Variables"">Variables</A></LABEL></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("		  <TD COLSPAN=2><HR></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("        <TD COLSPAN=2>")

	
	
	'**+ The insPreVariables function is executed to show the values in the grid.
	'+ Se ejecuta la funcion insPreVariables, para mostrar los valores en el grid.
	
	Call insPreVariables()
	
Response.Write("" & vbCrLf)
Response.Write("    </TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    </TABLE>")

	
	Response.Write("<SCRIPT>insShowHeader('" & Session("nGroup") & "','" & lclsGroupParams.sDescript & "')</" & "Script>" & mobjValues.BeginPageButton)
	lclsGroupParams = Nothing
End Sub

'**% insPreVariables: This procedure to show the information in the "Variables" section. 
'% insPreVariables: Este procedimiento permite mostrar los datos en la sección de "Variables".
'---------------------------------------------------------------------------
Private Sub insPreVariables()
	'---------------------------------------------------------------------------
	Dim lcolGroupVariables As eLetter.GroupVariabless
	Dim lclsGroupVar As Object
	
	Call insDefineHeaderVar()
	
	lcolGroupVariables = New eLetter.GroupVariabless
	
	If lcolGroupVariables.Find(Session("nGroup")) Then
		For	Each lclsGroupVar In lcolGroupVariables
			mobjGrid.Columns("tctVariable").defvalue = lclsGroupVar.sVariable
			mobjGrid.Columns("tctDescript").defvalue = lclsGroupVar.sDescript
			If lclsGroupVar.nTypVariable = 1 Then
				mobjGrid.Columns("chkTypVariable").checked = Cstr(1)
			Else
				mobjGrid.Columns("chkTypVariable").checked = Cstr(0)
			End If
			mobjGrid.Columns("tctTableName").defvalue = lclsGroupVar.sTablename
			mobjGrid.Columns("tctColumName").defvalue = lclsGroupVar.sColumName
			mobjGrid.Columns("tctAliasTable").defvalue = lclsGroupVar.sAliasTable
			mobjGrid.Columns("tctAliasColumn").defvalue = lclsGroupVar.sAliasColumn
			Response.Write(mobjGrid.doRow())
		Next lclsGroupVar
	End If
	
	lcolGroupVariables = Nothing
	
	Response.Write(mobjGrid.closeTable())
End Sub

'% insDefineHeaderVar: This function defined the columns of the grid of the Variables section.
'% insDefineHeaderVar: Esta funcion define las columnas del grid de la sección de Variables.
'---------------------------------------------------------------------------
Private Sub insDefineHeaderVar()
	'---------------------------------------------------------------------------
	mobjGrid= New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 09/05/2003 10:50:01 a.m.
	mobjGrid.sSessionID = Session.SessionID
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = "MLT001"
	
	
	With mobjGrid.Columns
		Call .AddTextColumn(7346,"Variable", "tctVariable", 12, vbNullString, False,"Código de la variable en cuestión",  ,  ,  , False)
		
		If UCase(Request.QueryString.Item("Action")) <> "ADD" Then
			mobjGrid.Columns("tctVariable").Disabled = True
		End If
		
		Call .AddTextColumn(7340,"Descripción", "tctDescript", 30, vbNullString, False,"Descripción del contenido de la variable",  ,  ,  , False)
		Call .AddCheckColumn(7347,"Variable del sistema", "chkTypVariable", "", False, CStr(1),  , False,"Indicador de si es o no una variable del sistema")
		
		If Request.QueryString.Item("Type") <> "PopUp" Then
			mobjGrid.Columns("chkTypVariable").Disabled = True
		Else
			mobjGrid.Columns("chkTypVariable").Disabled = False
			mobjGrid.Columns("chkTypVariable").OnClick = "insTypVariableClick(this)"
		End If
		
		Call .AddTextColumn(7348,"Tabla", "tctTableName", 20, vbNullString, False,"Nombre de la tabla donde la información manejada por la variable será encontrada",  ,  , "insAliasText(1,this.value);")
		Call .AddTextColumn(7349,"Columna", "tctColumName", 30, vbNullString, False,"Nombre de la columna en la tabla donde la información manejada por la variable esta registrada",  ,  , "insAliasText(2,this.value);")
		
		If Request.QueryString.Item("Type") = "PopUp" Then
			Call .AddTextColumn(7350,"Alias de la tabla", "tctAliasTable", 30, vbNullString,  ,"Nombre usado para referirse a la tabla donde la información esta registrada",  ,  , "insAliasText(1,this.value);")
			Call .AddTextColumn(7351,"Alias de la columna", "tctAliasColumn", 30, vbNullString,  ,"Nombre usado para referirse a la columna donde la información esta registrada",  ,  , "insAliasText(2,this.value);")
		Else
			Call .AddHiddenColumn("tctAliasTable", vbNullString)
			Call .AddHiddenColumn("tctAliasColumn", vbNullString)
		End If
		Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	End With
	
	With mobjGrid
		.sArrayName = "marrArray2"
		.Codispl = "MLT001"
		.Height = 290
		.Width = 400
		.Columns("Sel").OnClick = "MarkRecordmarrArray2(this)"
		
		If Request.QueryString.Item("nMainAction") <> CStr(eFunctions.Menues.TypeActions.clngActionQuery) Then
			.Columns("tctVariable").EditRecord = True
		End If
		
		.sEditRecordParam = "sParameters=' + document.forms[0].tctParameters.value + '&sLettDescript='+ document.forms[0].tctDescript.value  + '"
		.sDelRecordParam = "sVariable=' + marrArray2[lintIndex].tctVariable + '"
		.sReloadAction = Request.QueryString.Item("ReloadAction")
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		
		If Request.QueryString.Item("nMainAction") = "401" Then
			.DeleteButton = False
			.AddButton = False
			.bOnlyForQuery = True
			.Columns("Sel").Disabled = True
		End If
	End With
End Sub

'**% insPreMLT001Upd: This procedure calls to the PopUp.
'% insPreMLT001Upd: Este procedimiento llama a la PopUp.
'---------------------------------------------------------------------------
Private Sub insPreMLT001Upd()
	'---------------------------------------------------------------------------
	Dim lobjGroupVariables As eLetter.GroupVariables
	If UCase(Request.QueryString.Item("Action")) <> "DEL" Then
		
Response.Write("" & vbCrLf)
Response.Write("<SCRIPT>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("//**% insTypVariableClick: This function enabled or disabled the columns ""Table and column"". " & vbCrLf)
Response.Write("//% insTypVariableClick: Esta función permite habilitar o inhabilitar las columnas" & vbCrLf)
Response.Write("//% ""Tabla y columna""." & vbCrLf)
Response.Write("//---------------------------------------------------------------------------" & vbCrLf)
Response.Write("function insTypVariableClick(Field){" & vbCrLf)
Response.Write("//---------------------------------------------------------------------------" & vbCrLf)
Response.Write("    with (self.document.forms[0]){" & vbCrLf)
Response.Write("		tctTableName.disabled = Field.checked;" & vbCrLf)
Response.Write("		tctColumName.disabled = Field.checked;" & vbCrLf)
Response.Write("		tctAliasTable.disabled = Field.checked;" & vbCrLf)
Response.Write("		tctAliasColumn.disabled = Field.checked;" & vbCrLf)
Response.Write("	if (Field.checked)" & vbCrLf)
Response.Write("		{" & vbCrLf)
Response.Write("			tctTableName.value = '';" & vbCrLf)
Response.Write("			tctColumName.value = '';" & vbCrLf)
Response.Write("			tctAliasTable.value = '';" & vbCrLf)
Response.Write("			tctAliasColumn.value = '';" & vbCrLf)
Response.Write("		}" & vbCrLf)
Response.Write("    }" & vbCrLf)
Response.Write("}" & vbCrLf)
Response.Write("</" & "SCRIPT>")

If UCase(Request.QueryString.Item("Action")) = "UPDATE" Then
    mobjGrid.Columns("tctTableName").Disabled = True
    mobjGrid.Columns("tctColumName").Disabled = True
End If
		
	Else
		lobjGroupVariables = New eLetter.GroupVariables
		
		Call lobjGroupVariables.insPostMLT001(Request.QueryString.Item("Action"), Session("nGroup"), CStr(eRemoteDB.Constants.strNull), CStr(eRemoteDB.Constants.strNull), Request.QueryString.Item("sVariable"), CStr(eRemoteDB.Constants.strNull), CStr(eRemoteDB.Constants.strNull), CStr(eRemoteDB.Constants.strNull), eRemoteDB.Constants.intNull, Session("nUsercode"), CStr(eRemoteDB.Constants.strNull), CStr(eRemoteDB.Constants.strNull))
		
		Response.Write(mobjValues.ConfirmDelete())
		
		lobjGroupVariables = Nothing
	End If
	
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valMantLetter.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("nMainAction"),  , CShort(Request.QueryString.Item("Index"))) & mobjValues.HiddenControl("tctParameters", Request.QueryString.Item("sParameters")) & mobjValues.HiddenControl("tctLettDescript", Request.QueryString.Item("sLettDescript")) & mobjValues.HiddenControl("sAction", Request.QueryString.Item("Action")))
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
Call mobjNetFrameWork.BeginPage("MLT001")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 09/05/2003 10:50:01 a.m.
mobjValues.sSessionID = Session.SessionID
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "MLT001"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 09/05/2003 10:50:01 a.m.
mobjMenu.sSessionID = Session.SessionID
'~End Body Block VisualTimer Utility
%>
<HTML>
<HEAD>
	<META NAME = "GENERATOR" Content = "Microsoft Visual Studio	6.0">
<SCRIPT	LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/General.aspx" -->

<SCRIPT> 
//**+ For the Source Safe control "DO NOT REMOVE"
//+ Para Control de Versiones "NO REMOVER"
//------------------------------------------------------------------------------
	document.VssVersion="$$Revision: 4 $|$$Date: 7/08/04 11:53a $"
//------------------------------------------------------------------------------
</SCRIPT>
<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write("<SCRIPT>var	nMainAction	= 0" & Request.QueryString.Item("nMainAction") & "</SCRIPT>")
	
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write(mobjMenu.setZone(2, "MLT001", Request.QueryString.Item("sWindowDescript")))
		mobjMenu = Nothing
	End If
	
	.Write(mobjValues.WindowsTitle("MLT001", Request.QueryString.Item("sWindowDescript")))
End With
%>
<SCRIPT>

//**% insShowHeader: This function to update the information of the header of the window.
//%insShowHeader: Esta funcion se encarga de actualizar los datos del encabezado de
//%la ventana.
//---------------------------------------------------------------------------------
function insShowHeader(lstrGroup,lstrDescript){
//---------------------------------------------------------------------------------
    var lblnDoit=true
    if (typeof(top.frames["fraHeader"].document)!='undefined')
        if (typeof(top.frames["fraHeader"].document.forms[0].elements["valGroup"])!='undefined'){
			top.frames["fraHeader"].document.forms[0].elements["valGroup"].value = lstrGroup
            top.frames["fraHeader"].UpdateDiv('valGroupDesc',lstrDescript,'Normal')
			lblnDoit = false
		}
    if (lblnDoit) setTimeout("insShowHeader(" + lstrGroup  + ",'" + lstrDescript + "')",50)
}

//**% insAliasText: One is in charge to update the fields alias of the PopUp
//%insAliasText: Se encarga de actualizar los campos alias de la PopUp
//---------------------------------------------------------------------------------
function insAliasText(lstrgrup,lstrvalues)
//---------------------------------------------------------------------------------
{	
	var lstrAliasTable = '';
	var lstrAliasColum = '';
	var lstrTemp = '';
	var lstrAliasField1 = self.document.forms[0].tctAliasTable.value;
	var lstrAliasField2 = self.document.forms[0].tctAliasColumn.value;

	for(i = 0; i < lstrAliasField1.length + 1;i++)
	{
		lstrTemp = lstrAliasField1.substr(i,1);
		lstrAliasTable = lstrAliasTable + lstrTemp.replace(' ','');
	}

	for(i = 0; i < lstrAliasField2.length + 1;i++)
	{
		lstrTemp = lstrAliasField2.substr(i,1);
		lstrAliasColum = lstrAliasColum + lstrTemp.replace(' ','');
	}

	if(lstrgrup == 1)
	{
		if(lstrAliasTable == '')
		{
			self.document.forms[0].tctAliasTable.value = self.document.forms[0].tctTableName.value;
		}
	}
	else
	{
		if(lstrAliasColum == '')
		{
			self.document.forms[0].tctAliasColumn.value = self.document.forms[0].tctColumName.value;
		}
	}
}


</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST"	ID="FORM" NAME="frmMLT001" ACTION="valMantLetter.aspx?mode=1">

<%

Response.Write(mobjValues.ShowWindowsName("MLT001", Request.QueryString.Item("sWindowDescript")))

If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insDefineHeader()
	Call insPreMLT001()
Else
	Call insDefineHeaderVar()
	Call insPreMLT001Upd()
End If

mobjGrid = Nothing
mobjValues = Nothing
%>	  
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 09/05/2003 10:50:01 a.m.
Call mobjNetFrameWork.FinishPage("MLT001")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>









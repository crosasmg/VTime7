<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores.
Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenu As eFunctions.Menues


'% insDefineHeader: Este procedimiento se encarga de definir las columnas del grid y de 
'% habilitar o inhabilitar los botones de añadir y eliminar.
'---------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'---------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	mobjGrid.sCodisplPage = "dp013"
	
	'+ Se definen las columnas del Grid.
	With mobjGrid.Columns
		Call .AddNumericColumn(100335, GetLocalResourceObject("tcnAgeColumnCaption"), "tcnAge", 5, CStr(0), False, GetLocalResourceObject("tcnAgeColumnToolTip"))
		Call .AddNumericColumn(100335, GetLocalResourceObject("tcnMonthColumnCaption"), "tcnMonth", 5, CStr(0), False, GetLocalResourceObject("tcnMonthColumnToolTip"))
		Call .AddNumericColumn(100336, GetLocalResourceObject("tcnDeath_qxColumnCaption"), "tcnDeath_qx", 9, CStr(0), False, GetLocalResourceObject("tcnDeath_qxColumnToolTip"), True, 8)
		Call .AddHiddenColumn("Exist", CStr(0))
	End With
	
	With mobjGrid
		.Codispl = Request.QueryString.Item("sCodispl")
		.Codisp = "DP013"
		.Columns("tcnAge").Disabled = True
		.Columns("tcnMonth").Disabled = True
		.Columns("Sel").GridVisible = False
		If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Or Request.QueryString.Item("nMainAction") = vbNullString Then
			.ActionQuery = True
		End If
		If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionadd) Or Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionUpdate) Then
			.Columns("tcnDeath_qx").EditRecord = True
			.AddButton = False
			.DeleteButton = False
			.Height = 200
			.Width = 330
			If Request.QueryString.Item("Reload") = "1" Then
				.sReloadIndex = Request.QueryString.Item("ReloadIndex")
			End If
		End If
	End With
End Sub

'% insPreDP013: Esta función permite realizar la lectura de la tabla principal de la transacción.
'% Esta ventana tiene un manejo particular ya que posee un botón de refrescar para redimensionar
'% el grid con las edades iniciales y finales indicadas. Se utilizó un indicador y si este seencuentra
'% con el valor 1 quiere decir que se hizo uso del botón refrescar y es necesario redimensionar el grid
'% con las edades iniciales y finales.
'-----------------------------------------------------------------------------------------
Private Sub insPreDP013()
	'-----------------------------------------------------------------------------------------
	
Response.Write("" & vbCrLf)
Response.Write("<SCRIPT>" & vbCrLf)
Response.Write("//% insShowHeader: manejo de los campos del grid." & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("function insShowHeader(){" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("    var lblnAgain = true" & vbCrLf)
Response.Write("    if (typeof(top.fraHeader.document)!='undefined')" & vbCrLf)
Response.Write("        if (typeof(top.fraHeader.document.forms[0])!='undefined')" & vbCrLf)
Response.Write("            if (typeof(top.fraHeader.document.forms[0].valMortalco)!='undefined'){" & vbCrLf)
Response.Write("		        top.fraHeader.document.forms[0].valMortalco.value='")


Response.Write(Session("sMortalco"))


Response.Write("';" & vbCrLf)
Response.Write("                lblnAgain = false         " & vbCrLf)
Response.Write("            }" & vbCrLf)
Response.Write("   if (lblnAgain)" & vbCrLf)
Response.Write("      setTimeout(""insShowHeader"",50)" & vbCrLf)
Response.Write("}" & vbCrLf)
Response.Write("</" & "SCRIPT>")

	
	Dim lintIndex As Object
	Dim lcolMortalitys As eProduct.Mortalitys
	Dim lclsMortality As Object
	Dim lblnfind As Boolean
	
	lcolMortalitys = New eProduct.Mortalitys
	If Request.QueryString.Item("sReloadDP013") = "1" Then
		lblnfind = lcolMortalitys.FindReload(Session("sMortalco"), mobjValues.StringToType(Request.QueryString.Item("nInitAge"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nEndAge"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nInitAgeOld"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nEndAgeOld"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nLiveLx"), eFunctions.Values.eTypeData.etdDouble))
	Else
		lblnfind = lcolMortalitys.Find(Session("sMortalco"), 0, True)
	End If
	
Response.Write("" & vbCrLf)
Response.Write("<SCRIPT>" & vbCrLf)
Response.Write("//% insPreZone: Se cargan las acciones a ser utilizadas." & vbCrLf)
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
Response.Write("//% valInitField : Verifica el valor de la edad inicial" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("function valInitField(Field,nType){" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("    switch (nType){" & vbCrLf)
Response.Write("//+ Se prepara el campo ""Edad Inicial""" & vbCrLf)
Response.Write("        case 0: if (Field.value.replace(/ */,'') == '') self.document.forms[0].tcnInit_age.value=0;" & vbCrLf)
Response.Write("        case 1: if (Field.value.replace(/ */,'') == '') self.document.forms[0].tcnEnd_age.value=0;" & vbCrLf)
Response.Write("        case 2: if (Field.value.replace(/ */,'') == '') self.document.forms[0].tcnLive_lx.value=0;" & vbCrLf)
Response.Write("    }" & vbCrLf)
Response.Write("}    " & vbCrLf)
Response.Write("//% insReloadPage:Esta función o evento es llamada desde el evento OnClickAccept del objeto ButtonAcceptCancel" & vbCrLf)
Response.Write("//% La misma permite guardar en el Request.QueryString los valores indicados en las edades iniciales y finales(nuevas y viejas(en el caso " & vbCrLf)
Response.Write("//% que ya existieran y se modificaran nuevamente)), así como un indicador que me dice cuando se " & vbCrLf)
Response.Write("//% se le dió al botón para redimensionar el grid con las valores indicados en las edades iniciales y finales." & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("function insReloadPage(nInitAgeOld, nEndAgeOld, nLive_lx){" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("    var lstrAction = self.document.location.href" & vbCrLf)
Response.Write("    lstrAction = lstrAction.replace(/&sReLoadDP013.*/,'') + ""&sReLoadDP013=1""" & vbCrLf)
Response.Write("    lstrAction = lstrAction +" & vbCrLf)
Response.Write("                 ""&nInitAge="" + self.document.forms[0].tcnInit_age.value +" & vbCrLf)
Response.Write("                 ""&nEndAge="" + self.document.forms[0].tcnEnd_age.value +" & vbCrLf)
Response.Write("                 ""&nInitAgeOld="" + nInitAgeOld  +" & vbCrLf)
Response.Write("                 ""&nEndAgeOld="" + nEndAgeOld +" & vbCrLf)
Response.Write("                 ""&nLiveLx="" + self.document.forms[0].tcnLive_lx.value" & vbCrLf)
Response.Write("    self.document.forms[0].action = lstrAction;" & vbCrLf)
Response.Write("    self.document.forms[0].submit()" & vbCrLf)
Response.Write("}" & vbCrLf)
Response.Write("//% insClickAccept: Valores setados al ejecutar el botón aceptar   " & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("function insClickAccept(){" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("	top.frames[""fraHeader""].insHandImage(""A390"", true);" & vbCrLf)
Response.Write("	top.frames['fraHeader'].ClientRequest(390,1);" & vbCrLf)
Response.Write("	top.frames[""fraHeader""].insHandImage(""A390"", false);" & vbCrLf)
Response.Write("}" & vbCrLf)
Response.Write("</" & "SCRIPT>" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("    " & vbCrLf)
Response.Write("<!--Se carga la tabla con los objetos a mostrar en la parte puntual de la ventana -->" & vbCrLf)
Response.Write("<!--Si el indicador sReloadDP013 se encuentra con 1 quiere decir que se hizo uso del botón -->" & vbCrLf)
Response.Write("<!--y se va a redimensionar el grid, entonces se mandan los valoes que se encuentran en el  -->" & vbCrLf)
Response.Write("<!--QueryString, caso contrario se mandan los valores que devolvió el recordset -->" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("    <TD VALIGN=TOP>" & vbCrLf)
Response.Write("		<TABLE WIDTH=""100%"" COLS=2>" & vbCrLf)
Response.Write("			")

	If Request.QueryString.Item("sReloadDP013") <> "1" Then
Response.Write("			" & vbCrLf)
Response.Write("				<TR>" & vbCrLf)
Response.Write("			        <TD><LABEL ID=14222><A NAME=""Tabla"">" & GetLocalResourceObject("tcnInit_ageCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("			        <TD>")

		
		Response.Write(mobjValues.NumericControl("tcnInit_age", 5, CStr(lcolMortalitys.nInit_age), False, GetLocalResourceObject("tcnInit_ageToolTip"),  ,  ,  ,  ,  , "valInitField(this,0)",  , 2))
		Response.Write(mobjValues.HiddenControl("hddnInitAgeOld", CStr(lcolMortalitys.nInit_age)))
		
Response.Write("" & vbCrLf)
Response.Write("					</TD>" & vbCrLf)
Response.Write("				</TR>" & vbCrLf)
Response.Write("				<TR>" & vbCrLf)
Response.Write("					<TD><LABEL ID=14221><A NAME=""Tabla"">" & GetLocalResourceObject("tcnEnd_age2Caption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("					<TD>")

		
		Response.Write(mobjValues.NumericControl("tcnEnd_age", 5, CStr(lcolMortalitys.nEnd_age), False, GetLocalResourceObject("tcnEnd_ageToolTip"),  ,  ,  ,  ,  , "valInitField(this,1)",  , 3))
		Response.Write(mobjValues.HiddenControl("hddnEndAgeOld", CStr(lcolMortalitys.nEnd_age)))
		
Response.Write("" & vbCrLf)
Response.Write("					</TD>" & vbCrLf)
Response.Write("				</TR>" & vbCrLf)
Response.Write("				<TR>" & vbCrLf)
Response.Write("					<TD><LABEL ID=14223><A NAME=""Número de vivos"">" & GetLocalResourceObject("tcnLive_lxCaption") & "</A></LABEL></TD>		" & vbCrLf)
Response.Write("					<TD>")


Response.Write(mobjValues.NumericControl("tcnLive_lx", 12, CStr(lcolMortalitys.nLive_lx), False, GetLocalResourceObject("tcnLive_lxToolTip"),  , 4,  ,  ,  , "valInitField(this,2)",  , 4))


Response.Write(" </TD>" & vbCrLf)
Response.Write("				</TR>" & vbCrLf)
Response.Write("				<TR>" & vbCrLf)
Response.Write("					<TD>")


Response.Write(mobjValues.ButtonAcceptCancel("insClickAccept();",  , False,  , eFunctions.Values.eButtonsToShow.OnlyAccept))


Response.Write(" </TD>" & vbCrLf)
Response.Write("				</TR>" & vbCrLf)
Response.Write("			")

	Else
Response.Write("" & vbCrLf)
Response.Write("				<TR>" & vbCrLf)
Response.Write("			        <TD><LABEL ID=14222><A NAME=""Tabla"">" & GetLocalResourceObject("tcnInit_ageCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("			        <TD>")

		
		Response.Write(mobjValues.NumericControl("tcnInit_age", 5, Request.QueryString.Item("nInitAge"), False, GetLocalResourceObject("tcnInit_ageToolTip"),  ,  ,  ,  ,  ,  ,  , 2))
		Response.Write(mobjValues.HiddenControl("hddnInitAgeOld", Request.QueryString.Item("nInitAge")))
		
Response.Write("" & vbCrLf)
Response.Write("					</TD>" & vbCrLf)
Response.Write("				</TR>" & vbCrLf)
Response.Write("				<TR>" & vbCrLf)
Response.Write("					<TD><LABEL ID=14221><A NAME=""Tabla"">" & GetLocalResourceObject("tcnEnd_age2Caption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("					<TD>")

		
		Response.Write(mobjValues.NumericControl("tcnEnd_age", 5, Request.QueryString.Item("nEndAge"), False, GetLocalResourceObject("tcnEnd_ageToolTip"),  ,  ,  ,  ,  ,  ,  , 3))
		Response.Write(mobjValues.HiddenControl("hddnEndAgeOld", Request.QueryString.Item("nEndAge")))
		
Response.Write("" & vbCrLf)
Response.Write("					</TD>" & vbCrLf)
Response.Write("				</TR>" & vbCrLf)
Response.Write("				<TR>" & vbCrLf)
Response.Write("					<TD><LABEL ID=14223><A NAME=""Número de vivos"">" & GetLocalResourceObject("tcnLive_lxCaption") & "</A></LABEL></TD>		" & vbCrLf)
Response.Write("					<TD>")


Response.Write(mobjValues.NumericControl("tcnLive_lx", 12, Request.QueryString.Item("nLiveLx"), False, GetLocalResourceObject("tcnLive_lxToolTip"),  , 4,  ,  ,  ,  ,  , 4))


Response.Write(" </TD>" & vbCrLf)
Response.Write("				</TR>" & vbCrLf)
Response.Write("				<TR>" & vbCrLf)
Response.Write("					<TD>")


Response.Write(mobjValues.ButtonAcceptCancel("insClickAccept();",  , False,  , eFunctions.Values.eButtonsToShow.OnlyAccept))


Response.Write(" </TD>" & vbCrLf)
Response.Write("				</TR>" & vbCrLf)
Response.Write("			")

	End If
Response.Write("" & vbCrLf)
Response.Write("		</TABLE>" & vbCrLf)
Response.Write("    </TD>" & vbCrLf)
Response.Write("    <TD VALIGN=TOP>" & vbCrLf)
Response.Write("    <DIV ID=""Scroll"" style=""width:400;height:300;overflow:auto; outset gray"">")

	
	'- Se asignan a las columnas del grid los valores devueltos por el recordset.
	If lblnfind Then
		For	Each lclsMortality In lcolMortalitys
			With lclsMortality
				mobjGrid.Columns("tcnAge").DefValue = .nAge
				mobjGrid.Columns("tcnMonth").DefValue = .nMonth
				If .nDeath_qx = eRemoteDB.Constants.intNull Then
					mobjGrid.Columns("tcnDeath_qx").DefValue = CStr(0)
				Else
					mobjGrid.Columns("tcnDeath_qx").DefValue = .nDeath_qx
				End If
				
				mobjGrid.Columns("Exist").DefValue = .nExist
				
				Response.Write(mobjGrid.DoRow())
			End With
		Next lclsMortality
	End If
	
	Response.Write(mobjGrid.closeTable() & "</DIV></TD></TABLE>")
	
	lcolMortalitys = Nothing
	lclsMortality = Nothing
End Sub

'% insPreDP013Upd: Esta función permite realizar el llamado a la ventana PopUp en un grid.
'% También permite almacenar en un método HiddenControl el valor del número de vivos que se 
'% introduce en la ventana ya que como me encuentro en la ventana PopUp lo perdería. El mismo
'% es utilizado para enviarlo al Post de la ventana para que inserte el o los registros en la 
'% tabla respectiva.
'--------------------------------------------------------------------------------------------
Private Sub insPreDP013Upd()
	'--------------------------------------------------------------------------------------------
	With Response
		.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "ValProduct.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("nMainAction"),  , CShort(Request.QueryString.Item("Index"))))
		.Write(mobjValues.HiddenControl("tcnLivelxAux", "0"))
		.Write("<SCRIPT>self.document.forms[0].tcnLivelxAux.value=top.opener.document.forms[0].tcnLive_lx.value;</" & "Script>")
	End With
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = "dp013"
%>
<SCRIPT LANGUAGE="JavaScript">
//- Variable para el control de versiones
	document.VssVersion="$$Revision: 3 $|$$Date: 14/11/03 12:57 $|$$Author: Nvaplat18 $"

//% insCancel: se controla la acción Cancelar
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
</SCRIPT>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>




    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<%
With Response
	.Write(mobjValues.StyleSheet())
	If Request.QueryString.Item("Type") <> "PopUp" Then
		mobjMenu = New eFunctions.Menues
		.Write("<SCRIPT>var	nMainAction	= 0" & Request.QueryString.Item("nMainAction") & "</SCRIPT>")
		.Write(mobjMenu.setZone(2, "DP013", "DP013.aspx"))
		mobjMenu = Nothing
	End If
	If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Then
		mobjValues.ActionQuery = True
	End If
End With
%>
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="DP013" ACTION="valProduct.aspx?sZone=2&nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%
Response.Write(mobjValues.ShowWindowsName("DP013"))

Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreDP013()
Else
	Call insPreDP013Upd()
End If

mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>





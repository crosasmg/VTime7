<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClaim" %>
<%@ Import namespace="eClient" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.33.47
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las funciones generales del menú
Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo de las funciones generales del grid
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo de los datos generales del siniestro
Dim mclsClaim As eClaim.Claim

'- Variable para el control de bloqueo
Dim bDisab As Boolean


'% insDefineHeader: se definen las columnas del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.33.47
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = "si028"
	Call mobjGrid.SetWindowParameters(Request.QueryString("sCodispl"), Request.QueryString("sWindowDescript"), Request.QueryString("nWindowTy"))
	
	'+Se definen todas las columnas del Grid
	With mobjGrid.Columns
		Call .AddDateColumn(0, "Fecha", "tcdDate", "",  , "Fecha en que se realiza el diagnóstico.")
		Call .AddTextColumn(0, "Descripción", "tctDescript", 30, "",  , "Breve descripción del diagnóstico efectuado al paciente.")
		Call .AddButtonColumn(0, "Detalle del diagnóstico.", "SCA2-N", 0,  , Request.QueryString("Type") <> "PopUp")
		Call .AddPossiblesColumn(0, "Estado", "cbeStatus", "Table561", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  , "Estado del diagnóstico.")
	End With
	
	With mobjGrid
		.Codispl = "SI028"
		.Codisp = "SI028"
		.Top = 300
		.Left = 150
		.Width = 500
		.Height = 230
		.ActionQuery = Session("bQuery")
		.bOnlyForQuery = Session("bQuery")
		.sDelRecordParam = "dDiag_date='+ marrArray[lintIndex].tcdDate + '"
		.Columns("tcdDate").Disabled = Request.QueryString("Action") = "Update"
		.Columns("tctDescript").EditRecord = True
		If Request.QueryString("Reload") = "1" Then
			.sReloadIndex = Request.QueryString("ReloadIndex")
		End If
	End With
End Sub

'% insPreSI028: se manejan los campos puntuales de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreSI028()
	'--------------------------------------------------------------------------------------------
	Dim lclsCl_diagnostic As eClaim.Cl_diagnostic
	Dim lcolCl_diagnostic As eClaim.Cl_diagnostics
	Dim lclsClaimBenef As eClaim.ClaimBenef
	Dim lclsClaim_attm As eClaim.Claim_attm
	Dim lclsClient As eClient.Client
	
	lcolCl_diagnostic = New eClaim.Cl_diagnostics
	lclsClaimBenef = New eClaim.ClaimBenef
	lclsClaim_attm = New eClaim.Claim_attm
	lclsClient = New eClient.Client
	
	Call lclsClaimBenef.Find_Demandant(mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nCase_num")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nDeman_type")), eFunctions.Values.eTypeData.etdDouble))
	
	Call lclsClaim_attm.insPreSI028(CDbl(Session("nClaim")), CInt(Session("nCase_num")), CInt(Session("nDeman_type")), CDate(Session("dEffecdate")), Request.QueryString("ReloadAction"), Request.QueryString("valIllness"), Request.QueryString("dtcClient"), mobjValues.StringToType(Request.QueryString("cbeService"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString("dtcClientProf"), mobjValues.StringToType(Request.QueryString("tcdInitIlldate"), eFunctions.Values.eTypeData.etdDate), Request.QueryString("tctHealth_sys_other"))
	
	
	'	If lclsClient.Find(lclsClaim_attm.sClientProf,true) Then
	'		bDisab = True
	'	Else
	'		bDisab = False	
	'	End If
	
	bDisab = True
	
	
Response.Write("  " & vbCrLf)
Response.Write("<SCRIPT>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("//%InsAssignValue: Asigna el valor al ""OptionButton"" Sistema de salud" & vbCrLf)
Response.Write("//------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("function InsAssignValue(Field) {" & vbCrLf)
Response.Write("//------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("    with (self.document.forms[0]) {" & vbCrLf)
Response.Write("    if (Field == 1) " & vbCrLf)
Response.Write("        optHealth_system[1].checked = true;" & vbCrLf)
Response.Write("    if (Field == 2) " & vbCrLf)
Response.Write("        optHealth_system[0].checked = true;" & vbCrLf)
Response.Write("    if (Field == 3){" & vbCrLf)
Response.Write("        optHealth_system[2].checked = true;" & vbCrLf)
Response.Write("        tctHealth_sys_other.disabled = false;" & vbCrLf)
Response.Write("    };" & vbCrLf)
Response.Write("    };" & vbCrLf)
Response.Write("}" & vbCrLf)
Response.Write("	var mAuxClinic, mAuxClient, mAuxProf, mAuxClientProf" & vbCrLf)
Response.Write("//% ChangeValue: Asigna valores dependiendo del campo en tratamiento " & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("function ChangeValue(Option, Field){" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("	if(Field.value!='')" & vbCrLf)
Response.Write("		switch(Option){" & vbCrLf)
Response.Write("			case ""valClinic"":" & vbCrLf)
Response.Write("				mAuxClinic = Field.value" & vbCrLf)
Response.Write("				if(mAuxClient!=self.document.forms[0].dtcClient.value)" & vbCrLf)
Response.Write("					ShowPopUp(""/VTimeNet/Claim/CaseSeq/ShowDefValues.aspx?Field=ProviderCode&sFieldName=dtcClient&ShowClient=True&nProvider="" + Field.value + ""&nTypeProv=1"", ""ShowDefValuesClinic"", 1, 1,""no"",""no"",2000,2000);					" & vbCrLf)
Response.Write("				break;" & vbCrLf)
Response.Write("			case ""dtcClient"":" & vbCrLf)
Response.Write("				mAuxClient = self.document.forms[0].dtcClient.value" & vbCrLf)
Response.Write("				" & vbCrLf)
Response.Write("					ShowPopUp(""/VTimeNet/Claim/CaseSeq/ShowDefValues.aspx?Field=ProviderCode&sFieldName=valClinic&ShowClient=False&nProvider=0&nTypeProv=1&sClient="" + self.document.forms[0].dtcClient.value, ""ShowDefValuesClinic"", 1, 1,""no"",""no"",2000,2000);" & vbCrLf)
Response.Write("				break;" & vbCrLf)
Response.Write("			case ""valProf"":" & vbCrLf)
Response.Write("				mAuxProf = Field.value" & vbCrLf)
Response.Write("				if(mAuxClientProf!=self.document.forms[0].dtcClientProf.value)" & vbCrLf)
Response.Write("					ShowPopUp(""/VTimeNet/Claim/CaseSeq/ShowDefValues.aspx?Field=ProviderCode&sFieldName=dtcClientProf&ShowClient=True&nProvider="" + Field.value + ""&nTypeProv=3"", ""ShowDefValuesProf"", 1, 1,""no"",""no"",2000,2000);" & vbCrLf)
Response.Write("				break;" & vbCrLf)
Response.Write("			case ""dtcClientProf"":" & vbCrLf)
Response.Write("				mAuxClientProf = self.document.forms[0].dtcClientProf.value" & vbCrLf)
Response.Write("				" & vbCrLf)
Response.Write("					ShowPopUp(""/VTimeNet/Claim/CaseSeq/ShowDefValues.aspx?Field=ProviderCode&sFieldName=valProf&ShowClient=False&nProvider=0&nTypeProv=3&sClient="" + self.document.forms[0].dtcClientProf.value, ""ShowDefValuesProf"", 1, 1,""no"",""no"",2000,2000);" & vbCrLf)
Response.Write("					" & vbCrLf)
Response.Write("				break;" & vbCrLf)
Response.Write("			case ""optHealth_System"":" & vbCrLf)
Response.Write("			    self.document.forms[0].hddHealth_system.value = Field.value" & vbCrLf)
Response.Write("			    if (Field.value==3){" & vbCrLf)
Response.Write("			        self.document.forms[0].tctHealth_sys_other.disabled = false;}" & vbCrLf)
Response.Write("			    else{" & vbCrLf)
Response.Write("			        self.document.forms[0].tctHealth_sys_other.disabled = true;" & vbCrLf)
Response.Write("			        self.document.forms[0].tctHealth_sys_other.value = """";}" & vbCrLf)
Response.Write("			    break;" & vbCrLf)
Response.Write("		}		" & vbCrLf)
Response.Write("}" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("</" & "SCRIPT>" & vbCrLf)
Response.Write("	<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD WIDTH=""19%""><LABEL ID=9655>Diagnóstico</LABEL></TD>" & vbCrLf)
Response.Write("            <TD WIDTH=""81%"">")

	With mobjValues
		.Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nCertif", Session("nCertif"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("dEffecdate", mclsClaim.dDecladat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("sClient", mclsClaim.sClient2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		Response.Write(mobjValues.PossiblesValues("valIllness", "tabtab_am_ill", eFunctions.Values.eValuesType.clngWindowType, lclsClaim_attm.sIllness, True,  ,  ,  ,  ,  ,  , 8, "Código de la enfermedad diagnosticada, que ocasiona el siniestro.", eFunctions.Values.eTypeCode.eString))
	End With
	
Response.Write("" & vbCrLf)
Response.Write("			</TD>					" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=9650>Rut clínica</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.ClientControl("dtcClient", lclsClaim_attm.sClient,  , "Rut del cliente asociado a la clínica", "ChangeValue(""dtcClient"",this)",  , "lblnClient"))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TABLE WIDTH=""100%"">        " & vbCrLf)
Response.Write("				<TR>" & vbCrLf)
Response.Write("					<TD><LABEL ID=9654>Clínica</LABEL></TD>" & vbCrLf)
Response.Write("					<TD>")

	With mobjValues
		.Parameters.Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nCertif", Session("nCertif"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("sClient", lclsClaimBenef.sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("dEffecdate", mclsClaim.dDecladat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nTypeProv", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		Response.Write(.PossiblesValues("valClinic", "TabPolGroupProv", eFunctions.Values.eValuesType.clngWindowType, CStr(lclsClaim_attm.nClinic), True,  ,  ,  , 5, "ChangeValue(""valClinic"",this)",  ,  , "Código de la clínica u hospital que presta el servicio"))
	End With
	
Response.Write("" & vbCrLf)
Response.Write("					</TD>" & vbCrLf)
Response.Write("						<TD><LABEL ID=9658>Servicio</LABEL></TD>" & vbCrLf)
Response.Write("						<TD>")


Response.Write(mobjValues.PossiblesValues("cbeService", "Table277", 1, CStr(lclsClaim_attm.nService),  ,  ,  ,  ,  ,  ,  ,  , "Servicio prestado en el siniestro"))


Response.Write("</TD>" & vbCrLf)
Response.Write("						")


Response.Write(mobjValues.DIVControl("lblTitleLastName",  , ""))


Response.Write("" & vbCrLf)
Response.Write("						")


Response.Write(mobjValues.DIVControl("lblLastName",  , ""))


Response.Write("" & vbCrLf)
Response.Write("						")


Response.Write(mobjValues.DIVControl("lblTitleLastName2",  , ""))


Response.Write("" & vbCrLf)
Response.Write("						")


Response.Write(mobjValues.DIVControl("lblLastName2",  , ""))


Response.Write("" & vbCrLf)
Response.Write("						")


Response.Write(mobjValues.DIVControl("lblTitleFirstName",  , ""))


Response.Write("" & vbCrLf)
Response.Write("						")


Response.Write(mobjValues.DIVControl("lblFirstName",  , ""))


Response.Write("" & vbCrLf)
Response.Write("				</TR>" & vbCrLf)
Response.Write("				<TR>" & vbCrLf)
Response.Write("				    <TD><LABEL ID=9657>Médico</LABEL></TD>" & vbCrLf)
Response.Write("				    <TD>")

	With mobjValues
		.Parameters.Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nCertif", Session("nCertif"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("sClient", lclsClaimBenef.sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("dEffecdate", mclsClaim.dDecladat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nTypeProv", 3, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		Response.Write(.PossiblesValues("valProf", "TabPolGroupProv", eFunctions.Values.eValuesType.clngWindowType, CStr(lclsClaim_attm.nProf), True,  ,  ,  ,  , "ChangeValue(""valProf"",this)",  ,  , "Médico tratante del caso"))
	End With
	
Response.Write("" & vbCrLf)
Response.Write("					</TD>" & vbCrLf)
Response.Write("				    <TD><LABEL ID=9651>Rut médico</LABEL></TD>" & vbCrLf)
Response.Write("				    <TD>")


Response.Write(mobjValues.ClientControl("dtcClientProf", lclsClaim_attm.sClientProf,  , "Rut del cliente asociado al médico", "ChangeValue(""dtcClientProf"",this);",  ,  , True,  ,  ,  ,  ,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("				</TR>" & vbCrLf)
Response.Write("				<TR>            " & vbCrLf)
Response.Write("				    <TD><LABEL ID=0>Apellido paterno</LABEL></TD>        " & vbCrLf)
Response.Write("					<TD>")


Response.Write(mobjValues.TextControl("tctLastNameProf", 19, lclsClient.sLastname,  , "Apellido paterno del cliente asociado al médico",  ,  ,  ,  , bDisab))


Response.Write("</TD>						" & vbCrLf)
Response.Write("				    <TD><LABEL ID=0>Apellido materno</LABEL></TD>        " & vbCrLf)
Response.Write("					<TD>")


Response.Write(mobjValues.TextControl("tctLastName2Prof", 19, lclsClient.sLastname2,  , "Apellido materno del cliente asociado al médico",  ,  ,  ,  , bDisab))


Response.Write("</TD>" & vbCrLf)
Response.Write("				</TR>" & vbCrLf)
Response.Write("				<TR>            " & vbCrLf)
Response.Write("				    <TD><LABEL ID=0>Nombres</LABEL></TD>        " & vbCrLf)
Response.Write("					<TD>")


Response.Write(mobjValues.TextControl("tctFirstNameProf", 19, lclsClient.sFirstname,  , "Nombre del cliente asociado al médico",  ,  ,  ,  , bDisab))


Response.Write("</TD>						" & vbCrLf)
Response.Write("				    <TD><LABEL ID=9656>Inicio de enfermedad</LABEL></TD>" & vbCrLf)
Response.Write("				    <TD>")


Response.Write(mobjValues.DateControl("tcdInitIlldate", CStr(lclsClaim_attm.dInit_Illdate),  , "Fecha aproximada del comienzo de la enfermedad"))


Response.Write("</TD>" & vbCrLf)
Response.Write("				</TR>" & vbCrLf)
Response.Write("			</TABLE>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("				<TR>" & vbCrLf)
Response.Write("				    <TD COLSPAN=""2"" CLASS=""HighLighted""><LABEL ID=0>Sistema de salud</LABEL></TD>" & vbCrLf)
Response.Write("				</TR>" & vbCrLf)
Response.Write("				<TR>" & vbCrLf)
Response.Write("				    <TD COLSPAN=""2"" CLASS=""HorLine""></TD>" & vbCrLf)
Response.Write("				</TR>" & vbCrLf)
Response.Write("				<TR>" & vbCrLf)
Response.Write("				<TD>")


Response.Write(mobjValues.OptionControl(0, "optHealth_system", "Isapre", "1", "2", "ChangeValue(""optHealth_System"",this)"))


Response.Write("</TD>" & vbCrLf)
Response.Write("				</TR>" & vbCrLf)
Response.Write("				<TR>" & vbCrLf)
Response.Write("				<TD>")


Response.Write(mobjValues.OptionControl(0, "optHealth_system", "FONASA",  , "1", "ChangeValue(""optHealth_System"",this)"))


Response.Write("</TD>" & vbCrLf)
Response.Write("				</TR>" & vbCrLf)
Response.Write("				<TR>" & vbCrLf)
Response.Write("				<TD>")


Response.Write(mobjValues.OptionControl(0, "optHealth_system", "Otro",  , "3", "ChangeValue(""optHealth_System"",this)"))


Response.Write("</TD>" & vbCrLf)
Response.Write("				<TD>")


Response.Write(mobjValues.TextControl("tctHealth_sys_other", 30, lclsClaim_attm.sHealth_sys_other,  , "Descripción del sistema de salud",  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("				<TD>")


Response.Write(mobjValues.HiddenControl("hddHealth_system", lclsClaim_attm.sHealth_system))


Response.Write("</TD>        " & vbCrLf)
Response.Write("				</TR>        " & vbCrLf)
Response.Write("           </TABLE>" & vbCrLf)
Response.Write("        </TR>     " & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("			<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("				<TR>" & vbCrLf)
Response.Write("				    <TD COLSPAN=""2"" CLASS=""HighLighted""><LABEL ID=0>Diagnóstico</LABEL></TD>" & vbCrLf)
Response.Write("				</TR>" & vbCrLf)
Response.Write("				<TR>" & vbCrLf)
Response.Write("				    <TD COLSPAN=""2"" CLASS=""HorLine""></TD>" & vbCrLf)
Response.Write("				</TR>" & vbCrLf)
Response.Write("			</TABLE>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("    </TABLE>" & vbCrLf)
Response.Write("   ")

	
	
	Dim sDigit_Pro As String
	
	If Request.QueryString("optHealth_system") <> "" Then
		Response.Write("<SCRIPT>InsAssignValue(" & Request.QueryString("optHealth_system") & " ); </" & "Script>")
	Else
		Response.Write("<SCRIPT>InsAssignValue(" & lclsClaim_attm.sHealth_system & " ); </" & "Script>")
	End If
	
	If Request.QueryString("dtcClientProf_Digit") <> "" Then
		If Request.QueryString("dtcClientProf_Digit") = "k" Then
			sDigit_Pro = "K"
		Else
			sDigit_Pro = Request.QueryString("dtcClientProf_Digit")
		End If
		Response.Write("<SCRIPT>self.document.forms[0].dtcClientProf_Digit.value =  '" & sDigit_Pro & "' ; </" & "Script>")
	End If
	
	If Request.QueryString("tctLastNameProf") <> "" Then
		Response.Write("<SCRIPT>self.document.forms[0].tctLastNameProf.value =  '" & Request.QueryString("tctLastNameProf") & "' ; </" & "Script>")
	End If
	
	If Request.QueryString("tctLastName2Prof") <> "" Then
		Response.Write("<SCRIPT>self.document.forms[0].tctLastName2Prof.value =  '" & Request.QueryString("tctLastName2Prof") & "' ; </" & "Script>")
	End If
	
	If Request.QueryString("tctFirstNameProf") <> "" Then
		Response.Write("<SCRIPT>self.document.forms[0].tctFirstNameProf.value =  '" & Request.QueryString("tctFirstNameProf") & "' ; </" & "Script>")
	End If
	
	With mobjGrid
		
		If lcolCl_diagnostic.Find(CDbl(Session("nClaim")), CInt(Session("nCase_num")), CInt(Session("nDeman_type")), CDate(Session("dEffecdate"))) Then
			For	Each lclsCl_diagnostic In lcolCl_diagnostic
				.Columns("tcdDate").DefValue = CStr(lclsCl_diagnostic.dDiag_date)
				.Columns("tctDescript").DefValue = lclsCl_diagnostic.sDescript
				.Columns("btnNotenum").nNotenum = lclsCl_diagnostic.nNotenum
				.Columns("cbeStatus").DefValue = CStr(lclsCl_diagnostic.nEvalStat)
				Response.Write(.DoRow)
			Next lclsCl_diagnostic
		End If
		Response.Write(.closeTable)
	End With
	
	Response.Write(mobjValues.BeginPageButton)
	
	'UPGRADE_NOTE: Object lclsClaimBenef may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lclsClaimBenef = Nothing
	'UPGRADE_NOTE: Object lcolCl_diagnostic may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lcolCl_diagnostic = Nothing
End Sub

'% insPreSI028Upd: se realiza el manejo de los campos de la PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub insPreSI028Upd()
	'--------------------------------------------------------------------------------------------
	Dim lclsClaim_attm As eClaim.Claim_attm
	With Request
		If .QueryString("Action") = "Del" Then
			lclsClaim_attm = New eClaim.Claim_attm
			Response.Write(mobjValues.ConfirmDelete())
			Call lclsClaim_attm.insPostSI028Upd(mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nCase_num")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nDeman_type")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("dDiag_date"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(CStr(Session("dEffecdate")), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdDouble), "Delete")
		End If
		
		Response.Write(mobjGrid.DoFormUpd(.QueryString("Action"), "/VTimeNet/Claim/CaseSeq/valCaseSeq.aspx", .QueryString("sCodispl"), .QueryString("nMainAction"), CBool(Session("bQuery")), .QueryString("Index")))
		
		With Response
			.Write(mobjValues.HiddenControl("valIllness", ""))
			.Write(mobjValues.HiddenControl("dtcClient", ""))
			.Write(mobjValues.HiddenControl("cbeService", ""))
			.Write(mobjValues.HiddenControl("dtcClientProf", ""))
			.Write(mobjValues.HiddenControl("dtcClientProf_Digit", ""))
			.Write(mobjValues.HiddenControl("tcdInitIlldate", ""))
			.Write(mobjValues.HiddenControl("valClinic", ""))
			.Write(mobjValues.HiddenControl("valProf", ""))
			.Write(mobjValues.HiddenControl("hddHealth_system_2", ""))
			.Write(mobjValues.HiddenControl("hddHealth_sys_other", ""))
			.Write(mobjValues.HiddenControl("hddLastName", ""))
			.Write(mobjValues.HiddenControl("hddLastName2", ""))
			.Write(mobjValues.HiddenControl("hddFirstName", ""))
			.Write(mobjValues.HiddenControl("tctLastNameProf", ""))
			.Write(mobjValues.HiddenControl("tctLastName2Prof", ""))
			.Write(mobjValues.HiddenControl("tctFirstNameProf", ""))
			.Write("<SCRIPT>")
			.Write("with(self.document.forms[0]){")
			.Write("valIllness.value=top.opener.document.forms[0].valIllness.value;")
			.Write("dtcClient.value=top.opener.document.forms[0].dtcClient.value;")
			.Write("cbeService.value=top.opener.document.forms[0].cbeService.value;")
			.Write("dtcClientProf.value=top.opener.document.forms[0].dtcClientProf.value;")
			.Write("dtcClientProf_Digit.value=top.opener.document.forms[0].dtcClientProf_Digit.value;")
			.Write("tcdInitIlldate.value=top.opener.document.forms[0].tcdInitIlldate.value;")
			.Write("valClinic.value=top.opener.document.forms[0].valClinic.value;")
			.Write("valProf.value=top.opener.document.forms[0].valProf.value;")
			.Write("hddHealth_system_2.value=top.opener.document.forms[0].hddHealth_system.value;")
			.Write("hddHealth_sys_other.value=top.opener.document.forms[0].tctHealth_sys_other.value;")
			.Write("if(typeof(top.opener.document.forms[0].tctLastName)!='undefined')hddLastName.value=top.opener.document.forms[0].tctLastName.value;")
			.Write("if(typeof(top.opener.document.forms[0].tctLastName2)!='undefined')hddLastName2.value=top.opener.document.forms[0].tctLastName2.value;")
			.Write("if(typeof(top.opener.document.forms[0].tctFirstName)!='undefined')hddFirstName.value=top.opener.document.forms[0].tctFirstName.value;")
			
			.Write("tctLastNameProf.value=top.opener.document.forms[0].tctLastNameProf.value;")
			.Write("tctLastName2Prof.value=top.opener.document.forms[0].tctLastName2Prof.value;")
			.Write("tctFirstNameProf.value=top.opener.document.forms[0].tctFirstNameProf.value;")
			.Write("}")
			.Write("</" & "Script>")
		End With
	End With
	'UPGRADE_NOTE: Object lclsClaim_attm may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lclsClaim_attm = Nothing
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("si028")
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.33.47
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "si028"
mclsClaim = New eClaim.Claim

mclsClaim.Find(CDbl(Session("nClaim")))
%>
<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/GenFunctions.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
    <%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file 'VTime/Includes/Constantes.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

<%Response.Write(mobjValues.StyleSheet())
If Request.QueryString("Type") <> "PopUp" Then
	mobjMenu = New eFunctions.Menues
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.33.47
	mobjMenu.sSessionID = Session.SessionID
	mobjMenu.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	With Response
		.Write(mobjMenu.setZone(2, "SI028", Request.QueryString("sWindowDescript"), Request.QueryString("nWindowTy")))
		.Write("<SCRIPT> var nMainAction=top.frames[""fraSequence""].plngMainAction</SCRIPT>")
	End With
	'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	mobjMenu = Nothing
End If
%>    
<SCRIPT>
//+ Esta línea guarda la versión procedente de VSS 
   document.VssVersion="$$Revision: 5 $|$$Date: 5/01/04 19:10 $|$$Author: Nvaplat11 $"
</SCRIPT>   
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="SI028" ACTION="/VTimeNet/Claim/CaseSeq/valCaseSeq.aspx?sTime=2">
    <%Response.Write(mobjValues.ShowWindowsName("SI028", Request.QueryString("sWindowDescript")))

Call insDefineHeader()
If Request.QueryString("Type") = "PopUp" Then
	Call insPreSI028Upd()
Else
	Call insPreSI028()
End If
'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing
'UPGRADE_NOTE: Object mobjGrid may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjGrid = Nothing
'UPGRADE_NOTE: Object mclsClaim may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mclsClaim = Nothing
%>
</FORM>
</BODY>
</HTML>

<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.33.47
Call mobjNetFrameWork.FinishPage("si028")
'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjNetFrameWork = Nothing

'^End Footer Block VisualTimer%>





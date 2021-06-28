<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<%@ Import namespace="ePolicy" %>
<%@ Import namespace="eGeneral" %>
<script language="VB" runat="Server">
    
'^Begin Header Block VisualTimer Utility 1.1 31/3/03 19.44.14
Dim mobjNetFrameWork As eNetFrameWork.Layout

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del grid de la página
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues

'-Variables que guardan el módulo y la cobertura seleccionada
Dim nModulec As Object
Dim nCover As String
    Dim nRole As Object
    
'-Variables que guardan el módulo y la cobertura del find
Dim nModulec_rec As Object
    Dim nCover_rec As Object
    Dim nRole_rec As Object


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.44.14
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = Request.QueryString.Item("sCodispl")
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		.AddNumericColumn(0, GetLocalResourceObject("tcnConsecColumnCaption"), "tcnConsec", 5, vbNullString,  , GetLocalResourceObject("tcnConsecColumnToolTip"),  ,  ,  ,  ,  , True)
		.AddPossiblesColumn(0, GetLocalResourceObject("cbeSexinsurColumnCaption"), "cbeSexinsur", "Table18", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeSexinsurColumnToolTip"))
		.AddNumericColumn(0, GetLocalResourceObject("tcnAgestartColumnCaption"), "tcnAgestart", 5, vbNullString,  , GetLocalResourceObject("tcnAgestartColumnToolTip"))
		.AddNumericColumn(0, GetLocalResourceObject("tcnAgeendColumnCaption"), "tcnAgeend", 5, vbNullString,  , GetLocalResourceObject("tcnAgeendColumnToolTip"))
		.AddNumericColumn(0, GetLocalResourceObject("tcnCapstartColumnCaption"), "tcnCapstart", 18, vbNullString,  , GetLocalResourceObject("tcnCapstartColumnToolTip"), True, 6)
		.AddNumericColumn(0, GetLocalResourceObject("tcnCapendColumnCaption"), "tcnCapend", 18, vbNullString,  , GetLocalResourceObject("tcnCapendColumnToolTip"), True, 6)
		.AddPossiblesColumn(0, GetLocalResourceObject("cbeCrthecniColumnCaption"), "cbeCrthecni", "Table32", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeCrthecniColumnToolTip"))
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "VI641"
		.ActionQuery = mobjValues.ActionQuery
		.Columns("tcnConsec").EditRecord = True
		.Columns("cbeSexinsur").EditRecord = True
		.Height = 350
		.Width = 350
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = Not .ActionQuery
            '.AddButton = Request.QueryString.Item("nCover") <> vbNullString
            .AddButton = Request.QueryString.Item("nRole") <> vbNullString
		
            .sEditRecordParam = "nModulec=' + self.document.forms[0].valModulec.value + '" &
                                "&nCover=' + self.document.forms[0].valCover.value + '" &
                                "&nRole=' + self.document.forms[0].valRole.value + '"
		
		.sDelRecordParam = .sEditRecordParam & "&nConsec=' + marrArray[lintIndex].tcnConsec + '"
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% insPreVI641: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreVI641()
	'--------------------------------------------------------------------------------------------
	'- Objeto para el manejo particular de los datos de la página
	Dim lcolLife_p_speci As ePolicy.Life_p_specis
	Dim lclsLife_p_speci As Object
	Dim lblnFound As Boolean
	Dim mclsProductli As eProduct.Product
	Dim mcolLife_specis As eProduct.Life_specis
	Dim lclsGeneral As eGeneral.GeneralFunction
	
	mcolLife_specis = New eProduct.Life_specis
	mclsProductli = New eProduct.Product
	lcolLife_p_speci = New ePolicy.Life_p_specis
	lclsGeneral = New eGeneral.GeneralFunction
	
        
        lblnFound = lcolLife_p_speci.InsPreVI641(Session("sCertype"), Session("nBranch"), Session("nProduct"),
                                                 Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(nModulec, eFunctions.Values.eTypeData.etdDouble),
                                                 mobjValues.StringToType(nCover, eFunctions.Values.eTypeData.etdDouble), Session("dEffecdate"),
                                                 mobjValues.StringToDate(Session("dNulldate")), Session("nUsercode"), Session("nTransaction"),
                                                 mobjValues.StringToType(nRole, eFunctions.Values.eTypeData.etdInteger))
	
        If Not lcolLife_p_speci.bIsModule Then
            nModulec = 0
        End If
	
        Call mcolLife_specis.FindLife_speci(Session("nBranch"), Session("nProduct"), Session("dEffecdate"),
                                            mobjValues.StringToType(nModulec, eFunctions.Values.eTypeData.etdDouble),
                                            mobjValues.StringToType(nCover, eFunctions.Values.eTypeData.etdDouble),
                                            mobjValues.StringToType(nRole, eFunctions.Values.eTypeData.etdInteger))
	
        Call mclsProductli.FindProduct_li(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble),
                                          mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble),
                                          mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
	
	If mclsProductli.nCurrency <> mcolLife_specis.nCurrencyAux And mcolLife_specis.nCurrencyAux > 0 Then
		Response.Write("<SCRIPT> alert(""" & "11407: " & lclsGeneral.insLoadMessage(11407) & """); </" & "Script> ")
	End If
	
Response.Write("" & vbCrLf)
Response.Write("<TABLE WIDTH=100%>" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("		<TD><LABEL ID=14390>" & GetLocalResourceObject("cbeCurrencyCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		<TD>")


Response.Write(mobjValues.PossiblesValues("cbeCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType, CStr(lcolLife_p_speci.nCurrency),  , True,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeCurrencyToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("		<TD><LABEL ID=0>" & GetLocalResourceObject("valModulecCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		<TD>" & vbCrLf)
Response.Write("		")

	
	With mobjValues
		.Parameters.Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nCertif", Session("nCertif"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nGroup", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		Response.Write(mobjValues.PossiblesValues("valModulec", "TABTABMODUL_CO_PG_DI", eFunctions.Values.eValuesType.clngWindowType, nModulec, True,  ,  ,  ,  , "InsChangeField(this.name)", Not lcolLife_p_speci.bIsModule,  , GetLocalResourceObject("valModulecToolTip"),  ,  ,  , True))
	End With
	
Response.Write("</TD>" & vbCrLf)
Response.Write("		<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("		<TD><LABEL ID=0>" & GetLocalResourceObject("valCoverCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		<TD>" & vbCrLf)
Response.Write("		")

	
	With mobjValues
		.Parameters.Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nCertif", Session("nCertif"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nGroup", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nModulec", mobjValues.StringToType(nModulec, eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Response.Write(mobjValues.PossiblesValues("valCover", "tabcoverpolicy", eFunctions.Values.eValuesType.clngWindowType, nCover, True, , , , , "InsChangeField(this.name)", , , GetLocalResourceObject("valCoverToolTip"), , , , True))
	End With


        Response.Write("" & vbCrLf)
        Response.Write("			</TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("		<TR>	" & vbCrLf)
        Response.Write("		    <TD><LABEL ID=13052>" & GetLocalResourceObject("valRoleCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD>" & vbCrLf)
        Response.Write("                ")

        mobjValues.ActionQuery = Session("bQuery")
        With mobjValues
            .Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", mobjValues.StringToType(nCover, eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nModulec", mobjValues.StringToType(nModulec, eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
        End With
        Response.Write(mobjValues.PossiblesValues("valRole", "tabtab_covrol3", eFunctions.Values.eValuesType.clngWindowType, nRole, True, , , , , "InsChangeField(this.name)", , , GetLocalResourceObject("valRoleToolTip")))
        
        
        
Response.Write("</TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("</TABLE>")

	mobjValues.ActionQuery = Session("bQuery")
	If lblnFound Then
		For	Each lclsLife_p_speci In lcolLife_p_speci
			With mobjGrid
				.Columns("tcnConsec").DefValue = lclsLife_p_speci.nConsec
				.Columns("cbeSexinsur").DefValue = lclsLife_p_speci.sSexclien
				.Columns("tcnAgestart").DefValue = lclsLife_p_speci.nAgestart
				.Columns("tcnAgeend").DefValue = lclsLife_p_speci.nAgeend
				.Columns("tcnCapstart").DefValue = lclsLife_p_speci.nCapstart
				.Columns("tcnCapend").DefValue = lclsLife_p_speci.nCapend
				.Columns("cbeCrthecni").DefValue = lclsLife_p_speci.nCrthecni
				nModulec_rec = mobjValues.StringToType(lclsLife_p_speci.nModulec, eFunctions.Values.eTypeData.etdDouble)
                    nCover_rec = mobjValues.StringToType(lclsLife_p_speci.nCover, eFunctions.Values.eTypeData.etdDouble)
                    nRole_rec = mobjValues.StringToType(lclsLife_p_speci.nRole, eFunctions.Values.eTypeData.etdInteger)
				
				Response.Write(.DoRow)
			End With
		Next lclsLife_p_speci
	End If
	Response.Write(mobjGrid.closeTable())
	
	mcolLife_specis = Nothing
	mclsProductli = Nothing
	lcolLife_p_speci = Nothing
	lclsGeneral = Nothing
End Sub

'% insPreVI641Upd: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub insPreVI641Upd()
	'--------------------------------------------------------------------------------------------
	Dim lclsLife_p_speci As ePolicy.Life_p_speci
	
	lclsLife_p_speci = New ePolicy.Life_p_speci
	With Request
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
                lclsLife_p_speci.InsPostVI641Upd(.QueryString.Item("Action"), Session("sCertype"), Session("nBranch"),
                                                 Session("nProduct"), Session("nPolicy"), Session("nCertif"),
                                                 mobjValues.StringToType(nModulec, eFunctions.Values.eTypeData.etdDouble),
                                                 mobjValues.StringToType(nCover, eFunctions.Values.eTypeData.etdDouble),
                                                 Session("dEffecdate"), mobjValues.StringToType(.QueryString.Item("nConsec"), eFunctions.Values.eTypeData.etdDouble),
                                                 Session("dNulldate"), Session("nUsercode"), Session("nTransaction"), 0, 0, 0, 0, 0, 0, 0,
                                                 mobjValues.StringToType(nRole, eFunctions.Values.eTypeData.etdInteger))
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "ValPolicySeq.aspx", "VI641", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
		If .QueryString.Item("Action") = "Add" Then
			Response.Write("<SCRIPT>insDefValuesAdd()</" & "Script>")
		End If
	End With
	lclsLife_p_speci = Nothing
End Sub

</script>
<%
    Response.Expires = -1441
    mobjNetFrameWork = New eNetFrameWork.Layout
    mobjNetFrameWork.sSessionID = Session.SessionID
    mobjNetFrameWork.nUsercode = Session("nUsercode")
    Call mobjNetFrameWork.BeginPage("VI641")
    '~End Header Block VisualTimer Utility
    Response.CacheControl = "private"

    mobjValues = New eFunctions.Values

    '^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.44.14
    mobjValues.sSessionID = Session.SessionID
    mobjValues.nUsercode = Session("nUsercode")
    '~End Body Block VisualTimer Utility

    mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
    mobjMenu = New eFunctions.Menues
    '^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.44.14
    mobjMenu.sSessionID = Session.SessionID
    mobjMenu.nUsercode = Session("nUsercode")
    '~End Body Block VisualTimer Utility
    nModulec = mobjValues.StringToType(Request.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble)
    nCover = mobjValues.StringToType(Request.QueryString.Item("nCover"), eFunctions.Values.eTypeData.etdDouble)
    nRole = mobjValues.StringToType(Request.QueryString.Item("nRole"), eFunctions.Values.eTypeData.etdInteger)
%>
<HTML>
<HEAD>
<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT>
	var lblnContinue = true;
	var lblnReload = true;	
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 4 $|$$Date: 13/01/04 18:34 $|$$Author: Nvaplat15 $"

//%InsChangeField: Recarga la página y le pasa por el QueryString el módulo y cobertura
//-----------------------------------------------------------------------------------------
function InsChangeField(sField){
//-----------------------------------------------------------------------------------------
	var lstrstring = '';
	var nModulec_old = '<%=Request.QueryString.Item("nModulec")%>';
	var nCover_old = '<%=Request.QueryString.Item("nCover")%>';
	var nRole_old = '<%=Request.QueryString.Item("nRole")%>';

	if (lblnReload){
		with (self.document.forms[0]){
			if (nModulec_old != valModulec.value ||
			    nCover_old   != valCover.value ||
                nRole_old    != valRole.value){
				if (sField == 'valModulec'){
					valCover.Parameters.Param1.sValue = '<%=Session("sCertype")%>';
					valCover.Parameters.Param2.sValue = '<%=Session("nBranch")%>';
					valCover.Parameters.Param3.sValue = '<%=Session("nProduct")%>';
					valCover.Parameters.Param4.sValue = '<%=Session("nPolicy")%>';
					valCover.Parameters.Param5.sValue = '<%=Session("nCertif")%>';
					valCover.Parameters.Param6.sValue = '<%=Session("dEffecdate")%>';
					valCover.Parameters.Param7.sValue = 0;
					if (valModulec.value!='')
						valCover.Parameters.Param8.sValue = valModulec.value;
					else
						valCover.Parameters.Param8.sValue = 0
					valCover.value = '';
					valCover.disabled = sField.value=='';
					btnvalCover.disabled = valCover.disabled;
					UpdateDiv('valCoverDesc', '');
				}
				if (sField == 'valCover'){
					//lstrstring += document.location;
					//lstrstring = lstrstring.replace(/&nModulec=.*/, "");
					//lstrstring = lstrstring.replace(/&nCover=.*/, "");
					//lstrstring = lstrstring + "&nCover=" + valCover.value + "&nModulec=" + valModulec.value;
					//if (nCover_old!="")
				    //document.location = lstrstring;

                    valRole.Parameters.Param1.sValue = '<%=Session("nBranch")%>';
                    valRole.Parameters.Param2.sValue = '<%=Session("nProduct")%>';
                    valRole.Parameters.Param3.sValue = valCover.value;
                    valRole.Parameters.Param4.sValue = '<%=Session("dEffecdate")%>';
                    valRole.Parameters.Param5.sValue = valModulec.value;


				}

                if (sField == 'valRole') {

                    self.document.forms[0].target = 'fraGeneric';
                    UpdateDiv('lblWaitProcess', '<MARQUEE>Procesando, por favor espere...</MARQUEE>', '');


	                lstrstring += document.location;
	                lstrstring = lstrstring.replace(/&nModulec=.*/, "");
	                lstrstring = lstrstring.replace(/&nCover=.*/, "");
	                lstrstring = lstrstring.replace(/&nRole=.*/, "");
	                lstrstring = lstrstring + "&nCover=" + valCover.value + "&nModulec=" + valModulec.value + "&nRole=" + valRole.value ;
	                document.location = lstrstring;
	            }

			}
		}
	}
	//if (sField == 'valCover') {
    if (sField == 'valRole'){
		lblnReload = true;
	}
}

//%insShowDescript: Descripcion del modulo
//-----------------------------------------------------------
function insShowDescript(){
//-----------------------------------------------------------
    with(self.document.forms[0]){
		if (lblnContinue){
		   $(valModulec).change();
		   $(valCover).change();
		   lblnContinue = false
        }
    }
}

//% insDefValuesAdd: Asignar los valores por defecto a los campos de la página
//-------------------------------------------------------------------------------------------
function insDefValuesAdd(){
//-------------------------------------------------------------------------------------------
//- Variable para almacenar el consecutivo más alto existente en el grid
    var llngMax = 0

//+ Se genera el número consecutivo para el campo "Id"
	with (top.opener){
		for(var llngIndex = 0;llngIndex<marrArray.length;llngIndex++){
		    if(eval(marrArray[llngIndex].tcnConsec) > eval(llngMax))
		       {
		        llngMax = marrArray[llngIndex].tcnConsec
		       } 
		    }
	}

//+ Se asignan los valores a los campos de la página
	self.document.forms[0].tcnConsec.value = ++llngMax;
}

</SCRIPT>
<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "VI641", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="VI641" ACTION="ValPolicySeq.aspx?sMode=2">
<%
Response.Write(mobjValues.ShowWindowsName("VI641", Request.QueryString.Item("sWindowDescript")))
Call insDefineHeader()
If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreVI641Upd()
Else
	Call insPreVI641()
End If

mobjValues = Nothing
mobjGrid = Nothing

%>
</FORM> 
</BODY>
</HTML>
<SCRIPT>
	var Modulec = '<%=nModulec%>'
	var Cover = '<%=nCover%>'
	var Role = '<%=nRole%>'
	var Type = '<%=Request.QueryString.Item("Type")%>';	

	if (Modulec <= "0" &&
		Cover   <= "0" && 
		Type    == ''){
		//lblnReload = false;
		with (self.document.forms[0]){
			valModulec.Parameters.Param1.sValue = '<%=Session("sCertype")%>';
			valModulec.Parameters.Param2.sValue = '<%=Session("nBranch")%>';
			valModulec.Parameters.Param3.sValue = '<%=Session("nProduct")%>';
			valModulec.Parameters.Param4.sValue = '<%=Session("nPolicy")%>';
			valModulec.Parameters.Param5.sValue = '<%=Session("nCertif")%>';
			valModulec.Parameters.Param6.sValue = '<%=Session("dEffecdate")%>';
			valModulec.Parameters.Param7.sValue = 0;
			valModulec.value = '<%=nModulec_rec%>';
			
			valCover.Parameters.Param1.sValue = '<%=Session("sCertype")%>';
			valCover.Parameters.Param2.sValue = '<%=Session("nBranch")%>';
			valCover.Parameters.Param3.sValue = '<%=Session("nProduct")%>';
			valCover.Parameters.Param4.sValue = '<%=Session("nPolicy")%>';
			valCover.Parameters.Param5.sValue = '<%=Session("nCertif")%>';
			valCover.Parameters.Param6.sValue = '<%=Session("dEffecdate")%>';
			valCover.Parameters.Param7.sValue = 0;
			valCover.Parameters.Param8.sValue =  ('<%=nModulec_rec%>'==''?0:'<%=nModulec_rec%>');
			valCover.value = '<%=nCover_rec%>';
		}	
	
		if (lblnContinue)
		    setTimeout("insShowDescript()",50);
	}	    

</SCRIPT>
<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.44.14
Call mobjNetFrameWork.FinishPage("VI641")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>





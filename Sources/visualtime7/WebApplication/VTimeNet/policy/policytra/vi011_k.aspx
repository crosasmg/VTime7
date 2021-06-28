<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.31.39
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues
'~End Body Block VisualTimer Utility
Dim mobjLoans As ePolicy.Loans
'- Variable que almacena la transacción que llama a esta
Dim mstrCodisplOri As String

Dim mdtmEffecdate As Object


'% insPreVI011: Se realiza carga inicial de datos
'---------------------------------------------------------------------------
Sub insPreVI011()
	'---------------------------------------------------------------------------	
	With Request
		If .QueryString.Item("sCodisplOri") = vbNullString Then
			mstrCodisplOri = "VI011"
			Session("nBranch") = eRemoteDB.Constants.intNull
			Session("nProduct") = eRemoteDB.Constants.intNull
			Session("nPropoNum") = eRemoteDB.Constants.intNull
			Session("nCertif") = eRemoteDB.Constants.intNull
			Session("nPolicy") = eRemoteDB.Constants.intNull
			Session("nValCode") = eRemoteDB.Constants.intNull
			Session("nPolicy") = eRemoteDB.Constants.intNull
		Else
			mstrCodisplOri = .QueryString.Item("sCodisplOri")
		End If
		Call mobjLoans.insPreVI011_K(mstrCodisplOri, .QueryString.Item("nOperat"))
	End With
	With Response
		.Write("<SCRIPT>")
		If mobjLoans.DefaultValueVI011("optExecuteEnabled") Then
			.Write("var blnLetDisabled = false;")
		Else
			.Write("var blnLetDisabled = true;")
            End If
            If mstrCodisplOri = "BUC" Then
                .Write("var blnLetDisabled = true;")
            End If
            .Write("</" & "Script>")
        End With
End Sub
'% insVI011Upd: Ejecuta proceso de actualización si se
'%				cancela el proceso de la transacción
'--------------------------------------------------------------------------- 
Private Sub insVI011Upd()
	'--------------------------------------------------------------------------- 
	With Request
		If CDbl(.QueryString.Item("nMainAction")) = 391 Then ' Acceptdatacancel
			Call mobjLoans.insPostVI011(.QueryString.Item("sCodisplOri"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), .QueryString.Item("sCertype"), mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), .QueryString.Item("nExecute"), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, "", "", eRemoteDB.Constants.intNull, mobjValues.StringToType(.QueryString.Item("nNoteNum"), eFunctions.Values.eTypeData.etdDouble), .QueryString.Item("sDescript"), "", mobjValues.StringToType(.QueryString.Item("nAgency"), eFunctions.Values.eTypeData.etdDouble))
			With Response
				.Write("<SCRIPT>")
				.Write("insReloadTop(false);")
				.Write("</" & "Script>")
			End With
		End If
	End With
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("vi011_k")
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.39
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "vi011_k"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.39
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
mobjLoans = New ePolicy.Loans

'+ Se realiza carga inicial de datos
Call insPreVI011()
%> 


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<%
If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) <> 391 Then
	Response.Write("<SCRIPT LANGUAGE=""JavaScript"" SRC=""/VTimeNet/Scripts/tmenu.js""></SCRIPT>")
End If
%>
<SCRIPT>
    //- Variable para el control de versiones
    document.VssVersion = "$$Revision: 7 $|$$Date: 10/05/04 19:41 $|$$Author: Nvaplat40 $"

    //% insFinish: Terminar transacción
    //-------------------------------------------------------------------------------------------
    function insFinish() {
        //-------------------------------------------------------------------------------------------
        var nAction = new TypeActions();

        //+ En modo consulta refresca la página
        if (top.frames["fraSequence"].plngMainAction == nAction.clngActionQuery) {
            insReloadTop(false);
            return false;
        }
        else
        //+ En otro modo ejecuta la validación
            return true;
    }

    //% insCancel: Anular ingreso
    //-------------------------------------------------------------------------------------------
    function insCancel() {
        //-------------------------------------------------------------------------------------------    
        return true;
    }

    //% insStateZone: Habilita los campos de la forma según la acción a ejecutar
    //-------------------------------------------------------------------------------------------
    function insStateZone() {
        //-------------------------------------------------------------------------------------------    
        var lintIndex;
        var llngerror;
        var nActions = new TypeActions();
        var nMainAction = top.frames["fraSequence"].plngMainAction;
        try {
            if (!blnLetDisabled) {
                with (self.document.forms[0]) {
                    for (lintIndex = 0; (lintIndex < elements.length); lintIndex++) {
                        elements[lintIndex].disabled = false;
                        if (self.document.images.length > 0)
                            if (typeof (self.document.images["btn" + elements[lintIndex].name]) != 'undefined')
                                self.document.images["btn" + elements[lintIndex].name].disabled = elements[lintIndex].disabled
                        }
                        btn_tcdEffecdate.disabled = false; 
                        //+ Se inhabilitan los campos segun modo consulta 
                        valCode.disabled = nMainAction != nActions.clngActionQuery;
                        btnvalCode.disabled = nMainAction != nActions.clngActionQuery;
                        if (valCode.disabled)
                            valCode.value = 0;
                        valCode.disabled = true;
                        btnvalCode.disabled = true;
                        valProduct.disabled = true;
                        btnvalProduct.disabled = true;
                        optExecute[0].disabled = nMainAction == nActions.clngActionQuery;
                        optExecute[1].disabled = nMainAction == nActions.clngActionQuery;
                        cbeAgency.disabled = nMainAction == nActions.clngActionQuery;
                        btncbeAgency.disabled = nMainAction == nActions.clngActionQuery;
                        cbeOffice.disabled = nMainAction == nActions.clngActionQuery;
                        cbeOfficeAgen.disabled = nMainAction == nActions.clngActionQuery;
                        btncbeOfficeAgen.disabled = nMainAction == nActions.clngActionQuery;
                        tcnProponum.disabled = true;
                    }
                }
            }
            catch (llngerror) { }

        }
        //% ShowChangeValues: Se cargan los valores de acuerdo producto seleccionado 
        //------------------------------------------------------------------------------------------- 
        function ShowChangeValues(sField) {
            //------------------------------------------------------------------------------------------- 
            var lstrParams;
            switch (sField) {
                case "Curren_pol":
                    with (self.document.forms[0]) {
                        lstrParams = "nBranch=" + cbeBranch.value +
			     			 "&nProduct=" + valProduct.value +
			    			 "&dEffecdate=" + tcdEffecdate.value +
			    			 "&nPolicy=" + tcnPolicy.value +
			    			 "&nCertif=" + tcnCertif.value +
			    			 "&sCertype=2"
                    }
                    insDefValues(sField, lstrParams, "/VTimeNet/Policy/PolicyTra");
                    break;
                case "Policy_CA099":
                    with (self.document.forms[0]) {
                        lstrParams = "nBranch=" + cbeBranch.value +
			     			 "&nProduct=" + valProduct.value +
			    			 "&dEffecdate=" + tcdEffecdate.value +
			    			 "&nPolicy=" + tcnPolicy.value +
			    			 "&nCertif=" + tcnCertif.value +
			    			 "&sCertype=2" +
			    			 "&sCodispl=VI011" +
			    			 "&nAction=" + top.frames["fraSequence"].plngMainAction
                    }
                    insDefValues(sField, lstrParams, "/VTimeNet/Policy/PolicyTra");
                    break;
            }
        }
        //% insChangeField: Se recargan los valores cuando cambia el campo
        //-------------------------------------------------------------------------------------------
        function insChangeField(Field) {
            //-------------------------------------------------------------------------------------------    
            with (self.document.forms[0]) {
                switch (Field.name) {
                    case "tcdEffecdate":
                        break;
                    case "cbeBranch":
                        valCode.Parameters.Param1.sValue = Field.value;
                        valProduct.Parameters.Param1.sValue = Field.value
                        if (tcnPolicy.value != "") {
                            ShowChangeValues("Policy_CA099");
                        }
                        break;
                    case "valProduct":
                        valCode.Parameters.Param2.sValue = Field.value;
                        if (tcnPolicy.value != "") {
                            ShowChangeValues("Policy_CA099");
                        }
                        break;
                    case "tcnPolicy": 
                        valCode.Parameters.Param3.sValue = Field.value;
                        ShowChangeValues("Policy_CA099");

                        valCode.Parameters.Param4.sValue = '0';
                        break;
                    case "tcnCertif":
                        valCode.Parameters.Param4.sValue = Field.value;
                        ShowChangeValues("Curren_pol");
                        break;
                }
            }
        }

        //% insInitialAgency: manejo de sucursal/oficina/agencia
        //-------------------------------------------------------------------------------------------
        function insInitialAgency(nInd) {
            //-------------------------------------------------------------------------------------------
            with (self.document.forms[0]) {
                //+ Cambia la sucursal 
                switch (nInd) {
                    case 1:
                        if (typeof (cbeOffice) != 'undefined') {
                            if (cbeOffice.value != 0) {
                                if (typeof (cbeOfficeAgen) != 'undefined') {
                                    cbeOfficeAgen.Parameters.Param1.sValue = (cbeOffice.value == '' ? 0 : cbeOffice.value);
                                    cbeOfficeAgen.Parameters.Param2.sValue = 0;
                                    cbeAgency.Parameters.Param1.sValue = (cbeOffice.value == '' ? 0 : cbeOffice.value);
                                    if (cbeOfficeAgen.value != "" && cbeOfficeAgen.value > 0)
                                        cbeAgency.Parameters.Param2.sValue = (cbeOfficeAgen.value == '' ? 0 : cbeOfficeAgen.value);
                                    else
                                        cbeAgency.Parameters.Param2.sValue = 0;
                                }
                            }
                            else {
                                if (typeof (cbeOfficeAgen) != 'undefined') {
                                    cbeOfficeAgen.Parameters.Param1.sValue = (cbeOffice.value == '' ? 0 : cbeOffice.value);
                                    cbeOfficeAgen.Parameters.Param2.sValue = 0;
                                    cbeAgency.Parameters.Param1.sValue = (cbeOffice.value == '' ? 0 : cbeOffice.value);
                                    if (cbeOfficeAgen.value != "" && cbeOfficeAgen.value > 0)
                                        cbeAgency.Parameters.Param2.sValue = (cbeOfficeAgen.value == '' ? 0 : cbeOfficeAgen.value);
                                    else
                                        cbeAgency.Parameters.Param2.sValue = 0;
                                }
                            }
                        }
                        break;

                    //+ Cambia la oficina 
                    case 2:
                        if (cbeOfficeAgen.value != "" && cbeOfficeAgen.value > 0) {
                            cbeAgency.Parameters.Param1.sValue = (cbeOffice.value == '' ? 0 : cbeOffice.value);
                            cbeAgency.Parameters.Param2.sValue = (cbeOfficeAgen.value == '' ? 0 : cbeOfficeAgen.value);
                            cbeOffice.value = cbeOfficeAgen_nBran_off.value;
                            cbeOfficeAgen.Parameters.Param1.sValue = (cbeOffice.value == '' ? 0 : cbeOffice.value);
                        }
                        else {
                            cbeAgency.Parameters.Param1.sValue = 0;
                            cbeAgency.Parameters.Param2.sValue = 0;
                        }
                        break;
                    //+ Cambia la Agencia			 
                    case 3:
                        if (cbeAgency.value != "") {
                            cbeOffice.value = cbeAgency_nBran_off.value;
                            if (cbeOfficeAgen.value == '') {
                                cbeOfficeAgen.value = cbeAgency_nOfficeAgen.value;
                                UpdateDiv('cbeOfficeAgenDesc', cbeAgency_sDesAgen.value);
                            }
                            cbeOfficeAgen.Parameters.Param1.sValue = (cbeOffice.value == '' ? 0 : cbeOffice.value);
                            cbeAgency.Parameters.Param1.sValue = (cbeOffice.value == '' ? 0 : cbeOffice.value);
                            cbeAgency.Parameters.Param2.sValue = (cbeOfficeAgen.value == '' ? 0 : cbeOfficeAgen.value);
                        }
                }
            }
        }

        //% ShowChangeValues1: Se habilitan/deshabilitan los controles de acuerdo a lo definido para
        //%	producto, póliza o certificado
        //-------------------------------------------------------------------------------------------
        function ShowChangeValues1(sField) {
            //-------------------------------------------------------------------------------------------
            var lstrParams
            with (self.document.forms[0]) {
                switch (sField) {
                    case "Agency":
                        if (cbeAgency.value != "")
                            insDefValues(sField, "nAgency=" + cbeAgency.value + "&nOfficeAgen=" + cbeOfficeAgen.value + "&nOffice=" + cbeOffice.value, '/VTimeNet/Policy/PolicySeq')
                        break;
                    case "Loans":
                        if (valCode.value != "") {
                            lstrParams = "nBranch=" + cbeBranch.value +
					 			 "&nProduct=" + valProduct.value +
								 "&dEffecdate=" + tcdEffecdate.value +
								 "&nPolicy=" + tcnPolicy.value +
								 "&nCertif=" + tcnCertif.value +
								 "&nLoans=" + valCode.value +
								 "&sCertype=2";
                            insDefValues(sField, lstrParams, '/VTimeNet/Policy/PolicyTra');
                        }
                        break;
                }
            }
        }

</SCRIPT>
<HTML>
<HEAD>
<%
With Response
	.Write(mobjValues.StyleSheet())
	If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) <> 391 Then
		.Write(mobjMenu.MakeMenu("VI011", "VI011_k.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
	End If
End With
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<BR><BR>
<FORM METHOD="post" ID="FORM" NAME="VI011" ACTION=valPolicyTra.aspx?x=1<%="&nAmount=" & Request.QueryString.Item("nAmount")%>>
<%
If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 391 Then
	insVI011Upd()
Else
	
	mdtmEffecdate = Today
	
	If Not IsNothing(Request.QueryString.Item("dEffecdate")) Then
		mdtmEffecdate = Request.QueryString.Item("dEffecdate")
	End If
	
	%>
    <TABLE WIDTH="100%" BORDER="0">
		<TR>
			<TD COLSPAN="2" WIDTH="30%" CLASS="HIGHLIGHTED"><LABEL ID=0><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
			<TD COLSPAN="2">&nbsp;</TD>
		</TR>
		<TR>
			<TD COLSPAN="2" CLASS="HORLINE"></TD>
			<TD COLSPAN="2"></TD>
		</TR>
        <TR>
			<TD><%=mobjValues.OptionControl(0, "optExecute", GetLocalResourceObject("optExecute_1Caption"), mobjLoans.DefaultValueVI011("optExecutePre"), "1",  , True,  , GetLocalResourceObject("optExecute_1ToolTip"))%></TD>
			<TD><%=mobjValues.OptionControl(0, "optExecute", GetLocalResourceObject("optExecute_2Caption"), mobjLoans.DefaultValueVI011("optExecuteDef"), "2",  , True,  , GetLocalResourceObject("optExecute_2ToolTip"))%></TD>
			<TD><LABEL ID=13722><%= GetLocalResourceObject("tcdEffecdateCaption") %></LABEL></TD>
		
        	<TD><%
        	        If Request.QueryString.Item("sCodisplOri") = "BUC" Then
        	            Response.Write(mobjValues.DateControl("tcdEffecdate", Today, , GetLocalResourceObject("tcdEffecdateToolTip"), , , , , True))
        	        Else
        	            Response.Write(mobjValues.DateControl("tcdEffecdate", mdtmEffecdate, , GetLocalResourceObject("tcdEffecdateToolTip"), , , , "insChangeField(this);", True))
        	        End If
                %></TD>        	        
        	        
		</TR>
		<TR>
            <TD><LABEL ID=13717><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
			<TD><%=mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"), Session("nBranch"),  ,  ,  ,  , "insChangeField(this);", True)%></TD>
            <TD><LABEL ID=13725><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
			<TD><%=mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"), CStr(0), eFunctions.Values.eValuesType.clngWindowType, True, Session("nProduct"),  ,  ,  , "insChangeField(this);")%></TD> 
		</TR>
		<TR>
            <TD><LABEL ID=13724><%= GetLocalResourceObject("tcnPolicyCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnPolicy", 10, Session("nPropoNum"),  , GetLocalResourceObject("tcnPolicyToolTip"),  , 0,  ,  ,  , "insChangeField(this);", True)%></TD>
            <TD><LABEL ID=13718><%= GetLocalResourceObject("tcnCertifCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnCertif", 10, 0,  , GetLocalResourceObject("tcnCertifToolTip"),  , 0,  ,  ,  , "insChangeField(this);", True)%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=13719><%= GetLocalResourceObject("valCodeCaption") %></LABEL></TD>
            <%	
	With mobjValues.Parameters
		.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    If mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble, True)  > 0 Then
                        .Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    Else
                        .Add("nPolicy", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    End If
                    .Add("nCertif", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                End With
	%>
            <TD><%=mobjValues.PossiblesValues("valCode", "Tabtab_loans", eFunctions.Values.eValuesType.clngWindowType, Session("nValCode"), True,  ,  ,  ,  , "ShowChangeValues1(""Loans"");", True, 10, GetLocalResourceObject("valCodeToolTip"))%></TD>
            <TD><LABEL ID=13720><%= GetLocalResourceObject("Anchor2Caption") %></LABEL></TD>
            <TD COLSPAN="2"><%=mobjValues.DIVControl("lblDesCurrency")%></TD>
        </TR>
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcnProponumCaption") %></LABEL></TD>
			<TD COLSPAN="3"><%=mobjValues.NumericControl("tcnProponum", 10, Session("nPolicy"),  , GetLocalResourceObject("tcnProponumToolTip"),  ,  ,  ,  ,  ,  , True)%></TD>
        </TR>
		<TR>
			<TD COLSPAN="4" CLASS="HIGHLIGHTED"><LABEL ID=0><%= GetLocalResourceObject("Anchor3Caption") %></LABEL></TD>
		</TR>
		<TR>
			<TD COLSPAN="4" CLASS="HORLINE"></TD>
		</TR>
		<TR>
			<TD><LABEL ID=13378><%= GetLocalResourceObject("cbeOfficeCaption") %></LABEL></TD>
			<TD>
				<%	
	mobjValues.TypeOrder = 1
	Response.Write(mobjValues.PossiblesValues("cbeOffice", "Table9", eFunctions.Values.eValuesType.clngComboType, vbNullString,  ,  ,  ,  ,  , "insInitialAgency(1)", True,  , GetLocalResourceObject("cbeOfficeToolTip")))
	%>
	        </TD>
		    <TD><LABEL ID=0><%= GetLocalResourceObject("cbeOfficeAgenCaption") %></LABEL></TD>
			<TD>
				<%	
	With mobjValues
		.Parameters.Add("nOfficeAgen", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nAgency", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.ReturnValue("nBran_off",  ,  , True)
		Response.Write(.PossiblesValues("cbeOfficeAgen", "TabAgencies_T5556", eFunctions.Values.eValuesType.clngWindowType, vbNullString, True,  ,  ,  ,  , "insInitialAgency(2)", True,  , GetLocalResourceObject("cbeOfficeAgenToolTip")))
	End With
	%>
			</TD>
	    </TR>
		<TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("Anchor3Caption") %></LABEL></TD>
			<TD COLSPAN="3">
				<%	
	With mobjValues
		.Parameters.Add("nOfficeAgen", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nAgency", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.ReturnValue("nBran_off",  ,  , True)
		.Parameters.ReturnValue("nOfficeAgen",  ,  , True)
		.Parameters.ReturnValue("sDesAgen",  ,  , True)
		If IsNothing(Request.QueryString.Item("nAgency")) Then
			Response.Write(mobjValues.PossiblesValues("cbeAgency", "TabAgencies_T5555", eFunctions.Values.eValuesType.clngWindowType, Session("nAgency"), True,  ,  ,  ,  , "insInitialAgency(3)", True,  , GetLocalResourceObject("cbeAgencyToolTip")))
		Else
			Response.Write(mobjValues.PossiblesValues("cbeAgency", "TabAgencies_T5555", eFunctions.Values.eValuesType.clngWindowType, Request.QueryString.Item("nAgency"), True,  ,  ,  ,  , "insInitialAgency(3)", True,  , GetLocalResourceObject("cbeAgencyToolTip")))
		End If
	End With
	%>
			</TD>
		</TR>
    </TABLE>
	<SCRIPT>	    insInitialAgency(1)</SCRIPT>
<%	
	With Response
		.Write(mobjValues.HiddenControl("tcnCurrency", "0")) ' Actualizado con DefValues
        If Request.QueryString.Item("sCodisplOri") = "BUC" Then
            .Write(mobjValues.HiddenControl("tctCodisplOri", "VI011"))
            Session("SessionID") = Format(Today, "yyyymmdd")
        Else
            .Write(mobjValues.HiddenControl("tctCodisplOri", Request.QueryString.Item("sCodisplOri")))
        End If
        .Write(mobjValues.HiddenControl("tcnNoteNum", Request.QueryString.Item("nNoteNum")))
        .Write(mobjValues.HiddenControl("tcnOperat", Session("nOperat")))
        .Write(mobjValues.HiddenControl("tctDescript", Request.QueryString.Item("sDescript")))
        .Write(mobjValues.HiddenControl("tctCertype", "2"))
    End With
End If
%>
</FORM>
<%
'If Request.QueryString("sCodisplOri") <> vbNullString Then
Response.Write("<SCRIPT>")
'Response.Write "self.document.forms[0].cbeAgency.value='" & Request.QueryString("nAgency") & "';"
Response.Write("setTimeout('$(self.document.forms[0].cbeAgency).change()', 1);")
'Response.Write "setTimeout('$(self.document.forms[0].tcnPolicy).change()', 1);"
Response.Write("</SCRIPT>")
'End If
%></BODY>
</HTML>
<%
'+ Si fue llamado desde otra transacción, se deja automaticamente en modo de ingreso
If mstrCodisplOri <> "VI011" And CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) <> 391 Then
	Response.Write("<SCRIPT>ClientRequest(301,1);</SCRIPT>")
End If
mobjLoans = Nothing


    If Request.QueryString.Item("sCodisplOri") = "CA767" Then
        Response.Write("<SCRIPT>ShowChangeValues('Curren_pol');</SCRIPT>")
    End If
    If Request.QueryString.Item("sCodisplOri") <> "BUC" Then
        If mobjValues.StringToType(Session("nPropoNum"), eFunctions.Values.eTypeData.etdDouble, True) > 0 Then
            Response.Write("<SCRIPT>ShowChangeValues('Policy_CA099');</SCRIPT>")
            '        Response.Write("<SCRIPT>ShowChangeValues('Curren_pol');</SCRIPT>")
            'Response.Write("<SCRIPT>insChangeField('tcnPolicy');</SCRIPT>")
        End If
    Else
        Response.Write("<SCRIPT>ShowChangeValues('Policy_CA099');</SCRIPT>")
        
    End If
    '    If Request.QueryString.Item("sCodisplOri") = "BUC" Then
    'Response.Write("<SCRIPT>self.document.forms[0].tcdEffecdate.disabled = true;</SCRIPT>")
    'Response.Write("<SCRIPT>self.document.forms[0].btn_tcdEffecdate.disabled = true;</SCRIPT>")
    'End If
    mobjValues = Nothing
    %>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.31.39
Call mobjNetFrameWork.FinishPage("vi011_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>






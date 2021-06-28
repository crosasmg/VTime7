<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneralForm" %>
<%@ Import namespace="ePolicy" %>
<%@ Import namespace="eGeneral" %>
<script language="VB" runat="Server">

    '^Begin Header Block VisualTimer Utility 1.1 27/05/2003 07:39:47 a.m.
    Dim mobjNetFrameWork As eNetFrameWork.Layout
    '~End Header Block VisualTimer Utility

    '- Objeto para el manejo de las funciones generales de carga de valores
    Dim mobjValues As eFunctions.Values

    '- Objeto para el manejo de las zonas de la página    
    Dim mobjMenu As eFunctions.Menues
    '~End Body Block VisualTimer Utility    

    Dim lclsCivil As ePolicy.Civil
    Dim lstrActReg As String
    Dim mblnDatConst As Boolean
    Dim mclsGeneralForm As eGeneralForm.GeneralForm


    '%insPreRC001. Esta funcion se encarga deralizar la busqueda de los datos de cliente
    '------------------------------------------------------------------------------------
    Private Sub insPreRC001()
        '------------------------------------------------------------------------------------
        Dim lcolCivils As ePolicy.Civils
        Dim lclsMulti_risk As ePolicy.multi_risk
        Dim lcolMulti_risks As ePolicy.multi_risks
        Dim lclsRoles As ePolicy.Roles
        Dim lclsGeneral As eGeneral.Business_Functs
        With Request
		
            lcolCivils = New ePolicy.Civils
            Call lcolCivils.Find(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), .QueryString.Item("sCodispl"))
            If lcolCivils.Count > 0 Then
                lclsCivil = lcolCivils(1)
                If lclsCivil.sComplCod = vbNullString Then
                    lclsRoles = New ePolicy.Roles
                    lclsGeneral = New eGeneral.Business_Functs
				
                    With lclsRoles
                        If .Find(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), 1, vbNullString, Session("dEffecdate")) Then
						
                            If .sComplCod <> vbNullString Then
                                lclsCivil.sComplCod = .sComplCod
                                lclsCivil.nBusinessty = lclsGeneral.getBusinessty(.sComplCod)
                                lclsCivil.nCommergrp = lclsGeneral.getCommergrp(.sComplCod)
                                lclsCivil.nCodkind = lclsGeneral.getCodkind(.sComplCod)
                            End If
                        End If
                    End With
                End If
            Else
                lcolMulti_risks = New ePolicy.multi_risks
                lclsCivil = New ePolicy.Civil
			
                If CStr(Session("sBrancht")) = "2" Then
                    Call lcolMulti_risks.Find(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), .QueryString.Item("sCodispl"))
                    If lcolMulti_risks.Count > 0 Then
                        lclsMulti_risk = lcolMulti_risks(1)
                        lclsCivil.sComplCod = lclsMulti_risk.sComplCod
                        lclsCivil.nBusinessty = lclsMulti_risk.nBusinessty
                        lclsCivil.nCommergrp = lclsMulti_risk.nCommergrp
                        lclsCivil.nCodkind = lclsMulti_risk.nCodkind
                        lclsCivil.sDescBussi = lclsMulti_risk.sDescBussi
                        lclsCivil.nConstCat = lclsMulti_risk.nConstcat
                        'UPGRADE_NOTE: Object lclsMulti_risk may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                        lclsMulti_risk = Nothing
                    End If
				
                    '+ Aviso de carga de datos en la ventana de Multiriesgo
				
                    If lclsCivil.sComplCod = vbNullString Then
                        Response.Write("<SCRIPT>alert('Debe ingresar datos en la ventana de Multiriesgo');</" & "Script>")
                    End If
                End If
            End If
		
        End With
        'UPGRADE_NOTE: Object lcolCivils may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        lcolCivils = Nothing
        'UPGRADE_NOTE: Object lcolMulti_risks may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        lcolMulti_risks = Nothing
        'UPGRADE_NOTE: Object lclsRoles may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        lclsRoles = Nothing
        'UPGRADE_NOTE: Object lclsGeneral may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        lclsGeneral = Nothing
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub
</script>
<%  Response.Expires = -1441
    mobjNetFrameWork = New eNetFrameWork.Layout
    mobjNetFrameWork.sSessionID = Session.SessionID
    Call mobjNetFrameWork.BeginPage("RC001")

    mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 27/05/2003 07:39:47 a.m.
    mobjValues.sSessionID = Session.SessionID
    mobjValues.sCodisplPage = "RC001"
    mclsGeneralForm = New eGeneralForm.GeneralForm

    lstrActReg = "S" 'Considerar solo los registros activos de los combos de Giro de Negocio	
%>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->
                                                                                               
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>
<SCRIPT>
    //% InsParamValue: Asigna Articulo
    //---------------------------------------------------------------------------------------------------
    function InsParamValue() {
        //---------------------------------------------------------------------------------------------------	
        with (self.document.forms[0]) {
            cbeDetailArt.Parameters.Param1.sValue = cbeArticle.value;

            cbeDetailArt.disabled = (cbeArticle.value == '') ? true : false;
            btncbeDetailArt.disabled = (cbeArticle.value == '') ? true : false;
        }
    }
    //% InsValueInit: Limpia valiables de llave de acceso
    //---------------------------------------------------------------------------------------------------
    function InsValueInit() {
        //---------------------------------------------------------------------------------------------------
        with (self.document.forms[0]) {
            cbeDetailArt.value = "";
            UpdateDiv('cbeDetailArtDesc', "")
        }
    }

    //% InsChangeField: Se controla el cambio de valor de los campos de la página.
    //-----------------------------------------------------------------------------
    function InsChangeField(Field) {
        //-----------------------------------------------------------------------------
        with (self.document.forms[0]) {
            switch (Field.name) {
                case "cbeBusinessTy":
                    if (li_cbeBusinessTy != cbeBusinessTy.value) {
                        valConstCat.disabled = true;
                        btnvalConstCat.disabled = true;
                        valConstCat.value = '';
                        UpdateDiv('valConstCatDesc', '');

                        valCodKind.disabled = true;
                        btnvalCodKind.disabled = true;
                        valCodKind.value = '';
                        UpdateDiv('valCodKindDesc', '');

                        valCommerGrp.disabled = true;
                        btnvalCommerGrp.disabled = true;
                        valCommerGrp.value = '';
                        UpdateDiv('valCommerGrpDesc', '');

                        li_cbeBusinessTy = cbeBusinessTy.value;
                    }

                    if ((Field.value != '') && (Field.value != '0')) {
                        valCommerGrp.disabled = false;
                        btnvalCommerGrp.disabled = false;
                        // Asignar el valor del parámetro Tipo del Grupo Comercial
                        valCommerGrp.Parameters.Param1.sValue = Field.value;
                        // Asignar el valor del parámetro Tipo del Giro de Negocio
                        valCodKind.Parameters.Param1.sValue = Field.value;
                        // Asignar el valor del parámetro Tipo del Tipo de Construccion
                        valConstCat.Parameters.Param1.sValue = Field.value;
                    }
                    break;
                case "valCommerGrp":
                    if (li_valCommerGrp != valCommerGrp.value) {
                        valConstCat.disabled = true;
                        btnvalConstCat.disabled = true;
                        valConstCat.value = '';
                        UpdateDiv('valConstCatDesc', '');

                        valCodKind.disabled = true;
                        btnvalCodKind.disabled = true;
                        valCodKind.value = '';
                        UpdateDiv('valCodKindDesc', '');

                        li_valCommerGrp = valCommerGrp.value;
                    }
                    if (Field.value != '') {
                        // Asignar el valor del parámetro Grupo Comercial del Giro de Negocio
                        valCodKind.disabled = false;
                        btnvalCodKind.disabled = false;
                        valCodKind.Parameters.Param2.sValue = Field.value;
                        //Asignar el valor del parámetro Grupo Comercial del Tipo de Construcción
                        valConstCat.Parameters.Param2.sValue = Field.value;
                    }
                    break;
                case "valCodKind":
                    if (li_valCodKind != valCodKind.value) {
                        valConstCat.disabled = true;
                        btnvalConstCat.disabled = true;
                        valConstCat.value = '';
                        UpdateDiv('valConstCatDesc', '');

                        li_valCodKind = valCodKind.value;
                    }

                    if (Field.value != '') {
                        // Asignar el valor del parámetro Giro de Negocio del Tipo de Construcción
                        valConstCat.disabled = false;
                        btnvalConstCat.disabled = false;
                        // Asignar el valor del parámetro Giro de Negocio del Tipo de Construcción
                        valConstCat.Parameters.Param3.sValue = Field.value;
                    }
                    break;
            }
        }
    }
</SCRIPT>
<META NAME="GENERATOR" Content="eTransaction Designer for Visual TIME">
<%
    If CDbl(IIf(Request.QueryString.Item("nMainAction") = "", 0, Request.QueryString.Item("nMainAction"))) = 401 Then
        mobjValues.ActionQuery = True
    End If

    Response.Write(mobjValues.StyleSheet())
    mobjMenu = New eFunctions.Menues
    Response.Write(mobjMenu.setZone(2, "RC001", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
    mobjMenu = Nothing
    Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript")))
%>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<%
    '+Se realiza el llamado a la funcion insPreRC001, para obtener los datos del cliente en tratamiento

    Call insPreRC001()
%>
<FORM METHOD="POST" ID="FORM" NAME="frmRC001" ACTION="valpolicyseq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%="<SCRIPT>"%>
<%="var li_cbeBusinessTy=" & lclsCivil.nBusinessTy & ";"%>
<%="var li_valCommerGrp=" & lclsCivil.nCommerGrp & ";"%>
<%="var li_valCodKind=" & lclsCivil.nCodKind & ";"%>
<%="</SCRIPT>"%>
  <TABLE WIDTH="100%">
        <TR>
            <TD COLSPAN="8" CLASS="HighLighted"><LABEL ID=0><A NAME="Clasificación del riesgo"><%= GetLocalResourceObject("AnchorCaption")%></A></LABEL></TD>                    
        </TR>
        <TR>
		    <TD COLSPAN="8" CLASS="Horline"></TD>		    
		</TR>
        <TR>
		<TD><LABEL ID=0><%= GetLocalResourceObject("cbeBusinessTyCaption")%></LABEL></TD>
		<TD><% mobjValues.Parameters.Add("sFlgActReg", lstrActReg, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		        Response.Write(mobjValues.PossiblesValues("cbeBusinessTy", "tabBusinessTy", eFunctions.Values.eValuesType.clngComboType, CStr(lclsCivil.nBusinessty), True, , , , , "InsChangeField(this);", CStr(Session("sBrancht")) = "2", , GetLocalResourceObject("cbeBusinessTyTooltip")))
	        %>
	    </TD>			
		<TD>&nbsp;</TD>			 
	    <TD><LABEL ID=0><%= GetLocalResourceObject("valCommerGrpCaption")%></LABEL></TD>	    	
	    <TD><%
mobjValues.Parameters.Add("nBusinessTy", lclsCivil.nBusinessTy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
mobjValues.Parameters.Add("sFlgActReg", lstrActReg, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

	            Response.Write(mobjValues.PossiblesValues("valCommerGrp", "tabCommerGrp", eFunctions.Values.eValuesType.clngWindowType, CStr(lclsCivil.nCommergrp), True, , , , , "InsChangeField(this);", lclsCivil.nCommergrp <= 0 Or CStr(Session("sBrancht")) = "2", , GetLocalResourceObject("valCommerGrpTooltip")))%></TD>
        <TD>&nbsp;</TD>
    	<TD><LABEL ID=0><%= GetLocalResourceObject("valCodKindCaption")%></LABEL></TD>	    		    	
		<TD><%
mobjValues.Parameters.Add("nBusinessTy", lclsCivil.nBusinessTy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
mobjValues.Parameters.Add("nCommerGrp", lclsCivil.nCommerGrp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
mobjValues.Parameters.Add("sFlgActReg", lstrActReg, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

		        Response.Write(mobjValues.PossiblesValues("valCodKind", "TabBussKind", eFunctions.Values.eValuesType.clngWindowType, CStr(lclsCivil.nCodkind), True, , , , , "InsChangeField(this);", lclsCivil.nCodkind <= 0 Or CStr(Session("sBrancht")) = "2", , GetLocalResourceObject("valCodKindTooltip")))%></TD>	    		    
		</TR>
		<TR>			
        <TD><LABEL ID=0><%= GetLocalResourceObject("tctDescBussiCaption")%></LABEL></TD>
        <TD COLSPAN = 4><%= mobjValues.TextControl("tctDescBussi", 30, lclsCivil.sDescBussi, , GetLocalResourceObject("tctDescBussiTooltip"), , , , , CStr(Session("sBrancht")) = "2")%></TD>
        <TD>&nbsp;</TD>			
        <TD><LABEL ID=0><%= GetLocalResourceObject("valConstCatCaption")%></LABEL></TD>  
        <TD><%mobjValues.Parameters.Add("nBusinessTy", lclsCivil.nBusinessTy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
mobjValues.Parameters.Add("nCommerGrp", lclsCivil.nCommerGrp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
mobjValues.Parameters.Add("nCodKind", lclsCivil.nCodKind, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
mobjValues.Parameters.Add("sFlgActReg", lstrActReg, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

If lclsCivil.nBusinessTy <> eRemoteDB.Constants.intNull And lclsCivil.nCommerGrp <> eRemoteDB.Constants.intNull Then
	mblnDatConst = False
Else
	mblnDatConst = True
End If

                Response.Write(mobjValues.PossiblesValues("valConstCat", "TabConstClass", eFunctions.Values.eValuesType.clngWindowType, CStr(lclsCivil.nConstCat), True, , , , , , mblnDatConst Or CStr(Session("sBrancht")) = "2", , GetLocalResourceObject("valConstCatTooltip")))%>
        </TD>            
		</TR>      
        <TR>
            <TD COLSPAN="8" CLASS="HighLighted"><LABEL ID=0><A NAME="Unidades"><%= GetLocalResourceObject("Anchor2Caption")%></A></LABEL></TD>
        </TR>
        <TR>
		    <TD COLSPAN="8" CLASS="Horline"></TD>		    
		</TR>        
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("cbeUnit_typeCaption")%></LABEL></TD>
            <TD><%= mobjValues.PossiblesValues("cbeUnit_type", "TABLE242", eFunctions.Values.eValuesType.clngComboType, CStr(lclsCivil.nUnit_type), , , , , , , False, 2, GetLocalResourceObject("cbeUnit_typeTooltip"), eFunctions.Values.eTypeCode.eNumeric)%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=0><%= GetLocalResourceObject("tcnUnit_quanCaption")%></LABEL></TD>
            <TD><%= mobjValues.NumericControl("tcnUnit_quan", 4, mobjValues.StringToType(CStr(lclsCivil.nUnit_quan), eFunctions.Values.eTypeData.etdLong, True), False, GetLocalResourceObject("tcnUnit_quanTooltip"), , , , , , , False)%></TD>
            <TD>&nbsp;</TD>
        </TR>
  </TABLE>
<SCRIPT>
    with (self.document.forms[0]) {
        if (cbeBusinessTy.value == 0) {
            valCommerGrp.value = '';
            valCodKind.value = '';
            valConstCat.value = '';
            UpdateDiv('valCommerGrpDesc', '');
            UpdateDiv('valCodKindDesc', '');
            UpdateDiv('valConstCatDesc', '');
            btnvalCommerGrp.disabled = true;
            btnvalCodKind.disabled = true;
            btnvalConstCat.disabled = true;
            valCommerGrp.disabled = true;
            valCodKind.disabled = true;
            valConstCat.disabled = true;
        }
    }
</SCRIPT>
</FORM>
</BODY>
</HTML>
    
<%
    mobjValues = Nothing
    lclsCivil = Nothing
%>
<%'^Begin Footer Block VisualTimer Utility 1.1 27/05/2003 07:39:47 a.m.
    Call mobjNetFrameWork.FinishPage("RC001")
    mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>











<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCoReinsuran" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues

'- Se define la variable mobjGrid para el manejo del Grid de la ventana
Dim mobjGrid As eFunctions.Grid

'- Se define la variable modular utilizada para la carga y actualización de datos de la forma
Dim mclsContrnpro As eCoReinsuran.Contrnpro

Dim sReinsuran_1 As Object
Dim sReinsuran_2 As Object



'% insPreCR305: Realiza la lectura para la carga de los datos de la forma
'------------------------------------------------------------------------------------------------
Private Sub insPreCR305()
	'------------------------------------------------------------------------------------------------	
	Call mclsContrnpro.Find(Session("nNumber"), Session("nType"), Session("nBranch_rei"), Session("dEffecdate"), True)
	
	If mclsContrnpro.sReinsuran = "2" Then
		sReinsuran_2 = 1
		sReinsuran_1 = ""
	Else
		sReinsuran_1 = 1
		sReinsuran_2 = ""
	End If
End Sub


'%insDefineGrid. Definición de columnas del GRID
'--------------------------------------------------------------------------------------------
Private Sub insDefineGrid()
	'--------------------------------------------------------------------------------------------	
	mobjGrid.sCodisplPage = "cr305"
	
        If Not Request.QueryString.Item("nMainAction") = eFunctions.Menues.TypeActions.clngActionQuery Then
            mobjGrid.ActionQuery = Session("bQuery")
        Else
            mobjGrid.bOnlyForQuery = False
        End If
        
        
	With mobjGrid
		.Codispl = Request.QueryString.Item("sCodispl")
		.Width = 350
		.Height = 250
		.Top = 120
	End With
	'+Se definen todas las columnas del Grid
	With mobjGrid.Columns
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnYearColumnCaption"), "tcnYear", 4, "",  , GetLocalResourceObject("tcnYearColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnMonthColumnCaption"), "tcnMonth", 2, "",  , GetLocalResourceObject("tcnMonthColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPaySessColumnCaption"), "tcnPaySess", 18, "",  , GetLocalResourceObject("tcnPaySessColumnToolTip"), True, 6)
		Call .AddCheckColumn(0, GetLocalResourceObject("chkAccountColumnCaption"), "chkAccount", "",  ,  ,  , True)
		Call .AddHiddenColumn("hddPrem_dep", "")
		Call .AddHiddenColumn("hddPlan_pay", "")
	End With
	
	With mobjGrid
		.DeleteButton = True
		.AddButton = True
		If Request.QueryString.Item("nPlan_pay") <> "0" Then
			.Columns("tcnYear").EditRecord = True
		End If
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		
		If Request.QueryString.Item("Action") = "Update" Then
			.Columns("tcnYear").Disabled = True
			.Columns("tcnMonth").Disabled = True
		End If
		
		If Session("bQuery") Then
			.DeleteButton = False
			.AddButton = False
			.Columns("Sel").GridVisible = False
			.bOnlyForQuery = True
		End If
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		.sDelRecordParam = "nYear='+ marrArray[lintIndex].tcnYear + '" & "&nMonth='+ marrArray[lintIndex].tcnMonth + '"
	End With
End Sub

'% DoFormCR731: Esta función se encarga de cargar los datos en la forma "Folder" 
'--------------------------------------------------------------------------------------------
Private Sub DoFormCR305()
	'--------------------------------------------------------------------------------------------
	Dim lclsContrnpro As eCoReinsuran.Contrnpro
	Dim lcolContrnpros As eCoReinsuran.Contrnpros
	Dim lintAux As Byte
	
	lclsContrnpro = New eCoReinsuran.Contrnpro
	lcolContrnpros = New eCoReinsuran.Contrnpros
	
	lintAux = 1
	If mclsContrnpro.sAgreementPays = "2" Then
		lintAux = 2
	End If
	
	If Request.QueryString.Item("blnPlan_pay") = "false" Then
		lintAux = 2
	Else
		If Request.QueryString.Item("blnPlan_pay") = "true" Then
			lintAux = 1
		End If
	End If
	mobjGrid.DeleteButton = False
	mobjGrid.AddButton = False
	
	If lintAux = 1 Then
		mobjGrid.DeleteButton = True
		mobjGrid.AddButton = True
		mobjGrid.Columns("tcnYear").EditRecord = True
		
		If lcolContrnpros.Find(Session("nNumber"), Session("nType"), Session("nBranch_rei"), Session("dEffecdate")) Then
			For	Each lclsContrnpro In lcolContrnpros
				With mobjGrid
					.Columns("tcnYear").DefValue = CStr(lclsContrnpro.nYear)
					.Columns("tcnMonth").DefValue = CStr(lclsContrnpro.nMonth)
					.Columns("tcnPaySess").DefValue = CStr(lclsContrnpro.nAmount)
					.Columns("chkAccount").DefValue = lclsContrnpro.sCuenTecn
				End With
				Response.Write(mobjGrid.DoRow)
			Next lclsContrnpro
		End If
	End If
	
	'Se llama a la propiedad CloseTable, para dar por finalizada la creación de la tabla (GRID)	
	Response.Write(mobjGrid.CloseTable())
	
	If Not IsNothing(Request.QueryString.Item("blnPlan_pay")) Then
		Response.Write("<SCRIPT>self.document.forms[0].chkPlan_pay.checked=" & Request.QueryString.Item("blnPlan_pay") & ";</" & "Script>")
	End If
	
	lclsContrnpro = Nothing
	lcolContrnpros = Nothing
        'Response.Write("<SCRIPT>DisabledFields(self.document.forms[0].chkPlan_pay,2);</" & "Script>")
End Sub

'% DoFormCR305Upd. Se define esta funcion para contruir el contenido de la ventana UPD de las Compañías participantes
'--------------------------------------------------------------------------------------------------------------------
Private Sub DoFormCR305Upd()
	'--------------------------------------------------------------------------------------------------------------------		
	Dim lblnPost As Boolean
	Dim lclsContrnpro As eCoReinsuran.Contrnpro
	
	If Request.QueryString.Item("Action") = "Del" Then
		Response.Write(mobjValues.ConfirmDelete())
		With Request
			lclsContrnpro = New eCoReinsuran.Contrnpro
			lblnPost = lclsContrnpro.InsPostCR305("CR305", 303, Session("nUsercode"), Session("dEffecdate"), Session("nNumber"), Session("nType"), Session("nBranch_rei"), eRemoteDB.Constants.StrNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, mobjValues.StringToType(Request.QueryString.Item("nYear"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nMonth"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.intNull, "PopUp", eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull)
			
			If lblnPost Then
				Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valCoReinsuran.aspx", "CR305", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
			End If
			
		End With
		lclsContrnpro = Nothing
	Else
		Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valCoReinsuran.aspx", "CR305", Request.QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(Request.QueryString.Item("Index"))))
		
		Response.Write("<SCRIPT>self.document.forms[0].hddPrem_dep.value = top.opener.document.forms[0].elements['tcnPrem_dep'].value;</" & "Script>")
		Response.Write("<SCRIPT>self.document.forms[0].hddPlan_pay.value = top.opener.document.forms[0].elements['chkPlan_pay'].checked;</" & "Script>")
	End If
	
End Sub

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mobjGrid = New eFunctions.Grid
mclsContrnpro = New eCoReinsuran.Contrnpro

mobjValues.sCodisplPage = "cr305"

If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "CR305", "CR305.aspx"))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction='" & Request.QueryString.Item("nMainAction") & "';</SCRIPT>")
End If
mobjValues.ActionQuery = Session("bQuery")

Call insPreCR305()
%>
<SCRIPT>
    //- Variable para el control de versiones.
    document.VssVersion = "$$Revision: 2 $|$$Date: 15/10/03 16.59 $|$$Author: Nvaplat60 $"

    // EnabledFields: Habilita los campos de acuerdo al tipo de contrato y la acción
    //--------------------------------------------------------------------------------
    function EnabledFields(Field) {
        //--------------------------------------------------------------------------------

            switch (Field.name) {
                //Tasa Fija.  
                case "tcnRate_fij":
                    {
                        if (Field.value != 0) {
                            self.document.forms[0].tcnRate_min.disabled = true;
                            self.document.forms[0].tcnRate_max.disabled = true;
                            self.document.forms[0].tcnPrem_fij.disabled = true;
                            //self.document.forms[0].tctRoutine.disabled = true;
                            self.document.forms[0].chkPlan_pay.disabled = true;
                            //self.document.forms[0].tctRoutine.value = '';
                            self.document.forms[0].tcnPrem_fij.value = '0';
                            self.document.forms[0].tcnRate_min.value = '0';
                            self.document.forms[0].tcnRate_max.value = '0';
                            break;
                        }
                        else {
                            self.document.forms[0].tcnRate_min.disabled = false;
                            self.document.forms[0].tcnRate_max.disabled = false;
                            self.document.forms[0].tcnPrem_fij.disabled = false;
                            //self.document.forms[0].tctRoutine.disabled = false;
                            self.document.forms[0].chkPlan_pay.disabled = false;
                            break;
                        }
                    }
                case "tcnRate_min":
                    {
                        if (Field.value != 0) {
                            self.document.forms[0].tcnRate_fij.disabled = true;
                            self.document.forms[0].tcnRate_fij.value = '';
                            break;
                        }
                        else {
                            self.document.forms[0].tcnRate_fij.disabled = false;
                            break;
                        }
                    }
                case "tcnRate_max":
                    {
                        if (Field.value != 0) {
                            self.document.forms[0].tcnRate_fij.disabled = true;
                            self.document.forms[0].tcnRate_fij.value = '';
                            break;
                        }
                        else {
                            self.document.forms[0].tcnRate_fij.disabled = false;
                            break;
                        }
                    }
                case "tcnPrem_fij":
                    {
                        if (Field.value != 0) {
                            self.document.forms[0].tcnRate_fij.disabled = true;
                            self.document.forms[0].tcnRate_min.disabled = true;
                            self.document.forms[0].tcnRate_max.disabled = true;
                            self.document.forms[0].tcnPrem_min.disabled = true;
                            //self.document.forms[0].tctRoutine.disabled = true;
                            self.document.forms[0].chkPlan_pay.disabled = true;
                            //self.document.forms[0].tctRoutine.value = '';
                            self.document.forms[0].tcnRate_fij.value = '0';
                            self.document.forms[0].tcnRate_min.value = '0';
                            self.document.forms[0].tcnRate_max.value = '0';
                            self.document.forms[0].tcnPrem_min.value = '0';
                            break;
                        }
                        else {
                            self.document.forms[0].tcnRate_fij.disabled = false;
                            self.document.forms[0].tcnRate_min.disabled = false;
                            self.document.forms[0].tcnRate_max.disabled = false;
                            self.document.forms[0].tcnPrem_min.disabled = false;
                            //self.document.forms[0].tctRoutine.disabled = false;
                            self.document.forms[0].chkPlan_pay.disabled = false;
                            break;
                        }
                        with (self.document.forms[0]) {
                            lstrAction = self.document.location.href
                            lstraux = lstrAction
                            lstrAction = lstrAction.replace(/\?.*/, '') + '?sCodispl=CR305' + '&nPlan_pay=' + chkPlan_pay.value + '&blnPlan_pay=' + chkPlan_pay.checked + '&nMainAction=' + lstraux.replace(/http.*\&nMainAction=/, '')
                            self.document.location.href = lstrAction;
                        }

                    }
            }
        }
    }

    // DisabledFields: Desabilita los campos Tasa, Prima y rutina si se indica el plan de pago.
    //--------------------------------------------------------------------------------
    function DisabledFields(Field, lintReload) {
        //--------------------------------------------------------------------------------
        var lstrAction
        var lstraux

        if (self.document.forms[0].tcnPrem_fij.value != 0) {
            self.document.forms[0].tcnRate_fij.disabled = true;
            self.document.forms[0].tcnRate_min.disabled = true;
            self.document.forms[0].tcnRate_max.disabled = true;
            self.document.forms[0].tcnPrem_min.disabled = true;
            self.document.forms[0].chkPlan_pay.disabled = true;
            self.document.forms[0].tcnRate_fij.value = '0';
            self.document.forms[0].tcnRate_min.value = '0';
            self.document.forms[0].tcnRate_max.value = '0';
            self.document.forms[0].tcnPrem_min.value = '0';
        }
        if (self.document.forms[0].chkPlan_pay.checked == false) {
            self.document.forms[0].tcnRate_fij.disabled = false;
            self.document.forms[0].tcnRate_min.disabled = false;
            self.document.forms[0].tcnRate_max.disabled = false;
            self.document.forms[0].tcnPrem_min.disabled = false;
            self.document.forms[0].tcnPrem_fij.disabled = false;
        }
        if (self.document.forms[0].chkPlan_pay.checked == true) {
            self.document.forms[0].tcnRate_fij.disabled = true;
            self.document.forms[0].tcnRate_min.disabled = true;
            self.document.forms[0].tcnRate_max.disabled = true;
            self.document.forms[0].tcnPrem_min.disabled = true;
            self.document.forms[0].tcnPrem_fij.disabled = true;
            self.document.forms[0].tcnRate_fij.value = '0';
            self.document.forms[0].tcnRate_min.value = '0';
            self.document.forms[0].tcnRate_max.value = '0';
            self.document.forms[0].tcnPrem_min.value = '0';
            self.document.forms[0].tcnPrem_fij.value = '0';

        }
        if (lintReload == 1) {
            with (self.document.forms[0]) {
                lstrAction = self.document.location.href
                lstraux = lstrAction
                lstrAction = lstrAction.replace(/\?.*/, '') + '?sCodispl=CR305' + '&blnPlan_pay=' + chkPlan_pay.checked + '&nMainAction=' + lstraux.replace(/http.*\&nMainAction=/, '')
                self.document.location.href = lstrAction;
            }
        }

    }

    // DisabledRoutine: Desabilita los campos de plan de pago, Tasa minima, tasa maxima ni Prima minima 
    //                  y los respectivos valores se llevan a cero
    //--------------------------------------------------------------------------------
    function DisabledRoutine(Field) {
        //--------------------------------------------------------------------------------
        if (Field > '') {
            /*self.document.forms[0].tcnRate_min.disabled = true;
            self.document.forms[0].tcnRate_max.disabled = true;*/
            self.document.forms[0].tcnPrem_fij.disabled = true;
            self.document.forms[0].chkPlan_pay.disabled = true;
            /*self.document.forms[0].tcnRate_min.value = '0';
            self.document.forms[0].tcnRate_max.value = '0';*/
            self.document.forms[0].tcnPrem_fij.value = '0';
        }
        else {
            /*self.document.forms[0].tcnRate_min.disabled = false;
            self.document.forms[0].tcnRate_max.disabled = false;*/
            self.document.forms[0].tcnPrem_fij.disabled = false;
            self.document.forms[0].chkPlan_pay.disabled = false;
            /*self.document.forms[0].tcnRate_min.value = '';
            self.document.forms[0].tcnRate_max.value = '';*/
            self.document.forms[0].tcnPrem_fij.value = '';
        }

    }
    function ChangePren_Dep() {

        self.document.forms[0].action = self.document.forms[0].action + "&nPrem_dep=" + self.document.forms[0].tcnPrem_dep.value

    }
    // EnabledFields: Habilita los campos de acuerdo al tipo de contrato y la acción
    //--------------------------------------------------------------------------------
    function DisabledEpiCapital(Field) {
        //--------------------------------------------------------------------------------
        
        switch (Field.name) {
            //Tasa Fija.  
            case "tcnEpi":
                {
                    if (Field.value != 0) {
                        self.document.forms[0].tcnCapitalref.disabled = true;
                        self.document.forms[0].tcnCapitalref.value = '0';
                        break;
                    }
                    else {
                        self.document.forms[0].tcnCapitalref.disabled = false;
                        self.document.forms[0].tcnCapitalref.value = '';
                        break;
                    }
                }
            case "tcnCapitalref":
                {
                    if (Field.value != 0) {
                        self.document.forms[0].tcnEpi.disabled = true;
                        self.document.forms[0].tcnEpi.value = '0';
                        break;
                    }
                    else {
                        self.document.forms[0].tcnEpi.disabled = false;
                        self.document.forms[0].tcnEpi.value = '';
                        break;
                    }
                }
     
        }
    }
</SCRIPT>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
    <%
Response.Write(mobjValues.StyleSheet())
Response.Write(mobjValues.WindowsTitle("CR305"))
Response.Write(mobjValues.ShowWindowsName("CR305"))
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<TD><BR></TD>
<FORM METHOD="post" ID="FORM" NAME="frmCR305" ACTION="valCoReinsuran.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">

    <%If Request.QueryString.Item("Type") <> "PopUp" Then%>

		<TABLE WIDTH="100%">
			<TR>                       
				<TD WIDTH="45%" COLSPAN="2" CLASS="HighLighted"><LABEL><A NAME="Aplicación"><%= GetLocalResourceObject("AnchorAplicación2Caption") %></A></LABEL></TD>
		    </TR>        
		    <TR>
			    <TD COLSPAN="2"><HR></TD>		    
		    </TR>
		    <TR>
		        <TD><%=mobjValues.OptionControl(0, "optReinsuran", GetLocalResourceObject("optReinsuran_1Caption"), sReinsuran_1, "1",  ,  ,  , GetLocalResourceObject("optReinsuran_1ToolTip"))%></TD>
		        <TD><%=mobjValues.OptionControl(0, "optReinsuran", GetLocalResourceObject("optReinsuran_2Caption"), sReinsuran_2, "2",  ,  ,  , GetLocalResourceObject("optReinsuran_2ToolTip"))%></TD>
		        <TD WIDTH="10%">&nbsp;</TD>            
		        <TD><LABEL><%= GetLocalResourceObject("tcnPrem_depCaption") %></LABEL></TD>
				<%	If Not IsNothing(Request.QueryString.Item("nPrem_dep")) Then%>		        
					<TD><%=mobjValues.NumericControl("tcnPrem_dep", 18, Request.QueryString.Item("nPrem_dep"),  , GetLocalResourceObject("tcnPrem_depToolTip"), True, 6,  ,  ,  , "ChangePren_Dep(this.value);")%></TD>
			    <%	Else%>
					<TD><%=mobjValues.NumericControl("tcnPrem_dep", 18, CStr(mclsContrnpro.nPrem_dep),  , GetLocalResourceObject("tcnPrem_depToolTip"), True, 6,  ,  ,  , "ChangePren_Dep(this.value);")%></TD>
			    <%	End If%>    
		    </TR>
            <TR>
            <TD>&nbsp;</TD>    
            <TD>&nbsp;</TD>    
            <TD>&nbsp;</TD>    
            <TD>&nbsp;</TD>    
            <TD>&nbsp;</TD>    
            </TR>
		    <TR>                   
		        <TD><LABEL><%= GetLocalResourceObject("tcnEpiCaption") %></LABEL></TD>
		        <TD><%=mobjValues.NumericControl("tcnEpi", 18, mclsContrnpro.nEpi, , GetLocalResourceObject("tcnEpiToolTip"), True, 6, , , , "DisabledEpiCapital(this)")%></TD>
                <TD>&nbsp;</TD>
                <TD><LABEL><%= GetLocalResourceObject("tcnCapitalrefCaption") %></LABEL></TD>
		        <TD><%=mobjValues.NumericControl("tcnCapitalref", 18, mclsContrnpro.nCapitalref, , GetLocalResourceObject("tcnCapitalrefToolTip"),true , 6, ,,, "DisabledEpiCapital(this)")%></TD>
		    </TR>        
            
            <TR>                   
		        <TD><LABEL><%= GetLocalResourceObject("tctRoutineCaption") %></LABEL></TD>
		        <TD><%=mobjValues.TextControl("tctRoutine", 12, mclsContrnpro.sRouCessPR, , GetLocalResourceObject("tctRoutineToolTip"), , , , "DisabledRoutine(this.value);")%></TD>
                <TD>&nbsp;</TD>
                <TD><LABEL><%= GetLocalResourceObject("tcnTaxCaption") %></LABEL></TD>
		        <TD><%=mobjValues.NumericControl("tcnTax", 5, mclsContrnpro.nTax,  , GetLocalResourceObject("tcnTaxToolTip"), , 3, , "")%></TD>
		    </TR>        
            

		    <TR>
		        <TD><LABEL><%= GetLocalResourceObject("tcnClaimadjCaption") %></LABEL></TD>
		        <TD><%=mobjValues.NumericControl("tcnClaimadj", 9, mclsContrnpro.nClaimadj,  , GetLocalResourceObject("tcnClaimadjToolTip"), , 6, , "")%></TD>
		        <TD>&nbsp;</TD>
		        <%	If mclsContrnpro.sAgreementPays <> vbNullString And mclsContrnpro.sAgreementPays = "1" Then%>
					<TD><%=mobjValues.CheckControl("chkPlan_pay", GetLocalResourceObject("chkPlan_payCaption"), mclsContrnpro.sAgreementPays, CStr(1), "DisabledFields(this,1);", False,  , GetLocalResourceObject("chkPlan_payToolTip"))%></TD>
				<%	Else%>
		        
					<%		If Request.QueryString.Item("nPlan_pay") <> vbNullString And Request.QueryString.Item("nPlan_pay") = "1" Then%>
						<TD><%=mobjValues.CheckControl("chkPlan_pay", GetLocalResourceObject("chkPlan_payCaption"), CStr(True), "1", "DisabledFields(this,1);", False,  , GetLocalResourceObject("chkPlan_payToolTip"))%></TD>
					<%		Else%>
					    <TD><%=mobjValues.CheckControl("chkPlan_pay", GetLocalResourceObject("chkPlan_payCaption"), CStr(False), "2", "DisabledFields(this,1);", False,  , GetLocalResourceObject("chkPlan_payToolTip"))%></TD>
					<%		End If%>
				<%	End If%>
		    </TR>
		</TABLE>		        
		<TABLE WIDTH="100%">
			<TR>	
				<TD>&nbsp;</TD>  
			</TR>   	
			<TR>                       
				<TD WIDTH="45%" COLSPAN="4" CLASS="HighLighted"><LABEL ID=100648><A NAME="Tasa"><%= GetLocalResourceObject("AnchorTasa2Caption") %></A></LABEL></TD>
		        <TD WIDTH="10%">&nbsp;</TD>
		        <TD WIDTH="10%">&nbsp;</TD>                        
		        <TD COLSPAN="4" CLASS="HighLighted"><LABEL ID=100649><A NAME="Prima"><%= GetLocalResourceObject("AnchorPrima2Caption") %></A></LABEL></TD>
		    </TR>        
		    <TR>
			    <TD COLSPAN="4"><HR></TD>		    
			    <TD WIDTH="10%">&nbsp;</TD>
			    <TD COLSPAN="4"><HR></TD>
		    </TR>      								            		 	            	
		    <TR>                       
		        <TD><LABEL ID=100650><%= GetLocalResourceObject("tcnRate_fijCaption") %></LABEL></TD>
		        <TD><%=mobjValues.NumericControl("tcnRate_fij", 6, CStr(mclsContrnpro.nRate_fij),  , GetLocalResourceObject("tcnRate_fijToolTip"), True, 4,  ,  ,  , "EnabledFields(this)")%></TD>
		        <TD><LABEL ID=100651><%= GetLocalResourceObject("tcnRate_minCaption") %></LABEL></TD>            
		        <TD><%=mobjValues.NumericControl("tcnRate_min", 6, CStr(mclsContrnpro.nRate_min),  , GetLocalResourceObject("tcnRate_minToolTip"), True, 4,  ,  ,  , "EnabledFields(this)")%></TD>
		        <TD WIDTH="10%">&nbsp;</TD>            
		        <TD><LABEL ID=100652><%= GetLocalResourceObject("tcnPrem_fijCaption") %></LABEL></TD>
		        <TD><%=mobjValues.NumericControl("tcnPrem_fij", 18, CStr(mclsContrnpro.nPrem_fij),  , GetLocalResourceObject("tcnPrem_fijToolTip"), True, 6,  ,  ,  , "EnabledFields(this)")%></TD>            
		    </TR>
		    <TR>
				<TD WIDTH="10%">&nbsp;</TD>
				<TD WIDTH="10%">&nbsp;</TD>
		        <TD><LABEL ID=100653><%= GetLocalResourceObject("tcnRate_maxCaption") %></LABEL></TD>
		        <TD><%=mobjValues.NumericControl("tcnRate_max", 6, CStr(mclsContrnpro.nRate_max),  , GetLocalResourceObject("tcnRate_maxToolTip"), True, 4,  ,  ,  , "EnabledFields(this)")%></TD>
		        <TD WIDTH="10%">&nbsp;</TD>            
		        <TD><LABEL ID=100654><%= GetLocalResourceObject("tcnPrem_minCaption") %></LABEL></TD>
		        <TD><%=mobjValues.NumericControl("tcnPrem_min", 18, CStr(mclsContrnpro.nPrem_min),  , GetLocalResourceObject("tcnPrem_minToolTip"), True, 6)%></TD>
		    </TR>
		</TABLE>
		
		<TABLE WIDTH="100%">
			<BR><BR>
			<TR>                       
				<TD CLASS="HighLighted"><LABEL ID=0><A NAME="PlanPay"><%= GetLocalResourceObject("AnchorPlanPay2Caption") %></A></LABEL></TD>
		    </TR>        
		    <TR>
			    <TD><HR></TD>		    
		    </TR>
		</TABLE>
	<%End If
	    Response.Write("<SCRIPT>")
	    If Request.QueryString.Item("nMainAction") <> 401 Then
	        If mclsContrnpro.nEpi > 0 Then
	            Response.Write("self.document.forms[0].tcnCapitalref.disabled = true;")
	            Response.Write("self.document.forms[0].tcnCapitalref.value = 0;")
	        Else
	            Response.Write("self.document.forms[0].tcnEpi.disabled = true;")
	            Response.Write("self.document.forms[0].tcnEpi.value = 0;")
	        End If
	        If mclsContrnpro.nRate_fij > 0 Then
	            Response.Write("self.document.forms[0].tcnPrem_fij.disabled = true;")
	            Response.Write("self.document.forms[0].tcnRate_min.disabled = true;")
	            Response.Write("self.document.forms[0].tcnRate_max.disabled = true;")
	            Response.Write("self.document.forms[0].tcnPrem_min.disabled = true;")
	            Response.Write("self.document.forms[0].chkPlan_pay.disabled = true;")
	            Response.Write("self.document.forms[0].tcnRate_fij.value = 0;")
	            Response.Write("self.document.forms[0].tcnRate_min.value = 0;")
	            Response.Write("self.document.forms[0].tcnRate_max.value = 0;")
	            Response.Write("self.document.forms[0].tcnPrem_min.value = 0;")
	            Response.Write("self.document.forms[0].tcnPrem_fij.value = 0;")
	        End If

	        If mclsContrnpro.nRate_min > 0 Then
	            Response.Write("self.document.forms[0].tcnPrem_fij.disabled = true;")
	            Response.Write("self.document.forms[0].tcnPrem_min.disabled = true;")
	            Response.Write("self.document.forms[0].chkPlan_pay.disabled = true;")
	            Response.Write("self.document.forms[0].tcnRate_fij.value = 0;")
	            Response.Write("self.document.forms[0].tcnPrem_min.value = 0;")
	            Response.Write("self.document.forms[0].tcnPrem_fij.value = 0;")
	        End If

	        If mclsContrnpro.nRate_max > 0 Then
	            Response.Write("self.document.forms[0].tcnPrem_fij.disabled = true;")
	            Response.Write("self.document.forms[0].tcnPrem_min.disabled = true;")
	            Response.Write("self.document.forms[0].chkPlan_pay.disabled = true;")
	            Response.Write("self.document.forms[0].tcnRate_fij.value = 0;")
	            Response.Write("self.document.forms[0].tcnPrem_min.value = 0;")
	            Response.Write("self.document.forms[0].tcnPrem_fij.value = 0;")
	        End If
	        Response.Write("</" & "Script>")
	    End If
	    
	    Call insDefineGrid()
	    If Request.QueryString.Item("Type") <> "PopUp" Then
	        Call DoFormCR305()
	        Response.Write(mobjValues.BeginPageButton)
	    Else
	        Call DoFormCR305Upd()
	    End If
	    mobjGrid = Nothing%>
</FORM>
</BODY>
</HTML>
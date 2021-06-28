<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eBranches" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenu As eFunctions.Menues

'- Declaraciòn de Variables locales
Dim mblnVisible As Object
Dim mblnDisabled As Boolean

'- Declaraciòn de Variables para la recarga y bùsqueda
Dim mlngTariff As Object
Dim mstrDefaulti As String
Dim mstrChanges As String
Dim mlngBenef_type As Object
Dim mdblLimit As Object
Dim mdblDeduc_amount As Object


    Dim mintModulec As Object    
    Dim mintCover As Object
    Dim mstrDescript As String
    
'% insLoadDP057: Dibuja los campos no repetitivos de la pantalla, con sus respectivos
'  valores segùn sea el caso.
'------------------------------------------------------------------------------------------
Private Sub insLoadDP057()
	'------------------------------------------------------------------------------------------
        Dim lblnModul As Boolean
        Dim lclsProduct As eProduct.Product
        
        lblnModul = True
	
        lclsProduct = New eProduct.Product
	
        If lclsProduct.IsModule(Session("nBranch"), Session("nProduct"), Session("dEffecdate")) Then
            lblnModul = False
        End If
        
                
Response.Write("" & vbCrLf)
Response.Write("    <TABLE BORDER=0 WIDTH=""100%"">" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
        
        Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("cbeModulecCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("	        <TD>" & vbCrLf)
        Response.Write("		    ")

	
        With mobjValues
            Call .Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Call .Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Call .Parameters.Add("dEffecdate", Session("deffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Response.Write(mobjValues.PossiblesValues("cbeModulec", "tabtab_modul", eFunctions.Values.eValuesType.clngComboType, mintModulec, True, , , , , "changeValues(this)", lblnModul, , GetLocalResourceObject("cbeModulecToolTip")))
        End With
	
        Response.Write("" & vbCrLf)
        Response.Write("	        </TD>" & vbCrLf)
        Response.Write("	        <TD><LABEL ID=0>" & GetLocalResourceObject("valCoverCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD>" & vbCrLf)
        Response.Write("		    ")

	
        With mobjValues
            Call .Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Call .Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Call .Parameters.Add("nModulec", mintModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Call .Parameters.Add("dEffecdate", Session("deffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Response.Write(mobjValues.PossiblesValues("valCover", "tablife_covmod", eFunctions.Values.eValuesType.clngWindowType, mintCover, True,  , , , , "", , , GetLocalResourceObject("valCoverToolTip"),,,,true))
        End With

        Response.Write("</TD>          " & vbCrLf)
        Response.Write("        </TR>    " & vbCrLf)
        Response.Write("        <TR>    " & vbCrLf)

        

        Response.Write("            <TD><LABEL ID=0>" & GetLocalResourceObject("tcnTariffCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD>")

        Response.Write(mobjValues.NumericControl("tcnTariff", 2, mlngTariff, True, GetLocalResourceObject("tcnTariffToolTip"), , , , , , "LoadSeqTarAttMed(this)"))
        Response.Write("</TD>" & vbCrLf)
        Response.Write("            <TD><LABEL ID=0>" & GetLocalResourceObject("tctDescript") & "</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD>")

        Response.Write(mobjValues.TextControl("tctDescript", 30, mstrDescript, True, GetLocalResourceObject("tctDescriptToolTip")))
        mobjValues.ActionQuery = Session("bQuery")
        Response.Write("</TD>" & vbCrLf)
        Response.Write("        </TR>    " & vbCrLf)
        Response.Write("        <TR>    " & vbCrLf)


        Response.Write("            <TD><LABEL ID=0>" & GetLocalResourceObject("cbeBenefTypeCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD>")
        

        Response.Write(mobjValues.PossiblesValues("cbeBenefType", "table270", eFunctions.Values.eValuesType.clngComboType, mlngBenef_type, , , , , , , , , GetLocalResourceObject("cbeBenefTypeToolTip")))

        
        Response.Write("</TD>          " & vbCrLf)
        Response.Write("            <TD>")


Response.Write(mobjValues.CheckControl("chkDefaulti", GetLocalResourceObject("chkDefaultiCaption"), mstrDefaulti, CStr(1)))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.CheckControl("chkChanges", GetLocalResourceObject("chkChangesCaption"), mstrChanges, CStr(1)))


        Response.Write("</TD> " & vbCrLf)
        Response.Write("        </TR>    " & vbCrLf)
        Response.Write("        <TR>    " & vbCrLf)

Response.Write("            <TD><LABEL ID=0>" & GetLocalResourceObject("tcnLimitCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.NumericControl("tcnLimit", 18, mdblLimit,  , GetLocalResourceObject("tcnLimitToolTip"), True, 6))


Response.Write("</TD>" & vbCrLf)
        Response.Write("            <TD><LABEL ID=0>" & GetLocalResourceObject("tcnDed_amountCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.NumericControl("tcnDed_amount", 18, mdblDeduc_amount,  , GetLocalResourceObject("tcnDed_amountToolTip"), True, 6))


Response.Write("</TD>          " & vbCrLf)
Response.Write("        </TR>       " & vbCrLf)
Response.Write("    </TABLE>")

	
	
End Sub

'% insOldValues: Se encarga de asignar el valor de las variables  vbscript, a las
'% variables JavaScript
'------------------------------------------------------------------------------------------
Private Sub insOldValues()
	'-----------------------------------------------------------------------------------------
        If mstrChanges <> "" And _
            mlngBenef_type <> eRemoteDB.Constants.intNull And _
            mstrDefaulti <> "" And _
            mlngTariff <> eRemoteDB.Constants.intNull And _
            mstrChanges <> "" And _
            mdblDeduc_amount <> eRemoteDB.Constants.intNull And _
            mintModulec <> eRemoteDB.Constants.intNull Then
            
            With Response
                .Write("<SCRIPT>")
                .Write("var mlngTariff         = " & mlngTariff & ";")
                .Write("var mstrDefaulti       = " & mstrDefaulti & ";")
                .Write("var mstrChanges        = " & mstrChanges & ";")
                .Write("var mdblLimit          = " & mdblLimit & ";")
                .Write("var mlngBenef_type     = " & mlngBenef_type & ";")
                .Write("var mdblDeduc_amount   = " & mdblDeduc_amount & ";")
                .Write("var mintModulec        = " & mintModulec & ";")
                .Write("var mintCover          = " & mintCover & ";")
                .Write("var mstrDescript = '" & mstrDescript & "';")
                .Write("</" & "Script>")
            End With
        Else
            With Response
                .Write("<SCRIPT>")
                .Write("var mlngTariff         = 1;")
                .Write("var mstrDefaulti       = 0;")
                .Write("var mstrChanges        = 0;")
                .Write("var mdblLimit          = 0;")
                .Write("var mdblDeduc_amount   = 0;")
                .Write("var mlngBenef_type     = 0;")
                .Write("var mintModulec        = 0;")
                .Write("var mintCover          = 0;")
                .Write("var mstrDescript = '';")
                
                .Write("</" & "Script>")
            End With
        End If
End Sub

'% insReaInitial: Se encarga de asignar el valor del queryString a las variables declaradas como vbscript
'--------------------------------------------------------------------------------------------------------
Private Function insReaInitial() As Object
	'--------------------------------------------------------------------------------------------------------
	Dim lclsTar_am_Basprod As eBranches.Tar_am_basprod
	Dim lcolTar_am_Basprods As eBranches.Tar_am_basprods
	lclsTar_am_Basprod = New eBranches.Tar_am_basprod
	lcolTar_am_Basprods = New eBranches.Tar_am_basprods
	
	If Request.QueryString.Item("nTariff") = vbNullString Then
            If lcolTar_am_Basprods.Find(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), _
                                        mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), _
                                        mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
			
                mlngTariff = lcolTar_am_Basprods.Item(1).nTariff
                mstrChanges = lcolTar_am_Basprods.Item(1).sChanges
                mlngBenef_type = lcolTar_am_Basprods.Item(1).nBenef_type
                mdblDeduc_amount = lcolTar_am_Basprods.Item(1).nDed_amount
                mstrDefaulti = lcolTar_am_Basprods.Item(1).sDefaulti
                mdblLimit = lcolTar_am_Basprods.Item(1).nLimit
                mintModulec = lcolTar_am_Basprods.Item(1).nModulec
                mintCover = lcolTar_am_Basprods.Item(1).nCover
                mstrDescript = lcolTar_am_Basprods.Item(1).sDescript
            End If
	Else
            Call lclsTar_am_Basprod.Find_nTariff(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), _
                                                 mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), _
                                                 mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), _
                                                 mobjValues.StringToType(Request.QueryString.Item("nTariff"), eFunctions.Values.eTypeData.etdDouble), _
                                                 False, _
                                                 mobjValues.StringToType(Request.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble), _
                                                 mobjValues.StringToType(Request.QueryString.Item("nCover"), eFunctions.Values.eTypeData.etdDouble))
		
            mlngTariff = Request.QueryString.Item("nTariff")
            mstrChanges = lclsTar_am_Basprod.sChanges
            mlngBenef_type = lclsTar_am_Basprod.nBenef_type
            mdblDeduc_amount = lclsTar_am_Basprod.nDed_amount
            mstrDefaulti = lclsTar_am_Basprod.sDefaulti
            mdblLimit = lclsTar_am_Basprod.nLimit
            mintModulec = Request.QueryString.Item("nModulec")
            mintCover = Request.QueryString.Item("nCover")
            mstrDescript = lclsTar_am_Basprod.sDescript
        End If
	
	lclsTar_am_Basprod = Nothing
	lcolTar_am_Basprods = Nothing
	
End Function

'% insDefineHeader: Se definen los campos del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	'+ Se definen las columnas del grid
	With mobjGrid.Columns
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAgeInitColumnCaption"), "tcnAgeInit", 2, CStr(eRemoteDB.Constants.intNull),  , GetLocalResourceObject("tcnAgeInitColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAgeEndColumnCaption"), "tcnAgeEnd", 2, CStr(eRemoteDB.Constants.intNull),  , GetLocalResourceObject("tcnAgeEndColumnToolTip"))
		Call .AddPossiblesColumn(0, GetLocalResourceObject("tcnComp_groupColumnCaption"), "tcnComp_group", "table268", eFunctions.Values.eValuesType.clngComboType, CStr(1),  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("tcnComp_groupColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPremiumColumnCaption"), "tcnPremium", 18, CStr(eRemoteDB.Constants.intNull),  , GetLocalResourceObject("tcnPremiumColumnToolTip"),  , 6)
		Call .AddHiddenColumn("sParam", vbNullString)
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		If Request.QueryString.Item("Action") = "Update" Then
			.Columns("Sel").GridVisible = False
			.Columns("tcnAgeInit").Disabled = True
			.Columns("tcnComp_group").Disabled = True
			.Columns("tcnAgeEnd").Disabled = True
		End If
		.Columns("Sel").GridVisible = True
		.Codispl = "DP057"
		.Width = 400
		.Height = 250
		.DeleteButton = True
		.AddButton = True
		If Session("bQuery") Then
			.DeleteButton = False
			.AddButton = False
			.Columns("Sel").Disabled = True
			.bOnlyForQuery = True
		Else
			.Columns("tcnAgeInit").EditRecord = True
		End If
		.sDelRecordParam = "' + marrArray[lintIndex].sParam + '"
		
            .sEditRecordParam = "nTariff=' + self.document.forms[0].tcnTariff.value + '" & "&nBenef_type=' + self.document.forms[0].cbeBenefType.value + '" & _
                                "&nLimit=' + self.document.forms[0].tcnLimit.value + '" & "&sChanges=' + ((self.document.forms[0].chkChanges.checked)?1:2) + '" & _
                                "&sDefaulti=' + ((self.document.forms[0].chkDefaulti.checked)?1:2) + '" & "&nDeduc_amount=' + self.document.forms[0].tcnDed_amount.value + '" & _
                                "&nModulec=' + self.document.forms[0].cbeModulec.value + '" & "&nCover=' + self.document.forms[0].valCover.value +'" & "&sDescript=' + self.document.forms[0].tctDescript.value +'"
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% insPreDP057: Se cargan los controles de la página, tanto de la parte fija como del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreDP057()
	'--------------------------------------------------------------------------------------------
	Dim lclsTar_am_detprod As eBranches.Tar_am_detprod
	Dim lcolTar_am_detprods As eBranches.Tar_am_detprods
	
	With Server
		lclsTar_am_detprod = New eBranches.Tar_am_detprod
		lcolTar_am_detprods = New eBranches.Tar_am_detprods
	End With
        If lcolTar_am_detprods.Find(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), _
                                    mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), _
                                    mobjValues.StringToType(mlngTariff, eFunctions.Values.eTypeData.etdDouble), _
                                    mobjValues.StringToDate(Session("dEffecdate")), False, _
                                    mobjValues.StringToType(mintModulec, eFunctions.Values.eTypeData.etdInteger), _
                                    mobjValues.StringToType(mintCover, eFunctions.Values.eTypeData.etdInteger)) Then
            
            For Each lclsTar_am_detprod In lcolTar_am_detprods
                With mobjGrid
                    .Columns("tcnAgeInit").DefValue = CStr(lclsTar_am_detprod.nAge_init)
                    .Columns("tcnAgeEnd").DefValue = CStr(lclsTar_am_detprod.nAge_end)
                    .Columns("tcnComp_group").DefValue = CStr(lclsTar_am_detprod.nGroup_comp)
                    .Columns("tcnPremium").DefValue = CStr(lclsTar_am_detprod.nPremium)
				
                    .Columns("sParam").DefValue = "&nBranch=" & Session("nBranch") & "&nProduct=" & Session("nProduct") & "&dEffecdate=" & Session("dEffecdate") & _
                                                  "&nTariff=" & lclsTar_am_detprod.nTariff & "&sDefaulti=" & mstrDefaulti & "&sChanges=" & mstrChanges & _
                                                  "&nBenef_type=" & mlngBenef_type & "&nLimit=" & mdblLimit & "&nDed_amount=" & mdblDeduc_amount & _
                                                  "&nGroup_comp=" & lclsTar_am_detprod.nGroup_comp & "&nAge_Init=" & lclsTar_am_detprod.nAge_init & _
                                                  "&nAge_End=" & lclsTar_am_detprod.nAge_end & "&nPremium=" & lclsTar_am_detprod.nPremium & _
                                                  "&nUsercode=" & Session("nUsercode") & "&nModulec=" & lclsTar_am_detprod.nModulec & "&nCover=" & lclsTar_am_detprod.nCover
                    Response.Write(.DoRow)
                End With
            Next lclsTar_am_detprod
        End If
        'Response.Write("<SCRIPT>Enabled()</" & "Script>")
	Call insReaInitial()
	Response.Write(mobjGrid.closeTable())
	
	lclsTar_am_detprod = Nothing
	lcolTar_am_detprods = Nothing
End Sub


'% insPreDP057Upd: Se muetra la ventana Popup para efecto de actualización del Gird
'--------------------------------------------------------------------------------------------
Private Sub insPreDP057Upd()
	'--------------------------------------------------------------------------------------------
	
	Dim lclsTar_am_Basprod1 As eBranches.Tar_am_basprod
	
	lclsTar_am_Basprod1 = New eBranches.Tar_am_basprod
	
	If Request.QueryString.Item("Action") = "Del" Then
		Response.Write(mobjValues.ConfirmDelete)
            Call lclsTar_am_Basprod1.insPostDP057("DP057", Request.QueryString.Item("Action"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), _
                                                  mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), _
                                                  mobjValues.StringToType(Request.QueryString.Item("nTariff"), eFunctions.Values.eTypeData.etdDouble), _
                                                  Request.QueryString.Item("sChanges"), Request.QueryString.Item("sDefaulti"), _
                                                  mobjValues.StringToType(Request.QueryString.Item("nBenef_type"), eFunctions.Values.eTypeData.etdDouble), _
                                                  mobjValues.StringToType(Request.QueryString.Item("nLimit"), eFunctions.Values.eTypeData.etdDouble), _
                                                  mobjValues.StringToType(Request.QueryString.Item("nDed_amount"), eFunctions.Values.eTypeData.etdDouble), _
                                                  mobjValues.StringToType(Request.QueryString.Item("nAge_Init"), eFunctions.Values.eTypeData.etdDouble), _
                                                  mobjValues.StringToType(Request.QueryString.Item("nAge_End"), eFunctions.Values.eTypeData.etdDouble), _
                                                  CInt(Request.QueryString.Item("nGroup_comp")), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), _
                                                  mobjValues.StringToType(Request.QueryString.Item("nPremium"), eFunctions.Values.eTypeData.etdDouble), _
                                                  mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), "PopUp", _
                                                  mobjValues.StringToType(Request.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdInteger), _
                                                  mobjValues.StringToType(Request.QueryString.Item("nCover"), eFunctions.Values.eTypeData.etdInteger), _
                                                  Request.QueryString.Item("sDescript"))
		
	End If
	With Request
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valProductSeq.aspx", "DP057", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
		Response.Write(mobjValues.HiddenControl("hddDefaulti", .QueryString.Item("sDefaulti")))
		Response.Write(mobjValues.HiddenControl("hddLimit", .QueryString.Item("nLimit")))
		Response.Write(mobjValues.HiddenControl("hddBenef_type", .QueryString.Item("nBenef_type")))
		Response.Write(mobjValues.HiddenControl("hddChanges", .QueryString.Item("sChanges")))
		Response.Write(mobjValues.HiddenControl("hddTariff", .QueryString.Item("nTariff")))
            Response.Write(mobjValues.HiddenControl("hddDeduc_amount", .QueryString.Item("nDeduc_amount")))
            Response.Write(mobjValues.HiddenControl("hddModulec", .QueryString.Item("nModulec")))
            Response.Write(mobjValues.HiddenControl("hddCover", .QueryString.Item("nCover")))
            Response.Write(mobjValues.HiddenControl("hddDescript", .QueryString.Item("sDescript")))
    End With
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjGrid = New eFunctions.Grid
mobjMenu = New eFunctions.Menues

mobjGrid.sCodisplPage = "DP057"
mobjValues.sCodisplPage = "DP057"

mobjGrid.ActionQuery = Session("bQuery")

%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">




    <%With Response
	.Write(mobjValues.StyleSheet())
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write("<SCRIPT>var nMainAction=304</SCRIPT>")
		.Write(mobjMenu.setZone(2, "DP057", "DP057.aspx"))
		mobjMenu = Nothing
	End If
End With%>
<SCRIPT LANGUAGE="JavaScript">

//+ Variable para el control de versiones
       document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 17:02 $"

// % Enabled: funciòn que inhabilita los campos segùn el resultado de la bùsqueda 
//-------------------------------------------------------------------------------------------
function Enabled(){
//-------------------------------------------------------------------------------------------
   if (typeof(self.document.forms[0].tcnTariff)!='undefined') 
      self.document.forms[0].tcnTariff.disabled=false;
}

//%InsSelected: Verifica si está seleccionado el ckeck
//------------------------------------------------------------------------------------------
function InsSelected(nIndex, bChecked){
//------------------------------------------------------------------------------------------
	if(document.forms[0].sAuxSel.length>0){
		document.forms[0].sAuxSel[nIndex].value =(bChecked?1:2);
		document.forms[0].chkRequire[nIndex].checked = (document.forms[0].sAuxSel[nIndex].checked?true:false);
	}
	else 
	{	document.forms[0].sAuxSel.value =(bChecked?1:2);}
    
    if (bChecked )
		self.document.forms[0].tcnCountReg.value++ ;
	else
		self.document.forms[0].tcnCountReg.value--;
}

//%checkValue: Asigna valores al ckeck
//------------------------------------------------------------------------------------------
function checkValue(Field){
//------------------------------------------------------------------------------------------
	with(self.document.forms[0]){
		if (!Sel[Field.value].checked)
		{ tcnCountReg.value++ }
		if(chkAuxRequire.length>0){
			chkAuxRequire[Field.value].value=(Field.checked?1:2);
			if (Field.checked) {
				Sel[Field.value].checked=true;
				sAuxSel[Field.value].value =(Field.checked?1:2);
			}
		}
		else {
			chkAuxRequire.value=(Field.checked)?1:2;
			if (Field.checked) {
				Sel.checked=true;
				InsSelected(Field.value,true);
			}
		}
    }
}

//% insCancel: Se controla la acción Cancelar de la página
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}

// % LoadSeqTratPol: Se encarga de recargar la pàgina y enviar a variables tipo QueryString
// el valor introducido por el usuario al momento de ejecuratse la funciòn
//-------------------------------------------------------------------------------------------
function LoadSeqTarAttMed(Field){
//-------------------------------------------------------------------------------------------
    self.document.location.href = "DP057.aspx?sCodispl=DP057&nTariff=" + self.document.forms[0].tcnTariff.value 
                                                                       + "&nModulec=" + self.document.forms[0].cbeModulec.value
                                                                       + "&nCover=" + self.document.forms[0].valCover.value
}

//-------------------------------------------------------------------------------------------
function changeValues(Field) {
    with (document.forms[0]) {
        if (Field.name == 'cbeModulec') {
            if (typeof (cbeModulec) != 'undefined') {
                valCover.Parameters.Param3.sValue = cbeModulec.value;
                valCover.disabled = false;
                btnvalCover.disabled = false;
            }
        }
    }
}

</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
	<%Response.Write(mobjValues.ShowWindowsName("DP057"))%>
	<FORM METHOD="POST" ID="FORM" NAME="frmDP057" ACTION="valProductSeq.aspx?sContent=1">
	    <TABLE WIDTH="100%">
			<%
If Request.QueryString.Item("Action") = "Update" Then
	mblnDisabled = True
End If

Call insReaInitial()
Call insDefineHeader()

If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreDP057Upd()
Else
	Call insLoadDP057()
	Call insPreDP057()
	Call insOldValues()
End If
%>
	    </TABLE>
	</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
mobjGrid = Nothing

%>





<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<%@ Import namespace="eProduct" %>
<%@ Import namespace="eApvc" %>
<%@ Import namespace="eBranches" %>
<script language="VB" runat="Server">

'**- Object for the handling of the general functions of load of values.
'- Objeto para el manejo de las mercado generales de carga de valores.

Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjGrid2 As eFunctions.Grid
Dim mobjMenu As eFunctions.Menues
Dim mobjErrors As eFunctions.Errors
Dim mintParticip As Integer
Dim mintCount As Integer
Dim mstrApv As String
Dim lclsCertificat As ePolicy.Certificat
Dim lclsErrors As eFunctions.Errors

Dim mstrMsgLevel As Object


    Dim lclsDynamics_Table_Certificat As ePolicy.Dynamics_Table_Certificat
    Dim sType_profile As String
'**% insDefineHeader: The field of the GRID is defined.
'% insDefineHeader: Se definen los campos del grid.
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	
	'**+ The column of the GRID are defined.
	'+ Se definen las columnas del grid.
	With mobjGrid.Columns
		Call .AddTextColumn(100745, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", 30, vbNullString,  , GetLocalResourceObject("tctDescriptColumnToolTip"),  ,  ,  , True)
		Call .AddNumericColumn(103509, GetLocalResourceObject("tcnPartic_minColumnCaption"), "tcnPartic_min", 4, CStr(0),  , GetLocalResourceObject("tcnPartic_minColumnToolTip"), True, 2,  ,  ,  , True)
		Call .AddNumericColumn(100744, GetLocalResourceObject("tcnParticipColumnCaption"), "tcnParticip", 5, CStr(0),  , GetLocalResourceObject("tcnParticipColumnToolTip"),  , 2,  ,  ,  , False)
		Call .AddHiddenColumn("tcnFunds", CStr(0))
		Call .AddHiddenColumn("hddPartic_min", CStr(0))
		Call .AddHiddenColumn("hddParticip", CStr(0))
		If CStr(Session("sCertype")) = "1" Or CStr(Session("sCertype")) = "3" Then
			Call .AddNumericColumn(100746, GetLocalResourceObject("tcnIntProyColumnCaption"), "tcnIntProy", 5, CStr(0),  , GetLocalResourceObject("tcnIntProyColumnToolTip"),  , 2,  ,  ,  , True)
			Call .AddNumericColumn(100747, GetLocalResourceObject("tcnIntProyVarColumnCaption"), "tcnIntProyVar", 5, CStr(0),  , GetLocalResourceObject("tcnIntProyVarColumnToolTip"),  , 2,  ,  ,  , True)
			Call .AddHiddenColumn("chkActivFound", "1")
		Else
			Call .AddHiddenColumn("tcnIntProy", CStr(0))
			Call .AddHiddenColumn("tcnIntProyVar", CStr(0))
			Call .AddCheckColumn(0, GetLocalResourceObject("chkActivFoundColumnCaption"), "chkActivFound", vbNullString,  ,  ,  , Request.QueryString.Item("Type") <> "PopUp", GetLocalResourceObject("chkActivFoundColumnToolTip"))
		End If
	End With
	
	'**+ The properties of the GRID are defined.
	'+ Se definen las propiedades generales del grid.
	
	With mobjGrid
		.Codispl = Request.QueryString.Item("sCodispl")
		.Height = 300
		.Width = 400
		.Columns("Sel").Title = "Sel"
		.AddButton = False
		.DeleteButton = False
		.bCheckVisible = False
		.sEditRecordParam = "nDisable=' + self.document.forms[0].hddDisable.value + '" & "&nOrigin=' + self.document.forms[0].cbeAccount.value + '"
		If mobjValues.ActionQuery <> True Then
			.Columns("tctDescript").EditRecord = True
		Else
			.Columns("Sel").Disabled = True
		End If
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		
		If CStr(Session("sCertype")) = "1" Or CStr(Session("sCertype")) = "3" Then
			.Splits_Renamed.AddSplit(0, "", 4)
			.Splits_Renamed.AddSplit(0, GetLocalResourceObject("3ColumnCaption"), 3)
		End If
		
		.Columns("Sel").OnClick = "insSelected(this)"
		.Columns("chkActivFound").OnClick = "insChangeValues(this)"
	End With
End Sub

'**% insPreVI006: Read the information of the policy funds.
'% insPreVI006: Obtiene los datos de los fondos de la póliza.
'--------------------------------------------------------------------------------------------
Private Sub insPreVI006()
	'--------------------------------------------------------------------------------------------
    Dim lclsFunds As Object
	Dim lclsFunds2 As Object        
	Dim lcolFundss As Object
    Dim lcolFundss2 As Object
    '+ APVC INICIO
	'+ verifica si es apcv para utilizar metodo particular 
	Dim lclsProduct As eProduct.Product
	Dim lclsPolicy As ePolicy.Policy
	Dim lclbapvc As Boolean
	lclbapvc = False
	Dim lindex As Double
	Dim lintOrigin(20, 1) As Object
	Dim lclsTab_Ord_Origin As eBranches.Tab_Ord_Origin
	Dim lcolTab_Ord_Origins As eBranches.Tab_Ord_Origins
	Dim lintOriginTemp As Integer
    Dim lintPreSelected As Short
    Dim nIntProyvarMax2 As double        
	
	mintCount = 0
	lintOriginTemp = 0
	
	lclsProduct = New eProduct.Product
	Call lclsProduct.FindProduct_li(Session("nBranch"), Session("nProduct"), Session("dEffecdate"))
	
	mstrApv = lclsProduct.sApv
	Session("sApv_VI006") = lclsProduct.sApv
	If lclsProduct.sApv = "1" Then
		lclsPolicy = New ePolicy.Policy
		Call lclsPolicy.Find(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble, True))
		If lclsPolicy.sPolitype = "2" Then
			lclbapvc = True
		End If
		lclsPolicy = Nothing
		
	End If
	lclsProduct = Nothing
	If lclbapvc Then
        lclsFunds = New eApvc.Funds
        lclsFunds2 = New eApvc.Funds
        lcolFundss = New eApvc.Fundss
        lcolFundss2 = New eApvc.Fundss
        
	Else
        lclsFunds = New ePolicy.Funds
        lclsFunds2 = New ePolicy.Funds
        lcolFundss = New ePolicy.Fundss
        lcolFundss2 = New ePolicy.Fundss
	End If
	'+ APVC fin
	
	lcolTab_Ord_Origins = New eBranches.Tab_Ord_Origins
	
	mintCount = lcolTab_Ord_Origins.Count
	
	If lcolTab_Ord_Origins.Find(Session("nBranch"), Session("nProduct")) Then
		
		For	Each lclsTab_Ord_Origin In lcolTab_Ord_Origins
			lintOrigin(lclsTab_Ord_Origin.nOrigin, 0) = lclsTab_Ord_Origin.sDescript
			lintOrigin(lclsTab_Ord_Origin.nOrigin, 1) = 0
		Next lclsTab_Ord_Origin
	End If
	
	mintParticip = 0
	
	lclsTab_Ord_Origin = Nothing
	lcolTab_Ord_Origins = Nothing
	
	'+ Se verifica si la póliza tiene fondos registrados
	If lcolFundss.PolicyHasAnyFund(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif")) Or lclbapvc Then
		lintPreSelected = 2
	Else
		lintPreSelected = 1
	End If
	If lcolFundss.Find_FundstoPol(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(CStr(Session("dEffecdate")), eFunctions.Values.eTypeData.etdDate), Session("sSche_code"), "VI006", Session("nOrigin"), Session("nTransaction"), lintPreSelected) Then
		
		For	Each lclsFunds In lcolFundss
			With mobjGrid
				.Columns("Sel").checked = lclsFunds.nSelected
				.Columns("tctDescript").DefValue = lclsFunds.nFunds & " - " & lclsFunds.sDescript
				.Columns("tcnFunds").DefValue = lclsFunds.nFunds
				.Columns("tcnPartic_min").DefValue = lclsFunds.nPartic_min
				.Columns("tcnParticip").DefValue = lclsFunds.nParticip
				.Columns("hddPartic_min").DefValue = lclsFunds.nPartic_min
				.Columns("hddParticip").DefValue = lclsFunds.nParticip
				.Columns("hddParticip").DefValue = lclsFunds.nParticip
                .Columns("tcnIntProy").DefValue = lclsFunds.nIntProy
                 Session("nIntProy") = lclsFunds.nIntProy
                
                    'Si no trae valor para el fondo seleccionado desde la rateproyection, realiza la busqueda en la tabla de fondos 
                If CInt(lclsFunds.nIntProyvarMax) = eRemoteDB.Constants.intNull Then
                        If lcolFundss2.Find(Session("nBranch"), Session("nProduct"), mobjValues.StringToType(CStr(Session("dEffecdate")), eFunctions.Values.eTypeData.etdDate)) Then
                            
                            For Each lclsFunds2 In lcolFundss2
                                With mobjGrid2
                                    'busca el fondo seleccionada y asigana el valor
                                    If lclsFunds.nFunds = lclsFunds2.nFunds Then
                                        nIntProyvarMax2 = lclsFunds2.nIntProyvarMax
                                    End If
                                End With
                            Next lclsFunds2
                        
                        End If
                            
                End If
                    If lclsFunds.nIntProyvarMax = eRemoteDB.Constants.intNull Then
                        .Columns("tcnIntProyvar").DefValue = nIntProyvarMax2
                    Else
                        .Columns("tcnIntProyvar").DefValue = lclsFunds.nIntProyvarMax
                    End If
    
                
                
        If lclsFunds.sActivFound = "1" Then
            .Columns("chkActivFound").Checked = CShort("1")
            mintParticip = mintParticip + lclsFunds.nParticip
            lintOrigin(lclsFunds.nOrigin, 1) = lintOrigin(lclsFunds.nOrigin, 1) + lclsFunds.nParticip
        Else
            .Columns("chkActivFound").Checked = CShort("2")
        End If
				
        If lclsFunds.nSelected = "1" Then
            If lclsFunds.sActivFound <> "1" Then
                .Columns("tcnPartic_min").DefValue = ""
                If lclsFunds.nSelected <> 1 Then
                    .Columns("tcnParticip").DefValue = ""
                End If
            End If
        Else
            .Columns("chkActivFound").Checked = CShort("1")
        End If
        Response.Write(.DoRow)
                End With
		Next lclsFunds
	End If
	
        Response.Write(mobjGrid.closeTable())
	
        Response.Write("      <TABLE WIDTH=""30%"">	" & vbCrLf)
        Response.Write("      <BR></BR>" & vbCrLf)
        Response.Write("")

        lindex = 1
        Do While lindex < 20
            If lintOrigin(lindex, 1) > 0 Then
			
                Response.Write("" & vbCrLf)
                Response.Write("            <TR>" & vbCrLf)
                Response.Write("                <TD ><LABEL ID=0>")


                Response.Write(lintOrigin(lindex, 0))


                Response.Write(" ( % Participación )</LABEL></TD>" & vbCrLf)
                Response.Write("                <TD>")


                Response.Write(mobjValues.NumericControl("tcnParticip", 3, lintOrigin(lindex, 1), , GetLocalResourceObject("tcnParticipToolTip"), , , True, , , , True))


                Response.Write("</TD>" & vbCrLf)
                Response.Write("            </TR>" & vbCrLf)
                Response.Write("  " & vbCrLf)
                Response.Write("    ")

            End If
            lindex = lindex + 1
        Loop
        Response.Write("" & vbCrLf)
        Response.Write("    " & vbCrLf)
        Response.Write("    </TABLE>	" & vbCrLf)
        Response.Write("	")

	
        If lcolFundss.bUpdateFound Then
            Response.Write(mobjValues.HiddenControl("hddDisable", "2"))
        Else
            Response.Write(mobjValues.HiddenControl("hddDisable", "1"))
        End If
	
        lcolFundss = Nothing
        lclsFunds = Nothing
End Sub

'%** insPreVI006Upd: Show the pop up windows for the updates.
'% insPreVI006Upd: Muestra la ventana Popup para las actualizaciones.
'--------------------------------------------------------------------------------------------
Private Sub insPreVI006Upd()
	'--------------------------------------------------------------------------------------------
	Dim lclsFunds_Pol As ePolicy.Funds_Pol
	lclsFunds_Pol = New ePolicy.Funds_Pol
	
	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			
			Response.Write(mobjValues.ConfirmDelete())
			
			Call lclsFunds_Pol.insPostVI006(.QueryString.Item("sCodispl"), .QueryString.Item("Action"), CInt(.QueryString.Item("nFunds")), CInt(.QueryString.Item("nParticip")), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("nUsercode"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("dNulldate"), eFunctions.Values.eTypeData.etdDate), Session("nTransaction"), vbNullString, vbNullString, CInt(.QueryString.Item("nOrigin")))
		End If
		
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "ValPolicySeq.aspx", Request.QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
		
		If .QueryString.Item("Action") <> "Del" Then
			Response.Write("<SCRIPT>self.document.forms[0].tcnParticip.disabled = false;</" & "Script>")
		End If
	End With
	
	
	lclsFunds_Pol = Nothing
End Sub

</script>
<%Response.Expires = -1

With Server
	mobjValues = New eFunctions.Values
	mobjGrid = New eFunctions.Grid
	mobjMenu = New eFunctions.Menues
	mobjErrors = New eFunctions.Errors
End With

mobjValues.ActionQuery = Session("bQuery")
mstrMsgLevel = mobjErrors.ErrorMessage("CA017A", 56209, 0, 2, "", True, "")

%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">




<SCRIPT>    
//**+ For the Source Safe control. 
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 6 $|$$Date: 13/10/15 12:49 $|$$Author: Gletelier $"
	
//% insCheckSelClick: Permite levantar la ventana Popup para actualizar el registro.
//-------------------------------------------------------------------------------------------
function insSelected(Field){
//-------------------------------------------------------------------------------------------
	if(Field.checked) 
		EditRecord(Field.value,nMainAction, 'Update',"nOrigin=" + self.document.FORM.cbeAccount.value)
    else{ 
        EditRecord(Field.value,nMainAction, 'Del',
                   "nFunds=" + marrArray[Field.value].tcnFunds + 
                   "&nParticip=" + marrArray[Field.value].tcnParticip + 
                   "&nPartic_min=" + marrArray[Field.value].tcnPartic_min +
                   "&nOrigin=" + self.document.FORM.cbeAccount.value +  
                   "&nIntProy=" + marrArray[Field.value].tcnIntProy + 
                   "&nIntProyVar=" + marrArray[Field.value].tcnIntProyVar +
				   "&nTypeProfile=" + self.document.FORM.cbeTypeProfile.value)
    }
    Field.checked = !Field.checked
}

//% insChangeValues: Permite actualizar los campos al hacer el check del active found 
//-------------------------------------------------------------------------------------------
function insChangeValues(Field){
//-------------------------------------------------------------------------------------------
	with(self.document.forms[0]){
		switch(Field.name){
			case "chkActivFound":
				if ((Field.checked==true) || (<%=session("sCertype")%> == "3")){ 
// si esta desmarcado y se marca 
					chkActivFound.defvalue = "1";
					tcnPartic_min.value=hddPartic_min.value;
					tcnParticip.value=hddParticip.value;
					tcnParticip.disabled=false;
					tcnIntProy.disabled=false;
				}
				else{
// si esta marcado y se desmarca 
					chkActivFound.defvalue = "2";
					tcnPartic_min.value="";
					tcnParticip.value="";
					tcnParticip.disabled=true;																				
				}
				break;
		}
    }
}

function insChangeOrigin(){
	var lstrAction
	with(self.document.forms[0])
	{
		lstrAction = self.document.location.href
		lstrAction = lstrAction.replace(/\&nOrigin=.*/, '') + '&nOrigin=' + cbeAccount.value  
	    self.document.location.href=lstrAction;
	}        
}

</SCRIPT>
<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle(Request.QueryString.Item("sCodispl")))
	
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
		.Write("<SCRIPT>var nMainAction=304</SCRIPT>")
		.Write(mobjMenu.setZone(2, Request.QueryString.Item("sCodispl"), "VI006.aspx"))
		mobjMenu = Nothing
	End If
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmVI006" ACTION="../../Policy/PolicySeq/ValPolicySeq.aspx?mode=2">

<%
lclsCertificat = New ePolicy.Certificat
Call lclsCertificat.Find(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("ncertif"), eFunctions.Values.eTypeData.etdDouble, True))

If Request.QueryString.Item("nOrigin") <> vbNullString Then
	Session("nOrigin") = Request.QueryString.Item("nOrigin")
Else
	Session("nOrigin") = lclsCertificat.nOrigin
End If

    lclsDynamics_Table_Certificat = New ePolicy.Dynamics_Table_Certificat
    Call lclsDynamics_Table_Certificat.Find_date(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdLong), _
                                                 mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdLong, True), _
                                                 mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble, True), _
                                                 mobjValues.StringToType(Session("ncertif"), eFunctions.Values.eTypeData.etdDouble, True), _
                                                 90078, _
                                                 3, _
                                                 mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
    If lclsDynamics_Table_Certificat.sValue <> vbNullString Then
        sType_profile = lclsDynamics_Table_Certificat.sValue
    End If

    lclsDynamics_Table_Certificat = Nothing
lclsCertificat = Nothing

If Request.QueryString.Item("Type") <> "PopUp" Then
	%>
<TABLE>
	<TR>
		<TD>
			<LABEL ID=0><%= GetLocalResourceObject("cbeAccountCaption") %></LABEL>
		</TD>
		<TD>
			<%	With mobjValues
		.Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nCertif", Session("nCertif"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		
		Response.Write(mobjValues.PossiblesValues("cbeAccount", "TABFUNDSACCOUNT", eFunctions.Values.eValuesType.clngWindowType, Session("nOrigin"), True,  ,  ,  ,  , "insChangeOrigin();",  ,  , GetLocalResourceObject("cbeAccountToolTip"), eFunctions.Values.eTypeCode.eNumeric))
	End With

	
	%>			
		</TD>
    <TD>&nbsp; &nbsp; &nbsp; </TD>
	<TD><LABEL ID=LABEL2><%= GetLocalResourceObject("cbeTypeProfileCaption") %></LABEL></TD>
	<TD><%=mobjValues.PossiblesValues("cbeTypeProfile", "Table8320", eFunctions.Values.eValuesType.clngWindowType, sType_profile, , , , , , , True, , GetLocalResourceObject("cbeTypeProfileToolTip"))%> </TD>
		
	</TR>
</TABLE>
&nbsp;	


<%
Else
    Response.Write(mobjValues.HiddenControl("cbeTypeProfile", sType_profile))
End If
Call insDefineHeader()

If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreVI006()
Else
	Call insPreVI006Upd()
End If

mobjValues = Nothing
mobjGrid = Nothing
If Request.QueryString.Item("Type") = "PopUp" Then
%>
<SCRIPT>
	if (<%=mintCount%> == 1 || <%=mstrApv%> == '1'){
		self.document.FORM.cbeAccount.disabled=true;
		self.document.FORM.btncbeAccount.disabled=true;
	}
	else {
		self.document.FORM.cbeAccount.disabled=false;
		self.document.FORM.btncbeAccount.disabled=false;
	}	
</SCRIPT>
<%
end if
%>
</FORM>
</BODY>
</HTML>






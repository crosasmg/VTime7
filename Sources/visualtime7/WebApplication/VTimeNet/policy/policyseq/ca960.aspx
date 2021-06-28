<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mobjGrid As eFunctions.Grid
Dim mclsfranchise As ePolicy.Franchise
Dim mblnFound As Boolean

Dim mintModulec As Object
Dim mstrType As Object
Dim mblnGroup As Boolean
Dim mintGroup As Object
Dim mintGroupChange As Object

Dim lclsGroups As ePolicy.Groups



'**% insDefineHeader: Defines the columns of the grid 
'% insDefineHeader: Define las columnas del grid
'-------------------------------------------------------------------------------------------
Private Function insDefineHeader() As Object
	'-------------------------------------------------------------------------------------------
	Dim lstrQuery As Object
	
	mobjGrid = New eFunctions.Grid
	
	If Request.QueryString.Item("Type") <> "PopUp" Then
		If mclsfranchise.insPreCA960(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mintGroupChange) Then
			mblnFound = True
		Else
			mblnFound = False
		End If
		
	End If
	
	With mobjGrid.Columns
		.AddNumericColumn(0, GetLocalResourceObject("tcnSeqColumnCaption"), "tcnSeq", 5, vbNullString, True, GetLocalResourceObject("tcnSeqColumnToolTip"))
		.AddPossiblesColumn(0, GetLocalResourceObject("tcnLevelColumnCaption"), "tcnLevel", "Table21", 2,  , False,  ,  ,  , "insActiveFields(this)",  ,  , GetLocalResourceObject("tcnLevelColumnToolTip"))
		.AddPossiblesColumn(0, GetLocalResourceObject("tcsFrancAplColumnCaption"), "tcsFrancApl", "Table33", 1,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("tcsFrancAplColumnToolTip"), 2)
		.AddPossiblesColumn(0, GetLocalResourceObject("tcnDed_TypeColumnCaption"), "tcnDed_Type", "Table269", 1,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("tcnDed_TypeColumnToolTip"))
		.AddPossiblesColumn(0, GetLocalResourceObject("tcnModulecColumnCaption"), "tcnModulec", "tabTab_modul", eFunctions.Values.eValuesType.clngWindowType, "0", True,  ,  ,  , "InsChangeField(this,""Modulec"");", True,  , GetLocalResourceObject("tcnModulecColumnToolTip"))
		.AddPossiblesColumn(0, GetLocalResourceObject("tcnCoverColumnCaption"), "tcnCover", "tablife_cover", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  , True,  , GetLocalResourceObject("tcnCoverColumnToolTip"))
		.AddPossiblesColumn(0, GetLocalResourceObject("tcnRoleColumnCaption"), "tcnRole", "Table12", 1,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("tcnRoleColumnToolTip"))
		.AddPossiblesColumn(0, GetLocalResourceObject("tcnPay_ConcepColumnCaption"), "tcnPay_Concep", "Table160", 1,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("tcnPay_ConcepColumnToolTip"))
		.AddNumericColumn(0, GetLocalResourceObject("tcnRateColumnCaption"), "tcnRate", 5, vbNullString, True, GetLocalResourceObject("tcnRateColumnToolTip"))
		.AddPossiblesColumn(0, GetLocalResourceObject("tcnCurrencyColumnCaption"), "tcnCurrency", "Table11", 1,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("tcnCurrencyColumnToolTip"))
		.AddNumericColumn(0, GetLocalResourceObject("tcnFixAmountColumnCaption"), "tcnFixAmount", 18, vbNullString, True, GetLocalResourceObject("tcnFixAmountColumnToolTip"),  , 6)
		.AddNumericColumn(0, GetLocalResourceObject("tcnMinAmountColumnCaption"), "tcnMinAmount", 18, vbNullString, True, GetLocalResourceObject("tcnMinAmountColumnToolTip"),  , 6)
		.AddNumericColumn(0, GetLocalResourceObject("tcnMaxAmountColumnCaption"), "tcnMaxAmount", 18, vbNullString, True, GetLocalResourceObject("tcnMaxAmountColumnToolTip"),  , 6)
		.AddNumericColumn(0, GetLocalResourceObject("tcnOrderColumnCaption"), "tcnOrder", 5, vbNullString, True, GetLocalResourceObject("tcnOrderColumnToolTip"))
		.AddHiddenColumn("tcnGroup", mintGroupChange)
		
	End With
	
	With mobjGrid
		.Columns("Sel").OnClick = "OnChangeSel(this);"
		.AddButton = mblnFound
		.DeleteButton = True
		.ActionQuery = Session("bQuery")
		.Codispl = Request.QueryString.Item("sCodispl")
		.Width = 350
		.Height = 530
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Top = 10
		
		.Columns("tcnModulec").Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("tcnModulec").Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("tcnModulec").Parameters.Add("dEffecdate", mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		
		.Columns("tcnCover").Parameters.Add("sStatregt", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("tcnCover").Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("tcnCover").Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("tcnCover").Parameters.Add("nModulec", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("tcnCover").Parameters.Add("dEffecdate", mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("tcnCover").Parameters.Add("nCovernoshow", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("tcnCover").Parameters.Add("nCovermax", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		
		.Columns("tcnSeq").EditRecord = True
		
            .sEditRecordParam = "nGroup=" & mintGroupChange
		
            .sDelRecordParam = "tcnSeq=' + marrArray[lintIndex].tcnSeq + '&nGroup=" & mintGroupChange
		
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		
		If Request.QueryString.Item("Type") = "PopUp" And Request.QueryString.Item("Action") = "Update" Then
			.Columns("tcnSeq").Disabled = True
			.Columns("tcnLevel").Disabled = True
		End If
	End With
End Function

'%insPreCA960: Se cargan los controles de la página
'-------------------------------------------------------------------------------------------
Private Sub insPreCA960()
	'-------------------------------------------------------------------------------------------
	
Response.Write("" & vbCrLf)
Response.Write("	<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("		")

	If mblnGroup Then
		mobjValues.ActionQuery = False
		With mobjValues
			.Parameters.Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		End With
		Response.Write(mobjValues.HiddenControl("lblnGroup", CStr(True)))
		
Response.Write("	<TD WIDTH=""25%""><LABEL ID=0>" & GetLocalResourceObject("cbeGroupCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD WIDTH=""25%"">")

		Response.Write(mobjValues.PossiblesValues("cbeGroup", "TABGROUPS", eFunctions.Values.eValuesType.clngWindowType, mintGroupChange, True,  ,  ,  ,  , "insReload(this)"))
Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD WIDTH=""50%"">&nbsp;</TD>" & vbCrLf)
Response.Write("		")

	Else
Response.Write("" & vbCrLf)
Response.Write("			<TD WIDTH=""25%"">&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD WIDTH=""25%"">&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD WIDTH=""50%"">&nbsp;</TD>" & vbCrLf)
Response.Write("				")

		Response.Write(mobjValues.HiddenControl("lblnGroup", CStr(False)))
Response.Write("" & vbCrLf)
Response.Write("				")

		Response.Write(mobjValues.HiddenControl("cbeGroup", "0"))
Response.Write("" & vbCrLf)
Response.Write("		")

	End If
Response.Write("" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("	</TABLE>")

	
	insLoadGrid()
	Dim lobjError As eFunctions.Errors
	If Not mblnFound And Request.QueryString.Item("nGroup") = vbNullString Then
		If mclsfranchise.nError > 0 Then
			lobjError = New eFunctions.Errors
			'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.03
			lobjError.sSessionID = Session.SessionID
			lobjError.nUsercode = Session("nUsercode")
			'~End Body Block VisualTimer Utility
			Response.Write(lobjError.ErrorMessage("CA960", mclsfranchise.nError,  ,  ,  , True))
			lobjError = Nothing
		End If
		
	End If
	
	Response.Write(mobjValues.BeginPageButton)
End Sub

'%insLoadGrid: define el grid según lo leído de las tablas incolucradas
'%insLoadGrid: defines grid according to the read thing of the incolucradas tables  
'-------------------------------------------------------------------------------------------
Private Sub insLoadGrid()
	'-------------------------------------------------------------------------------------------
	Dim mclsfranchise As Object
	Dim mcolfranchises As ePolicy.Franchises
	Dim llngIndex As Integer
	
	mcolfranchises = New ePolicy.Franchises
	llngIndex = 0
	
	If mcolfranchises.Find(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("deffecdate"), mintGroupChange) Then
            For llngIndex = 1 To mcolfranchises.Count
                With mobjGrid
                    .Columns("tcnSeq").DefValue = CStr(mcolfranchises.Item(llngIndex).nSeq)
                    .Columns("tcnLevel").DefValue = CStr(mcolfranchises.Item(llngIndex).nLevel)
                    .Columns("tcsFrancApl").DefValue = mcolfranchises.Item(llngIndex).sFrancApl
                    .Columns("tcnDed_Type").DefValue = CStr(mcolfranchises.Item(llngIndex).nDed_Type)
                    .Columns("tcnModulec").DefValue = CStr(mcolfranchises.Item(llngIndex).nModulec)
                    .Columns("tcnCover").Parameters.Add("nModulec", mobjValues.StringToType(CStr(mcolfranchises.Item(llngIndex).nModulec), eFunctions.Values.eTypeData.etdDouble, True), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Columns("tcnCover").DefValue = CStr(mcolfranchises.Item(llngIndex).nCover)
                    .Columns("tcnPay_Concep").DefValue = CStr(mcolfranchises.Item(llngIndex).nPay_Concep)
                    .Columns("tcnRate").DefValue = CStr(mcolfranchises.Item(llngIndex).nRate)
                    .Columns("tcnCurrency").DefValue = CStr(mcolfranchises.Item(llngIndex).nCurrency)
                    .Columns("tcnFixamount").DefValue = CStr(mcolfranchises.Item(llngIndex).nFixamount)
                    .Columns("tcnMaxamount").DefValue = CStr(mcolfranchises.Item(llngIndex).nMaxamount)
                    .Columns("tcnMinamount").DefValue = CStr(mcolfranchises.Item(llngIndex).nMinamount)
                    .Columns("tcnRole").DefValue = CStr(mcolfranchises.Item(llngIndex).nRole)
                    .Columns("tcnOrder").DefValue = CStr(mcolfranchises.Item(llngIndex).nOrder)
                    Response.Write(.DoRow)
                End With
            Next
	End If
	Response.Write(mobjGrid.CloseTable)
	mcolfranchises = Nothing
	mclsfranchise = Nothing
	
End Sub



'%insPreCA960Upd: Se cargan los controles de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreCA960Upd()
	'--------------------------------------------------------------------------------------------
	Dim lclsfranchise As ePolicy.Franchise
	'Response.Write "<NOTSCRIPT>alert('"& Request.QueryString("Action") &"');</" & "Script>"
	If Request.QueryString.Item("Action") = "Del" Then
		With Request
			lclsfranchise = New ePolicy.Franchise
			If lclsfranchise.Deletefranchise(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), CInt(Request.QueryString.Item("tcnSeq")), CInt(Request.QueryString.Item("nGroup")), Session("nUsercode")) Then
				Response.Write(mobjValues.ConfirmDelete())
				Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location='/VTimeNet/Policy/PolicySeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "&sGoToNext=NO" & "';</" & "Script>")
				lclsfranchise = Nothing
			End If
		End With
	End If
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "ValPolicySeq.aspx", "CA960", Request.QueryString.Item("nMainAction"), Session("bQuery"), CShort(Request.QueryString.Item("Index"))))
	
End Sub

</script>
<%Response.Expires = -1
Response.CacheControl = "private"

With Server
	mobjValues = New eFunctions.Values
	mobjMenu = New eFunctions.Menues
	mclsfranchise = New ePolicy.Franchise
End With
mobjValues.ActionQuery = Session("bQuery")
%>
<HTML>
    <HEAD>
	<META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>




<%
With Response
	.Write(mobjValues.WindowsTitle(Request.QueryString.Item("sCodispl")))
	.Write(mobjValues.StyleSheet())
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write(mobjMenu.setZone(2, Request.QueryString.Item("sCodispl"), "CA960.aspx"))
		mobjMenu = Nothing
		Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction;</SCRIPT>")
	End If
End With
%>
<SCRIPT>
//% For the Source Safe control
//% Para control de versiones
//---------------------------------------------------------------------------------------------------------------------
document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:03 $"

//%insReload: Se encarga de recargar la página al cambiar algún valor de cualquier combo de la página.
//-------------------------------------------------------------------------------------------
function insReload(Field){
//-------------------------------------------------------------------------------------------
	var lstr_docloc = "";
	var lblnOk = false;
	with(self.document.forms[0])	{
		lstr_docloc = document.location.href;
		//if(typeof(cbeGroup)!='undefined')
		if(cbeGroup.value!='')
			lstr_docloc = lstr_docloc.replace(/&nGroup=[0-9].*/,'') + "&nGroup=" + cbeGroup.value;
		else
			lstr_docloc = lstr_docloc.replace(/&nGroup=[0-9].*/,'') + "&nGroup=0";
			
		if(typeof(cbeGroup)!='undefined'){
			if (cbeGroup.value!=mintGroupChange)
				lblnOk = true;
		}
		
			if(typeof(cbeGroup)!='undefined'){
				mintGroupChange = cbeGroup.value
				document.location.href = lstr_docloc;
			}
	}
}

//%DisabledItem: Desabilita los campos de cuando es asegurado
//-------------------------------------------------------------------------------------------
function DisabledItem(){
//-------------------------------------------------------------------------------------------
	
	var Type='<%=mstrType%>';
    var nLevel = self.document.forms[0].tcnLevel.value

 	with(self.document.forms[0]){		
		if (Type=="PopUp"){
			switch (nLevel)
			{
               case "1":  //Póliza
                  tcnModulec.disabled=true;
                  tcnCover.disabled=true;
                  tcnPay_Concep.disabled=true;
                  tcnRole.disabled=true;
               case "2": // Certificado
                  tcnModulec.disabled=true;
                  tcnCover.disabled=true;
                  tcnPay_Concep.disabled=true;
                  tcnRole.disabled=true;
               case "3": // Cobertura
                  tcnModulec.disabled=false;
                  tcnCover.disabled=false;
                  tcnPay_Concep.disabled=true;
                  tcnRole.disabled=true;
               case "4": // Módulo
                  tcnModulec.disabled=false;
                  tcnCover.disabled=true;
                  tcnPay_Concep.disabled=true;
                  tcnRole.disabled=true;
               case "5": // Rol
                  tcnModulec.disabled=true;
                  tcnCover.disabled=true;
                  tcnPay_Concep.disabled=true;
                  tcnRole.disabled=false;
               case "6": //Prestación
                  tcnModulec.disabled=true;
                  tcnCover.disabled=true;
                  tcnPay_Concep.disabled=false;
                  tcnRole.disabled=true;
			}	
		}
	}
}


//% insActiveFields: Se encarga de activar campos del límite combinado
//--------------------------------------------------------------------------------------------
function insActiveFields(nLevel){
//--------------------------------------------------------------------------------------------	
//   var nLevel = self.document.forms[0].tcnLevel.value;

   with (self.document.forms[0])
   {        
      switch (nLevel.value)
      {
         case "1":  //Póliza
            tcnModulec.disabled=true;
            tcnCover.disabled=true;
            tcnPay_Concep.disabled=true;
            tcnRole.disabled=true;
            break;
         case "2": // Certificado
            tcnModulec.disabled=true;
            tcnCover.disabled=true;
            tcnPay_Concep.disabled=true;
            tcnRole.disabled=true;
            break;            
         case "3": // Cobertura
            tcnModulec.disabled=false;
            btntcnModulec.disabled=false;
            tcnCover.disabled=false;
            btntcnCover.disabled=false;
            tcnPay_Concep.disabled=true;
            tcnRole.disabled=true;
            break;            
         case "4": // Módulo
            tcnModulec.disabled=false;
            btntcnModulec.disabled=false;
            tcnCover.disabled=true;
            tcnPay_Concep.disabled=true;
            tcnRole.disabled=true;
            break;
         case "5": // Rol
            tcnModulec.disabled=true;
            tcnCover.disabled=true;
            tcnPay_Concep.disabled=true;
            tcnRole.disabled=false;
            btntcnRole.disabled=false;
            break;            
         case "6": //Prestación
            tcnModulec.disabled=true;
            tcnCover.disabled=true;
            tcnPay_Concep.disabled=false;
            btntcnPay_Concep.disabled=false;
            tcnRole.disabled=true;
            break;            
      }	
   }
}


//% Cambios en la lógica de descuento de los costos coberturas. 
//% InsChangeField: se controla el cambio de valor de los campos de la página
//--------------------------------------------------------------------------------------------
function InsChangeField(vObj, sField){
//--------------------------------------------------------------------------------------------    
	var sValue;
	sValue = vObj.value;
	if (vObj.disabled==false) {
		with (self.document.forms[0]){
			switch (sField){
				case 'Modulec':
					if (sValue!='')
						tcnCover.Parameters.Param4.sValue=sValue;
					break;
			}
		}
	}
	else{
	    vObj.value=0;
	}    
}


//---------------------------------------------------------------------------------------------------------------------
//% OnChangeSel: Verifica si es posible borrar
//---------------------------------------------------------------------------------------------------------------------
function OnChangeSel(Field){
//---------------------------------------------------------------------------------------------------------------------
    with(self.document.forms[0]){
        insDefValues('sSel', "sCertype=" + '<%=Session("sCertype")%>' + "&nBranch=" + <%=Session("nBranch")%> + "&nProduct=" + <%=Session("nProduct")%> + "&nPolicy=" + <%=Session("nPolicy")%>  + "&nCertif=" + <%=Session("nCertif")%> + "&nSeq=" + marrArray[Field.value].tcnSeq + "&sCodispl=" + "CA960" + "&nIndex=" + Field.value ,'/VTimeNet/Policy/PolicySeq')
	}
}              
</SCRIPT>
	<%If Request.QueryString.Item("nGroup") = vbNullString Then
	mintGroupChange = 0
Else
	mintGroupChange = Request.QueryString.Item("nGroup")
End If

Response.Write("<SCRIPT>var mintGroupChange = " & CStr(mintGroupChange) & ";</SCRIPT>")
%>
	</HEAD>
    <BODY ONUNLOAD="closeWindows();">
        <FORM METHOD="post" ID="FORM" NAME="frmCA960" ACTION="ValPolicySeq.aspx?nGroup="<%=Request.QueryString.Item("nGroup")%>>
<%Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript")))%>
<%lclsGroups = New ePolicy.Groups
If lclsGroups.valGroupExist(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("deffecdate")) Then
	mblnGroup = True
End If
If mintGroupChange = 0 Then
	mintGroupChange = lclsGroups.nGroup
End If

lclsGroups = Nothing

Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreCA960()
Else
	Call insPreCA960Upd()
End If
mobjValues = Nothing
mobjGrid = Nothing
%>
        </FORM>
    </BODY>
</HTML>





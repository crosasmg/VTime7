<%@ Page Language="VB" explicit="true"  Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false" %>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">

'^Begin Header Block VisualTimer Utility 1.1 27/05/2003 07:39:47 a.m.
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Se define la variable mobjGrid para el manejo del Grid de la ventana
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo de las zonas de la página
Dim mobjMenu As eFunctions.Menues

Dim lclsTransport As ePolicy.transport
Dim mclsGeneral As eGeneral.GeneralFunction
Dim mstrError As String


'%insDefineHeader: Se definen las columnas del grid
'------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	'+Se definen todas las columnas del Grid
	
	With mobjGrid.Columns
		Call .AddPossiblesColumn(9507,"Mercancía", "cbeClassMerch", "Table232", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , 2,"Indica el tipo de mercancía que se transporta", eFunctions.Values.eTypeCode.eNumeric)
		Call .AddPossiblesColumn(9508,"Embalaje", "cbePacking", "Table237", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , 2,"tipo de embalaje utilizado en el transporte de la mercancía", eFunctions.Values.eTypeCode.eNumeric)
		Call .AddNumericColumn(9509,"Límite", "tcnLimit", 12, Request.QueryString.Item("tcnLimitCapital"), False,"Monto límite del valor de la mercancía", True, 0,  ,  ,  , False)
		Call .AddNumericColumn(9510,"Tasa", "tcnRate", 9, "", False,"Tasa a aplicara a la mercancía", True, 6,  ,  ,  , False)
		Call .AddPossiblesColumn(9511,"Tipo", "cbeType", "Table33", eFunctions.Values.eValuesType.clngComboType, CStr(1),  ,  ,  ,  , "insDisabledFields(this)",  , 2,"Tipo de deducible", eFunctions.Values.eTypeCode.eNumeric)
		mobjGrid.Columns("cbeType").BlankPosition = False
		
		If Request.QueryString.Item("Action") = "Add" Then
			Call .AddNumericColumn(9512,"Monto", "tcnAmo_deduc", 18, "", False,"Monto del deducible", True, 6,  ,  ,  , True)
			Call .AddNumericColumn(9513,vbNullString, "tcnDeduc", 4, "", False,"Porcentaje del deducible", True, 2,  ,  , "insDisabledFields(this)", True)
			Call .AddNumericColumn(9514,"Monto mínimo", "tcnMinAmount", 18, "", False,"Importe mínimo del deducible", True, 6,  ,  ,  , True)
			Call .AddNumericColumn(9515,"Monto máximo", "tcnMaxAmount", 18, "", False,"Importe máximo del deducible", True, 6,  ,  ,  , True)
		Else
			Call .AddNumericColumn(9512,"Monto", "tcnAmo_deduc", 18, "", False,"Monto del deducible", True, 6,  ,  ,  , mobjValues.StringToType(Request.QueryString.Item("cbeType"), eFunctions.Values.eTypeData.etdLong) = 1)
			Call .AddNumericColumn(9513,vbNullString, "tcnDeduc", 4, "", False,"Porcentaje del deducible", True, 2,  ,  , "insDisabledFields(this)", mobjValues.StringToType(Request.QueryString.Item("cbeType"), eFunctions.Values.eTypeData.etdLong) = 1)
			Call .AddNumericColumn(9514,"Monto mínimo", "tcnMinAmount", 18, "", False,"Importe mínimo del deducible", True, 6,  ,  ,  , mobjValues.StringToType(Request.QueryString.Item("tcnDeduc"), eFunctions.Values.eTypeData.etdLong) = eRemoteDB.Constants.intNull Or mobjValues.StringToType(Request.QueryString.Item("cbeType"), eFunctions.Values.eTypeData.etdLong) = 1)
			Call .AddNumericColumn(9515,"Monto máximo", "tcnMaxAmount", 18, "", False,"Importe máximo del deducible", True, 6,  ,  ,  , mobjValues.StringToType(Request.QueryString.Item("tcnDeduc"), eFunctions.Values.eTypeData.etdLong) = eRemoteDB.Constants.intNull Or mobjValues.StringToType(Request.QueryString.Item("cbeType"), eFunctions.Values.eTypeData.etdLong) = 1)
		End If
	End With
	
	With mobjGrid
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Codispl = Request.QueryString.Item("sCodispl")
		.Codisp = Request.QueryString.Item("sCodispl")
		.Top = 100
		.Height = 300
		.Width = 625
		.ActionQuery = mobjValues.ActionQuery
		.bOnlyForQuery = Request.QueryString.Item("nMainAction") = "401"
		.Columns("Sel").GridVisible = Not .ActionQuery
		.sDelRecordParam = "nClassMerch='+ marrArray[lintIndex].cbeClassMerch + '" & "&nPacking='+ marrArray[lintIndex].cbePacking + '"
		.Columns("cbeClassMerch").EditRecord = True
		
		.sEditRecordParam = "tcnLimitCapital=' + self.document.forms[0].tcnLimitCapital.value + '"
		
		If Request.QueryString.Item("Type") <> "PopUp" Then
			.Splits_Renamed.AddSplit(0, "", 4)
			.Splits_Renamed.AddSplit(9516,"Deducible", 5)
		End If
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub
'%insPreTR6000. Se crea la ventana madre (Principal)
'------------------------------------------------------------------------------
Private Sub insReaEstAmount()
	'------------------------------------------------------------------------------
	lclsTransport = New ePolicy.transport
	
	Call lclsTransport.Find(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"))
End Sub

'%insPreTR6000. Se crea la ventana madre (Principal)
'------------------------------------------------------------------------------
Private Sub insPreTR6000()
	'------------------------------------------------------------------------------
	Dim lcolTran_rates As ePolicy.Tran_rates
	Dim lclsTran_rate As Object
	Dim lintIndex As Short
	
	lcolTran_rates = New ePolicy.Tran_rates
	
	With mobjGrid
		If lcolTran_rates.Find(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate")) Then
			lintIndex = 0
			
			For	Each lclsTran_rate In lcolTran_rates
				.Columns("cbeClassMerch").DefValue = lclsTran_rate.nClassMerch
				.Columns("cbePacking").DefValue = lclsTran_rate.nPacking
				.Columns("tcnAmo_deduc").DefValue = lclsTran_rate.nAmo_deduc
				.Columns("tcnLimit").DefValue = lclsTran_rate.nLimitcapital
				.Columns("tcnRate").DefValue = lclsTran_rate.nRate
				.Columns("cbeType").DefValue = lclsTran_rate.sFrancapl
				.Columns("tcnAmo_deduc").DefValue = lclsTran_rate.nAmo_deduc
				.Columns("tcnDeduc").DefValue = lclsTran_rate.nDeductible
				.Columns("tcnMinAmount").DefValue = lclsTran_rate.nMinamount
				.Columns("tcnMaxAmount").DefValue = lclsTran_rate.nMaxamount
				
				.sEditRecordParam = "tcnLimitCapital=' + self.document.forms[0].tcnLimitCapital.value + '" & "&tcnDeduc=' + marrArray[" & lintIndex & "].tcnDeduc + '" & "&cbeType=' + marrArray[" & lintIndex & "].cbeType + '"
				
				Response.Write(mobjGrid.DoRow())
				lintIndex = lintIndex + 1
			Next lclsTran_rate
		End If
	End With
	
	Response.Write(mobjGrid.CloseTable())
	Response.Write(mobjValues.BeginPageButton)
	
	lclsTran_rate = Nothing
	lcolTran_rates = Nothing
End Sub

'% insPreTR6000Upd. Se define esta funcion para contruir el contenido de la 
'%                     ventana UPD de los archivos de datos particulares
'------------------------------------------------------------------------------
Private Sub insPreTR6000Upd()
	'------------------------------------------------------------------------------
	Dim lclsTran_rate As ePolicy.Tran_rate
	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			
			lclsTran_rate = New ePolicy.Tran_rate
			
			Call lclsTran_rate.InsPostTR6000(False, .QueryString.Item("sCodispl"), CInt(.QueryString.Item("nMainAction")), .QueryString.Item("Action"), Session("nUsercode"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), mobjValues.StringToType(.QueryString.Item("nClassMerch"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString.Item("nPacking"), eFunctions.Values.eTypeData.etdInteger), 0, 0, 0, 0, 0, 0, CStr(0))
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valPolicyseq.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"),  , CShort(.QueryString.Item("Index"))))
	End With
	
	lclsTran_rate = Nothing
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
Call mobjNetFrameWork.BeginPage(Request.QueryString.Item("sCodispl"))

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 27/05/2003 07:39:47 a.m.
mobjValues.sSessionID = Session.SessionID
mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
'~End Body Block VisualTimer Utility

mobjValues.ActionQuery = (Request.QueryString.Item("nMainAction") = "401")

If Not mobjValues.ActionQuery Then
	mclsGeneral = New eGeneral.GeneralFunction
	mstrError = mclsGeneral.insLoadMessage(90097)
	mclsGeneral = Nothing
End If
%> 
<SCRIPT  LANGUAGE="JavaScript">
//**-Objetive: This line keep the source safe version
//-Objeto: Esta línea guarda la versión procedente de VSS 
//------------------------------------------------------------------------------------------
    document.VssVersion="$$Revision: 4 $|$$Date: 13/09/04 2:56p $$Author: Pvillegas $"
//------------------------------------------------------------------------------------------

//**-Objetive: enabled and disabled the fields minimum and maximum
//-Objeto: habilita y deshabilita los campos de mínimo y máximo
//----------------------------------------------------------------------
function insDisabledFields(Field){
//----------------------------------------------------------------------
	if (Field.name=='cbeType')
	{
		if (Field.value==1)
		{
			with (self.document.forms[0])
			{
				tcnMinAmount.disabled=true;
				tcnMaxAmount.disabled=true;
				tcnDeduc.disabled=true;
				tcnAmo_deduc.disabled=true;
				tcnMaxAmount.value='';
				tcnMinAmount.value='';
				tcnDeduc.value='';
				tcnAmo_deduc.value='';
			}
		}
		else
		{
			with (self.document.forms[0])
			{
				tcnDeduc.disabled=false;
				tcnAmo_deduc.disabled=false;
			}
		}
	}
	else
	{
		if (insConvertNumber(Field.value)>0)
		{
			with (self.document.forms[0])
			{
				tcnMinAmount.disabled=false;
				tcnMaxAmount.disabled=false;
			}
		}
		else
		{
			with (self.document.forms[0])
			{
				tcnMinAmount.disabled=true;
				tcnMaxAmount.disabled=true;
				tcnMaxAmount.value='';
				tcnMinAmount.value='';
			}
		}
	}
}

//----------------------------------------------------------------------
function insValField(Field){
//----------------------------------------------------------------------
	with(self.document.forms[0]){
	
	    if(insConvertNumber(Field.value) > hddLimit.value){
		    alert("Err. 90097:" + "<%=mstrError%>");		     
		    tcnLimitCapital.value = VTFormat(hddLimit.value, '', '<%=Session("Session_DecimalSeparator")%>', '<%=Session("Session_ThousandSeparator")%>', tcnLimitCapital.DecimalPlace);
	    }
	    else{
	        with(document.location){
		    href = href.replace(/&tcnLimitCapital.*/,'') + '&tcnLimitCapital=' + Field.value
		    }
	    }
    }	    
}
</SCRIPT>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
  <HEAD>
	<META NAME="GENERATOR" Content="eTransaction Designer for Visual TIME">
<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write("<SCRIPT>var	nMainAction	= " & CShort("0" & Request.QueryString.Item("nMainAction")) & "</SCRIPT>")
	If Request.QueryString.Item("Type") <> "PopUp" Then
		mobjMenu = New eFunctions.Menues
		.Write(mobjMenu.setZone(2, Request.QueryString.Item("sCodispl"), "TR6000.aspx"))
		mobjMenu = Nothing
	End If
End With
%>
</HEAD>	  
<BODY ONUNLOAD="closeWindows();">      
 <FORM METHOD="POST"	ID="FORM" NAME="frmTR6000" ACTION="valPolicyseq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
 <%Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))

If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insReaEstAmount()
	%>
		<TABLE ALIGN="CENTER" WIDTH="50%">
			<TR>
				<TD><LABEL ID=9505>Límite de Capital</LABEL></TD>
				<TD>
					<%	
	mobjValues.BlankPosition = False
	
    If Not String.IsNullOrEmpty(Request.QueryString.Item("tcnLimitCapital")) then
	    If CDbl(Request.QueryString.Item("tcnLimitCapital")) > 0 Then
		    Response.Write(mobjValues.NumericControl("tcnLimitCapital", 12, Request.QueryString.Item("tcnLimitCapital"),  ,"Monto límite del valor de la mercancía", True,  ,  ,  ,  , "insValField(this)"))
	    Else
		    Response.Write(mobjValues.NumericControl("tcnLimitCapital", 12, CStr(lclsTransport.nMaxLimTrip),  ,"Monto límite del valor de la mercancía", True,  ,  ,  ,  , "insValField(this)"))
	    End If
	Else
        Response.Write(mobjValues.NumericControl("tcnLimitCapital", 12, CStr(lclsTransport.nMaxLimTrip),  ,"Monto límite del valor de la mercancía", True,  ,  ,  ,  , "insValField(this)"))					        
	End If
	%>
				</TD>
				<%	
	Response.Write(mobjValues.HiddenControl("hddLimit", CStr(lclsTransport.nMaxLimTrip)))
	%>
			</TR>        
		</TABLE>
<%	
End If

Call insDefineHeader()

If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreTR6000()
Else
	Call insPreTR6000Upd()
End If

lclsTransport = Nothing
mobjValues = Nothing
mobjGrid = Nothing
%>	  
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 27/05/2003 07:39:47 a.m.
Call mobjNetFrameWork.FinishPage(Request.QueryString.Item("sCodispl"))
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>









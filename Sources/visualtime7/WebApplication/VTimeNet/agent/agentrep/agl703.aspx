<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eAgent" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.44.07
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenu As eFunctions.Menues


'% insDefineHeader: Se definen los campos del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.44.07
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = "AGL703"
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	'+ Se definen las columnas del grid  
	
	With mobjGrid.Columns
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnintertypColumnCaption"), "tcnintertyp", 5, CStr(0))
		Call .AddTextColumn(0, GetLocalResourceObject("tcsintertypColumnCaption"), "tcsintertyp", 30, "",  , GetLocalResourceObject("tcsintertypColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnpay_commColumnCaption"), "tcnpay_comm", 10, CStr(0))
		Call .AddDateColumn(0, GetLocalResourceObject("tcdpay_dateColumnCaption"), "tcdpay_date",  , True, GetLocalResourceObject("tcdpay_dateColumnToolTip"))
		Call .AddDateColumn(0, GetLocalResourceObject("tcdprocsupColumnCaption"), "tcdprocsup",  , True, GetLocalResourceObject("tcdprocsupColumnToolTip"))
		Call .AddDateColumn(0, GetLocalResourceObject("tcdval_dateColumnCaption"), "tcdval_date",  , True, GetLocalResourceObject("tcdval_dateColumnCaption"))
		Call .AddDateColumn(0, GetLocalResourceObject("tcdcompdateColumnCaption"), "tcdcompdate",  , True, GetLocalResourceObject("tcdcompdateColumnCaption"))
		Call .AddCheckColumn(0, GetLocalResourceObject("chkRequireColumnCaption"), "chkRequire", vbNullString)
		Call .AddHiddenColumn("hddsRequire", "")
		Call .AddHiddenColumn("hddnintertyp", "")
		Call .AddHiddenColumn("hddsintertyp", "")
		Call .AddHiddenColumn("hddnpay_comm", "")
		Call .AddHiddenColumn("hdddpay_date", "")
		Call .AddHiddenColumn("hdddprocsup", "")
		Call .AddHiddenColumn("hdddval_date", "")
		Call .AddHiddenColumn("hdddcompdate", "")
	End With
	
	'+ Se definen las propiedades generales del grid
	
	With mobjGrid
		.Codispl = "AGL703"
		.DeleteButton = False
		.AddButton = False
		.Columns("Sel").GridVisible = False
	End With
	
	If Request.QueryString.Item("nMainAction") = "401" Or Request.QueryString.Item("nMainAction") = vbNullString Then
		mobjGrid.ActionQuery = False
		mobjGrid.Columns("Sel").GridVisible = True
	End If
	
End Sub

'% insPreAGL703: Se cargan los controles de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreAGL703()
	'--------------------------------------------------------------------------------------------
	Dim lclsPay_Comm As eAgent.pay_comm
	Dim lclsPay_Comms As eAgent.pay_comms
	Dim lintCount As Short
	
	lclsPay_Comm = New eAgent.pay_comm
	lclsPay_Comms = New eAgent.pay_comms
	If lclsPay_Comms.FindAGL703(mobjValues.StringToType(Request.QueryString.Item("dDateIni"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString.Item("dDateEnd"), eFunctions.Values.eTypeData.etdDate)) Then
		lintCount = 0
		
		For	Each lclsPay_Comm In lclsPay_Comms
			With mobjGrid
				
				.Columns("tcnintertyp").DefValue = CStr(lclsPay_Comm.nInterTyp)
				.Columns("tcsintertyp").DefValue = lclsPay_Comm.sIntertyp
				.Columns("tcnpay_comm").DefValue = CStr(lclsPay_Comm.nPay_Comm)
				.Columns("tcdpay_date").DefValue = mobjValues.TypeToString(lclsPay_Comm.dPay_Date, eFunctions.Values.eTypeData.etdDate)
				.Columns("tcdprocsup").DefValue = mobjValues.TypeToString(lclsPay_Comm.dProcSup, eFunctions.Values.eTypeData.etdDate)
				.Columns("tcdval_date").DefValue = mobjValues.TypeToString(lclsPay_Comm.dVal_Date, eFunctions.Values.eTypeData.etdDate)
				.Columns("tcdcompdate").DefValue = mobjValues.TypeToString(lclsPay_Comm.dCompdate, eFunctions.Values.eTypeData.etdDate)
				
				.Columns("hddsRequire").DefValue = CStr(2)
				.Columns("hddnintertyp").DefValue = CStr(lclsPay_Comm.nInterTyp)
				.Columns("hddsintertyp").DefValue = lclsPay_Comm.sIntertyp
				.Columns("hddnpay_comm").DefValue = CStr(lclsPay_Comm.nPay_Comm)
				.Columns("hdddpay_date").DefValue = mobjValues.TypeToString(lclsPay_Comm.dPay_Date, eFunctions.Values.eTypeData.etdDate)
				.Columns("hdddprocsup").DefValue = mobjValues.TypeToString(lclsPay_Comm.dProcSup, eFunctions.Values.eTypeData.etdDate)
				.Columns("hdddval_date").DefValue = mobjValues.TypeToString(lclsPay_Comm.dVal_Date, eFunctions.Values.eTypeData.etdDate)
				.Columns("hdddcompdate").DefValue = mobjValues.TypeToString(lclsPay_Comm.dCompdate, eFunctions.Values.eTypeData.etdDate)
				
				.Columns("chkRequire").OnClick = "insChangeRequire(this, " & lintCount & ");"
				.Columns("Sel").Checked = CShort("0")
				'                .Columns("Sel").OnClick = "insSelect(this, " & lintCount & ");"
				lintCount = lintCount + 1
				Response.Write(.DoRow)
			End With
			If lintCount = 200 Then
				Exit For
			End If
		Next lclsPay_Comm
		
	End If
	Response.Write(mobjGrid.closeTable())
	
	Response.Write(mobjValues.HiddenControl("hdddDateIni", mobjValues.StringToType(Request.QueryString.Item("dDateIni"), eFunctions.Values.eTypeData.etdDate)))
	Response.Write(mobjValues.HiddenControl("hdddDateEnd", mobjValues.StringToType(Request.QueryString.Item("dDateEnd"), eFunctions.Values.eTypeData.etdDate)))
	'+ Se reasignan los valores del ancabezado de la forma
	lclsPay_Comm = Nothing
	lclsPay_Comms = Nothing
End Sub

</script>
<%
Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("AGL703")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.44.07
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "AGL703"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.44.07
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
%>
<SCRIPT LANGUAGE="JavaScript">
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
//------------------------------------------------------------------------------------------
function insPrintCollection(){
//------------------------------------------------------------------------------------------
	insDefValues("COL747_REP"," ");
}
</SCRIPT>




<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>
<SCRIPT>
//+ Variable para el control de versiones
     document.VssVersion="$$Revision: 2 $|$$Date: 25/10/04 14:18 $|$$Author: Nvaplat11 $"

//%insSelect: Actualiza las columnas ocultas
//-------------------------------------------------------------------------------------------
function insSelect(Field, Index){
//-------------------------------------------------------------------------------------------
    with(self.document.forms[0]){
        if (typeof(hddsSel[Index])=='undefined'){
            hddsSel.value=(Field.checked)?1:2
            if (!Field.checked) chkRequire.checked = false;
        }
        else{
            hddsSel[Index].value=(Field.checked)?1:2
            if (!Field.checked) chkRequire[Index].checked = false;
        }
    }
}

//%insChangeRequire: Actualiza la columna de selección
//-------------------------------------------------------------------------------------------
function insChangeRequire(Field, Index){
//-------------------------------------------------------------------------------------------
    with(self.document.forms[0]){
        if (typeof(hddsRequire[Index])=='undefined'){
            hddsRequire.value=(Field.checked)?1:2
            if (!Field.checked) chkRequire.checked = false;
        }
        else{
            hddsRequire[Index].value=(Field.checked)?1:2
        }
    }
}
</SCRIPT>
<META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<%Response.Write(mobjValues.StyleSheet())
Response.Write(mobjMenu.setZone(2, "AGL703", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))

If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Then
	mobjValues.ActionQuery = True
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="AGL703" ACTION="valagentrep.aspx?sMode=2"> 
<%
Response.Write(mobjValues.ShowWindowsName("AGL703", Request.QueryString.Item("sWindowDescript")))
Call insDefineHeader()
Call insPreAGL703()
mobjGrid = Nothing
mobjValues = Nothing
%>     

</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.44.07
Call mobjNetFrameWork.FinishPage("AGL703")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>





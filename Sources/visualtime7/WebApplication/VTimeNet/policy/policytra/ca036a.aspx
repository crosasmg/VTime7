<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
    '^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.31.20
    Dim mobjNetFrameWork As eNetFrameWork.Layout
    '~End Header Block VisualTimer Utility

    '- Objeto para el manejo de las funciones generales de carga de valores

    Dim mobjValues As eFunctions.Values
    Dim mobjGrid As eFunctions.Grid
    Dim mobjMenues As eFunctions.Menues


    '% insDefineHeader: se definen las características del grid
    '--------------------------------------------------------------------------------------------
    Private Sub insDefineHeader()
        '--------------------------------------------------------------------------------------------
        mobjGrid = New eFunctions.Grid
        '^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.20
        mobjGrid.sSessionID = Session.SessionID
        mobjGrid.nUsercode = Session("nUsercode")
        '~End Body Block VisualTimer Utility

        mobjGrid.sCodisplPage = "ca036a"
        Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))

        With mobjGrid.Columns
            Call .AddNumericColumn(0, GetLocalResourceObject("tcnCertifColumnCaption"), "tcnCertif", 10, vbNullString,  , GetLocalResourceObject("tcnCertifColumnToolTip"))
            Call .AddTextColumn(0, GetLocalResourceObject("tctYear_monthColumnCaption"), "tctYear_month", 7, vbNullString,  , GetLocalResourceObject("tctYear_monthColumnToolTip"))
            Call .AddDateColumn(0, GetLocalResourceObject("tcdStartdateColumnCaption"), "tcdStartdate", vbNullString,  , GetLocalResourceObject("tcdStartdateColumnToolTip"))
            Call .AddDateColumn(0, GetLocalResourceObject("tcdExpirdatColumnCaption"), "tcdExpirdat", vbNullString,  , GetLocalResourceObject("tcdExpirdatColumnToolTip"))
            Call .AddNumericColumn(0, GetLocalResourceObject("tcnPremiumColumnCaption"), "tcnPremium", 18, vbNullString,  , GetLocalResourceObject("tcnPremiumColumnToolTip"), True, 6)
            Call .AddPossiblesColumn(41459, GetLocalResourceObject("cbeCurrencyColumnCaption"), "cbeCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType, vbNullString,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeCurrencyColumnToolTip"))
            Call .AddHiddenColumn("hddDigit", CStr(0))
            Call .AddHiddenColumn("hddMovnumbe", CStr(0))
            Call .AddHiddenColumn("hddCertif", CStr(0))
            Call .AddHiddenColumn("hddSelAux", CStr(2))
        End With

        With mobjGrid
            .Codispl = Request.QueryString.Item("sCodispl")
            .AddButton = False
            .DeleteButton = False
            .Columns("Sel").OnClick = "calPremium(this)"
        End With
    End Sub

    '% insPreCA036A: se buscan los datos a mostrar en la página
    '--------------------------------------------------------------------------------------------
    Private Sub insPreCa036A()
        '--------------------------------------------------------------------------------------------
        Dim ldblPremium As Double
        Dim lclsOut_moveme As Object
        Dim lcolOut_movemes As ePolicy.Out_movemes
        lcolOut_movemes = New ePolicy.Out_movemes

        If lcolOut_movemes.Find(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dStart"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("dEnd"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nCurrency"), eFunctions.Values.eTypeData.etdDouble)) Then
            ldblPremium = 0
            For Each lclsOut_moveme In lcolOut_movemes
                With mobjGrid
                    .Columns("tcnCertif").DefValue = lclsOut_moveme.nCertif
                    If lclsOut_moveme.nYear_month <> eRemoteDB.Constants.intNull Then
                        .Columns("tctYear_month").DefValue = Mid(CStr(lclsOut_moveme.nYear_month), 1, 4) & " - " & Mid(CStr(lclsOut_moveme.nYear_month), 5)
                    End If
                    .Columns("tcdStartdate").DefValue = lclsOut_moveme.dStartdate
                    .Columns("tcdExpirdat").DefValue = lclsOut_moveme.dExpirdat
                    .Columns("tcnPremium").DefValue = lclsOut_moveme.nPremium
                    .Columns("cbeCurrency").DefValue = lclsOut_moveme.nCurrency
                    .Columns("hddDigit").DefValue = lclsOut_moveme.nDigit
                    .Columns("hddMovnumbe").DefValue = lclsOut_moveme.nMovnumbe
                    .Columns("hddCertif").DefValue = lclsOut_moveme.nCertif
                    .Columns("Sel").Checked = CShort("2")
                    .Columns("hddSelAux").DefValue = "2"
                    If lclsOut_moveme.sStatus_mov = "4" Then
                        .Columns("Sel").Checked = CShort("1")
                        .Columns("hddSelAux").DefValue = "1"
                        ldblPremium = ldblPremium + lclsOut_moveme.nPremium
                    End If
                    Response.Write(mobjGrid.DoRow())
                End With
            Next lclsOut_moveme
        End If

        Response.Write(mobjGrid.CloseTable())

        Response.Write("" & vbCrLf)
        Response.Write("	<BR>" & vbCrLf)
        Response.Write("	<TABLE WIDTH=100%>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("			<TD WIDTH=20%><LABEL>" & GetLocalResourceObject("AnchorCaption") & "<LABEL></TD>" & vbCrLf)
        Response.Write("			<TD><LABEL><DIV ID=""divPremium""></DIV></LABEL></TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("	</TABLE>")


        lclsOut_moveme = Nothing
        lcolOut_movemes = Nothing
    End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("ca036a")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.20
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "ca036a"
%>
<HTML>
<HEAD>
	<META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>    
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/ValFunctions.js"></SCRIPT>




<%
mobjMenues = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.20
mobjMenues.sSessionID = Session.SessionID
mobjMenues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenues.setZone(2, "CA036A", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
End With
mobjMenues = Nothing
%>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 3 $|$$Date: 15/10/03 16:53 $|$$Author: Nvaplat61 $"
	var nMainAction='<%=Request.QueryString.Item("nMainAction")%>';
    var mdblPremium = 0;

//% calPremium: se calcula el total de prima a facturar para los movimientos seleccionados
//-------------------------------------------------------------------------------------------
function calPremium(Field, nTotal){
//-------------------------------------------------------------------------------------------
	var lblnChecked
	if(typeof(nTotal)!='undefined'){
		for(lintIndex=0;lintIndex<marrArray.length;lintIndex++){
			if(marrArray.length==1){
				if(self.document.forms[0].Sel.checked)
					lblnChecked = true;
			}
			else{
				if(self.document.forms[0].Sel[lintIndex].checked)
					lblnChecked = true;
			}
			if(lblnChecked)
				if(marrArray[lintIndex].tcnPremium!='')
					mdblPremium = mdblPremium + insConvertNumber(marrArray[lintIndex].tcnPremium);
			lblnChecked = false;
		}
	}
	else{
		if(marrArray[Field.value].tcnPremium!='')
			if(Field.checked)
				mdblPremium = mdblPremium + insConvertNumber(marrArray[Field.value].tcnPremium);
			else{
				mdblPremium = mdblPremium*100 - insConvertNumber(marrArray[Field.value].tcnPremium)*100;
				mdblPremium = mdblPremium/100
			}	
		if (marrArray.length==1)
			self.document.forms[0].hddSelAux.value = (Field.checked?1:2);
		else{
			self.document.forms[0].hddSelAux[Field.value].value = (Field.checked?1:2);
			}
	}

	UpdateDiv('divPremium', VTFormat(mdblPremium, "", "", "", 6, true))
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="CA036A" ACTION="ValBillGroupPolSeq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%
Response.Write(mobjValues.ShowWindowsName("CA036A", Request.QueryString.Item("sWindowDescript")))
Call insDefineHeader()
Call insPreCa036A()

mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>
<SCRIPT>calPremium(1,1)</SCRIPT>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.31.20
Call mobjNetFrameWork.FinishPage("ca036a")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>





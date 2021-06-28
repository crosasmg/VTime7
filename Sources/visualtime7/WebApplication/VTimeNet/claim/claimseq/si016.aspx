<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClaim" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.35.39
Dim mobjNetFrameWork As eNetFrameWork.Layout

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenu As eFunctions.Menues


'% insDefineHeader: se definen los campos del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.39
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = "si016"
        Call mobjGrid.SetWindowParameters(Request.QueryString("sCodispl"), Request.QueryString("sWindowDescript"), Request.QueryString("nWindowTy"))
        If Session("nTransaction") = eClaim.Claim_win.eClaimTransac.clngClaimQuery Then
            mobjGrid.ActionQuery = Session("bQuery")
        Else
            mobjGrid.ActionQuery = False
            Session("bQuery") = False
        End If
	
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		Call .AddTextColumn(40328, "Casos", "nCase", 50, vbNullString)
		Call .AddPossiblesColumn(40327, "Estado", "nStatus", "Table903", eFunctions.Values.eValuesType.clngComboType, vbNullString)
		Call .AddCheckColumn(40329, "Pierde descto. no siniestralidad", "nDcto", vbNullString,  , "1")
		Call .AddHiddenColumn("nCase_num", vbNullString)
		Call .AddHiddenColumn("nDeman_type", vbNullString)
		Call .AddHiddenColumn("nBene_type", vbNullString)
		Call .AddHiddenColumn("sClient", vbNullString)
		Call .AddHiddenColumn("nAuxDcto", vbNullString)
		Call .AddHiddenColumn("nId", CStr(0))
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "SI016"
		.Codisp = "SI016"
		.DeleteButton = False
		.AddButton = False
		.Columns("Sel").GridVisible = False
		.Columns("nCase").HRefScript = "ShowCaseSeq()"
		.Columns("nDcto").Disabled = Session("bQuery")
	End With
End Sub

'% insPreSI016: se cargan los controles de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreSI016()
	'--------------------------------------------------------------------------------------------
	Dim lintIndex As Short
	Dim lcolClaim_cases As eClaim.Claim_cases
	Dim lclsClaim_case As eClaim.Claim_case
    Dim lclsCases_Win As eClaim.Cases_win  
	Dim lstrReloadBySeqCase As String
	Dim lobjTables As eFunctions.Tables
	Dim lstrStatusDesc As String
	
	lstrReloadBySeqCase = Request.QueryString("ReloadBySeqCase")
	
	lintIndex = 0
	lclsClaim_case = New eClaim.Claim_case
	lcolClaim_cases = New eClaim.Claim_cases
    lclsCases_Win = New eClaim.Cases_win      
	
	lcolClaim_cases.OnlyDemandant = True
	If lcolClaim_cases.Find(mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble)) Then
            
		lobjTables = New eFunctions.Tables
            
		For	Each lclsClaim_case In lcolClaim_cases
			With mobjGrid
				.Columns("nCase").DefValue = lclsClaim_case.nCase_num & " - " & lclsClaim_case.sDescript & " - " & lclsClaim_case.sCliename
                    If lclsClaim_case.sDescript = "Siniestrado" Then
                        Session("Siniestrado") = lclsClaim_case.sCliename
 						Session("NombreSiniestrado") = lclsClaim_case.sCliename
                    ElseIf lclsClaim_case.sDescript = "Beneficiario" Then
                        Session("Beneficiario") = lclsClaim_case.sCliename
						Session("NombreBeneficiario") = lclsClaim_case.sCliename
                    ElseIf lclsClaim_case.sDescript = "Reclamante" Then
                        Session("Reclamante") = lclsClaim_case.sCliename
						Session("NombreReclamante") = lclsClaim_case.sCliename
                    End If

                If lclsCases_Win.valFullCases(Session("nClaim"), lclsClaim_case.nCase_num, lclsClaim_case.nDeman_type) = 0 then
                    .Columns("nStatus").DefValue = "1"
		            If lobjTables.GetDescription("Table903", "1") Then
			            .Columns("nStatus").Descript = lobjTables.Descript
		            Else
			            .Columns("nStatus").Descript = ""
		            End If
                Else 
                    .Columns("nStatus").DefValue = "2"    
                        
		            If lobjTables.GetDescription("Table903", "2") Then
			            .Columns("nStatus").Descript = lobjTables.Descript
		            Else
			            .Columns("nStatus").Descript = ""
		            End If
                End If
				

				If lclsClaim_case.sClaim_affe = vbNullString Then
					.Columns("nDcto").Checked = CShort("2")
					.Columns("nAuxDcto").DefValue = "2"
				Else
					.Columns("nDcto").Checked = CShort(lclsClaim_case.sClaim_affe)
					.Columns("nAuxDcto").DefValue = "1"
				End If
				.Columns("nCase_num").DefValue = CStr(lclsClaim_case.nCase_num)
				.Columns("nDeman_type").DefValue = CStr(lclsClaim_case.nDeman_type)
				.Columns("nBene_type").DefValue = CStr(lclsClaim_case.nBene_type)
				.Columns("sClient").DefValue = lclsClaim_case.sClient
				
				.Columns("nDcto").OnClick = "changeValue(this," & lintIndex & ")"
				
				If lcolClaim_cases.Count > 1 Then
					.Columns("nCase").HRefScript = "ShowCaseSeq(" & lintIndex & ")"
				End If
				.Columns("nId").DefValue = CStr(lclsClaim_case.nId)
				Response.Write(.DoRow)
			End With
			
			lintIndex = lintIndex + 1
		Next lclsClaim_case
	End If
	Response.Write(mobjGrid.closeTable())
	
	'+ Si existe un solo caso, y no se está recargando desde la subsecuencia,
	'+ se invoca automáticamente
	If lcolClaim_cases.Count = 1 And lstrReloadBySeqCase = "" Then
		Response.Write("<SCRIPT>ShowCaseSeq()</" & "Script>")
	End If
	
	'UPGRADE_NOTE: Object lclsClaim_case may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lclsClaim_case = Nothing
	'UPGRADE_NOTE: Object lcolClaim_cases may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lcolClaim_cases = Nothing
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("si016")
'~End Header Block VisualTimer Utility
'Response.CacheControl = False
Response.CacheControl = "no-cache"
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.39
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "si016"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.39
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
    <SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    

    <%With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle("SI016", Request.QueryString("sWindowDescript")))
	.Write(mobjMenu.setZone(2, "SI016", Request.QueryString("sWindowDescript"), Request.QueryString("nWindowTy")))
End With
'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjMenu = Nothing
%>

<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 11-02-13 17:27 $"        

//% changeValue: se cambia el valor del campo auxiliar del Check, para poder utilizarlo en 
//				 valClaimSeq.aspx
//-------------------------------------------------------------------------------------------
function changeValue(Field, Index){
//-------------------------------------------------------------------------------------------
	if(typeof(self.document.forms[0].nAuxDcto[Index])=='undefined')
		(Field.checked)?self.document.forms[0].nAuxDcto.value="1":self.document.forms[0].nAuxDcto.value="2"	
	else
		(Field.checked)?self.document.forms[0].nAuxDcto[Index].value="1":self.document.forms[0].nAuxDcto[Index].value="2"
	
}

//% ShowCaseSeq: se invoca la subsecuencia de casos
//-------------------------------------------------------------------------------------------
function ShowCaseSeq(Index){
//-------------------------------------------------------------------------------------------
	if (marrArray.length == 1)
	{
		with(self.document.forms[0])
		{
			ShowPopUp("/VTimeNet/Common/SecWHeader.aspx?sCodispl=SI099" + 
					  "&sModule=Claim&sProject=CaseSeq&sConfig=InSequence" +
					  "&nCase_num=" + nCase_num.value + 
					  "&nDeman_type=" + nDeman_type.value + 
					  "&nBene_type=" + nBene_type.value +
					  "&nId=" + nId.value,
					  "CaseSeq", 1050, 700, "no", "no", 10, 10)
		}
	}
	else
	{
		with(self.document.forms[0])
		{
			ShowPopUp("/VTimeNet/Common/SecWHeader.aspx?sCodispl=SI099" + 
					  "&sModule=Claim&sProject=CaseSeq&sConfig=InSequence" +
					  "&nCase_num=" + nCase_num[Index].value + 
					  "&nDeman_type=" + nDeman_type[Index].value + 
					  "&nBene_type=" + nBene_type[Index].value +
					  "&nId=" + nId[Index].value,
					  "CaseSeq", 1050, 700, "no", "no", 10, 10)
		}
	}
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmSI016" ACTION="valClaimSeq.aspx?sMode=1">
<%
Response.Write(mobjValues.ShowWindowsName("SI016", Request.QueryString("sWindowDescript")))
Call insDefineHeader()
If Request.QueryString("Type") <> "PopUp" Then
	Call insPreSI016()
End If
'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing
'UPGRADE_NOTE: Object mobjGrid may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.35.39
Call mobjNetFrameWork.FinishPage("si016")
'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>





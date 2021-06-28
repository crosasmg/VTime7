<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">

Dim mobjValues As eFunctions.Values


'% insShowQuan: Obtiene las unidades establecidas para cada tipo de fondo
'--------------------------------------------------------------------------------------------
Private Sub insShowQuan()
	'--------------------------------------------------------------------------------------------
	Dim lclsFundinv As ePolicy.Fund_inv
	Dim lclsFundinvs As ePolicy.Fund_invs
	
	lclsFundinv = New ePolicy.Fund_inv
	lclsFundinvs = New ePolicy.Fund_invs
	
	If lclsFundinvs.Find(CBool(mobjValues.StringToDate(Session("dEffecdate")).ToOADate)) Then
		For	Each lclsFundinv In lclsFundinvs
			If Request.QueryString.Item("nFunds") = CStr(lclsFundinv.nFunds) Then
				Response.Write("opener.document.forms[0].tcnQuan_avail.value=" & CStr(lclsFundinv.nQuan_avail) & ";")
			End If
		Next lclsFundinv
	End If
	
	lclsFundinv = Nothing
	lclsFundinvs = Nothing
End Sub

'**% insShowQuan_avail: This procedure shows the units total available in the fund.
'% insShowQuan_avail: Permite mostrar el total de unidades disponibles en el fondo.
'--------------------------------------------------------------------------------------------
Private Sub insShowQuan_avail()
	'--------------------------------------------------------------------------------------------
	Dim lclsFund_inv As ePolicy.Fund_inv
	lclsFund_inv = New ePolicy.Fund_inv
	
	If lclsFund_inv.Find(CInt(Request.QueryString.Item("nFunds"))) Then
		Response.Write("UpdateDiv('lblQuan_avail','" & mobjValues.TypeToString(lclsFund_inv.nQuan_avail, eFunctions.Values.eTypeData.etdDouble, True, 2) & "','PopUp');")
	End If
	
	lclsFund_inv = Nothing
End Sub

'% ShowDate: Busca última fecha de ejecución del proceso unificado de inversiones más un día
'--------------------------------------------------------------------------------------------
Private Sub ShowDate()
	'--------------------------------------------------------------------------------------------
	Dim lclsPolicy As ePolicy.Policy
	Dim ldtmLast_date As Date
	Dim lstrDate As String
	
	lclsPolicy = New ePolicy.Policy
	
	ldtmLast_date = lclsPolicy.GetLast_date_APV(Request.QueryString("sCodisplOri"), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull)
	
	If Microsoft.VisualBasic.Day(ldtmLast_date) < 10 Then
		lstrDate = "0" & Microsoft.VisualBasic.Day(ldtmLast_date) & "/"
	Else
		lstrDate = Microsoft.VisualBasic.Day(ldtmLast_date) & "/"
	End If
	
	If Month(ldtmLast_date) < 10 Then
		lstrDate = lstrDate & "0" & Month(ldtmLast_date) & "/"
	Else
		lstrDate = lstrDate & Month(ldtmLast_date) & "/"
	End If
	lstrDate = lstrDate & Year(ldtmLast_date)
	
	Response.Write("top.frames['fraHeader'].document.forms[0].tcdEffecDate.value='" & lstrDate & "';")
	
	'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lclsPolicy = Nothing
End Sub


'% ShowDateMax: Busca última fecha de ejecución del proceso unificado de inversiones más un día
    '--------------------------------------------------------------------------------------------
    Private Sub ShowDateMax()
        '--------------------------------------------------------------------------------------------
        Dim lclsPolicy As ePolicy.Fund_distribution
        Dim ldtmLast_date As Date
        Dim lstrDate As String
	
        If Request.QueryString("dEffecdate") = vbNullString Then
            ldtmLast_date = Today
        Else
            ldtmLast_date = mobjValues.StringToType(Request.QueryString("dEffecdate"), Values.eTypeData.etdDate)
            
        End If
        If mobjValues.StringToType(Request.QueryString("nBranch"), eFunctions.Values.eTypeData.etdInteger) > 0 And _
                  mobjValues.StringToType(Request.QueryString("nProduct"), eFunctions.Values.eTypeData.etdDouble) > 0 And _
                  mobjValues.StringToType(Request.QueryString("nTypeProfile"), eFunctions.Values.eTypeData.etdDouble) > 0 Then
            lclsPolicy = New ePolicy.Fund_distribution
            If lclsPolicy.Find_date(mobjValues.StringToType(Request.QueryString("nBranch"), eFunctions.Values.eTypeData.etdDouble), _
                  mobjValues.StringToType(Request.QueryString("nProduct"), eFunctions.Values.eTypeData.etdDouble), _
                  mobjValues.StringToType(Request.QueryString("nTypeProfile"), eFunctions.Values.eTypeData.etdDouble)) Then
                If lclsPolicy.dEffecdate > ldtmLast_date Then
                    'Response.Write("alert('" & lclsPolicy.dEffecdate & "'); top.frames['fraHeader'].document.forms[0].tcdEffecDate.value='" & lclsPolicy.dEffecdate & "';")
                    ldtmLast_date = lclsPolicy.dEffecdate
                End If
            End If
        End If
        If Microsoft.VisualBasic.Day(ldtmLast_date) < 10 Then
            lstrDate = "0" & Microsoft.VisualBasic.Day(ldtmLast_date) & "/"
        Else
            lstrDate = Microsoft.VisualBasic.Day(ldtmLast_date) & "/"
        End If
	
        If Month(ldtmLast_date) < 10 Then
            lstrDate = lstrDate & "0" & Month(ldtmLast_date) & "/"
        Else
            lstrDate = lstrDate & Month(ldtmLast_date) & "/"
        End If
        lstrDate = lstrDate & Year(ldtmLast_date)
	                    
        Response.Write("top.frames['fraHeader'].document.forms[0].tcdEffecdate.value='" & lstrDate & "';")

        'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        lclsPolicy = Nothing
    End Sub
</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


<SCRIPT>
//+ Variable para el control de versiones
	document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:03 $|$$Author: Iusr_llanquihue $"
</SCRIPT>
</HEAD>
<BODY>
	<FORM NAME="ShowDefValues">
	</FORM>
</BODY>
<BODY>
</BODY>
</HTML>
<%
Response.Write(mobjValues.StyleSheet() & vbCrLf)
Response.Write("<SCRIPT>")

Select Case Request.QueryString.Item("Field")
	Case "Funds"
		Call insShowQuan()
	Case "nQuan_avail"
		Call insShowQuan_avail()
	Case "ShowDate"
		Call ShowDate()
    Case "ShowDateMax"
        Call ShowDateMax()
End Select

Response.Write(mobjValues.CloseShowDefValues(Request.QueryString.Item("sFrameCaller")))
Response.Write("</SCRIPT>")

mobjValues = Nothing
%>





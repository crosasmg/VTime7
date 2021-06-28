<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">

'+ Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mPriorGroup As Integer
Dim mcolGroups As ePolicy.Groups
Dim mcolGroupss As ePolicy.Groupss


'% insreaGroups: Lee los valores de la tabla Groups.
'%                   Los valores corresponden a los Certificados asociados a una póliza
'--------------------------------------------------------------------------------------------
Private Sub insreaGroups()
	'--------------------------------------------------------------------------------------------
	Dim Index As Object
	Dim mintIndex As Integer
	Call mcolGroupss.Find(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("dEffectdate"))
	'For mintIndex = 1 To mcolGroupss.Count
    For mintIndex = 0 To mcolGroupss.Count -1
		mPriorGroup = mcolGroupss.Item(mintIndex).nGroup
	Next 
	If mcolGroupss.Count > 0 Then
		Response.Write(mobjValues.ButtonDelete("DeleteRecord(-1)"))
	End If
	Response.Write(mobjValues.ButtonAdd("ShowPopUp('CA011Upd.aspx?Action=Add&nPriorGroup=" & mPriorGroup + 1 & "', 'CA011Upd', 350,210);"))
	
Response.Write("" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD WIDTH=""100%"" COLSPAN=""4"">&nbsp;</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TABLE WIDTH=""100%"" CLASS=GRDDATA>" & vbCrLf)
Response.Write("            <TH><LABEL ID=40759>&nbsp;</LABEL></TH>" & vbCrLf)
Response.Write("            <TH><LABEL ID=40760>" & GetLocalResourceObject("AnchorCaption") & "</LABEL></TH>" & vbCrLf)
Response.Write("            <TH><LABEL ID=40761>" & GetLocalResourceObject("Anchor2Caption") & "</LABEL></TH>" & vbCrLf)
Response.Write("            <TH><LABEL ID=40762>" & GetLocalResourceObject("Anchor3Caption") & "</LABEL></TH>" & vbCrLf)
Response.Write("            <TH><LABEL ID=40763>" & GetLocalResourceObject("Anchor4Caption") & "</LABEL></TH>")

	
	If mcolGroupss.Count > 0 Then
		'For mintIndex = 1 To mcolGroupss.Count
        For mintIndex = 0 To mcolGroupss.Count -1
			With mcolGroupss.Item(mintIndex)
				
Response.Write("" & vbCrLf)
Response.Write("					<TR>" & vbCrLf)
Response.Write("					<TD WIDTH=2%>")


Response.Write(mobjValues.CheckControl("chkDelete", "",  , CStr(mintIndex - 1), "MarkRecord(this)"))


Response.Write("</TD>" & vbCrLf)
Response.Write("					<TD ALIGN=""Right"" WIDTH=15%>")


Response.Write(mobjValues.NumericControl("tcnGroup", 4, CStr(.nGroup),  , "", False,  , True,  , ""))


Response.Write("</TD>" & vbCrLf)
Response.Write("					<TD ALIGN=""Left""  WIDTH=33%>")


Response.Write(mobjValues.TextControl("txtDescript", 30, .sDescript,  , "", True,  , "ShowPopUp('CA011Upd.aspx?Action=Update&Index=0" & CStr(mintIndex - 1) & "','CA011Upd',350,260)"))


Response.Write("</TD>" & vbCrLf)
Response.Write("					<TD ALIGN=""Right"" WIDTH=25%>")


Response.Write(mobjValues.NumericControl("tcnParticip", 5, CStr(.nParticip),  , "", True, 2, True,  , ""))


Response.Write("</TD>" & vbCrLf)
Response.Write("					<TD ALIGN=""Left""  WIDTH=25%>")


Response.Write(mobjValues.PossiblesValues("cbeStatregt", "Table26", 1, .sStatregt,  , True))


Response.Write("</TD>					" & vbCrLf)
Response.Write("					</TR>")

				
				Response.Write("<SCRIPT>" & "insAddGroups(""" & .sCertype & """" & ",""" & .nBranch & """" & ",""" & .nPolicy & """" & ",""" & .nProduct & """" & ",""" & .nGroup & """" & ",""" & .dCompdate & """" & ",""" & .sClient & """" & ",""" & .sDescript & """" & ",""" & .nParticip & """" & ",""" & .sStatregt & """" & ",""" & .nUserCode & """" & ",""" & .nGroup & """" & ",false) </" & "Script>")
				mPriorGroup = .nGroup
			End With
		Next 
	Else
		'+ No existe data para ser mostrada
		Response.Write("<tr><td colspan=5 align=center><b>" & mobjValues.DataNotFound & "</b></td></tr>")
	End If
	
Response.Write("           " & vbCrLf)
Response.Write("    </TABLE>" & vbCrLf)
Response.Write("</FORM>" & vbCrLf)
Response.Write("</BODY>" & vbCrLf)
Response.Write("</HTML>")

	
	'+ UNA VEZ CULMINADA LA FUNCIÓN O EL MÉTODO, SE DEBEN DESTRUIR LAS INSTANCIAS CREADAS 
	'+ DE LOS OBJETOS QUE SE ENCUENTRAN EN EL SERVIDOR, PARA ASÍ LIBERAR LA MEMORIA
	mcolGroups = Nothing
	mcolGroupss = Nothing
	mobjValues = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mcolGroups = New ePolicy.Groups
mcolGroupss = New ePolicy.Groupss

mobjValues.ActionQuery = Session("bQuery")
%>
<SCRIPT LANGUAGE="JavaScript" SRC="../../Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript">
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:03 $|$$Author: Iusr_llanquihue $"

    var marrCA011 = []
    var mintCount = -1
    
/* %	insAddGroups: carga el arreglo con los datos de los Certificados (Tabla Groups)
-------------------------------------------------------------------------------------------*/
function insAddGroups(sCertype,  
						nBranch,   
						nPolicy,
						nProduct,  
						nGroup,    
						dCompdate,
						sClient,   
						sDescript, 
						nParticip,
						sStatregt, 
						nUsercode,
						nPriorGroup,
						DeleteRecord){
/*-------------------------------------------------------------------------------------------*/
    var ludtGroupsFields   = []
    
    ludtGroupsFields[0]    = sCertype
    ludtGroupsFields[1]    = nBranch
    ludtGroupsFields[2]    = nPolicy
    ludtGroupsFields[3]    = nProduct
    ludtGroupsFields[4]    = nGroup
    ludtGroupsFields[5]    = dCompdate
    ludtGroupsFields[6]    = sClient
    ludtGroupsFields[7]    = sDescript
    ludtGroupsFields[8]    = nParticip
    ludtGroupsFields[9]    = sStatregt
    ludtGroupsFields[10]   = nUsercode
    ludtGroupsFields[11]   = nPriorGroup
    ludtGroupsFields[12]   = DeleteRecord 
    marrCA011[++mintCount] = ludtGroupsFields
}

/* %	MarkRecord: marca el registro para eliminar
/-------------------------------------------------------------------------------------------*/
function MarkRecord(Field){
/*-------------------------------------------------------------------------------------------*/
    marrCA011[Field.value][12] = Field.checked
}

/* %	DeleteRecord: Elimina los registros seleccionados en la página de la Tabla Groups
 -------------------------------------------------------------------------------------------*/
function DeleteRecord(BeginIndex){
/*-------------------------------------------------------------------------------------------*/
	var lintIndex
	for (lintIndex=(BeginIndex+1);(lintIndex<=mintCount) && (!marrCA011[lintIndex][12]);lintIndex++){
	}
	if (lintIndex<=mintCount){
		ShowPopUp("CA011upd.aspx?Action=Delete&nGroup="+marrCA011[lintIndex][4]+"&Index="+lintIndex, "CA011upd", 350, 100);
	}
}

</SCRIPT>

<HTML>
<HEAD>


    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
    <LINK REL="StyleSheet" TYPE="text/css" HREF="../../Common/Custom.css">
<%
Response.Write(mobjValues.WindowsTitle("CA011"))
Response.Write(mobjMenu.setZone(2, Request.QueryString.Item("sCodispl"), "CA011.aspx"))
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmCA011" ACTION="CA011upd.aspx?x=1">
<%
Response.Write(mobjValues.ShowWindowsName("CA011"))

Call insreaGroups()

If Request.QueryString.Item("Reload") = "1" Then
	'+ Se recarga la ventana PopUp, en caso que el check de "Continuar" se encuentre marcado
	Select Case Request.QueryString.Item("ReloadAction")
		Case "Add"
			mPriorGroup = mPriorGroup + 1
			Response.Write("<SCRIPT>ShowPopUp(""CA011Upd.aspx?Action=Add&nPriorGroup=" & mPriorGroup & """, ""CA011Upd"", 350, 210)</SCRIPT>")
		Case "Update"
			'+ Se valida que el valor de el parámetro "RELOADINDEX" sea diferente de "0undefined" para que no recargue
			'+ la ventana POPUP sin registros y no se den errores de JScript al tratar de re-dibujar la ventana y asignarle
			'+ valores nulos - ACM - 17/02/2001
			If Request.QueryString.Item("ReloadIndex") <> "0undefined" Then
				Response.Write("<SCRIPT>ShowPopUp(""CA011Upd.aspx?Action=Update&Index=" & Request.QueryString.Item("ReloadIndex") & """,""CA011Upd"",350,260)</SCRIPT>")
			End If
	End Select
End If
%>





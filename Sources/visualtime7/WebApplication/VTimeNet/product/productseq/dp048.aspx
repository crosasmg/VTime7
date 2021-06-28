<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenu As eFunctions.Menues
Dim lintTratypep As Object

'- Objeto para el manejo de los datos de la ventana
Dim mclsSection_po As eProduct.Section_po


'% insDefineHeader: Se definen las propiedades de los campos del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	With mobjGrid.Columns
            Call .AddTextColumn(0, GetLocalResourceObject("tctSectionColumnCaption"), "tctSection", 30, vbNullString, , GetLocalResourceObject("tctSectionColumnToolTip"), , , , True)
            Call .AddTextColumn(0, "Reporte", "tctsReport", 30, vbNullString, , "Nombre Reporte ", , , , False)
            Call .AddTextColumn(0, "Orden", "tctnOrder", 5, vbNullString, , "Orden del reporte", , , , False)
            Call .AddTextColumn(0, "Rutina", "tctsRoutine", 12, vbNullString, , "Rutina para oculta la el reporte ", , , , False)
            Call .AddHiddenColumn("hddSel", CStr(2))
            Call .AddHiddenColumn("hddCodispl", vbNullString)
            Call .AddHiddenColumn("hddsPolitype", vbNullString)
            Call .AddHiddenColumn("hddsCompon", vbNullString)
            Call .AddHiddenColumn("hddnTratypep", vbNullString)
            Call .AddHiddenColumn("hddnId", vbNullString)
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "DP048"
		.Width = 380
            .Height = 350
		.DeleteButton = False
		.AddButton = False
		.ActionQuery = Session("bQuery") Or mclsSection_po.bError
		.bOnlyForQuery = .ActionQuery
		.Columns("tctSection").EditRecord = Not Session("bQuery")
		.DeleteScriptName = vbNullString
		.MoveRecordScript = "insDefValues()"
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		
		.Columns("Sel").OnClick = "InsSelected(this.value, this.checked)"
	End With
End Sub

'% insPreDP048: Se cargan los controles de la página, tanto de la parte fija como del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreDP048()
	'--------------------------------------------------------------------------------------------
	Dim lcolSection_pos As eProduct.Section_pos
	Dim lclsErrors As eFunctions.Errors
	
	Response.Write(mobjValues.HiddenControl("hddMassive", "1"))
	
	
Response.Write("" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""2"" CLASS=""HighLighted""><LABEL ID=41449>" & GetLocalResourceObject("AnchorCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD WIDTH=15%>&nbsp;</TD>" & vbCrLf)
Response.Write("            <TD COLSPAN=""2"" CLASS=""HighLighted""><LABEL ID=0>" & GetLocalResourceObject("Anchor2Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""2"" CLASS=""HorLine""></TD>" & vbCrLf)
Response.Write("            <TD></TD>" & vbCrLf)
Response.Write("            <TD CLASS=""HorLine""></TD>" & vbCrLf)
Response.Write("        </TR>        " & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""2"">")


Response.Write(mobjValues.OptionControl(0, "optPolitype", GetLocalResourceObject("optPolitype_CStr1Caption"), mclsSection_po.DefaultValueDP048("optInd_value"), CStr(1), "LoadSection_po(0);", mclsSection_po.DefaultValueDP048("optInd_disabled"), 1, GetLocalResourceObject("optPolitype_CStr1ToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("            <TD COLSPAN=""2"">")


Response.Write(mobjValues.OptionControl(0, "optCompon", GetLocalResourceObject("optCompon_CStr1Caption"), mclsSection_po.DefaultValueDP048("optPol_value"), CStr(1), "LoadSection_po(0);", mclsSection_po.DefaultValueDP048("optPol_disabled"), 4, GetLocalResourceObject("optCompon_CStr1ToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>    " & vbCrLf)
Response.Write("            <TD COLSPAN=""2"">")


Response.Write(mobjValues.OptionControl(0, "optPolitype", GetLocalResourceObject("optPolitype_CStr2Caption"), mclsSection_po.DefaultValueDP048("optCol_value"), CStr(2), "LoadSection_po(1);", mclsSection_po.DefaultValueDP048("optCol_disabled"), 2, GetLocalResourceObject("optPolitype_CStr2ToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("            <TD COLSPAN=""2"">")


Response.Write(mobjValues.OptionControl(0, "optCompon", GetLocalResourceObject("optCompon_CStr2Caption"), mclsSection_po.DefaultValueDP048("optCert_value"), CStr(2), "LoadSection_po(1);", mclsSection_po.DefaultValueDP048("optCert_disabled"), 5, GetLocalResourceObject("optCompon_CStr2ToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>    " & vbCrLf)
Response.Write("            <TD COLSPAN=""5"">")


Response.Write(mobjValues.OptionControl(0, "optPolitype", GetLocalResourceObject("optPolitype_CStr3Caption"), mclsSection_po.DefaultValueDP048("optMul_value"), CStr(3), "LoadSection_po(2);", mclsSection_po.DefaultValueDP048("optMul_disabled"), 3, GetLocalResourceObject("optPolitype_CStr3ToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>    " & vbCrLf)
Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL>" & GetLocalResourceObject("cbeTratypepCaption") & "</LABEL></TD>            " & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.PossiblesValues("cbeTratypep", "table5588", eFunctions.Values.eValuesType.clngComboType, lintTratypep,  ,  ,  ,  ,  , "LoadSection_po();",  ,  , GetLocalResourceObject("cbeTratypepToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("            <TD><LABEL>" & GetLocalResourceObject("valOriginCaption") & "</LABEL></TD>            " & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.PossiblesValues("valOrigin", "table5580", eFunctions.Values.eValuesType.clngWindowType, Request.QueryString.Item("nOrigin"),  ,  ,  ,  ,  , "LoadSection_po();",  ,  , GetLocalResourceObject("valOriginToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=0>" & GetLocalResourceObject("valType_amendCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")

	
	With mobjValues
		.Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		Response.Write(mobjValues.PossiblesValues("valType_amend", "Tabtype_amend", eFunctions.Values.eValuesType.clngWindowType, Request.QueryString.Item("nType_amend"), True,  ,  ,  ,  , "LoadSection_po();", True,  , GetLocalResourceObject("valType_amendToolTip")))
	End With
	
Response.Write("" & vbCrLf)
Response.Write("            </TD>" & vbCrLf)
Response.Write("            <TD>&nbsp;</TD>            " & vbCrLf)
Response.Write("            </TD>" & vbCrLf)
Response.Write("            ")

	mobjValues.ActionQuery = Session("bQuery")
	If Not mobjValues.ActionQuery Then
		Response.Write("<TD WIDTH=""10%"">" & mobjValues.AnimatedButtonControl("btn_Apply", "/VTimeNet/images/btnAcceptOff.png", GetLocalResourceObject("btn_ApplyToolTip"),  , "insAccept()",  , 10) & "</TD>")
	End If
	
Response.Write("" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        " & vbCrLf)
Response.Write("        ")


Response.Write(mobjValues.HiddenControl("hddPolitype", mclsSection_po.sPolitype))


Response.Write("" & vbCrLf)
Response.Write("        ")


Response.Write(mobjValues.HiddenControl("hddCompon", mclsSection_po.sCompon))


Response.Write("        " & vbCrLf)
Response.Write("        " & vbCrLf)
Response.Write("    </TABLE>" & vbCrLf)
Response.Write("    <BR>")

	
	
	With Response
		'+ Variables JScript para almacenar la última condición de búsqueda de la página
		.Write("<SCRIPT>")
		.Write("var mstrPolitype = '" & mclsSection_po.sPolitype & "';")
		.Write("var mstrCompon = '" & mclsSection_po.sCompon & "';")
		.Write("</" & "Script>")
	End With
	
	If mclsSection_po.bError Then
		lclsErrors = New eFunctions.Errors
		Response.Write(mobjGrid.closeTable())
		Response.Write(lclsErrors.ErrorMessage("DP012", 11399,  ,  ,  , True))
	Else
		lcolSection_pos = New eProduct.Section_pos
		
		If lcolSection_pos.Find(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), lintTratypep, mclsSection_po.sPolitype, mclsSection_po.sCompon, mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString.Item("nType_amend"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.QueryString.Item("nOrigin"), eFunctions.Values.eTypeData.etdDouble, True)) Then
			For	Each mclsSection_po In lcolSection_pos
				With mobjGrid
                        .Columns("tctSection").DefValue = mclsSection_po.sDescript
                        .Columns("tctsReport").DefValue = mclsSection_po.sReport
                        If mclsSection_po.nOrder > 0 Then
                            .Columns("tctnOrder").DefValue = mclsSection_po.nOrder
                        Else
                            .Columns("tctnOrder").DefValue = ""
                        End If
                        If mclsSection_po.nId > 0 Then
                            .Columns("tctSection").EditRecord = True
                        Else
                            .Columns("tctSection").HRefScript = ""
                            .Columns("tctSection").EditRecord = False
                        End If
                        .Columns("tctsRoutine").DefValue = mclsSection_po.sRoutine
                        
                        .Columns("Sel").Checked = 2
                        .Columns("hddSel").DefValue = CStr(2)
					
                        If mclsSection_po.nSequence = 0 Then
                            .Columns("Sel").Checked = 1
                            .Columns("hddSel").DefValue = CStr(1)
                        End If
                        
                        .Columns("hddsPolitype").DefValue = mclsSection_po.sPolitype
                        .Columns("hddsCompon").DefValue = mclsSection_po.sCompon
                        .Columns("hddnTratypep").DefValue = mclsSection_po.nTratypep
                        .Columns("hddnId").DefValue = mclsSection_po.nId
                        .Columns("hddCodispl").DefValue = mclsSection_po.sCodispl
                        
					Response.Write(.DoRow)
				End With
			Next mclsSection_po
		End If
		
		With Response
			.Write(mobjGrid.closeTable())
			.Write(mobjValues.BeginPageButton)
		End With
	End If
	
	lcolSection_pos = Nothing
	lclsErrors = Nothing
    End Sub
            
    '--------------------------------------------------------------------------------------------
    Private Sub insPreDP048Upd()
        '--------------------------------------------------------------------------------------------
        With Request
            Response.Write(mobjGrid.DoFormUpd(.QueryString("Action"), "valProductSeq.aspx", "DP048", .QueryString("nMainAction"), mobjValues.ActionQuery, .QueryString("Index")))
        End With
    End Sub



</script>
<%Response.Expires = -1

With Server
	mobjValues = New eFunctions.Values
	mobjGrid = New eFunctions.Grid
	mobjMenu = New eFunctions.Menues
	mclsSection_po = New eProduct.Section_po
End With

    If String.IsNullOrEmpty(Request.QueryString.Item("nTratypep")) Then
        lintTratypep = 7
    Else
        lintTratypep = Request.QueryString.Item("nTratypep")
    End If


mobjGrid.sCodisplPage = "DP048"
mobjValues.sCodisplPage = "DP048"
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">




	<%
With Response
	.Write(mobjValues.StyleSheet())
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write("<SCRIPT>var nMainAction=304</SCRIPT>")
		.Write(mobjMenu.setZone(2, "DP048", "DP048.aspx"))
	End If
	mobjMenu = Nothing
End With
%>
<SCRIPT>
//- Variable para el control de versiones
	document.VssVersion="$$Revision: 4 $|$$Date: 11/12/03 10:57 $|$$Author: Nvaplat11 $"

//% insDefValues: se asignan los valores por defecto a los campos de la página
//-------------------------------------------------------------------------------------------
function insDefValues(){
//-------------------------------------------------------------------------------------------
//+ Se define la variable para almacenar el consecutivo más alto existente en el grid
    var llngMax = 0

	if(self.document.forms[0].tcnOrder.value==0){
//+ Se genera el número consecutivo para el campo "Orden de aparición"
		with (top.opener){
			for(var llngIndex = 0;llngIndex<marrArray.length;llngIndex++)
			    if(marrArray[llngIndex].tcnOrder>llngMax)
			        llngMax = marrArray[llngIndex].tcnOrder
		}
	
//+ Se asignan los valores a los campos de la página	
		with (self.document.forms[0]){
		    if(++llngMax.length > tcnOrder.maxLength)
				tcnOrder.value = "";
			else
				tcnOrder.value = ++llngMax;
		}
	}
}

//% LoadSection_po: Se recarga la página con los nuevos parámetros de búsqueda
//-------------------------------------------------------------------------------------------
function LoadSection_po(nIndex){
//-------------------------------------------------------------------------------------------
	var lstrPolitype = '';
	var lstrCompon = '';
	var lintIndex = 0;

    var lintOrigin 
    var lintType_amend
		
	with (self.document.forms[0].elements){
        for (lintIndex=0;lintIndex<optPolitype.length;lintIndex++)
            if (optPolitype[lintIndex].checked)
                lstrPolitype = optPolitype[lintIndex].value;

		if (lstrPolitype == '1')
			lstrCompon = '1'
		else
	        for (lintIndex=0;lintIndex<optCompon.length;lintIndex++)
		        if (optCompon[lintIndex].checked)
		            lstrCompon = optCompon[lintIndex].value;

        lintTratypep = cbeTratypep.value;
        lintOrigin = valOrigin.value;
        lintType_amend = valType_amend.value;                

        if (mstrPolitype != lstrPolitype ||
		    mstrCompon != lstrCompon ||
		    lintTratypep != "")
		    
			self.document.location.href="/VTimeNet/Product/ProductSeq/DP048.aspx?sCodispl=DP048&sOnSeq=1&nMainAction=304&sPolitype=" + lstrPolitype + "&sCompon=" + lstrCompon + "&nTratypep=" + lintTratypep + "&nOrigin=" + lintOrigin + "&nType_amend=" + lintType_amend  
			
    }
}

//% insTratypep: controla el estado de los campos de la página cuando se cambia el el combo
//%           de cotizacion / propuesta
//------------------------------------------------------------------------------------------
function insTratypep(){
//------------------------------------------------------------------------------------------      

//+ Si es cotizacion, se sacan las opciones de Anulacion, Rehabilitacion, 
//+ Saldado, Prorrogado, Rescate y Prestamo
    with (self.document.forms[0]) {
        if (typeof (cbeTratypep) != 'undefined') {
            if (cbeTratypep.value == '6' ||
	           cbeTratypep.value == '7') {
                valOrigin.disabled = false
                btnvalOrigin.disabled = false
                //			valType_amend.value       = ''
                //			UpdateDiv('valType_amendDesc',"")

                if (cbeTratypep.value == '6') {
                    valOrigin.List = "4,5,6,7,8,9"
                    valOrigin.TypeList = 2
                    //			    valOrigin.value    = "";
                    //			    UpdateDiv('valOriginDesc',"")
                }
                else {

                    valOrigin.TypeList = 0
                    //			    valOrigin.value    = "";
                    //			    UpdateDiv('valOriginDesc',"")
                }
            }
            else {
                valOrigin.disabled = true
                btnvalOrigin.disabled = true
                valOrigin.value = ''
                UpdateDiv('valOriginDesc', "")
                valType_amend.disabled = true
                btnvalType_amend.disabled = true
                valType_amend.value = ''
                UpdateDiv('valType_amendDesc', "")
            }
        }
    }
}

//% insTratypep: controla el estado de los campos de la página cuando se cambia el el combo
//%           de cotizacion / propuesta
//------------------------------------------------------------------------------------------
function insOrigin(){
//------------------------------------------------------------------------------------------

    with (self.document.forms[0]) {
        if (typeof (valOrigin) != 'undefined') {
            if (valOrigin.value == '2') {
                valType_amend.disabled = false
                btnvalType_amend.disabled = false
                //			valType_amend.value       = ''
                //			UpdateDiv('valType_amendDesc',"")
            }
            else {
                valType_amend.disabled = true
                btnvalType_amend.disabled = true
                valType_amend.value = ''
                UpdateDiv('valType_amendDesc', "")

            }
        }
    }
}

//% insAccept: Se acpta la secuencia en tratamiento 
//------------------------------------------------------------------------------------------
function insAccept(){
//------------------------------------------------------------------------------------------
	self.document.forms[0].hddMassive.value=2;
	top.frames['fraHeader'].ClientRequest(390,2);
}

//% InsSelected: Se actualiza el campo oculta imagen del campo Sel
//------------------------------------------------------------------------------------------
function InsSelected(nIndex, bChecked){
//------------------------------------------------------------------------------------------
	with(document.forms[0]){
		if(hddSel.length>0){
			hddSel[nIndex].value =(bChecked?1:2);
		}
		else {
			hddSel.value =(bChecked?1:2);
		}			
	}
}

</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmDP048" ACTION="valProductSeq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%Response.Write(mobjValues.ShowWindowsName("DP048"))
Response.Write("<BR>")
    Call mclsSection_po.inspreDP048(Session("nBranch"), Session("nProduct"), Session("dEffecdate"), Request.QueryString.Item("sPolitype"), Request.QueryString.Item("sCompon"))
    
    Call insDefineHeader()
    
    If Request.QueryString("Type") <> "PopUp" Then
        Call insPreDP048()
    Else
        Call insPreDP048Upd()
    End If

mobjValues = Nothing
mobjGrid = Nothing
mclsSection_po = Nothing
%>
</FORM>
</BODY>
</HTML>
<SCRIPT>
	insTratypep();
	insOrigin();	
</SCRIPT>




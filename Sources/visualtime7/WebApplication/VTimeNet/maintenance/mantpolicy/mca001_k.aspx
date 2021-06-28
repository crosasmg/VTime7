<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las funciones de menu
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = 0

mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = "MCA001_k"
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/ValFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
<SCRIPT>
//- Variable para el control de versiones
	document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:03 $|$$Author: Iusr_llanquihue $"

//% insStateZone: habilita/deshabilita los campos de la ventana
//------------------------------------------------------------------------------------------
function insStateZone(){
//------------------------------------------------------------------------------------------
    with (self.document.forms[0])
    {   optBussiTyp[0].disabled = false
        optBussiTyp[1].disabled = false
        optBussiTyp[2].disabled = false
    
        optPoliType[0].disabled = false
        optPoliType[1].disabled = false
        optPoliType[2].disabled = false
      
        cbeTratypep.disabled = false
        cbeType_Amend.disabled = false
        cbeBrancht.disabled = false
    }
}

//% insCancel: controla la acción Cancelar de la página
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}

//% insFinish: controla la acción de Finalizar de la página.
//------------------------------------------------------------------------------------------
function insFinish(){
//------------------------------------------------------------------------------------------
	return true;
}

//% PoliType_Changed: Habilita/Deshabilita los "Camponentes" dependiendo del tipo de póliza
//-----------------------------------------------------------------------------------------------------------------------------------
function PoliType_Changed(Field){
//-----------------------------------------------------------------------------------------------------------------------------------
    var lblnEnabled = false
    
    if(Field.value==1)
    {
        with(self.document.forms[0]){
            optCompon[0].value = 1
			optCompon[0].disabled = true
			optCompon[1].disabled = true
		}
    }
    else
    {
        with(self.document.forms[0]){
			optCompon[0].disabled = false
			optCompon[1].disabled = false
		}
    }
}

//% ShowType_Amend: Muestra y oculta el campo de tipo de endoso.
//-----------------------------------------------------------------------------------------------------------------------------------
function ShowType_Amend(Field) {
//-----------------------------------------------------------------------------------------------------------------------------------
	
	if (Field.value!=2) {
	   ShowDiv('divType_amend', 'hide');
	   ShowDiv('divType_amend2', 'hide');
	   self.document.forms[0].cbeType_Amend.value = '';
	   UpdateDiv('cbeType_AmendDesc','');   
	}
	else {
	   ShowDiv('divType_amend', 'show');
	   ShowDiv('divType_amend2', 'show');
	}
}

</SCRIPT>
<%
With Response
	.Write(mobjValues.StyleSheet())
	mobjMenu = New eFunctions.Menues
	.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "MCA001_k.aspx", 1, ""))
	mobjMenu = Nothing
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmSeqWinPol" ACTION="ValMantPolicy.aspx?mode=1">
    <BR><BR>
    <TABLE WIDTH="100%">
    	<TR>
			<TD CLASS="HighLighted"><LABEL ID=0><A NAME="Tipo de negocio"><%= GetLocalResourceObject("AnchorTipo de negocioCaption") %></A></LABEL></TD>
			<TD>&nbsp;</TD>
            <TD CLASS="HighLighted"><LABEL ID=0><A NAME="Tipo de póliza"><%= GetLocalResourceObject("AnchorTipo de pólizaCaption") %></A></LABEL></TD>
            <TD>&nbsp;</TD>
            <TD CLASS="HighLighted"><LABEL ID=0><A NAME="Tipo de componente"><%= GetLocalResourceObject("AnchorTipo de componenteCaption") %></A></LABEL></TD>
        </TR>
        <TR>
		    <TD CLASS="Horline"></TD>
		    <TD></TD>
		    <TD CLASS="Horline"></TD>
		    <TD></TD>
		    <TD CLASS="Horline"></TD>
        </TR>      		
        <TR>
            <TD><%=mobjValues.OptionControl(0, "optBussiTyp", GetLocalResourceObject("optBussiTyp_CStr1Caption"), CStr(1), CStr(1),  , True)%></TD>
            <TD>&nbsp;</TD>
            <TD><%=mobjValues.OptionControl(0, "optPoliType", GetLocalResourceObject("optPoliType_CStr1Caption"), CStr(1), CStr(1), "PoliType_Changed(this);", True)%></TD>
            <TD>&nbsp;</TD>
            <TD><%=mobjValues.OptionControl(0, "optCompon", GetLocalResourceObject("optCompon_CStr1Caption"), CStr(1), CStr(1),  , True)%></TD>
            <TD>&nbsp;</TD>
        </TR>
        <TR>
            <TD><%=mobjValues.OptionControl(0, "optBussiTyp", GetLocalResourceObject("optBussiTyp_CStr2Caption"), CStr(0), CStr(2),  , True)%></TD>
            <TD>&nbsp;</TD>
            <TD><%=mobjValues.OptionControl(0, "optPoliType", GetLocalResourceObject("optPoliType_CStr2Caption"), CStr(0), CStr(2), "PoliType_Changed(this);", True)%></TD>
            <TD>&nbsp;</TD>
            <TD><%=mobjValues.OptionControl(0, "optCompon", GetLocalResourceObject("optCompon_CStr2Caption"), CStr(0), CStr(2),  , True)%></TD>
        </TR>
        <TR>
            <TD><%=mobjValues.OptionControl(0, "optBussiTyp", GetLocalResourceObject("optBussiTyp_CStr3Caption"), CStr(0), CStr(3),  , True)%></TD>
            <TD>&nbsp;</TD>
            <TD><%=mobjValues.OptionControl(0, "optPoliType", GetLocalResourceObject("optPoliType_CStr3Caption"), CStr(0), CStr(3), "PoliType_Changed(this);", True)%></TD>
            <TD>&nbsp;</TD>
            <TD></TD>
            <TD>&nbsp;</TD>
        </TR>
    </TABLE>
	<BR>
	<TABLE WIDTH="100%">
		<TR>
			<TD WIDTH=13%><DIV ID="divType_amend"  style="display:none"><LABEL ID=0><%= GetLocalResourceObject("cbeType_AmendCaption") %></LABEL></DIV></TD>
            <TD><DIV ID="divType_amend2" style="display:none"><%
Response.Write(mobjValues.PossiblesValues("cbeType_Amend", "Table6059", 2,  ,  ,  ,  ,  ,  ,  , False,  , GetLocalResourceObject("cbeType_AmendToolTip"),  , 1))
%>
			</DIV></TD>
		</TR>
        <TR>
            <TD WIDTH=20%><LABEL ID=0><%= GetLocalResourceObject("cbeTratypepCaption") %></LABEL></TD>
            <TD><%mobjValues.BlankPosition = False
Response.Write(mobjValues.PossiblesValues("cbeTratypep", "Table5588", 1, CStr(1),  ,  ,  ,  ,  , "ShowType_Amend(this);", True,  , GetLocalResourceObject("cbeTratypepToolTip")))
%>
            </TD>
            <TD WIDTH=5%><LABEL ID=9175><%= GetLocalResourceObject("cbeBranchtCaption") %></LABEL></TD>
            <TD><%mobjValues.BlankPosition = False
Response.Write(mobjValues.PossiblesValues("cbeBrancht", "Table37", 1, CStr(1),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeBranchtToolTip")))
%>           
            </TD>
        </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>




